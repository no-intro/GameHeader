namespace GameHeader
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FileWorkerInfo
    {
        public readonly int Blocks;
        public readonly string FileName;

        public FileWorkerInfo(int blocks, string file_name)
        {
            this.Blocks = blocks;
            this.FileName = file_name;
        }
    }
}