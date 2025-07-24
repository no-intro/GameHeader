namespace GameHeader.Systems
{
    using GameHeader;
    using GameHeader.Abstract;
    using System;
    using System.Security.Cryptography;

    public class NDSProcessor : Processor
    {
        private byte[] __data;
        private CRC16 __header16;
        private CRC16 __icon16;
        private CRC16 __logo16;
        private CRC16 __secure16;
        public const int Header_Length = 0x1000;
        public const int Header_Start = 0;
        public const int Icon_Length = 0x840;
        public const int Secure_Length = 0x4000;
        public const int Secure_Start = 0x4000;

        public NDSProcessor() : base(SystemType.NDS)
        {
            this.__icon16 = new CRC16(0xffff);
            this.__logo16 = new CRC16(0xffff);
            this.__secure16 = new CRC16(0xffff);
            this.__header16 = new CRC16(0xffff);
            base._extensions = base._config.GetValue("AUTODETECT_EXTENSIONS", "NDS").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override bool DetectExtension(string ext)
        {
            foreach (string str in base._extensions)
            {
                if (str == ext)
                {
                    return true;
                }
            }
            return false;
        }

        public override string System()
        {
            return SystemName.NDS;
        }

        public override void Initialize(GameFileInfo game_info)
        {
            base._game = new NDSGame(game_info);
            base.Initialize(game_info);
            this.__data = new byte[game_info.Length];
            this.__icon16.Initialize();
            this.__logo16.Initialize();
            this.__secure16.Initialize();
            this.__header16.Initialize();
        }

        public override void ProcessBlock(byte[] data, long start)
        {
            base.ProcessBlock(data, start);
            Util.CopyTo(data, start, this.__data, 0, data.Length);
        }

        public override Game Finalize()
        {
            NDSGame game = (NDSGame) base.Finalize();
            game.Header = Util.Extract(this.__data, 0, 0x1000);
            int start = game.Icon_Start;
            game.Icon = Util.Extract(this.__data, start, 0x840);
            game.SecureArea = Util.Extract(this.__data, 0x4000, 0x4000);
            this.__header16.TransformFinalBlock(this.__data, 0, 0xffe);
            this.__icon16.TransformFinalBlock(this.__data, start + 0x20, 0x820);
            this.__logo16.TransformFinalBlock(this.__data, 0xc0, 0x9c);
            this.__secure16.TransformFinalBlock(this.__data, 0x4000, 0x4000);
            game.HeaderCRC16 = Util.Hex(this.__header16.Hash, 0, 2);
            game.IconCRC16 = Util.Hex(this.__icon16.Hash, 0, 2);
            game.LogoCRC16 = Util.Hex(this.__logo16.Hash, 0, 2);
            game.SecureCRC16 = Util.Hex(this.__secure16.Hash, 0, 2);
            if (game.Decrypted && Program.GetConfig().GetFlag("NDS_OUTPUT", "CalculateEncryptedData"))
            {
                byte[] buffer = new NDSEcryptor().Encrypt(game.SecureArea, (uint) Util.Int(Util.RHex(game.Header, 12, 4)));
                game.Encrypted_SecureArea = buffer;
                buffer.CopyTo(this.__data, 0x4000);
                CRC16 crc = new CRC16(0xffff);
                game.Encrypted_SecureCRC16 = Util.Hex(crc.ComputeHash(buffer, 0, buffer.Length), 0, 2);
                game.Encrypted_CRC32 = new CRC32().ComputeHash(this.__data, 0, this.__data.Length);
                game.Encrypted_MD5 = MD5.Create().ComputeHash(this.__data, 0, this.__data.Length);
                game.Encrypted_SHA1 = SHA1.Create().ComputeHash(this.__data, 0, this.__data.Length);
                game.Encrypted_SHA256 = SHA256.Create().ComputeHash(this.__data, 0, this.__data.Length);
            }
            this.__data = null;
            base._game = null;
            return game;
        }
    }
}