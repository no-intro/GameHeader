namespace GameHeader
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ProgressUpdateInfo
    {
        public readonly int PercentDelta;
        public readonly int PercentDone;
        public ProgressUpdateInfo(int delta, int done)
        {
            this.PercentDelta = delta;
            this.PercentDone = done;
        }
    }
}