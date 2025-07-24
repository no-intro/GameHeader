namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct NGCHeader
    {
        private byte[] __header;

        public NGCHeader(byte[] header)
        {
            this.__header = header;
        }

        public string SystemId
        {
            get
            {
                return Util.ASCII(this.__header, 0, 1);
            }
        }

        public string GameCode
        {
            get
            {
                return Util.ASCII(this.__header, 1, 2);
            }
        }

        public string RegionCode
        {
            get
            {
                return Util.ASCII(this.__header, 3, 1);
            }
        }

        public string MakerCode
        {
            get
            {
                return Util.ASCII(this.__header, 4, 2);
            }
        }

        public string DiscId
        {
            get
            {
                return Util.Hex(this.__header, 6, 1);
            }
        }

        public string Version
        {
            get
            {
                return Util.Hex(this.__header, 7, 1);
            }
        }

        public string GameName
        {
            get
            {
                return Util.ASCII(this.__header, 0x20, 992);
            }
        }
    }
}