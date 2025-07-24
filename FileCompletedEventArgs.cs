namespace GameHeader
{
    using GameHeader.Abstract;
    using System;

    public class FileCompletedEventArgs : EventArgs
    {
        public readonly GameHeader.Abstract.Game Game;

        public FileCompletedEventArgs(GameHeader.Abstract.Game game)
        {
            this.Game = game;
        }
    }
}