namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;

    public class N64Processor : Processor
    {
        private byte[] __data;
        public const int Boot_Length = 0xfc0;
        public const int Boot_Start = 0x40;
        public const int Checksum_Length = 0x100000;
        public const int Checksum_Start = 0x1000;
        public const int Header_Length = 0x1000;
        public const int Header_Start = 0;

        public N64Processor() : base(SystemType.N64)
        {
            base._extensions = base._config.GetValue("AUTODETECT_EXTENSIONS", "N64").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
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
            return SystemName.N64;
        }

        public override void Initialize(GameFileInfo game_info)
        {
            base._game = new N64Game(game_info);
            base.Initialize(game_info);
            this.__data = new byte[game_info.Length];
        }

        public override void ProcessBlock(byte[] data, long start)
        {
            base.ProcessBlock(data, start);
            Util.CopyTo(data, start, this.__data, 0, data.Length);
        }

        public uint BYTES2LONG(byte[] b, int i)
        {
            return (uint) ((((b[i] << 0x18) | (b[i + 1] << 0x10)) | (b[i + 2] << 8)) | b[i + 3]);
        }

        public uint ROL(uint x, byte n)
        {
            return ((x << n) | (x >> (0x20 - n)));
        }

        public override Game Finalize()
        {
            int num2;
            uint num6;
            uint num7;
            uint num8;
            uint num9;
            uint num10;
            N64Game game = (N64Game) base.Finalize();
            game.Header = Util.Extract(this.__data, 0, 0x1000);
            CRC32 crc = new CRC32();
            uint num = (uint) Util.Int(Util.Hex(crc.ComputeHash(this.__data, 0x40, 0xfc0), 0, 4));
            uint num4 = 0;
            uint[] numArray = new uint[2];
            switch (num)
            {
                case 0x90bb6cb5:
                case 0x9e9ea3:
                    game.BootChip = "CIC-NUS-6102";
                    num4 = 0xf8ca4ddc;
                    num2 = 0x17d6;
                    break;

                case 0x98bc2c86:
                    game.BootChip = "CIC-NUS-6105";
                    num4 = 0xdf26f436;
                    num2 = 0x17d9;
                    break;

                case 0xacc8580a:
                    game.BootChip = "CIC-NUS-6106";
                    num4 = 0x1fea617a;
                    num2 = 0x17da;
                    break;

                case 0xb050ee0:
                    game.BootChip = "CIC-NUS-6103";
                    num4 = 0xa3886759;
                    num2 = 0x17d7;
                    break;

                case 0x6170a4a1:
                    game.BootChip = "CIC-NUS-6101";
                    num4 = 0xf8ca4ddc;
                    num2 = 0x17d5;
                    break;

                default:
                    num2 = 0;
                    game.BootChip = "Unknown";
                    break;
            }
            uint num5 = num6 = num7 = num8 = num9 = num10 = num4;
            for (int i = 0x1000; i < 0x101000; i += 4)
            {
                uint x = this.BYTES2LONG(this.__data, i);
                if ((num10 + x) < num10)
                {
                    num8++;
                }
                num10 += x;
                num7 ^= x;
                uint num11 = this.ROL(x, (byte) (x & 0x1f));
                num9 += num11;
                if (num6 > x)
                {
                    num6 ^= num11;
                }
                else
                {
                    num6 ^= num10 ^ x;
                }
                if (num2 == 0x17d9)
                {
                    num5 += this.BYTES2LONG(this.__data, 0x750 + (i & 0xff)) ^ x;
                }
                else
                {
                    num5 += num9 ^ x;
                }
            }
            switch (num2)
            {
                case 0x17d7:
                    numArray[0] = (num10 ^ num8) + num7;
                    numArray[1] = (num9 ^ num6) + num5;
                    break;

                case 0x17da:
                    numArray[0] = (num10 * num8) + num7;
                    numArray[1] = (num9 * num6) + num5;
                    break;

                default:
                    numArray[0] = (num10 ^ num8) ^ num7;
                    numArray[1] = (num9 ^ num6) ^ num5;
                    break;
            }
            game.CRC1 = Util.Hex((long) numArray[0], 8);
            game.CRC2 = Util.Hex((long) numArray[1], 8);
            return game;
        }
    }
}