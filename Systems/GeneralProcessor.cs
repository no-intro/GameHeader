namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;

    public class GeneralProcessor : Processor
    {
        public GeneralProcessor() : base(SystemType.General)
        {
        }

        public override bool DetectExtension(string ext)
        {
            return base._config.GetFlag("ENGINE", "ParseUnknownFiles");
        }

        public override string System()
        {
            return SystemName.General;
        }

        public override void Initialize(GameFileInfo game_info)
        {
            base._game = new GeneralGame(game_info);
            base.Initialize(game_info);
        }

        public override void ProcessBlock(byte[] data, long start)
        {
            base.ProcessBlock(data, start);
        }

        public override Game Finalize()
        {
            return (GeneralGame) base.Finalize();
        }
    }
}