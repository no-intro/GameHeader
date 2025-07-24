namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;

    public class NGCProcessor : Processor
    {
        private byte[] __data;
        public const int Header_Length = 0x1000;
        public const int Header_Start = 0;
        private bool passed;

        public NGCProcessor() : base(SystemType.NGC)
        {
            base._extensions = null;
        }

        public override bool DetectExtension(string ext)
        {
            return false;
        }

        public override string System()
        {
            return SystemName.NGC;
        }

        public override void Initialize(GameFileInfo game_info)
        {
            passed = false;
            base._game = new NGCGame(game_info);
            base.Initialize(game_info);
            this.__data = new byte[Header_Length];
        }

        public override void ProcessBlock(byte[] data, long start)
        {
            if (!passed)
            {
                base.ProcessBlock(data, Header_Start);
                Util.CopyTo(data, Header_Start, this.__data, Header_Start, Header_Length);
                passed = true;
            }
        }

        public override Game Finalize()
        {
            NGCGame game = (NGCGame)base.Finalize();
            game.Header = Util.Extract(this.__data, Header_Start, Header_Length);
            return game;
        }
    }
}