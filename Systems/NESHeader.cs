namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct NESHeader
    {
        private byte[] __header;

        public NESHeader(byte[] header)
        {
            this.__header = header;
        }

        public string Title
        {
            get
            {
                return Util.ASCII(this.__header, 0, 0x10);
            }
        }

        public string PRGChecksum
        {
            get
            {
                return Util.Hex(this.__header, 0x10, 2);
            }
        }

        public string CHRChecksum
        {
            get
            {
                return Util.Hex(this.__header, 0x12, 2);
            }
        }

        public string PRGSize
        {
            get
            {
                int num = this.__header[20] >> 4;
                return num.ToString();
            }
        }

        public string CHRSize
        {
            get
            {
                int num = this.__header[20] & 15;
                return num.ToString();
            }
        }

        public string Mirroring
        {
            get
            {
                int num = this.__header[0x15] >> 4;
                return num.ToString();
            }
        }

        public string BoardType
        {
            get
            {
                int num = this.__header[0x15] & 15;
                return num.ToString();
            }
        }

        public string Unknown1
        {
            get
            {
                return Util.Hex(this.__header, 0x16, 4);
            }
        }

        public string MakerCode
        {
            get
            {
                return Util.Hex(this.__header, 0x18, 1);
            }
        }

        public string Unknown2
        {
            get
            {
                return Util.Hex(this.__header, 0x19, 1);
            }
        }

        public string Vectors
        {
            get
            {
                return Util.Hex(this.__header, 0x1a, 6);
            }
        }
    }
}