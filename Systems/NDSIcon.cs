namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct NDSIcon
    {
        private byte[] __icon;

        public NDSIcon(byte[] icon)
        {
            this.__icon = icon;
        }

        public string Version
        {
            get
            {
                return Util.RHex(this.__icon, 0, 2);
            }
        }

        public string CRC16
        {
            get
            {
                return Util.RHex(this.__icon, 2, 2);
            }
        }

        public string Data
        {
            get
            {
                return Util.Hex(this.__icon, 0x20, 0x200);
            }
        }

        public string Palette
        {
            get
            {
                return Util.Hex(this.__icon, 0x220, 0x20);
            }
        }

        public string Japanese
        {
            get
            {
                return Util.UNICODE(this.__icon, 0x240, 0x100);
            }
        }

        public string English
        {
            get
            {
                return Util.UNICODE(this.__icon, 0x340, 0x100);
            }
        }

        public string French
        {
            get
            {
                return Util.UNICODE(this.__icon, 0x440, 0x100);
            }
        }

        public string German
        {
            get
            {
                return Util.UNICODE(this.__icon, 0x540, 0x100);
            }
        }

        public string Italian
        {
            get
            {
                return Util.UNICODE(this.__icon, 0x640, 0x100);
            }
        }

        public string Spanish
        {
            get
            {
                return Util.UNICODE(this.__icon, 0x740, 0x100);
            }
        }
    }
}