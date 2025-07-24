namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;

    public class N64Game : Game
    {
        private string __boot_chip;
        private string __crc1;
        private string __crc2;
        private byte[] __header;

        public N64Game(GameFileInfo game_info) : base(SystemType.N64, game_info)
        {
        }

        public override string Dump()
        {
            string str = base.Dump();
            N64Header header = new N64Header(this.__header);
            bool flag = Program.GetConfig().GetFlag("N64_OUTPUT", "FullHeaderDump");
            str = str + Util.Divider("Header Data", '-');
            if (flag)
            {
                string str2 = str;
                string str3 = str2 + Util.Pad("Fixed Value 1:") + "0x" + header.FixedValue1 + "\r\n";
                string str4 = str3 + Util.Pad("Compression Flag:") + "0x" + header.CompressionFlag + "\r\n";
                string str5 = str4 + Util.Pad("Fixed Value 2:") + "0x" + header.FixedValue2 + "\r\n";
                string str6 = str5 + Util.Pad("EntryPoint:") + "0x" + header.EntryPoint + "\r\n";
                string str7 = str6 + Util.Pad("Fixed Value 3:") + "0x" + header.FixedValue3 + "\r\n";
                str = str7 + Util.Pad("Unknown Value:") + "0x" + header.UnknownValue1 + "\r\n";
            }
            string str8 = str + Util.Pad("Boot Chip:") + this.BootChip + "\r\n";
            string str9 = str8 + Util.Pad("CRC1:") + "0x" + this.Header_CRC1 + " (" + ((this.Header_CRC1 == this.CRC1) ? "Ok" : ("Bad; 0x" + this.CRC1)) + ")\r\n";
            str = str9 + Util.Pad("CRC2:") + "0x" + this.Header_CRC2 + " (" + ((this.Header_CRC2 == this.CRC2) ? "Ok" : ("Bad; 0x" + this.CRC2)) + ")\r\n";
            if (flag)
            {
                string str10 = str;
                str = str10 + Util.Pad("Reserved 1:") + "0x" + header.Reserved1 + "\r\n";
            }
            str = str + Util.Pad("Title:") + header.Title + "\r\n";
            if (flag)
            {
                string str11 = str;
                str = str11 + Util.Pad("Reserved 2:") + "0x" + header.Reserved1 + "\r\n";
            }
            string str12 = str;
            string[] strArray11 = new string[6];
            strArray11[0] = str12;
            strArray11[1] = Util.Pad("Serial:");
            strArray11[2] = header.Serial;
            strArray11[3] = " (";
            char ch = header.Serial[3];
            strArray11[4] = Program.DB.GetValue("NINTENDO_REGIONS", ch.ToString(), "Unknown");
            strArray11[5] = ")\r\n";
            object obj2 = string.Concat(strArray11);
            return (string.Concat(new object[] { obj2, Util.Pad("Version:"), "0x", header.Version, " (v1.", Util.Int(header.Version), ")\r\n" }) + Util.Divider('='));
        }

        public override string System()
        {
            return SystemName.N64;
        }

        public string BootChip
        {
            get
            {
                return this.__boot_chip;
            }
            set
            {
                this.__boot_chip = value;
            }
        }

        public string CRC1
        {
            get
            {
                return this.__crc1;
            }
            set
            {
                this.__crc1 = value;
            }
        }

        public string CRC2
        {
            get
            {
                return this.__crc2;
            }
            set
            {
                this.__crc2 = value;
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

        public string Header_CRC1
        {
            get
            {
                return Util.Hex(this.__header, 0x10, 4);
            }
        }

        public string Header_CRC2
        {
            get
            {
                return Util.Hex(this.__header, 20, 4);
            }
        }
    }
}