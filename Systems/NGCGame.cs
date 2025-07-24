namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;

    public class NGCGame : Game
    {
        private byte[] __header;

        public NGCGame(GameFileInfo game_info) : base(SystemType.NGC, game_info)
        {
        }

        public override string Dump()
        {
            string str = base.Dump();
            NGCHeader header = new NGCHeader(this.__header);
            str = str + Util.Divider("Header Data", '-');
            str = str + Util.Pad("Game Title:") + header.GameName.Trim() + "\r\n";
            str = str + Util.Pad("Game Serial:") + header.SystemId + header.GameCode + header.RegionCode;
            str = str + " (" + Program.DB.GetValue("NINTENDO_REGIONS", header.RegionCode, "Unknown") + ")";
            str = str + "\r\n";
            str = str + Util.Pad("Maker Code:") + header.MakerCode;
            str = str + " (" + Program.DB.GetValue("NGC_MAKER_CODES", header.MakerCode, "Unknown") + ")";
            str = str + "\r\n";
            str = str + Util.Pad("Version:") + "0x" + header.Version + " (v1." + Util.Int(header.Version) + ")\r\n";
            str = str + Util.Pad("Disc Id:") + ((header.DiscId == "00") ? "1" : "2") + "\r\n";
            return (string.Concat(new object[] { str }) + Util.Divider('='));
        }

        public override string System()
        {
            return SystemName.NGC;
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
    }
}