namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;

    public class SNESGame : Game
    {
        private int __checksum;
        private byte[] __header;
        private int __inverse_checksum;
        private SNESROMType __rom;
        private SNESCartType __type;

        public SNESGame(GameFileInfo game_info) : base(SystemType.SNES, game_info)
        {
        }

        public override string Dump()
        {
            string str = base.Dump() + Util.Divider("Detection Data", '=') + Util.Pad("ROM Type:");
            SNESROMType type = this.__rom;
            if (type == SNESROMType.LoROM)
            {
                str = str + "LoROM";
            }
            else if (type == SNESROMType.HiROM)
            {
                str = str + "HiROM";
            }
            else if (type == SNESROMType.ExtendedHiROM)
            {
                str = str + "Extended HiROM";
            }
            else
            {
                str = str + "Undetermined";
            }
            str = str + "\r\n" + Util.Pad("Cartridge Type:");
            switch (this.__type)
            {
                case SNESCartType.Cart:
                    str = str + "Normal";
                    break;

                case SNESCartType.BSX:
                    str = str + "BSX";
                    break;

                case SNESCartType.BSCart:
                    str = str + "BS Add-on";
                    break;

                case SNESCartType.ST:
                    str = str + "Sufami Turbo";
                    break;

                default:
                    str = str + "Undetermined";
                    break;
            }
            str = str + "\r\n";
            if (this.__header != null)
            {
                str = str + Util.Divider("Header Data", '=');
                bool flag = Program.GetConfig().GetFlag("NDS_OUTPUT", "FullHeaderDump");
                SNESHeader header = new SNESHeader(this.__header);
                str = (str + Util.Pad("New Maker Code:") + ((header.OldMaker == "33") ? (header.NewMaker + " (" + Program.DB.GetValue("NINTENDO_MAKER_CODES", header.NewMaker, "Unknown") + ")") : " N/A") + "\r\n") + Util.Pad("Serial:") + ((header.OldMaker == "33") ? (header.Serial + " (" + Program.DB.GetValue("NINTENDO_REGIONS", header.Serial[3].ToString(), "Unknown") + ")") : " N/A") + "\r\n";
                if (flag)
                {
                    string str2 = str;
                    str = str2 + Util.Pad("SFX SRAM Size:") + "0x" + header.SFXSRAMSize + "\r\n";
                }
                string str3 = str + Util.Pad("Title:") + header.Title + "\r\n";
                string[] strArray2 = new string[9];
                strArray2[0] = str3;
                strArray2[1] = Util.Pad("Map Type:");
                strArray2[2] = "0x";
                strArray2[3] = header.MapType;
                strArray2[4] = " (";
                char ch2 = header.MapType[0];
                strArray2[5] = Program.DB.GetValue("NINTENDO_SNES_ROM_SPEEDS", ch2.ToString(), "Unknown");
                strArray2[6] = "; ";
                char ch3 = header.MapType[1];
                strArray2[7] = Program.DB.GetValue("NINTENDO_SNES_BANK_TYPES", ch3.ToString(), "Unknown");
                strArray2[8] = ")\r\n";
                str = string.Concat(strArray2);
                if (this.__type == SNESCartType.Cart)
                {
                    string str4 = str;
                    str = str4 + Util.Pad("ROM Type:") + "0x" + header.ROMType + " (" + Program.DB.GetValue("NINTENDO_SNES_ROM_TYPES", header.ROMType, "Unknown") + ")\r\n";
                    int num = ((int) 1) << ((Util.Int(header.ROMSize) - 7) + 0x11);
                    string str5 = str;
                    str = str5 + Util.Pad("ROM Size:") + "0x" + header.ROMSize + " (" + Util.BitSize((long) (num * 8)) + "; " + ((num == base._game_info.Length) ? "Ok" : "Bad") + ")\r\n";
                    int num2 = ((int) 1) << ((3 + Util.Int(header.SRAMSize)) + 7);
                    string str6 = str;
                    string str7 = str6 + Util.Pad("SRAM Size:") + "0x" + header.SRAMSize + " (" + ((header.SRAMSize == "00") ? "None" : Util.BitSize((long) (num2 * 8))) + ")\r\n";
                    str = str7 + Util.Pad("Country:") + "0x" + header.Country + " (" + Program.DB.GetValue("NINTENDO_SNES_COUNTRIES", header.Country, "Unknown") + ")\r\n";
                }
                else if ((this.__type == SNESCartType.BSCart) || (this.__type == SNESCartType.BSX))
                {
                    object obj2 = str;
                    object obj3 = string.Concat(new object[] { obj2, Util.Pad("BS Month:"), "0x", header.BSMonth, " (", Util.Int(header.BSMonth), ")\r\n" });
                    string str8 = string.Concat(new object[] { obj3, Util.Pad("BS Day:"), "0x", header.BSDay, " (", Util.Int(header.BSDay), ")\r\n" });
                    string str9 = str8 + Util.Pad("BS MapType:") + "0x" + header.BSMapType + "\r\n";
                    str = str9 + Util.Pad("BS Type:") + "0x" + header.BSType + "\r\n";
                }
                string str10 = str;
                object obj4 = str10 + Util.Pad("Old Maker Code:") + "0x" + header.OldMaker + " (" + Program.DB.GetValue("NINTENDO_MAKER_CODES", header.OldMaker, "Unknown") + ")\r\n";
                string str11 = string.Concat(new object[] { obj4, Util.Pad("Version:"), "0x", header.Version, " (v1.", Util.Int(header.Version), ")\r\n" });
                string str12 = str11 + Util.Pad("Inverse Checksum:") + "0x" + Util.Hex((long) header.InverseChecksum, 4) + " (" + ((this.InverseChecksum == header.InverseChecksum) ? "Ok" : ("Bad; 0x" + Util.Hex((long) this.InverseChecksum, 4))) + ")\r\n";
                str = str12 + Util.Pad("Checksum:") + "0x" + Util.Hex((long) header.Checksum, 4) + " (" + ((this.Checksum == header.Checksum) ? "Ok" : ("Bad; 0x" + Util.Hex((long) this.Checksum, 4))) + ")\r\n";
            }
            return (str + Util.Divider('='));
        }

        public override string System()
        {
            return SystemName.SNES;
        }

        public int Checksum
        {
            get
            {
                return this.__checksum;
            }
            set
            {
                this.__checksum = value;
            }
        }

        public byte[] Header
        {
            get
            {
                return this.__header;
            }
            set
            {
                this.__header = value;
            }
        }

        public int InverseChecksum
        {
            get
            {
                return this.__inverse_checksum;
            }
            set
            {
                this.__inverse_checksum = value;
            }
        }

        public SNESROMType ROM
        {
            get
            {
                return this.__rom;
            }
            set
            {
                this.__rom = value;
            }
        }

        public SNESCartType Type
        {
            get
            {
                return this.__type;
            }
            set
            {
                this.__type = value;
            }
        }
    }
}