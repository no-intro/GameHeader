namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FDSFileHeader
    {
        private byte[] __header;
        private int __index;

        public FDSFileHeader(byte[] header)
        {
            this.__header = header;
            this.__index = 0;
        }

        public FDSFileHeader(byte[] header, int index)
        {
            this.__header = header;
            this.__index = index;
        }

        public string BlockType
        {
            get
            {
                return Util.Hex(this.__header, this.__index, 1);
            }
        }

        public string FileNumber
        {
            get
            {
                return Util.Hex(this.__header, this.__index + 1, 1);
            }
        }

        public string FileID
        {
            get
            {
                return Util.Hex(this.__header, this.__index + 2, 1);
            }
        }

        public string FileName
        {
            get
            {
                return Util.ASCII(this.__header, this.__index + 3, 8);
            }
        }

        public string TargetAddress
        {
            get
            {
                return Util.RHex(this.__header, this.__index + 11, 2);
            }
        }

        public string FileSize
        {
            get
            {
                return Util.RHex(this.__header, this.__index + 13, 2);
            }
        }

        public string TargetArea
        {
            get
            {
                return Util.Hex(this.__header, this.__index + 15, 1);
            }
        }
    }
}