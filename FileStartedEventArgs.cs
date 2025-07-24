namespace GameHeader
{
    using System;

    public class FileStartedEventArgs : EventArgs
    {
        public readonly FileWorkerInfo Info;

        public FileStartedEventArgs(FileWorkerInfo info)
        {
            this.Info = info;
        }
    }
}