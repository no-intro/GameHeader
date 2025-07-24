namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct N64Header
    {
        private byte[] __header;

        public N64Header(byte[] header)
        {
            this.__header = header;
        }

        public string FixedValue1
        {
            get
            {
                return Util.Hex(this.__header, 0, 2);
            }
        }

        public string CompressionFlag
        {
            get
            {
                return Util.Hex(this.__header, 2, 1);
            }
        }

        public string FixedValue2
        {
            get
            {
                return Util.Hex(this.__header, 3, 5);
            }
        }

        public string EntryPoint
        {
            get
            {
                return Util.RHex(this.__header, 8, 4);
            }
        }

        public string FixedValue3
        {
            get
            {
                return Util.Hex(this.__header, 12, 3);
            }
        }

        public string UnknownValue1
        {
            get
            {
                return Util.Hex(this.__header, 15, 1);
            }
        }

        public string CRC1
        {
            get
            {
                return Util.Hex(this.__header, 0x10, 4);
            }
        }

        public string CRC2
        {
            get
            {
                return Util.Hex(this.__header, 20, 4);
            }
        }

        public string Reserved1
        {
            get
            {
                return Util.Hex(this.__header, 0x18, 8);
            }
        }

        public string Title
        {
            get
            {
                return Util.SJIS(this.__header, 0x20, 20);
            }
        }

        public string Reserved2
        {
            get
            {
                return Util.Hex(this.__header, 0x34, 7);
            }
        }

        public string Serial
        {
            get
            {
                return Util.ASCII(this.__header, 0x3b, 4);
            }
        }

        public string Version
        {
            get
            {
                return Util.Hex(this.__header, 0x3f, 1);
            }
        }
    }
}