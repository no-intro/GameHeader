namespace GameHeader
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ZipWorkerInfo
    {
        public readonly int Blocks;
        public readonly int Number;
        public readonly int Count;
        public readonly string FileName;
        public readonly string EntryName;
        public ZipWorkerInfo(int blocks, string file_name, string entry_name, int number, int count)
        {
            this.Blocks = blocks;
            this.Count = count;
            this.Number = number;
            this.FileName = file_name;
            this.EntryName = entry_name;
        }
    }
}

