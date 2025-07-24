namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;

    public class GBProcessor : Processor
    {
        private byte[] __data;
        private CRC16 __logocrc16;
        public const int Header_Length = 0x150;
        public const int Header_Start = 0x100;

        public GBProcessor() : base(SystemType.GB)
        {
            this.__logocrc16 = new CRC16();
            base._extensions = base._config.GetValue("AUTODETECT_EXTENSIONS", "GB").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override bool DetectExtension(string ext)
        {
            foreach (string str in base._extensions)
            {
                if (str == ext)
                {
                    return true;
                }
            }
            return false;
        }

        public override string System()
        {
            return SystemName.GB;
        }

        public override void Initialize(GameFileInfo game_info)
        {
            base._game = new GBGame(game_info);
            base.Initialize(game_info);
            this.__data = new byte[game_info.Length];
            this.__logocrc16.Initialize();
        }

        public override void ProcessBlock(byte[] data, long start)
        {
            base.ProcessBlock(data, start);
            Util.CopyTo(data, start, this.__data, 0, data.Length);
        }

        public override Game Finalize()
        {
            GBGame game = (GBGame) base.Finalize();
            game.Header = Util.Extract(this.__data, 0x100, 0x150);
            game.LogoCRC16 = Util.Hex(this.__logocrc16.ComputeHash(this.__data, 260, 0x30), 0, 2);
            int num = 0;
            for (int i = 0x134; i <= 0x14c; i++)
            {
                num = (num - this.__data[i]) - 1;
            }
            num &= 0xff;
            game.HeaderChecksum = Util.Hex((long) num, 2);
            int num3 = 0;
            for (int j = 0; j < this.__data.Length; j++)
            {
                if ((j != 0x14e) && (j != 0x14f))
                {
                    num3 += this.__data[j];
                }
            }
            num3 &= 0xffff;
            game.GlobalChecksum = Util.Hex((long) num3, 4);
            return game;
        }
    }
}