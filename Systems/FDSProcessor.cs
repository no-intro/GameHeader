namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;
    using System.Collections;
    using System.Security.Cryptography;

    public class FDSProcessor : Processor
    {
        private byte[] __data;

        public FDSProcessor() : base(SystemType.FDS)
        {
            base._extensions = base._config.GetValue("AUTODETECT_EXTENSIONS", "FDS").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
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
            return SystemName.FDS;
        }

        public override void Initialize(GameFileInfo game_info)
        {
            base._game = new FDSGame(game_info);
            base.Initialize(game_info);
            this.__data = new byte[base._game_info.Length];
        }

        public override void ProcessBlock(byte[] data, long start)
        {
            base.ProcessBlock(data, start);
            Util.CopyTo(data, start, this.__data, 0, data.Length);
        }

        public override Game Finalize()
        {
            FDSGame game = (FDSGame) base.Finalize();
            int start = 0;
            if (Util.Hex(this.__data, 0, 4) == "4644531A")
            {
                game.FDSHeader = Util.Extract(this.__data, start, 0x10);
                game.Headerless_Size = this.__data.Length - 0x10;
                game.Headerless_CRC32 = new CRC32().ComputeHash(this.__data, 0x10, this.__data.Length - 0x10);
                game.Headerless_MD5 = MD5.Create().ComputeHash(this.__data, 0x10, this.__data.Length - 0x10);
                game.Headerless_SHA1 = SHA1.Create().ComputeHash(this.__data, 0x10, this.__data.Length - 0x10);
                game.Headerless_SHA256 = SHA256.Create().ComputeHash(this.__data, 0x10, this.__data.Length - 0x10);
                start += 0x10;
            }
            ArrayList list = new ArrayList();
            list.AddRange(Util.Extract(this.__data, start, 0x3a));
            start += 0x3a;
            FDSNumberBlock block = new FDSNumberBlock(this.__data, start - 2);
            int num2 = Util.Int(block.NumFiles);
            for (int i = 0; i < num2; i++)
            {
                int num4 = Util.Int(Util.RHex(this.__data, start + 13, 2));
                list.AddRange(Util.Extract(this.__data, start, 0x10));
                start += 0x11 + num4;
            }
            game.DataSideA = (byte[]) list.ToArray(typeof(byte));
            start = 0xffdc;
            if ((start + 0x44) < base._game_info.Length)
            {
                if (Util.Hex(this.__data, 0, 4) == "4644531A")
                {
                    game.FDSHeader = Util.Extract(this.__data, start, 0x10);
                    start += 0x10;
                }
                ArrayList list2 = new ArrayList();
                list2.AddRange(Util.Extract(this.__data, start, 0x3a));
                start += 0x3a;
                FDSNumberBlock block2 = new FDSNumberBlock(this.__data, start - 2);
                num2 = Util.Int(block2.NumFiles);
                for (int j = 0; j < num2; j++)
                {
                    int num6 = Util.Int(Util.RHex(this.__data, start + 13, 2));
                    list2.AddRange(Util.Extract(this.__data, start, 0x10));
                    start += 0x11 + num6;
                }
                game.DataSideB = (byte[]) list2.ToArray(typeof(byte));
            }
            return game;
        }
    }
}