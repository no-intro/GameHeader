namespace GameHeader
{
    using System;

    public class UpdateLogEventArgs : EventArgs
    {
        public readonly UpdateLogInfo Info;

        public UpdateLogEventArgs(UpdateLogInfo info)
        {
            this.Info = info;
        }
    }
}