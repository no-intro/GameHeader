namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;

    public class GBCGame : Game
    {
        private string __global_checksum;
        private byte[] __header;
        private string __header_checksum;
        private string __logo_crc16;

        public GBCGame(GameFileInfo game_info) : base(SystemType.GBC, game_info)
        {
        }

        public override string Dump()
        {
            string str = base.Dump();
            GBCHeader header = new GBCHeader(this.__header);
            string str2 = str + Util.Divider("Header Data", '-');
            string str3 = ((str2 + Util.Pad("Entry Point:") + "0x" + header.EntryPoint + "\r\n") + Util.Pad("Logo Code: ") + ((this.LogoCRC16 == "6E93") ? "Ok" : "Bad") + "\r\n") + Util.Pad("Title:") + header.Title + "\r\n";
            string[] strArray2 = new string[6];
            strArray2[0] = str3;
            strArray2[1] = Util.Pad("Serial:");
            strArray2[2] = header.Serial;
            strArray2[3] = " (";
            char ch = header.Serial[3];
            strArray2[4] = Program.DB.GetValue("NINTENDO_REGIONS", ch.ToString(), "Unknown");
            strArray2[5] = ")\r\n";
            string str4 = string.Concat(strArray2);
            string str5 = (str4 + Util.Pad("GBC Flag:") + "0x" + header.GBCFlag + " (" + ((header.GBCFlag == "80") ? "GBC & GB Compatible" : ((header.GBCFlag == "C0") ? "GBC Only" : "Unknown")) + ")\r\n") + Util.Pad("New Licensee Code:") + ((header.OldLicensee == "33") ? (header.NewLicensee + " (" + Program.DB.GetValue("NINTENDO_MAKER_CODES", header.NewLicensee, "Unknown") + ")") : "N/A") + "\r\n";
            string str6 = str5 + Util.Pad("SGB Support:") + "0x" + header.SGBFlag + " (" + ((header.SGBFlag == "03") ? "Yes" : "No") + ")\r\n";
            str = str6 + Util.Pad("Cartridge Type:") + "0x" + header.CartridgeType + " (" + Program.DB.GetValue("NINTENDO_GAME_BOY_ROM_TYPES", header.CartridgeType, "Unknown") + ")\r\n";
            int num = ((int) 0x400) << (5 + Util.Int(header.ROMSize));
            string str7 = str;
            str = str7 + Util.Pad("ROM Size: ") + "0x" + header.ROMSize + " (" + ((num == base._game_info.Length) ? "Ok" : "Bad") + "; " + Util.BitSize((long) (num * 8)) + "; " + num.ToString() + " Bytes)\r\n";
            int num2 = ((int) 1) << ((Util.Int(header.RAMSize) * 2) + 2);
            string str8 = str;
            string str9 = str8 + Util.Pad("RAM Size:") + "0x" + header.RAMSize + " (" + ((header.RAMSize == "00") ? "None" : (num2 + " Kbit")) + ")\r\n";
            object obj2 = str9 + Util.Pad("Old Licensee Code:") + "0x" + header.OldLicensee + ((header.OldLicensee != "33") ? (" (" + Program.DB.GetValue("NINTENDO_MAKER_CODES", header.OldLicensee, "Unknown") + ")") : "") + "\r\n";
            string str10 = string.Concat(new object[] { obj2, Util.Pad("Version:"), "0x", header.Version, " (v1.", Util.Int(header.Version), ")\r\n" });
            string str11 = str10 + Util.Pad("Header Checksum:") + "0x" + header.Checksum + " (" + ((this.HeaderChecksum == header.Checksum) ? "Ok" : ("Bad; 0x" + this.HeaderChecksum)) + ")\r\n";
            return ((str11 + Util.Pad("Global Checksum:") + "0x" + header.GlobalChecksum + " (" + ((this.GlobalChecksum == header.GlobalChecksum) ? "Ok" : ("Bad; 0x" + this.GlobalChecksum)) + ")\r\n") + Util.Divider('='));
        }

        public override string System()
        {
            return SystemName.GBC;
        }

        public string GlobalChecksum
        {
            get
            {
                return this.__global_checksum;
            }
            set
            {
                this.__global_checksum = value;
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

        public string HeaderChecksum
        {
            get
            {
                return this.__header_checksum;
            }
            set
            {
                this.__header_checksum = value;
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
    }
}