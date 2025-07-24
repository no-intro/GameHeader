namespace GameHeader.Abstract
{
    using GameHeader;
    using System;

    public abstract class Game
    {
        protected byte[] _crc32;
        protected GameHeader.Abstract.GameFileInfo _game_info;
        protected byte[] _md5;
        protected byte[] _sha1;
        protected byte[] _sha256;
        protected GameHeader.Abstract.SystemType _system_type;

        public Game(GameHeader.Abstract.SystemType type, GameHeader.Abstract.GameFileInfo game_info)
        {
            this._system_type = type;
            this._game_info = game_info;
        }

        public virtual string Dump()
        {
            string str = Util.Divider("File Data", '=') + Util.Pad("System:") + this.System() + "\r\n";
            str = str + Util.Pad("Path:") + this._game_info.Path + "\r\n";
            // show archive / file
            if (this._game_info.Compressed)
            {
                str = str + Util.Pad("Archive:") + this._game_info.FileName + "\r\n";
                str = str + Util.Pad("File:") + this._game_info.EntryName + "\r\n";
            }
            else
            {
                str = str + Util.Pad("Archive:") + "" + "\r\n";
                str = str + Util.Pad("File:") + this._game_info.FileName + "\r\n";
            }
            // size
            str = str + Util.Pad("BitSize:") + Util.BitSize(this._game_info.Length * 8L) + "\r\n";
            str = str + Util.Pad("Size (Bytes):") + this._game_info.Length + "\r\n";
            // hashes
            str = str + Util.Pad("CRC32:") + Util.CaseHash(this._crc32) + "\r\n" + Util.Pad("MD5:") + Util.CaseHash(this._md5) + "\r\n" + Util.Pad("SHA1:") + Util.CaseHash(this._sha1) + "\r\n" + Util.Pad("SHA256:") + Util.CaseHash(this._sha256) + "\r\n";
            return str;
        }

        public abstract string System();

        public byte[] CRC32
        {
            get
            {
                return this._crc32;
            }
            set
            {
                this._crc32 = value;
            }
        }

        public GameHeader.Abstract.GameFileInfo GameFileInfo
        {
            get
            {
                return this._game_info;
            }
        }

        public byte[] MD5
        {
            get
            {
                return this._md5;
            }
            set
            {
                this._md5 = value;
            }
        }

        public byte[] SHA1
        {
            get
            {
                return this._sha1;
            }
            set
            {
                this._sha1 = value;
            }
        }

        public byte[] SHA256
        {
            get
            {
                return this._sha256;
            }
            set
            {
                this._sha256 = value;
            }
        }

        public GameHeader.Abstract.SystemType SystemType
        {
            get
            {
                return this._system_type;
            }
        }
    }
}