namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct NDSHeader
    {
        private byte[] __header;

        public NDSHeader(byte[] header)
        {
            this.__header = header;
        }

        public string Title
        {
            get
            {
                return Util.ASCII(this.__header, 0, 12);
            }
        }

        public string Serial
        {
            get
            {
                return Util.ASCII(this.__header, 12, 4);
            }
        }

        public string MakerCode
        {
            get
            {
                return Util.ASCII(this.__header, 0x10, 2);
            }
        }

        public string UnitCode
        {
            get
            {
                return Util.Hex(this.__header, 0x12, 1);
            }
        }

        public string EncryptionSeed
        {
            get
            {
                return Util.Hex(this.__header, 0x13, 1);
            }
        }

        public string DeviceSize
        {
            get
            {
                return Util.Hex(this.__header, 20, 1);
            }
        }

        public string Reserved1
        {
            get
            {
                return Util.Hex(this.__header, 0x15, 9);
            }
        }

        public string AsianRegion
        {
            get
            {
                return Util.Hex(this.__header, 0x1d, 1);
            }
        }

        public string Version
        {
            get
            {
                return Util.Hex(this.__header, 30, 1);
            }
        }

        public string Autostart
        {
            get
            {
                return Util.Hex(this.__header, 0x1f, 1);
            }
        }

        public string ARM9ROMOffset
        {
            get
            {
                return Util.RHex(this.__header, 0x20, 4);
            }
        }

        public string ARM9EntryAddress
        {
            get
            {
                return Util.RHex(this.__header, 0x24, 4);
            }
        }

        public string ARM9RAMAddress
        {
            get
            {
                return Util.RHex(this.__header, 40, 4);
            }
        }

        public string ARM9Size
        {
            get
            {
                return Util.RHex(this.__header, 0x2c, 4);
            }
        }

        public string ARM7ROMOffset
        {
            get
            {
                return Util.RHex(this.__header, 0x30, 4);
            }
        }

        public string ARM7EntryAddress
        {
            get
            {
                return Util.RHex(this.__header, 0x34, 4);
            }
        }

        public string ARM7RAMAddress
        {
            get
            {
                return Util.RHex(this.__header, 0x38, 4);
            }
        }

        public string ARM7Size
        {
            get
            {
                return Util.RHex(this.__header, 60, 4);
            }
        }

        public string FNTOffset
        {
            get
            {
                return Util.RHex(this.__header, 0x40, 4);
            }
        }

        public string FNTSize
        {
            get
            {
                return Util.RHex(this.__header, 0x44, 4);
            }
        }

        public string FATOffset
        {
            get
            {
                return Util.RHex(this.__header, 0x48, 4);
            }
        }

        public string FATSize
        {
            get
            {
                return Util.RHex(this.__header, 0x4c, 4);
            }
        }

        public string ARM9OverlayOffset
        {
            get
            {
                return Util.RHex(this.__header, 80, 4);
            }
        }

        public string ARM9OverlaySize
        {
            get
            {
                return Util.RHex(this.__header, 0x54, 4);
            }
        }

        public string ARM7OverlayOffset
        {
            get
            {
                return Util.RHex(this.__header, 0x58, 4);
            }
        }

        public string ARM7OverlaySize
        {
            get
            {
                return Util.RHex(this.__header, 0x5c, 4);
            }
        }

        public string NormalCMDSetting
        {
            get
            {
                return Util.RHex(this.__header, 0x60, 4);
            }
        }

        public string Key1CMDSetting
        {
            get
            {
                return Util.RHex(this.__header, 100, 4);
            }
        }

        public string IconOffset
        {
            get
            {
                return Util.RHex(this.__header, 0x68, 4);
            }
        }

        public string SecureCRC16
        {
            get
            {
                return Util.RHex(this.__header, 0x6c, 2);
            }
        }

        public string SecureTimeout
        {
            get
            {
                return Util.RHex(this.__header, 110, 2);
            }
        }

        public string ARM9AutoloadAddress
        {
            get
            {
                return Util.RHex(this.__header, 0x70, 4);
            }
        }

        public string ARM7AutoloadAddress
        {
            get
            {
                return Util.RHex(this.__header, 0x74, 4);
            }
        }

        public string SecureDisable
        {
            get
            {
                return Util.RHex(this.__header, 120, 8);
            }
        }

        public string UsedRomSize
        {
            get
            {
                return Util.RHex(this.__header, 0x80, 4);
            }
        }

        public string HeaderSize
        {
            get
            {
                return Util.RHex(this.__header, 0x84, 4);
            }
        }

        public string Reserved2
        {
            get
            {
                return Util.Hex(this.__header, 0x88, 0x38);
            }
        }

        public string NintendoLogo
        {
            get
            {
                return Util.Hex(this.__header, 0xc0, 0x9c);
            }
        }

        public string LogoCRC16
        {
            get
            {
                return Util.RHex(this.__header, 0x15c, 2);
            }
        }

        public string HeaderCRC16
        {
            get
            {
                return Util.RHex(this.__header, 350, 2);
            }
        }

        public string Reserved3
        {
            get
            {
                return Util.Hex(this.__header, 0x160, 0x20);
            }
        }

        public string ConfigSettings
        {
            get
            {
                return Util.Hex(this.__header, 0x180, 0x30);
            }
        }

        public string DSiRegionMask
        {
            get
            {
                return Util.RHex(this.__header, 0x1b0, 4);
            }
        }

        public string AccessControl
        {
            get
            {
                return Util.RHex(this.__header, 0x1b4, 4);
            }
        }

        public string ARM7SCFG
        {
            get
            {
                return Util.RHex(this.__header, 440, 4);
            }
        }

        public string DSiAppFlags
        {
            get
            {
                return Util.RHex(this.__header, 0x1bc, 4);
            }
        }

        public string DSi9RomOffset
        {
            get
            {
                return Util.RHex(this.__header, 0x1c0, 4);
            }
        }

        public string DSi9EntryAddress
        {
            get
            {
                return Util.RHex(this.__header, 0x1c4, 4);
            }
        }

        public string DSi9RamAddress
        {
            get
            {
                return Util.RHex(this.__header, 0x1c8, 4);
            }
        }

        public string DSi9Size
        {
            get
            {
                return Util.RHex(this.__header, 460, 4);
            }
        }

        public string DSi7RomOffset
        {
            get
            {
                return Util.RHex(this.__header, 0x1d0, 4);
            }
        }

        public string DSi7EntryAddress
        {
            get
            {
                return Util.RHex(this.__header, 0x1d4, 4);
            }
        }

        public string DSi7RamAddress
        {
            get
            {
                return Util.RHex(this.__header, 0x1d8, 4);
            }
        }

        public string DSi7Size
        {
            get
            {
                return Util.RHex(this.__header, 0x1dc, 4);
            }
        }

        public string DigestNTROffset
        {
            get
            {
                return Util.RHex(this.__header, 480, 4);
            }
        }

        public string DigestNTRSize
        {
            get
            {
                return Util.RHex(this.__header, 0x1e4, 4);
            }
        }

        public string DigestTWLOffset
        {
            get
            {
                return Util.RHex(this.__header, 0x1e8, 4);
            }
        }

        public string DigestTWLSize
        {
            get
            {
                return Util.RHex(this.__header, 0x1ec, 4);
            }
        }

        public string DigestSectorHashTableOffset
        {
            get
            {
                return Util.RHex(this.__header, 0x1f0, 4);
            }
        }

        public string DigestSectorHashTableSize
        {
            get
            {
                return Util.RHex(this.__header, 500, 4);
            }
        }

        public string DigestBlockHashTableOffset
        {
            get
            {
                return Util.RHex(this.__header, 0x1f8, 4);
            }
        }

        public string DigestBlockHashTableLength
        {
            get
            {
                return Util.RHex(this.__header, 0x1fc, 4);
            }
        }

        public string DigestSectorSize
        {
            get
            {
                return Util.RHex(this.__header, 0x200, 4);
            }
        }

        public string DigestBlockSectorCount
        {
            get
            {
                return Util.RHex(this.__header, 0x204, 4);
            }
        }

        public string Reserved4
        {
            get
            {
                return Util.Hex(this.__header, 520, 0x18);
            }
        }

        public string Modcrypt1Offset
        {
            get
            {
                return Util.RHex(this.__header, 0x220, 4);
            }
        }

        public string Modcrypt1Size
        {
            get
            {
                return Util.RHex(this.__header, 0x224, 4);
            }
        }

        public string Modcrypt2Offset
        {
            get
            {
                return Util.RHex(this.__header, 0x228, 4);
            }
        }

        public string Modcrypt2Size
        {
            get
            {
                return Util.RHex(this.__header, 0x22c, 4);
            }
        }

        public string TitleID
        {
            get
            {
                return Util.RHex(this.__header, 560, 8);
            }
        }

        public string Reserved5
        {
            get
            {
                return Util.Hex(this.__header, 0x238, 200);
            }
        }

        public string ARM9SHA1HMAC
        {
            get
            {
                return Util.RHex(this.__header, 0x300, 20);
            }
        }

        public string ARM7SHA1HMAC
        {
            get
            {
                return Util.RHex(this.__header, 0x314, 20);
            }
        }

        public string DigestMasterSHA1HMAC
        {
            get
            {
                return Util.RHex(this.__header, 0x328, 20);
            }
        }

        public string BannerSHA1HMAC
        {
            get
            {
                return Util.RHex(this.__header, 0x33c, 20);
            }
        }

        public string ARM9iSHA1HMAC
        {
            get
            {
                return Util.RHex(this.__header, 0x350, 20);
            }
        }

        public string ARM7iSHA1HMAC
        {
            get
            {
                return Util.RHex(this.__header, 0x364, 20);
            }
        }

        public string Reserved6
        {
            get
            {
                return Util.Hex(this.__header, 0x378, 40);
            }
        }

        public string UnknownHash
        {
            get
            {
                return Util.RHex(this.__header, 0x3a0, 20);
            }
        }

        public string Reserved7
        {
            get
            {
                return Util.Hex(this.__header, 0x3b4, 0xa4c);
            }
        }

        public string Reserved8
        {
            get
            {
                return Util.RHex(this.__header, 0xe00, 0x180);
            }
        }

        public string RSASignature
        {
            get
            {
                return Util.RHex(this.__header, 0xf80, 0x80);
            }
        }
    }
}