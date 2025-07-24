namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FDSNumberBlock
    {
        private byte[] __header;
        private int __index;

        public FDSNumberBlock(byte[] header)
        {
            this.__header = header;
            this.__index = 0;
        }

        public FDSNumberBlock(byte[] header, int index)
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

        public string NumFiles
        {
            get
            {
                return Util.Hex(this.__header, this.__index + 1, 1);
            }
        }
    }
}