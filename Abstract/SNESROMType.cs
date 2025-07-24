namespace GameHeader.Abstract
{
    using System;

    public enum SNESROMType
    {
        ExtendedHiROM = 0x40ffb0,
        HiROM = 0xffb0,
        LoROM = 0x7fb0,
        Undertermined = 0
    }
}