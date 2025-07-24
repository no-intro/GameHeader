namespace GameHeader.Abstract
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct GameFileInfo
    {
        public readonly string FileName;
        public readonly string EntryName;
        public readonly bool Compressed;
        public readonly long Length;
        public readonly string Path;
        public GameFileInfo(string file_name, string path, string entry_name, long length, bool compressed)
        {
            this.FileName = file_name;
            this.EntryName = entry_name;
            this.Compressed = compressed;
            this.Length = length;
            this.Path = path;
        }
    }
}