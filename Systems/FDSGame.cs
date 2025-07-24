namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;

    public class FDSGame : Game
    {
        private byte[] __data_side_a;
        private byte[] __data_side_b;
        private byte[] __fds_header;
        private int __headerless_size;
        private byte[] __headerless_crc32;
        private byte[] __headerless_md5;
        private byte[] __headerless_sha1;
        private byte[] __headerless_sha256;
        public static readonly string[] TargetAreas = new string[] { "Code", "Tiles", "Picture" };

        public FDSGame(GameFileInfo game_info) : base(SystemType.FDS, game_info)
        {
        }

        private string __DumpSide(string title, byte[] data)
        {
            string str = "";
            str = str + Util.Divider(title + " Header Data", '-');
            FDSSideHeader header = new FDSSideHeader(data);
            FDSNumberBlock block = new FDSNumberBlock(data, 0x38);
            string str2 = str + Util.Pad("Block Type") + header.BlockType + "\r\n";
            string str3 = str2 + Util.Pad("Disk ID:") + header.DiskID + " (" + ((header.DiskID == "*NINTENDO-HVC*") ? "Ok" : "Bad") + ")\r\n";
            object obj2 = (str3 + Util.Pad("Maker ID:") + header.MakerID + " (" + Program.DB.GetValue("NINTENDO_MAKER_CODES", header.MakerID, "Unknown") + ")\r\n") + Util.Pad("Game Name:") + header.GameName + "\r\n";
            string str4 = string.Concat(new object[] { obj2, Util.Pad("Version:"), header.Version, " (v1.", Util.Int(header.Version), ")\r\n" });
            object obj3 = str4 + Util.Pad("Side Number:") + header.SideNumber + " (" + ((header.SideNumber == "00") ? "A" : ((header.SideNumber == "01") ? "B" : "Unknown")) + ")\r\n";
            object obj4 = ((string.Concat(new object[] { obj3, Util.Pad("Disk Number:"), header.DiskNumber, " (", Util.Int(header.DiskNumber) + 1, ")\r\n" }) + Util.Pad("Extra Disk ID:") + header.ExtraDiskID + "\r\n") + Util.Pad("Max Autoload ID:") + header.MaxAutoloadID + "\r\n") + Util.Pad("Block Type:") + block.BlockType + "\r\n";
            str = string.Concat(new object[] { obj4, Util.Pad("File Count:"), block.NumFiles, " (", Util.Int(block.NumFiles), ")\r\n" });
            int index = 0x3a;
            str = (str + Util.Divider(title + " File Data", '-')) + "| Type |  No. |  ID  |   Name   |       Size       | Address |  Target Area   |\r\n" + "+------+------+------+----------+------------------+---------+----------------+\r\n";
            int num2 = Util.Int(block.NumFiles);
            for (int i = 0; i < num2; i++)
            {
                FDSFileHeader header2 = new FDSFileHeader(data, index);
                string introduced24 = (((str + "| ") + "0x" + header2.BlockType + " | ") + "0x" + header2.FileNumber + " | ") + "0x" + header2.FileID + " | ";
                string str5 = introduced24 + header2.FileName + " | ";
                str = (str5 + "0x" + header2.FileSize + " " + Util.RPad("(" + Util.Int(header2.FileSize) + " B)", 9) + " | ") + " 0x" + header2.TargetAddress + " | ";
                int num4 = Util.Int(header2.TargetArea);
                string str6 = str;
                str = (str6 + "0x" + header2.TargetArea + " " + Util.Pad("(" + ((num4 < TargetAreas.Length) ? TargetAreas[num4] : "Unknown") + ")", 9) + " |") + "\r\n";
                index += 0x10;
            }
            return str;
        }

        public override string Dump()
        {
            string str = base.Dump() + this.__DumpSide("Side A", this.__data_side_a);
            if (this.__data_side_b != null)
            {
                str = str + this.__DumpSide("Side B", this.__data_side_b);
            }
            if (this.__fds_header != null)
            {
                str = str + Util.Divider("Headerless Data", '-') + Util.Pad("Size (Bytes):") + this.Headerless_Size.ToString() + "\r\n" + Util.Pad("CRC32:") + Util.CaseHash(this.Headerless_CRC32) + "\r\n" + Util.Pad("MD5:") + Util.CaseHash(this.Headerless_MD5) + "\r\n" + Util.Pad("SHA1:") + Util.CaseHash(this.Headerless_SHA1) + "\r\n" + Util.Pad("SHA256:") + Util.CaseHash(this.Headerless_SHA256) + "\r\n";
            }
            return (str + Util.Divider('='));
        }

        public override string System()
        {
            return SystemName.FDS;
        }

        public byte[] DataSideA
        {
            get
            {
                return this.__data_side_a;
            }
            set
            {
                this.__data_side_a = value;
            }
        }

        public byte[] DataSideB
        {
            get
            {
                return this.__data_side_b;
            }
            set
            {
                this.__data_side_b = value;
            }
        }

        public byte[] FDSHeader
        {
            get
            {
                return this.__fds_header;
            }
            set
            {
                this.__fds_header = value;
            }
        }

        public byte[] Headerless_CRC32
        {
            get
            {
                if (this.__fds_header == null)
                {
                    return base._crc32;
                }
                return this.__headerless_crc32;
            }
            set
            {
                this.__headerless_crc32 = value;
            }
        }

        public byte[] Headerless_MD5
        {
            get
            {
                if (this.__fds_header == null)
                {
                    return base._md5;
                }
                return this.__headerless_md5;
            }
            set
            {
                this.__headerless_md5 = value;
            }
        }

        public byte[] Headerless_SHA1
        {
            get
            {
                if (this.__fds_header == null)
                {
                    return base._sha1;
                }
                return this.__headerless_sha1;
            }
            set
            {
                this.__headerless_sha1 = value;
            }
        }

        public byte[] Headerless_SHA256
        {
            get
            {
                if (this.__fds_header == null)
                {
                    return base._sha256;
                }
                return this.__headerless_sha256;
            }
            set
            {
                this.__headerless_sha256 = value;
            }
        }

        public int Headerless_Size
        {
            get
            {
                return this.__headerless_size;
            }
            set
            {
                this.__headerless_size = value;
            }
        }
    }
}