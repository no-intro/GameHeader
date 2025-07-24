namespace GameHeader
{
    using System;

    public class ProgressUpdateEventArgs : EventArgs
    {
        public readonly ProgressUpdateInfo Info;

        public ProgressUpdateEventArgs(ProgressUpdateInfo info)
        {
            this.Info = info;
        }
    }
}