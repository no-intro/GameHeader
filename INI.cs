namespace GameHeader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;

    public class INI
    {
        private Dictionary<string, Dictionary<string, string>> __data;
        private bool __exists;
        private string __fullname;
        private static Dictionary<string, INI> __inis = new Dictionary<string, INI>();
        private bool __modified;

        public INI(string iniFile)
        {
            FileInfo info = new FileInfo(iniFile);
            this.__data = new Dictionary<string, Dictionary<string, string>>();
            this.__exists = info.Exists;
            this.__fullname = info.FullName;
            if (this.__exists)
            {
                foreach (string str in this.__GetCategories(this.__fullname))
                {
                    List<string> list2 = this.__GetKeys(this.__fullname, str);
                    this.__data[str] = new Dictionary<string, string>();
                    foreach (string str2 in list2)
                    {
                        string str3 = this.__GetValue(this.__fullname, str, str2, "");
                        this.__data[str][str2] = str3;
                    }
                }
            }
            this.__modified = false;
        }

        private List<string> __GetCategories(string iniFile)
        {
            string lpReturnString = new string(' ', 0x10000);
            GetPrivateProfileString(null, null, null, lpReturnString, 0x10000, iniFile);
            List<string> list = new List<string>(lpReturnString.Split(new char[1]));
            list.RemoveRange(list.Count - 2, 2);
            return list;
        }

        private List<string> __GetKeys(string iniFile, string category)
        {
            string lpReturnString = new string(' ', 0x8000);
            GetPrivateProfileString(category, null, null, lpReturnString, 0x8000, iniFile);
            List<string> list = new List<string>(lpReturnString.Split(new char[1]));
            list.RemoveRange(list.Count - 2, 2);
            return list;
        }

        private string __GetValue(string iniFile, string category, string key, string defaultValue)
        {
            string lpReturnString = new string(' ', 0x400);
            GetPrivateProfileString(category, key, defaultValue, lpReturnString, 0x400, iniFile);
            return lpReturnString.Split(new char[1])[0];
        }

        private void __WriteValue(string iniFile, string category, string key, string value)
        {
            WritePrivateProfileString(category, key, value, iniFile);
        }

        public bool Exists()
        {
            return this.__exists;
        }

        public static INI Get(string iniFile)
        {
            if (!__inis.ContainsKey(iniFile))
            {
                return null;
            }
            return __inis[iniFile];
        }

        public string[] GetCategories()
        {
            return null;
        }

        public bool GetFlag(string category, string key)
        {
            return ((this.__data.ContainsKey(category) && this.__data[category].ContainsKey(key)) && (this.__data[category][key] == bool.TrueString));
        }

        public bool GetFlag(string category, string key, bool defaultFlag)
        {
            if (!this.__data.ContainsKey(category) || !this.__data[category].ContainsKey(key))
            {
                return defaultFlag;
            }
            return (this.__data[category][key] == bool.TrueString);
        }

        public string[] GetKeys(string category)
        {
            return null;
        }

        [DllImport("KERNEL32.DLL", EntryPoint="GetPrivateProfileStringW", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Unicode, SetLastError=true, ExactSpelling=true)]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnString, int nSize, string lpFilename);
        public string GetValue(string category, string key)
        {
            if (this.__data.ContainsKey(category) && this.__data[category].ContainsKey(key))
            {
                return this.__data[category][key];
            }
            return "";
        }

        public string GetValue(string category, string key, string defaultValue)
        {
            if (this.__data.ContainsKey(category) && this.__data[category].ContainsKey(key))
            {
                return this.__data[category][key];
            }
            return defaultValue;
        }

        public void InvertFlag(string category, string key)
        {
            if (!this.__data.ContainsKey(category))
            {
                this.__data[category] = new Dictionary<string, string>();
            }
            if (!this.__data[category].ContainsKey(key))
            {
                this.__data[category][key] = "False";
            }
            if (this.__data[category][key] == bool.FalseString)
            {
                this.__data[category][key] = bool.TrueString;
            }
            else
            {
                this.__data[category][key] = bool.FalseString;
            }
            this.__modified = true;
        }

        public static INI Load(string iniFile)
        {
            INI ini = new INI(iniFile);
            __inis[iniFile] = ini;
            return ini;
        }

        public bool Modified()
        {
            return this.__modified;
        }

        public void SetDefaultValue(string category, string key, string value)
        {
            if (!this.__data.ContainsKey(category) || !this.__data[category].ContainsKey(key))
            {
                this.__data[category] = new Dictionary<string, string>();
                this.__data[category][key] = value;
                this.__modified = true;
            }
        }

        public void SetFlag(string category, string key, bool flag)
        {
            this.__data[category][key] = flag.ToString();
            this.__modified = true;
        }

        public void SetValue(string category, string key, string value)
        {
            this.__data[category][key] = value;
            this.__modified = true;
        }

        [DllImport("KERNEL32.DLL", EntryPoint="WritePrivateProfileStringW", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Unicode, SetLastError=true, ExactSpelling=true)]
        private static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFilename);
        public bool WriteValues()
        {
            if (this.__exists)
            {
                foreach (KeyValuePair<string, Dictionary<string, string>> pair in this.__data)
                {
                    foreach (KeyValuePair<string, string> pair2 in pair.Value)
                    {
                        this.__WriteValue(this.__fullname, pair.Key, pair2.Key, pair.Value[pair2.Key]);
                    }
                }
            }
            return this.__exists;
        }
    }
}