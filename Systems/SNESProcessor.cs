namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;
    using System.Collections.Generic;

    public class SNESProcessor : Processor
    {
        private int __base_address;
        private byte[] __data;
        private SNESGame __game;
        public const int ExtendedHiROM_Start = 0x40ffb0;
        public const int Header_Length = 0x30;
        public const int HiROM_Start = 0xffb0;
        public const int LoROM_Start = 0x7fb0;

        public SNESProcessor() : base(SystemType.SNES)
        {
            base._extensions = base._config.GetValue("AUTODETECT_EXTENSIONS", "SNES").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override string System()
        {
            return SystemName.SNES;
        }

        public override void Initialize(GameFileInfo game_info)
        {
            base._game = new SNESGame(game_info);
            base.Initialize(game_info);
            this.__data = new byte[base._game_info.Length];
            this.__base_address = 0;
            this.__game = null;
        }

        public override void ProcessBlock(byte[] data, long start)
        {
            base.ProcessBlock(data, start);
            Util.CopyTo(data, start, this.__data, 0, data.Length);
        }

        public int ComputeChecksum(int start)
        {
            int num = 0;
            int num2 = 0;
            SNESCartType cartType = this.GetCartType(start);
            int num4 = ((int) 1) << ((this.__data[start + 0x27] - 7) + 0x11);
            int num3 = ((num4 >> 1) > base._game_info.Length) ? ((int) Math.Pow(2.0, Math.Ceiling(Math.Log((double) base._game_info.Length, 2.0)) - 1.0)) : (num4 >> 1);
            for (int i = 0; i < num3; i++)
            {
                num += this.__data[i];
            }
            for (int j = num3; j < this.__data.Length; j++)
            {
                num2 += this.__data[j];
            }
            num += num2;
            switch (cartType)
            {
                case SNESCartType.BSX:
                case SNESCartType.BSCart:
                    for (int k = 0; k < 0x30; k++)
                    {
                        num -= this.__data[start + k];
                    }
                    break;

                default:
                    if (((this.__data[start + 0x25] == 0x3a) && (this.__data[start + 0x26] == 0xf5)) && (this.__data[start + 0x27] == 12))
                    {
                        num *= 2;
                    }
                    else if (((this.__data[start + 0x25] != 0x3a) || (this.__data[start + 0x26] != 0xf9)) && (num3 != base._game_info.Length))
                    {
                        num += num2 * ((num3 / (((int) base._game_info.Length) - num3)) - 1);
                    }
                    break;
            }
            return (num & 0xffff);
        }

        private SNESCartType GetCartType(int start)
        {
            byte num = this.__data[start + 0x25];
            byte num2 = this.__data[start + 0x2a];
            byte num3 = this.__data[start + 0x27];
            byte num4 = this.__data[start + 0x26];
            if (((num2 == 0x33) || (num2 == 0xff)) && ((num == 0) || ((num & 0x83) == 0x80)))
            {
                int num5 = (num3 << 8) | num4;
                if (num5 == 0)
                {
                    return SNESCartType.BSCart;
                }
                if ((num5 == 0xffff) || (((num4 & 15) == 0) && (((num4 >> 4) - 1) < 12)))
                {
                    return SNESCartType.BSX;
                }
            }
            return SNESCartType.Cart;
        }

        private void SetDetectedHeader()
        {
            SNESROMType[] typeArray = new SNESROMType[] { SNESROMType.LoROM, SNESROMType.HiROM, SNESROMType.ExtendedHiROM };
            List<SNESHeader> list = new List<SNESHeader>();
            List<SNESROMType> list2 = new List<SNESROMType>();
            for (int i = 0; i < typeArray.Length; i++)
            {
                SNESROMType item = typeArray[i];
                int start = this.__base_address + (int) item;
                if (((this.__base_address + (int) item) + 0x30) <= base._game_info.Length)
                {
                    SNESHeader header = new SNESHeader(Util.Extract(this.__data, start, 0x30));
                    list.Add(header);
                    if (((header.CombinedChecksum == 0xffff) && (header.Checksum != 0xff)) && (header.Checksum != 0))
                    {
                        list2.Add(item);
                    }
                }
            }
            if (list2.Count > 1)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (list2.Contains(typeArray[j]) && (this.ComputeChecksum(this.__base_address + (int) typeArray[j]) != list[j].Checksum))
                    {
                        list2.Remove(typeArray[j]);
                    }
                }
            }
            int count = list2.Count;
            if (list2.Count == 0)
            {
                list2.Add(SNESROMType.LoROM);
            }
            this.SetHeader(list2[0]);
        }

        private void SetHeader(SNESROMType rom)
        {
            int start = this.__base_address + (int) rom;
            if ((start + 0x30) <= base._game_info.Length)
            {
                this.__game.ROM = rom;
                this.__game.Type = this.GetCartType(start);
                this.__game.Header = Util.Extract(this.__data, start, 0x30);
                int num2 = this.ComputeChecksum(start);
                this.__game.Checksum = num2;
                this.__game.InverseChecksum = ~num2 & 0xffff;
            }
        }

        public override Game Finalize()
        {
            this.__game = (SNESGame) base.Finalize();
            string str = Program.Config.GetValue("SNES_OUTPUT", "ROMDetection", "Auto");
            if (str == "LoROM")
            {
                this.SetHeader(SNESROMType.LoROM);
            }
            else if (str == "HiROM")
            {
                this.SetHeader(SNESROMType.HiROM);
            }
            else if (str == "ExtendedHiROM")
            {
                this.SetHeader(SNESROMType.ExtendedHiROM);
            }
            else
            {
                this.SetDetectedHeader();
            }
            return this.__game;
        }
    }
}