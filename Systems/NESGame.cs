namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;

    public class NESGame : Game
    {
        private string __banks;
        private byte[] __header;
        private int __headerless_size;
        private byte[] __headerless_crc32;
        private byte[] __headerless_md5;
        private byte[] __headerless_sha1;
        private byte[] __headerless_sha256;
        private byte[] __ines_header;
        private string __rom_crc32;
        private string __vrom_crc32;

        public NESGame(GameFileInfo game_info) : base(SystemType.NES, game_info)
        {
        }

        public override string Dump()
        {
            string str = base.Dump();
            if (this.__ines_header != null)
            {
                NESiNESHeader header = new NESiNESHeader(this.__ines_header);
                object obj2 = str + Util.Divider("iNES Header Data", '-');
                object obj3 = string.Concat(new object[] { obj2, Util.Pad("Num ROM Banks:"), "0x", header.NumPRGBanks, " (", Util.Int(header.NumPRGBanks) * 0x10, " KBytes)\r\n" });
                string str2 = string.Concat(new object[] { obj3, Util.Pad("Num VROM Banks:"), "0x", header.NumCHRBanks, " (", Util.Int(header.NumCHRBanks) * 8, " KBytes)\r\n" });
                string str3 = str2 + Util.Pad("Mirroring:") + header.Mirroring + " (" + ((header.Mirroring == "1") ? "Vertical" : "Horizontal") + ")\r\n";
                string str4 = str3 + Util.Pad("Save RAM:") + header.BatteryRAM + " (" + ((header.BatteryRAM == "1") ? "Yes" : "No") + ")\r\n";
                string str5 = str4 + Util.Pad("Trainer:") + header.Trainer + " (" + ((header.Trainer == "1") ? "Yes" : "No") + ")\r\n";
                string str6 = str5 + Util.Pad("4-Screen VRAM:") + header.Trainer + " (" + ((header.FourScreenVRAM == "1") ? "Yes" : "No") + ")\r\n";
                string str7 = str6 + Util.Pad("VS-System Cartridge:") + header.VSSystem + " (" + ((header.VSSystem == "1") ? "Yes" : "No") + ")\r\n";
                string str8 = str7 + Util.Pad("ROM Mapper:") + header.ROMMapperType + " (" + Program.DB.GetValue("INES_MAPPERS", header.ROMMapperType, "Unknown") + ")\r\n";
                string str9 = str8 + Util.Pad("Num RAM Banks:") + "0x" + header.NumRAMBanks + " (" + ((header.NumRAMBanks == "00") ? "None or 8 KBytes" : ((0x2000 * Util.Int(header.NumRAMBanks)) + " KBytes")) + ")\r\n";
                str = str9 + Util.Pad("Display:") + header.Display + " (" + ((header.Display == "1") ? "PAL" : "NTSC") + ")\r\n";
            }
            if (this.__header != null)
            {
                NESHeader header2 = new NESHeader(this.__header);
                string str10 = (str + Util.Divider("NES Header Data", '-')) + Util.Pad("Title:") + header2.Title + "\r\n";
                string str11 = str10 + Util.Pad("PRG-ROM Checksum:") + "0x" + header2.PRGChecksum + " (" + ((header2.PRGChecksum == this.PRGChecksum) ? "Ok" : ("Bad; 0x" + this.PRGChecksum)) + ")\r\n";
                object obj4 = str11 + Util.Pad("CHR-VROM Checksum:") + "0x" + header2.CHRChecksum + " (" + ((header2.CHRChecksum == this.CHRChecksum) ? "Ok" : ("Bad; 0x" + this.CHRChecksum)) + ")\r\n";
                string str12 = string.Concat(new object[] { obj4, Util.Pad("PRG Size:"), "0x", header2.PRGSize, " (", ((int) 2) << (Util.Int(header2.PRGSize) + 3), " KBytes)\r\n" });
                string str13 = str12 + Util.Pad("CHR Size:") + "0x" + header2.CHRSize + " (" + ((header2.CHRSize == "8") ? "None; VRAM Used" : ((((int) 2) << (Util.Int(header2.CHRSize) + 2)) + " KBytes")) + ")\r\n";
                string str14 = str13 + Util.Pad("Mirroring:") + "0x" + header2.Mirroring + " (" + ((header2.Mirroring == "8") ? "Vertical" : "Horizontal") + ")\r\n";
                string str15 = str14 + Util.Pad("Board Type:") + "0x" + header2.BoardType + "\r\n";
                string str16 = str15 + Util.Pad("Unknown 1:") + "0x" + header2.Unknown1 + "\r\n";
                string str17 = str16 + Util.Pad("Maker Code:") + "0x" + header2.MakerCode + " (" + Program.DB.GetValue("NINTENDO_MAKER_CODES", header2.MakerCode, "Unknown") + ")\r\n";
                string str18 = str17 + Util.Pad("Unknown 2:") + "0x" + header2.Unknown2 + "\r\n";
                str = str18 + Util.Pad("Vectors:") + "0x" + header2.Vectors + "\r\n";
            }
            if (this.__ines_header != null)
            {
                str = str + Util.Divider("Headerless Data", '-') + Util.Pad("Size (Bytes):") + this.Headerless_Size.ToString() + "\r\n" + Util.Pad("CRC32:") + Util.CaseHash(this.Headerless_CRC32) + "\r\n" + Util.Pad("MD5:") + Util.CaseHash(this.Headerless_MD5) + "\r\n" + Util.Pad("SHA1:") + Util.CaseHash(this.Headerless_SHA1) + "\r\n" + Util.Pad("SHA256:") + Util.CaseHash(this.Headerless_SHA256) + "\r\n";
            }
            return (str + Util.Divider('='));
        }

        public override string System()
        {
            return SystemName.NES;
        }

        public string Banks
        {
            get
            {
                return this.__banks;
            }
            set
            {
                this.__banks = value;
            }
        }

        public string CHRChecksum
        {
            get
            {
                return this.__vrom_crc32;
            }
            set
            {
                this.__vrom_crc32 = value;
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

        public byte[] Headerless_CRC32
        {
            get
            {
                if (this.__ines_header == null)
                {
                    return base._crc32;
                }
                return this.__headerless_crc32;
            }
            set
            {
                this.__headerless_crc32 = value;
            }
        }

        public byte[] Headerless_MD5
        {
            get
            {
                if (this.__ines_header == null)
                {
                    return base._md5;
                }
                return this.__headerless_md5;
            }
            set
            {
                this.__headerless_md5 = value;
            }
        }

        public byte[] Headerless_SHA1
        {
            get
            {
                if (this.__ines_header == null)
                {
                    return base._sha1;
                }
                return this.__headerless_sha1;
            }
            set
            {
                this.__headerless_sha1 = value;
            }
        }

        public byte[] Headerless_SHA256
        {
            get
            {
                if (this.__ines_header == null)
                {
                    return base._sha256;
                }
                return this.__headerless_sha256;
            }
            set
            {
                this.__headerless_sha256 = value;
            }
        }

        public int Headerless_Size
        {
            get
            {
                return this.__headerless_size;
            }
            set
            {
                this.__headerless_size = value;
            }
        }

        public byte[] iNESHeader
        {
            get
            {
                return this.__ines_header;
            }
            set
            {
                this.__ines_header = value;
            }
        }

        public string PRGChecksum
        {
            get
            {
                return this.__rom_crc32;
            }
            set
            {
                this.__rom_crc32 = value;
            }
        }
    }
}