namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct NESiNESHeader
    {
        private byte[] __header;

        public NESiNESHeader(byte[] header)
        {
            this.__header = header;
        }

        public static bool Detect(byte[] __header)
        {
            return (Program.Config.GetFlag("NES_OUTPUT", "Detect3rdPartyHeaders") && (Util.Hex(__header, 0, 4) == "4E45531A"));
        }

        public string iNES
        {
            get
            {
                return Util.Hex(this.__header, 0, 4);
            }
        }

        public string NumPRGBanks
        {
            get
            {
                return Util.Hex(this.__header, 4, 1);
            }
        }

        public string NumCHRBanks
        {
            get
            {
                return Util.Hex(this.__header, 5, 1);
            }
        }

        public string Mirroring
        {
            get
            {
                int num = this.__header[6] & 1;
                return num.ToString();
            }
        }

        public string BatteryRAM
        {
            get
            {
                int num = (this.__header[6] >> 1) & 1;
                return num.ToString();
            }
        }

        public string Trainer
        {
            get
            {
                int num = (this.__header[6] >> 2) & 1;
                return num.ToString();
            }
        }

        public string FourScreenVRAM
        {
            get
            {
                int num = (this.__header[6] >> 3) & 1;
                return num.ToString();
            }
        }

        public string VSSystem
        {
            get
            {
                int num = this.__header[7] & 1;
                return num.ToString();
            }
        }

        public string ROMMapperType
        {
            get
            {
                int num = (this.__header[7] & 240) + (this.__header[6] >> 4);
                return num.ToString();
            }
        }

        public string Reserved1
        {
            get
            {
                int num = (this.__header[7] >> 1) & 7;
                return num.ToString();
            }
        }

        public string NumRAMBanks
        {
            get
            {
                return Util.Hex(this.__header, 8, 1);
            }
        }

        public string Display
        {
            get
            {
                int num = this.__header[9] & 1;
                return num.ToString();
            }
        }

        public string Reserved2
        {
            get
            {
                int num = this.__header[9] >> 1;
                return num.ToString();
            }
        }

        public string Reserved3
        {
            get
            {
                return Util.Hex(this.__header, 0x1a, 0x1d);
            }
        }
    }
}