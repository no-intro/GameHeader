namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;

    public class SNESHeader
    {
        private byte[] __header;

        public SNESHeader(byte[] header)
        {
            this.__header = header;
        }

        public string BSDay
        {
            get
            {
                return Util.Hex(this.__header, 0x27, 1);
            }
        }

        public string BSMapType
        {
            get
            {
                return Util.Hex(this.__header, 40, 1);
            }
        }

        public string BSMonth
        {
            get
            {
                return Util.Hex(this.__header, 0x26, 1);
            }
        }

        public string BSType
        {
            get
            {
                return Util.Hex(this.__header, 0x29, 1);
            }
        }

        public int Checksum
        {
            get
            {
                return ((this.__header[0x2f] << 8) + this.__header[0x2e]);
            }
        }

        public int CombinedChecksum
        {
            get
            {
                return (this.Checksum + this.InverseChecksum);
            }
        }

        public string Country
        {
            get
            {
                return Util.Hex(this.__header, 0x29, 1);
            }
        }

        public byte[] Header
        {
            get
            {
                return this.__header;
            }
        }

        public int InverseChecksum
        {
            get
            {
                return ((this.__header[0x2d] << 8) + this.__header[0x2c]);
            }
        }

        public string MapType
        {
            get
            {
                return Util.Hex(this.__header, 0x25, 1);
            }
        }

        public string NewMaker
        {
            get
            {
                return Util.ASCII(this.__header, 0, 2);
            }
        }

        public string OldMaker
        {
            get
            {
                return Util.Hex(this.__header, 0x2a, 1);
            }
        }

        public string ROMSize
        {
            get
            {
                return Util.Hex(this.__header, 0x27, 1);
            }
        }

        public string ROMType
        {
            get
            {
                return Util.Hex(this.__header, 0x26, 1);
            }
        }

        public string Serial
        {
            get
            {
                return Util.ASCII(this.__header, 2, 4);
            }
        }

        public string SFXSRAMSize
        {
            get
            {
                return Util.Hex(this.__header, 13, 1);
            }
        }

        public string SRAMSize
        {
            get
            {
                return Util.Hex(this.__header, 40, 1);
            }
        }

        public string Title
        {
            get
            {
                return Util.SJIS(this.__header, 0x10, 0x15);
            }
        }

        public string Version
        {
            get
            {
                return Util.Hex(this.__header, 0x2b, 1);
            }
        }
    }
}