namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct GBAHeader
    {
        private byte[] __header;

        public GBAHeader(byte[] header)
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
                return Util.Hex(this.__header, 4, 0x9c);
            }
        }

        public string Title
        {
            get
            {
                return Util.SJIS(this.__header, 160, 12);
            }
        }

        public string Serial
        {
            get
            {
                return Util.ASCII(this.__header, 0xac, 4);
            }
        }

        public string MakerCode
        {
            get
            {
                return Util.ASCII(this.__header, 0xb0, 2);
            }
        }

        public string FixedValue
        {
            get
            {
                return Util.Hex(this.__header, 0xb2, 1);
            }
        }

        public string UnitCode
        {
            get
            {
                return Util.Hex(this.__header, 0xb3, 1);
            }
        }

        public string DeviceCode
        {
            get
            {
                return Util.Hex(this.__header, 180, 1);
            }
        }

        public string Reserved1
        {
            get
            {
                return Util.Hex(this.__header, 0xb5, 7);
            }
        }

        public string Version
        {
            get
            {
                return Util.Hex(this.__header, 0xbc, 1);
            }
        }

        public string Complement
        {
            get
            {
                return Util.Hex(this.__header, 0xbd, 1);
            }
        }

        public string Reserved2
        {
            get
            {
                return Util.Hex(this.__header, 190, 2);
            }
        }

        public string RAMEntryPoint
        {
            get
            {
                return Util.RHex(this.__header, 0xc0, 4);
            }
        }

        public string BootMode
        {
            get
            {
                return Util.Hex(this.__header, 0xc4, 1);
            }
        }

        public string SlaveID
        {
            get
            {
                return Util.Hex(this.__header, 0xc5, 1);
            }
        }

        public string Reserved3
        {
            get
            {
                return Util.Hex(this.__header, 0xc6, 0x1a);
            }
        }

        public string JoyBusEntryPoint
        {
            get
            {
                return Util.RHex(this.__header, 0xe4, 4);
            }
        }
    }
}