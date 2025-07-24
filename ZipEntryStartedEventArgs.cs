namespace GameHeader
{
    using System;

    public class ZipEntryStartedEventArgs : EventArgs
    {
        public readonly ZipWorkerInfo Info;

        public ZipEntryStartedEventArgs(ZipWorkerInfo info)
        {
            this.Info = info;
        }
    }
}

