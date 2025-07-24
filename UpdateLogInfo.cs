namespace GameHeader
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct UpdateLogInfo
    {
        public readonly string S;

        public UpdateLogInfo(string s)
        {
            this.S = s;
        }
    }
}