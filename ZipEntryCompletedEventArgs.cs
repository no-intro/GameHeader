namespace GameHeader
{
    using GameHeader.Abstract;
    using System;

    public class ZipEntryCompletedEventArgs : EventArgs
    {
        public readonly GameHeader.Abstract.Game Game;

        public ZipEntryCompletedEventArgs(GameHeader.Abstract.Game game)
        {
            this.Game = game;
        }
    }
}

