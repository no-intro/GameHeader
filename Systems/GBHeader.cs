namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct GBHeader
    {
        private byte[] __header;

        public GBHeader(byte[] header)
        {
            this.__header = header;
        }

        public string EntryPoint
        {
            get
            {
                return Util.RHex(this.__header, 0, 4);
            }
        }

        public string NintendoLogo
        {
            get
            {
                return Util.Hex(this.__header, 4, 0x30);
            }
        }

        public string Title
        {
            get
            {
                return Util.SJIS(this.__header, 0x34, 0x10);
            }
        }

        public string NewLicensee
        {
            get
            {
                return Util.ASCII(this.__header, 0x44, 2);
            }
        }

        public string SGBFlag
        {
            get
            {
                return Util.Hex(this.__header, 70, 1);
            }
        }

        public string CartridgeType
        {
            get
            {
                return Util.Hex(this.__header, 0x47, 1);
            }
        }

        public string ROMSize
        {
            get
            {
                return Util.Hex(this.__header, 0x48, 1);
            }
        }

        public string RAMSize
        {
            get
            {
                return Util.Hex(this.__header, 0x49, 1);
            }
        }

        public string RegionCode
        {
            get
            {
                return Util.Hex(this.__header, 0x4a, 1);
            }
        }

        public string OldLicensee
        {
            get
            {
                return Util.Hex(this.__header, 0x4b, 1);
            }
        }

        public string Version
        {
            get
            {
                return Util.Hex(this.__header, 0x4c, 1);
            }
        }

        public string Checksum
        {
            get
            {
                return Util.Hex(this.__header, 0x4d, 1);
            }
        }

        public string GlobalChecksum
        {
            get
            {
                return Util.Hex(this.__header, 0x4e, 2);
            }
        }
    }
}