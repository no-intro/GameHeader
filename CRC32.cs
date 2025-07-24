namespace GameHeader
{
    using System;
    using System.Security.Cryptography;

    public class CRC32 : HashAlgorithm
    {
        private uint __crc;
        private uint __seed;
        private uint[] __table;
        private const int __table_length = 0x100;
        public const uint DefaultPolynomial = 0xedb88320;
        public const uint DefaultSeed = uint.MaxValue;

        public CRC32()
        {
            this.__seed = uint.MaxValue;
            this.Initialize();
        }

        public CRC32(uint seed)
        {
            this.__seed = seed;
            this.Initialize();
        }

        protected override void HashCore(byte[] buffer, int start, int length)
        {
            for (int i = start; i < (start + length); i++)
            {
                byte index = (byte) (this.__crc ^ buffer[i]);
                this.__crc = (this.__crc >> 8) ^ this.__table[index];
            }
        }

        protected override byte[] HashFinal()
        {
            this.__crc = ~this.__crc;
            base.HashValue = new byte[] { (byte) ((this.__crc >> 0x18) & 0xff), (byte) ((this.__crc >> 0x10) & 0xff), (byte) ((this.__crc >> 8) & 0xff), (byte) (this.__crc & 0xff) };
            return base.HashValue;
        }

        public override void Initialize()
        {
            this.__crc = this.__seed;
            this.InitializeTable(0xedb88320);
        }

        private void InitializeTable(uint polynomial)
        {
            this.__table = new uint[0x100];
            for (int i = 0; i < 0x100; i++)
            {
                uint num = (uint) i;
                for (int j = 0; j < 8; j++)
                {
                    if ((num & 1) == 1)
                    {
                        num = (num >> 1) ^ polynomial;
                    }
                    else
                    {
                        num = num >> 1;
                    }
                }
                this.__table[i] = num;
            }
        }

        public override int HashSize
        {
            get
            {
                return 0x20;
            }
        }
    }
}