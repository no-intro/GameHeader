using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GameHeader
{
    static class Program
    {
        private static INI __config;
        private static INI __db;

        public static INI GetConfig()
        {
            return __config;
        }

        public static INI GetDB()
        {
            return __db;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            __config = INI.Load(Application.StartupPath + "/Settings.ini");
            __config.SetDefaultValue("GUI", "DefaultState", "Normal");
            __config.SetDefaultValue("GUI", "Width", "850");
            __config.SetDefaultValue("GUI", "Height", "530");
            __config.SetDefaultValue("GUI", "LastScanPath", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            __config.SetDefaultValue("OUTPUT", "Padding", "20");
            __config.SetDefaultValue("OUTPUT", "FullPath", "False");
            __config.SetDefaultValue("OUTPUT", "CaseHash", "ToLower");
            __config.SetDefaultValue("ENGINE", "Type", "Autodetect");
            __config.SetDefaultValue("ENGINE", "ParseUnknownFiles", "True");
            __config.SetDefaultValue("AUTODETECT_EXTENSIONS", "GBA", ".gba");
            __config.SetDefaultValue("AUTODETECT_EXTENSIONS", "GBC", ".gbc,.cgb");
            __config.SetDefaultValue("AUTODETECT_EXTENSIONS", "GB", ".gb");
            __config.SetDefaultValue("AUTODETECT_EXTENSIONS", "NDS", ".nds,.ids");
            __config.SetDefaultValue("AUTODETECT_EXTENSIONS", "FDS", ".fds");
            __config.SetDefaultValue("AUTODETECT_EXTENSIONS", "N64", ".n64");
            __config.SetDefaultValue("AUTODETECT_EXTENSIONS", "NES", ".nes");
            __config.SetDefaultValue("AUTODETECT_EXTENSIONS", "SNES", ".sfc,.smc");
            __config.SetDefaultValue("HASHCALC_ONLY", "Extensions", ".txt,.ini,.nfo,.sfv");
            __config.SetDefaultValue("NDS_OUTPUT", "ShowLanguageInfo", "True");
            __config.SetDefaultValue("NDS_OUTPUT", "CalculateEncryptedData", "True");
            __config.SetDefaultValue("NDS_OUTPUT", "FullHeaderDump", "True");
            __config.SetDefaultValue("N64_OUTPUT", "FullHeaderDump", "True");
            __config.SetDefaultValue("GBA_OUTPUT", "FullHeaderDump", "True");
            __config.SetDefaultValue("GBA_OUTPUT", "DetectBackupMedia", "True");
            __config.SetDefaultValue("NES_OUTPUT", "Detect3rdPartyHeaders", "True");
            __config.SetDefaultValue("NES_OUTPUT", "DetectInternalHeader", "True");
            __config.SetDefaultValue("SNES_OUTPUT", "FullHeaderDump", "True");
            __config.SetDefaultValue("SNES_OUTPUT", "ROMDetection", "Auto");
            __db = INI.Load(Application.StartupPath + "/Database.ini");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GForm(args));
            __config.WriteValues();
        }

        public static INI Config
        {
            get
            {
                return __config;
            }
        }

        public static INI DB
        {
            get
            {
                return __db;
            }
        }
	}
}