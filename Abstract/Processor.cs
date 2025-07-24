namespace GameHeader.Abstract
{
    using GameHeader;
    using System;
    using System.Security.Cryptography;

    public abstract class Processor
    {
        protected INI _config = Program.GetConfig();
        protected CRC32 _crc32;
        protected INI _db = Program.GetDB();
        protected string[] _extensions;
        protected Game _game;
        protected GameFileInfo _game_info;
        protected MD5 _md5;
        protected SHA1 _sha1;
        protected SHA256 _sha256;
        protected GameHeader.Abstract.SystemType _system_type;

        public Processor(GameHeader.Abstract.SystemType type)
        {
            this._system_type = type;
        }

        public virtual bool DetectExtension(string ext)
        {
            foreach (string str in this._extensions)
            {
                if (str == ext)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual Game Finalize()
        {
            this._crc32.TransformFinalBlock(new byte[0], 0, 0);
            this._md5.TransformFinalBlock(new byte[0], 0, 0);
            this._sha1.TransformFinalBlock(new byte[0], 0, 0);
            this._sha256.TransformFinalBlock(new byte[0], 0, 0);
            this._game.CRC32 = this._crc32.Hash;
            this._game.MD5 = this._md5.Hash;
            this._game.SHA1 = this._sha1.Hash;
            this._game.SHA256 = this._sha256.Hash;
            return this._game;
        }

        public virtual void Initialize(GameFileInfo game_info)
        {
            this._game_info = game_info;
            this._crc32 = new CRC32();
            this._md5 = MD5.Create();
            this._sha1 = SHA1.Create();
            this._sha256 = SHA256.Create();
        }

        public virtual void ProcessBlock(byte[] data, long start)
        {
            int inputOffset = 0;
            int length = data.Length;
            int outputOffset = 0;
            this._crc32.TransformBlock(data, inputOffset, length, data, outputOffset);
            this._md5.TransformBlock(data, inputOffset, length, data, outputOffset);
            this._sha1.TransformBlock(data, inputOffset, length, data, outputOffset);
            this._sha256.TransformBlock(data, inputOffset, length, data, outputOffset);
        }

        public abstract string System();

        public GameHeader.Abstract.SystemType SystemType
        {
            get
            {
                return this._system_type;
            }
        }
    }
}