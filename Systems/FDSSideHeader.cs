namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FDSSideHeader
    {
        private byte[] __header;

        public FDSSideHeader(byte[] header)
        {
            this.__header = header;
        }

        public string BlockType
        {
            get
            {
                return Util.Hex(this.__header, 0, 1);
            }
        }

        public string DiskID
        {
            get
            {
                return Util.ASCII(this.__header, 1, 14);
            }
        }

        public string MakerID
        {
            get
            {
                return Util.Hex(this.__header, 15, 1);
            }
        }

        public string GameName
        {
            get
            {
                return Util.ASCII(this.__header, 0x10, 4);
            }
        }

        public string Version
        {
            get
            {
                return Util.Hex(this.__header, 20, 1);
            }
        }

        public string SideNumber
        {
            get
            {
                return Util.Hex(this.__header, 0x15, 1);
            }
        }

        public string DiskNumber
        {
            get
            {
                return Util.Hex(this.__header, 0x16, 1);
            }
        }

        public string ExtraDiskID
        {
            get
            {
                return Util.RHex(this.__header, 0x17, 2);
            }
        }

        public string MaxAutoloadID
        {
            get
            {
                return Util.Hex(this.__header, 0x19, 1);
            }
        }

        public string Reserved
        {
            get
            {
                return Util.Hex(this.__header, 0x1a, 0x1d);
            }
        }
    }
}