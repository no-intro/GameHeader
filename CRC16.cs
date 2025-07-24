namespace GameHeader
{
    using System;
    using System.Security.Cryptography;

    public class CRC16 : HashAlgorithm
    {
        private ushort __crc;
        private ushort __seed;
        private ushort[] __table;
        private const int __table_length = 0x100;
        public const ushort DefaultPolynomial = 0xa001;
        public const ushort DefaultSeed = 0;

        public CRC16()
        {
            this.__seed = 0;
            this.Initialize();
        }

        public CRC16(ushort seed)
        {
            this.__seed = seed;
            this.Initialize();
        }

        protected override void HashCore(byte[] buffer, int start, int length)
        {
            for (int i = start; i < (start + length); i++)
            {
                byte index = (byte) (this.__crc ^ buffer[i]);
                this.__crc = (ushort) ((this.__crc >> 8) ^ this.__table[index]);
            }
        }

        protected override byte[] HashFinal()
        {
            base.HashValue = new byte[] { (byte) ((this.__crc >> 8) & 0xff), (byte) (this.__crc & 0xff) };
            return base.HashValue;
        }

        public override void Initialize()
        {
            this.__crc = this.__seed;
            this.InitializeTable(0xa001);
        }

        private void InitializeTable(ushort polynomial)
        {
            this.__table = new ushort[0x100];
            for (ushort i = 0; i < 0x100; i = (ushort) (i + 1))
            {
                ushort num = 0;
                ushort num2 = i;
                for (byte j = 0; j < 8; j = (byte) (j + 1))
                {
                    if (((num ^ num2) & 1) != 0)
                    {
                        num = (ushort) ((num >> 1) ^ polynomial);
                    }
                    else
                    {
                        num = (ushort) (num >> 1);
                    }
                    num2 = (ushort) (num2 >> 1);
                }
                this.__table[i] = num;
            }
        }

        public override int HashSize
        {
            get
            {
                return 0x10;
            }
        }
    }
}