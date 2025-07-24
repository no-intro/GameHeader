namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;
    using System.Security.Cryptography;

    public class NESProcessor : Processor
    {
        private byte[] __data;

        public NESProcessor() : base(SystemType.NES)
        {
            base._extensions = base._config.GetValue("AUTODETECT_EXTENSIONS", "NES").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
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
            return SystemName.NES;
        }

        public override void Initialize(GameFileInfo game_info)
        {
            base._game = new NESGame(game_info);
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
            NESGame game = (NESGame) base.Finalize();
            int start = 0;
            if (NESiNESHeader.Detect(this.__data))
            {
                game.iNESHeader = Util.Extract(this.__data, 0, 0x10);
                start += 0x10;
                NESiNESHeader header = new NESiNESHeader(game.iNESHeader);
                int num2 = Util.Int(header.NumPRGBanks);
                Util.Int(header.NumCHRBanks);
                start += ((num2 * 0x10) * 0x400) - 0x20;
                game.Headerless_Size = this.__data.Length - 0x10;
                game.Headerless_CRC32 = new CRC32().ComputeHash(this.__data, 0x10, this.__data.Length - 0x10);
                game.Headerless_MD5 = MD5.Create().ComputeHash(this.__data, 0x10, this.__data.Length - 0x10);
                game.Headerless_SHA1 = SHA1.Create().ComputeHash(this.__data, 0x10, this.__data.Length - 0x10);
                game.Headerless_SHA256 = SHA256.Create().ComputeHash(this.__data, 0x10, this.__data.Length - 0x10);
                if (!Program.Config.GetFlag("NES_OUTPUT", "DetectInternalHeader"))
                {
                    return game;
                }
                game.Header = Util.Extract(this.__data, start, 0x20);
                int num3 = 0x10;
                string str = "";
                int num4 = 1;
                string str2 = Util.Hex(this.__data, 0x800a, 6);
                while ((num3 + 0x4000) <= base._game_info.Length)
                {
                    num3 += 0x8000;
                    string str3 = Util.Hex(this.__data, num3 - 6, 6);
                    if (!(str2 == ""))
                    {
                        bool flag1 = str3 == str2;
                    }
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, "\r\n", num4, ":", str3 });
                    num4++;
                }
                game.Banks = str;
                int num5 = 0;
                int num6 = 0x10;
                if (base._game_info.Length < (num6 + ((num2 * 0x10) * 0x400)))
                {
                    return game;
                }
                for (int i = num6; i < (num6 + ((num2 * 0x10) * 0x400)); i++)
                {
                    if ((i < ((num6 + ((num2 * 0x10) * 0x400)) - 0x10)) || (i > ((num6 + ((num2 * 0x10) * 0x400)) - 15)))
                    {
                        num5 += this.__data[i];
                    }
                }
                game.PRGChecksum = Util.Hex((long) (num5 & 0xffff), 4);
                num5 = 0;
                num6 = 0x10 + ((num2 * 0x10) * 0x400);
                for (int j = num6; j < base._game_info.Length; j++)
                {
                    num5 += this.__data[j];
                }
                game.CHRChecksum = Util.Hex((long) (num5 & 0xffff), 4);
            }
            return game;
        }
    }
}