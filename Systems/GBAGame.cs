namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;

    public class GBAGame : Game
    {
        private string __complement;
        private byte[] __header;
        private string __logo_crc16;
        private string __save_size;
        private string __save_type;

        public GBAGame(GameFileInfo game_info) : base(SystemType.GBA, game_info)
        {
        }

        public override string Dump()
        {
            string str = base.Dump();
            GBAHeader header = new GBAHeader(this.__header);
            bool flag = Program.GetConfig().GetFlag("GBA_OUTPUT", "FullHeaderDump");
            string str2 = (((str + Util.Divider("Header Data", '-')) + Util.Pad("ROM Entry Point:") + header.EntryPoint + "\r\n") + Util.Pad("Logo Code:") + ((this.LogoCRC16 == "2E03") ? "Ok" : "Bad") + "\r\n") + Util.Pad("Title:") + header.Title + "\r\n";
            string[] strArray = new string[6];
            strArray[0] = str2;
            strArray[1] = Util.Pad("Serial:");
            strArray[2] = header.Serial;
            strArray[3] = " (";
            char ch = header.Serial[3];
            strArray[4] = Program.DB.GetValue("NINTENDO_REGIONS", ch.ToString(), "Unknown");
            strArray[5] = ")\r\n";
            string str3 = string.Concat(strArray);
            string str4 = str3 + Util.Pad("Maker Code:") + header.MakerCode + " (" + Program.DB.GetValue("NINTENDO_MAKER_CODES", header.MakerCode, "Unknown") + ")\r\n";
            string str5 = str4 + Util.Pad("Fixed Value:") + "0x" + header.FixedValue + " (" + ((header.FixedValue == "96") ? "Ok" : "Bad") + ")\r\n";
            string str6 = str5 + Util.Pad("Unit Code:") + "0x" + header.UnitCode + "\r\n";
            str = str6 + Util.Pad("Device Code:") + "0x" + header.DeviceCode + "\r\n";
            if (flag)
            {
                string str7 = str;
                str = str7 + Util.Pad("Reserved 1:") + "0x" + header.Reserved1 + "\r\n";
            }
            object obj2 = str;
            string str8 = string.Concat(new object[] { obj2, Util.Pad("Version:"), "0x", header.Version, " (v1.", Util.Int(header.Version), ")\r\n" });
            str = str8 + Util.Pad("Complement:") + "0x" + header.Complement + " (" + ((header.Complement == this.Complement) ? "Ok" : ("Bad; 0x" + header.Complement)) + ")\r\n";
            if (flag)
            {
                string str9 = str;
                str = str9 + Util.Pad("Reserved 2:") + "0x" + header.Reserved2 + "\r\n";
            }
            if (this.SaveType != null)
            {
                str = ((str + Util.Divider("Backup Media", '-')) + Util.Pad("Chip:") + this.SaveType + "\r\n") + Util.Pad("Size:") + this.SaveSize + "\r\n";
            }
            return (str + Util.Divider('='));
        }

        public override string System()
        {
            return SystemName.GBA;
        }

        public string Complement
        {
            get
            {
                return this.__complement;
            }
            set
            {
                this.__complement = value;
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

        public string LogoCRC16
        {
            get
            {
                return this.__logo_crc16;
            }
            set
            {
                this.__logo_crc16 = value;
            }
        }

        public string SaveSize
        {
            get
            {
                return this.__save_size;
            }
            set
            {
                this.__save_size = value;
            }
        }

        public string SaveType
        {
            get
            {
                return this.__save_type;
            }
            set
            {
                this.__save_type = value;
            }
        }
    }
}