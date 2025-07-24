namespace GameHeader.Systems
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FDSHeader
    {
        private byte[] __header;

        public FDSHeader(byte[] header)
        {
            this.__header = header;
        }
    }
}