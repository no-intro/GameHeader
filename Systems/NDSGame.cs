namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;
    using System.Text;

    public class NDSGame : Game
    {
        private bool __decrypted;
        private byte[] __encrypted_crc32;
        private byte[] __encrypted_md5;
        private byte[] __encrypted_secure_area;
        private string __encrypted_secure_crc16;
        private byte[] __encrypted_sha1;
        private byte[] __encrypted_sha256;
        private byte[] __header;
        private string __header_crc16;
        private byte[] __icon;
        private string __icon_crc16;
        private string __logo_crc16;
        private byte[] __secure_area;
        private string __secure_crc16;
        public NDSHeader header;

        public NDSGame(GameFileInfo game_info) : base(SystemType.NDS, game_info)
        {
            this.__encrypted_secure_area = null;
        }

        public override string Dump()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.Dump());
            this.header = new NDSHeader(this.__header);
            NDSIcon icon = new NDSIcon(this.__icon);
            bool flag = Program.GetConfig().GetFlag("NDS_OUTPUT", "FullHeaderDump");
            builder.Append(Util.Divider("Header Data", '-'));
            builder.Append(Util.Pad("Game Title:") + this.header.Title + "\r\n");
            string[] strArray = new string[5];
            strArray[0] = Util.Pad("Game Serial:");
            strArray[1] = this.header.Serial;
            strArray[2] = " (";
            char ch = this.header.Serial[3];
            strArray[3] = Program.DB.GetValue("NINTENDO_REGIONS", ch.ToString(), "Unknown");
            strArray[4] = ")\r\n";
            builder.Append(string.Concat(strArray));
            builder.Append(Util.Pad("Maker Code:") + this.header.MakerCode + " (" + Program.DB.GetValue("NINTENDO_MAKER_CODES", this.header.MakerCode, "Unknown") + ")\r\n");
            builder.Append(Util.Pad("Unit Code:") + "0x" + this.header.UnitCode + "\r\n");
            if (flag)
            {
                builder.Append(Util.Pad("Encryption Seed:") + "0x" + this.header.EncryptionSeed + "\r\n");
            }
            int num = ((int) 0x20000) << Util.Int(this.header.DeviceSize);
            int num2 = Util.Int(this.header.UsedRomSize);
            int num3 = num;
            while (num2 < (num3 / 2))
            {
                num3 /= 2;
            }
            builder.Append(string.Concat(new object[] { Util.Pad("Device Size:"), "0x", this.header.DeviceSize, " (", (num == base._game_info.Length) ? "Ok" : ((num < base._game_info.Length) ? "Overdump" : "Underdump"), "; ", Util.BitSize((long) (num * 8)), "; ", num, " Bytes", (num3 != num) ? ("; " + Util.BitSize((long) (8 * num3)) + " Size-Fixed") : "", ")\r\n" }));
            builder.Append(Util.Pad("Asian Region:") + "0x" + this.header.AsianRegion + "\r\n");
            if (flag)
            {
                builder.Append(Util.Pad("Reserved 1:") + "0x" + this.header.Reserved1 + "\r\n");
            }
            builder.Append(string.Concat(new object[] { Util.Pad("Version:"), "0x", this.header.Version, " (v1.", Util.Int(this.header.Version), ")\r\n" }));
            builder.Append(Util.Pad("Autostart:") + "0x" + this.header.Autostart + " (" + ((this.header.Autostart == "04") ? "Yes" : "No") + ")\r\n");
            if (flag)
            {
                builder.Append(Util.Pad("ARM9 ROM Offset:") + "0x" + this.header.ARM9ROMOffset + "\r\n");
                builder.Append(Util.Pad("ARM9 Entry Address:") + "0x" + this.header.ARM9EntryAddress + "\r\n");
                builder.Append(Util.Pad("ARM9 RAM Offset:") + "0x" + this.header.ARM9RAMAddress + "\r\n");
                builder.Append(Util.Pad("ARM7 ROM Offset:") + "0x" + this.header.ARM7ROMOffset + "\r\n");
                builder.Append(Util.Pad("ARM7 Entry Address:") + "0x" + this.header.ARM7EntryAddress + "\r\n");
                builder.Append(Util.Pad("ARM7 RAM Offset:") + "0x" + this.header.ARM7RAMAddress + "\r\n");
                builder.Append(Util.Pad("FNT Offset:") + "0x" + this.header.FNTOffset + "\r\n");
                builder.Append(Util.Pad("FNT Size:") + "0x" + this.header.FNTSize + "\r\n");
                builder.Append(Util.Pad("FAT Offset:") + "0x" + this.header.FATOffset + "\r\n");
                builder.Append(Util.Pad("FAT Size:") + "0x" + this.header.FATSize + "\r\n");
                builder.Append(Util.Pad("ARM9 Overlay Offset:") + "0x" + this.header.ARM9OverlayOffset + "\r\n");
                builder.Append(Util.Pad("ARM9 Overlay Size:") + "0x" + this.header.ARM9OverlaySize + "\r\n");
                builder.Append(Util.Pad("ARM7 Overlay Offset:") + "0x" + this.header.ARM7OverlayOffset + "\r\n");
                builder.Append(Util.Pad("ARM7 Overlay Size:") + "0x" + this.header.ARM9OverlaySize + "\r\n");
                builder.Append(Util.Pad("Normal CMD Setting:") + "0x" + this.header.NormalCMDSetting + "\r\n");
                builder.Append(Util.Pad("Key1 CMD Setting:") + "0x" + this.header.Key1CMDSetting + "\r\n");
                builder.Append(Util.Pad("Icon Address:") + "0x" + this.header.IconOffset + "\r\n");
            }
            builder.Append(Util.Pad("Secure CRC16:") + "0x" + this.header.SecureCRC16 + " (" + ((this.header.SecureCRC16 == this.SecureCRC16) ? "Ok" : ("Bad; 0x" + this.SecureCRC16)) + (this.Decrypted ? "; Decrypted" : "") + ")\r\n");
            if (flag)
            {
                builder.Append(Util.Pad("Secure Timeout:") + "0x" + this.header.SecureTimeout + "\r\n");
                builder.Append(Util.Pad("ARM9 Autoload Address:") + "0x" + this.header.ARM9AutoloadAddress + "\r\n");
                builder.Append(Util.Pad("ARM7 Autoload Address:") + "0x" + this.header.ARM7AutoloadAddress + "\r\n");
                builder.Append(Util.Pad("Secure Disable:") + "0x" + this.header.SecureDisable + "\r\n");
            }
            builder.Append(string.Concat(new object[] { Util.Pad("Used Rom Size:"), "0x", this.header.UsedRomSize, " (", num2, " Bytes)\r\n" }));
            builder.Append(string.Concat(new object[] { Util.Pad("Header Size:"), "0x", this.header.HeaderSize, " (", Util.Int(this.header.HeaderSize), " Bytes)\r\n" }));
            if (flag)
            {
                builder.Append(Util.Pad("Reserved 2:") + this.header.Reserved2 + "\r\n");
                builder.Append(Util.Pad("Nintendo Logo:") + this.header.NintendoLogo + "\r\n");
            }
            builder.Append(Util.Pad("Logo CRC16:") + "0x" + this.header.LogoCRC16 + " (" + ((this.header.LogoCRC16 == this.LogoCRC16) ? "Ok" : ("Bad; 0x" + this.LogoCRC16)) + ")\r\n");
            builder.Append(Util.Pad("Header CRC16:") + "0x" + this.header.HeaderCRC16 + " (" + ((this.header.HeaderCRC16 == this.HeaderCRC16) ? "Ok" : ("Bad; 0x" + this.HeaderCRC16)) + ")\r\n");
            if (flag)
            {
                builder.Append(Util.Pad("Reserved 3:") + "0x" + this.header.Reserved3 + "\r\n");
                builder.Append(Util.Pad("Config Settings:") + "0x" + this.header.ConfigSettings + "\r\n");
                builder.Append(Util.Pad("DSiRegionMask:") + "0x" + this.header.DSiRegionMask + "\r\n");
                builder.Append(Util.Pad("AccessControl:") + "0x" + this.header.AccessControl + "\r\n");
                builder.Append(Util.Pad("ARM7SCFG:") + "0x" + this.header.ARM7SCFG + "\r\n");
                builder.Append(Util.Pad("DSiAppFlags:") + "0x" + this.header.DSiAppFlags + "\r\n");
                builder.Append(Util.Pad("DSi9RomOffset:") + "0x" + this.header.DSi9RomOffset + "\r\n");
                builder.Append(Util.Pad("DSi9EntryAddress:") + "0x" + this.header.DSi9EntryAddress + "\r\n");
                builder.Append(Util.Pad("DSi9RamAddress:") + "0x" + this.header.DSi9RamAddress + "\r\n");
                builder.Append(Util.Pad("DSi9Size:") + "0x" + this.header.DSi9Size + "\r\n");
                builder.Append(Util.Pad("DSi7RomOffset:") + "0x" + this.header.DSi7RomOffset + "\r\n");
                builder.Append(Util.Pad("DSi7EntryAddress:") + "0x" + this.header.DSi7EntryAddress + "\r\n");
                builder.Append(Util.Pad("DSi7RamAddress:") + "0x" + this.header.DSi7RamAddress + "\r\n");
                builder.Append(Util.Pad("DSi7Size:") + "0x" + this.header.DSi7Size + "\r\n");
                builder.Append(Util.Pad("DigestNTROffset:") + "0x" + this.header.DigestNTROffset + "\r\n");
                builder.Append(Util.Pad("DigestNTRSize:") + "0x" + this.header.DigestNTRSize + "\r\n");
                builder.Append(Util.Pad("DigestTWLOffset:") + "0x" + this.header.DigestTWLOffset + "\r\n");
                builder.Append(Util.Pad("DigestTWLSize:") + "0x" + this.header.DigestTWLSize + "\r\n");
                builder.Append(Util.Pad("DigestSectorHashTableOffset:") + "0x" + this.header.DigestSectorHashTableOffset + "\r\n");
                builder.Append(Util.Pad("DigestSectorHashTableSize:") + "0x" + this.header.DigestSectorHashTableSize + "\r\n");
                builder.Append(Util.Pad("DigestBlockHashTableOffset:") + "0x" + this.header.DigestBlockHashTableOffset + "\r\n");
                builder.Append(Util.Pad("DigestBlockHashTableLength:") + "0x" + this.header.DigestBlockHashTableLength + "\r\n");
                builder.Append(Util.Pad("DigestSectorSize:") + "0x" + this.header.DigestSectorSize + "\r\n");
                builder.Append(Util.Pad("DigestBlockSectorCount:") + "0x" + this.header.DigestBlockSectorCount + "\r\n");
                builder.Append(Util.Pad("Reserved4:") + "0x" + this.header.Reserved4 + "\r\n");
                builder.Append(Util.Pad("Modcrypt1Offset:") + "0x" + this.header.Modcrypt1Offset + "\r\n");
                builder.Append(Util.Pad("Modcrypt1Size:") + "0x" + this.header.Modcrypt1Size + "\r\n");
                builder.Append(Util.Pad("Modcrypt2Offset:") + "0x" + this.header.Modcrypt2Offset + "\r\n");
                builder.Append(Util.Pad("Modcrypt2Size:") + "0x" + this.header.Modcrypt2Size + "\r\n");
                builder.Append(Util.Pad("TitleID:") + "0x" + this.header.TitleID + "\r\n");
                builder.Append(Util.Pad("Reserved5:") + "0x" + this.header.Reserved5 + "\r\n");
                builder.Append(Util.Pad("ARM9SHA1HMAC:") + "0x" + this.header.ARM9SHA1HMAC + "\r\n");
                builder.Append(Util.Pad("ARM7SHA1HMAC:") + "0x" + this.header.ARM7SHA1HMAC + "\r\n");
                builder.Append(Util.Pad("DigestMasterSHA1HMAC:") + "0x" + this.header.DigestMasterSHA1HMAC + "\r\n");
                builder.Append(Util.Pad("BannerSHA1HMAC:") + "0x" + this.header.BannerSHA1HMAC + "\r\n");
                builder.Append(Util.Pad("ARM9iSHA1HMAC:") + "0x" + this.header.ARM9iSHA1HMAC + "\r\n");
                builder.Append(Util.Pad("ARM7iSHA1HMAC:") + "0x" + this.header.ARM7iSHA1HMAC + "\r\n");
                builder.Append(Util.Pad("Reserved6:") + "0x" + this.header.Reserved6 + "\r\n");
                builder.Append(Util.Pad("UnknownHash:") + "0x" + this.header.UnknownHash + "\r\n");
                builder.Append(Util.Pad("Reserved7:") + "0x" + this.header.Reserved7 + "\r\n");
                builder.Append(Util.Pad("Reserved8:") + "0x" + this.header.Reserved8 + "\r\n");
                builder.Append(Util.Pad("RSASignature:") + "0x" + this.header.RSASignature + "\r\n");
            }
            builder.Append(Util.Divider("Icon/Title Data", '-'));
            builder.Append(Util.Pad("Icon Version:") + "0x" + icon.Version + "\r\n");
            builder.Append(Util.Pad("Icon CRC16:") + "0x" + icon.CRC16 + " (" + ((icon.CRC16 == this.IconCRC16) ? "Ok" : ("Bad; 0x" + this.IconCRC16)) + ")\r\n");
            if (Program.GetConfig().GetFlag("NDS_OUTPUT", "ShowLanguageInfo"))
            {
                builder.Append("Japanese Title:\r\n" + icon.Japanese + "\r\n");
                builder.Append("English Title:\r\n" + icon.English + "\r\n");
                builder.Append("French Title:\r\n" + icon.French + "\r\n");
                builder.Append("German Title:\r\n" + icon.German + "\r\n");
                builder.Append("Spanish Title:\r\n" + icon.Spanish + "\r\n");
                builder.Append("Italian Title:\r\n" + icon.Italian + "\r\n");
            }
            if (this.Decrypted && (this.__encrypted_secure_area != null))
            {
                builder.Append(Util.Divider("Encrypted Data", '-'));
                builder.Append(Util.Pad("Encrypted Secure:") + "0x" + this.Encrypted_SecureCRC16 + " (" + ((this.Encrypted_SecureCRC16 == this.header.SecureCRC16) ? "Ok" : ("Bad; 0x" + this.Encrypted_SecureCRC16)) + ")\r\n");
                builder.Append(Util.Pad("Encrypted CRC32:") + Util.CaseHash(this.Encrypted_CRC32) + "\r\n");
                builder.Append(Util.Pad("Encrypted MD5:") + Util.CaseHash(this.Encrypted_MD5) + "\r\n");
                builder.Append(Util.Pad("Encrypted SHA1:") + Util.CaseHash(this.Encrypted_SHA1) + "\r\n");
                builder.Append(Util.Pad("Encrypted SHA256:") + Util.CaseHash(this.Encrypted_SHA256) + "\r\n");
            }
            builder.Append(Util.Divider('='));
            return builder.ToString();
        }

        public override string System()
        {
            return SystemName.NDS;
        }

        public bool Decrypted
        {
            get
            {
                return this.__decrypted;
            }
        }

        public byte[] Encrypted_CRC32
        {
            get
            {
                if (!this.Decrypted)
                {
                    return base._crc32;
                }
                return this.__encrypted_crc32;
            }
            set
            {
                this.__encrypted_crc32 = value;
            }
        }

        public byte[] Encrypted_MD5
        {
            get
            {
                if (!this.Decrypted)
                {
                    return base._md5;
                }
                return this.__encrypted_md5;
            }
            set
            {
                this.__encrypted_md5 = value;
            }
        }

        public byte[] Encrypted_SecureArea
        {
            get
            {
                if (!this.Decrypted)
                {
                    return this.__secure_area;
                }
                return this.__encrypted_secure_area;
            }
            set
            {
                this.__encrypted_secure_area = value;
            }
        }

        public string Encrypted_SecureCRC16
        {
            get
            {
                if (!this.Decrypted)
                {
                    return this.__secure_crc16;
                }
                return this.__encrypted_secure_crc16;
            }
            set
            {
                this.__encrypted_secure_crc16 = value;
            }
        }

        public byte[] Encrypted_SHA1
        {
            get
            {
                if (!this.Decrypted)
                {
                    return base._sha1;
                }
                return this.__encrypted_sha1;
            }
            set
            {
                this.__encrypted_sha1 = value;
            }
        }

        public byte[] Encrypted_SHA256
        {
            get
            {
                if (!this.Decrypted)
                {
                    return base._sha256;
                }
                return this.__encrypted_sha256;
            }
            set
            {
                this.__encrypted_sha256 = value;
            }
        }

        public byte[] Header
        {
            get
            {
                return this.__header;
            }
            set
            {
                this.__header = value;
            }
        }

        public string HeaderCRC16
        {
            get
            {
                return this.__header_crc16;
            }
            set
            {
                this.__header_crc16 = value;
            }
        }

        public byte[] Icon
        {
            get
            {
                return this.__icon;
            }
            set
            {
                this.__icon = value;
            }
        }

        public int Icon_Start
        {
            get
            {
                return Util.Int(Util.RHex(this.__header, 0x68, 4));
            }
        }

        public string IconCRC16
        {
            get
            {
                return this.__icon_crc16;
            }
            set
            {
                this.__icon_crc16 = value;
            }
        }

        public string LogoCRC16
        {
            get
            {
                return this.__logo_crc16;
            }
            set
            {
                this.__logo_crc16 = value;
            }
        }

        public byte[] SecureArea
        {
            get
            {
                return this.__secure_area;
            }
            set
            {
                this.__secure_area = value;
                this.__decrypted = Util.RHex(this.__secure_area, 0, 8) == "E7FFDEFFE7FFDEFF";
            }
        }

        public string SecureCRC16
        {
            get
            {
                return this.__secure_crc16;
            }
            set
            {
                this.__secure_crc16 = value;
            }
        }
    }
}