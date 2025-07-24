namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;
    using System.Text.RegularExpressions;

    public class GBAProcessor : Processor
    {
        private byte[] __data;
        private CRC16 __logocrc16;
        public const int Header_Length = 0xc0;
        public const int Header_Start = 0;

        public GBAProcessor() : base(SystemType.GBA)
        {
            this.__logocrc16 = new CRC16();
            base._extensions = base._config.GetValue("AUTODETECT_EXTENSIONS", "GBA").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
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
            return SystemName.GBA;
        }

        public override void Initialize(GameFileInfo game_info)
        {
            base._game = new GBAGame(game_info);
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
            GBAGame game = (GBAGame) base.Finalize();
            game.Header = Util.Extract(this.__data, 0, 0xc0);
            game.LogoCRC16 = Util.Hex(this.__logocrc16.ComputeHash(this.__data, 4, 0x9c), 0, 2);
            int num = 0;
            for (int i = 160; i <= 0xbc; i++)
            {
                num -= this.__data[i];
            }
            num = (num - 0x19) & 0xff;
            game.Complement = Util.Hex((long) num, 2);
            if (Program.Config.GetFlag("GBA_OUTPUT", "DetectBackupMedia"))
            {
                string str;
                string str2;
                Match match = new Regex("(SRAM_V|FLASH(_|512_|1M_)V|EEPROM_V|SRAM_F_V)[0-9]{3,3}").Match(Util.ASCII(this.__data, 0, this.__data.Length));
                if (match.Success)
                {
                    str = match.Value;
                    if (str.Contains("SRAM_"))
                    {
                        str2 = "265 Kbit";
                    }
                    else if (str.Contains("EEPROM_"))
                    {
                        str2 = "4/64 Kbit";
                    }
                    else if (str.Contains("FLASH_") || str.Contains("FLASH512_"))
                    {
                        str2 = "512 Kbit";
                    }
                    else if (str.Contains("FLASH1M_"))
                    {
                        str2 = "1 Mbit";
                    }
                    else
                    {
                        str2 = "Unknown";
                    }
                }
                else
                {
                    str = "None";
                    str2 = "N/A";
                }
                game.SaveType = str;
                game.SaveSize = str2;
            }
            return game;
        }
    }
}