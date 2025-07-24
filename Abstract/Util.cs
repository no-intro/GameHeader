namespace GameHeader.Abstract
{
    using GameHeader;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class Util
    {
        private static ASCIIEncoding __ascii = new ASCIIEncoding();
        private static CRC16 __crc16 = new CRC16();
        private static CRC32 __crc32 = new CRC32();
        private static MD5CryptoServiceProvider __md5 = new MD5CryptoServiceProvider();
        private static SHA1CryptoServiceProvider __sha1 = new SHA1CryptoServiceProvider();
        private static SHA256CryptoServiceProvider __sha256 = new SHA256CryptoServiceProvider();
        private static Encoding __sjis = Encoding.GetEncoding("shift-jis");
        private static UnicodeEncoding __unicode = new UnicodeEncoding();

        public static string ASCII(byte[] bytes, int start, int len)
        {
            if (bytes != null)
            {
                return __ascii.GetString(bytes, start, len).Replace("\n", "\r\n").Replace("\0", " ");
            }
            return "";
        }

        public static string BitSize(long in_bits)
        {
            string[] strArray = new string[] { "", "K", "M", "G", "T" };
            int index = 0;
            while ((in_bits / 0x400L) > 0L)
            {
                index++;
                in_bits /= 0x400L;
            }
            return string.Concat(new object[] { in_bits, " ", strArray[index], "bit" });
        }

        public static void CopyTo(byte[] inarry, long instart, byte[] outarray, int outstart, int outlength)
        {
            long num = (instart < outstart) ? (outstart - instart) : 0L;
            long num2 = ((num + outlength) > inarry.Length) ? (inarry.Length - num) : ((long) outlength);
            long num3 = (instart < outstart) ? 0L : (instart - outstart);
            for (int i = 0; i < num2; i++)
            {
                outarray[(int) ((IntPtr) (num3 + i))] = inarry[(int) ((IntPtr) (num + i))];
            }
        }

        public static string Divider(char fill)
        {
            string str = "-";
            fill = '-';
            for (int i = 0; i < 0x41; i++)
            {
                str = str + fill;
            }
            return (str + "-\r\n");
        }

        public static string Divider(string str, char fill)
        {
            string str2 = "-";
            fill = '-';
            for (int i = 0; i < 3; i++)
            {
                str2 = str2 + fill;
            }
            str2 = str2 + "| " + str + " |";
            for (int j = 0; j < ((0x41 - str.Length) - 7); j++)
            {
                str2 = str2 + fill;
            }
            return (str2 + "-\r\n");
        }

        public static byte[] Extract(byte[] bytes, int start, int len)
        {
            byte[] buffer = new byte[len];
            for (int i = start; i < (start + len); i++)
            {
                buffer[i - start] = bytes[i];
            }
            return buffer;
        }

        public static string Hex(long num, int padding)
        {
            return string.Format("{0:X" + padding.ToString() + "}", num);
        }

        public static string Hex(byte[] bytes, int start, int len)
        {
            if (bytes == null)
            {
                return "";
            }
            string str = "";
            for (int i = start; i < (start + len); i++)
            {
                if (i < bytes.Length)
                {
                    str = str + string.Format("{0:X2}", bytes[i]);
                }
                else
                {
                    str = string.Format("{0:X2}", 0) + str;
                }
            }
            return str;
        }

        public static int Int(string str)
        {
            if (str != null)
            {
                return Convert.ToInt32(str, 0x10);
            }
            return -1;
        }

        public static string Md5(byte[] data, int start, int len)
        {
            byte[] bytes = __md5.ComputeHash(data, start, len);
            return Hex(bytes, 0, 0x10);
        }

        public static string Pad(string str)
        {
            //return str;
            return string.Format("{0,-" + Program.GetConfig().GetValue("OUTPUT", "Padding") + "}", str);
        }

        public static string Pad(string str, int pad)
        {
            return string.Format("{0,-" + pad + "}", str);
        }

        public static string CaseHash(byte[] hash)
        {
            if (hash == null)
            {
                return Hex(hash, 0, hash.Length);
            }
            string str = "";
            for (int i = 0; i < hash.Length; i += 4)
            {
                str = str + Hex(hash, i, Math.Min(4, hash.Length - i));
            }
            string option = Program.Config.GetValue("OUTPUT", "CaseHash", "ToLower");
            if (option == "ToLower")
            {
                str = str.ToLower();
            }
            else if (option == "ToUpper")
            {
                str = str.ToUpper();
            }
            return str;
        }

        public static byte[] RExtract(byte[] bytes, int start, int len)
        {
            byte[] buffer = new byte[len];
            for (int i = start; i < (start + len); i++)
            {
                buffer[((len - i) + start) - 1] = bytes[i];
            }
            return buffer;
        }

        public static string RHex(byte[] bytes, int start, int len)
        {
            if (bytes == null)
            {
                return "";
            }
            string str = "";
            for (int i = (start + len) - 1; i >= start; i--)
            {
                str = str + string.Format("{0:X2}", bytes[i]);
            }
            return str;
        }

        public static string RPad(string str, int pad)
        {
            return string.Format("{0," + pad + "}", str);
        }

        public static string Sha1(byte[] data, int start, int len)
        {
            byte[] bytes = __sha1.ComputeHash(data, start, len);
            return Hex(bytes, 0, 20);
        }
        public static string Sha256(byte[] data, int start, int len)
        {
            byte[] bytes = __sha256.ComputeHash(data, start, len);
            return Hex(bytes, 0, 32);
        }

        public static string SJIS(byte[] bytes, int start, int len)
        {
            if (bytes != null)
            {
                return __sjis.GetString(bytes, start, len).Replace("\n", "\r\n").Replace("\0", " ");
            }
            return "";
        }

        public static string UNICODE(byte[] bytes, int start, int len)
        {
            if (bytes != null)
            {
                return __unicode.GetString(bytes, start, len).Replace("\n", "\r\n").Replace("\0", " ");
            }
            return "";
        }
    }
}