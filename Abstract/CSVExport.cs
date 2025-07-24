namespace GameHeader.Abstract
{
    using GameHeader;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class CSVExport
    {
        // private static ASCIIEncoding __ascii = new ASCIIEncoding();

        private static string MakeValueCsvFriendly(string s)
        {
            if (s == "")
                return "\"\"";
            string output = "";
            if (s.Contains(",") || s.Contains("\""))
                output = '"' + s.Replace("\"", "\"\"") + '"';
            else
                output = '"' + s + '"';
            return output;
        }

        private static string AppendValue(string s)
        {
            string output = "";
            string value = s.Substring(Int32.Parse(Program.GetConfig().GetValue("OUTPUT", "Padding")));
            value = value.Replace("\r", "");
            value = value.Replace("\n", "");
            output = output + MakeValueCsvFriendly(value);
            return output;
        }

        public static string Export(string s)
        {
            string output = "";
            // header
            output = output + "\"Path\",\"Archive\",\"File\",\"Size\",\"CRC32\",\"MD5\",\"SHA1\",\"SHA256\"";
            output = output + "\r\n";
            // content
            var lines = s.Split('\n');
            foreach (var line in lines)
            {
                if ( line.Contains(Util.Pad("Path:")) )
                {
                    output = output + AppendValue(line) + ",";
                }
                else if (line.Contains(Util.Pad("Archive:")))
                {
                    output = output + AppendValue(line) + ",";
                }
                else if (line.Contains(Util.Pad("File:")))
                {
                    output = output + AppendValue(line) + ",";
                }
                else if (line.Contains(Util.Pad("Size (Bytes):")))
                {
                    output = output + AppendValue(line) + ",";
                }
                else if (line.Contains(Util.Pad("CRC32:")))
                {
                    output = output + AppendValue(line) + ",";
                }
                else if (line.Contains(Util.Pad("MD5:")))
                {
                    output = output + AppendValue(line) + ",";
                }
                else if (line.Contains(Util.Pad("SHA1:")))
                {
                    output = output + AppendValue(line) + ",";
                }
                else if (line.Contains(Util.Pad("SHA256:")))
                {
                    output = output + AppendValue(line);
                }
                else if (line.Contains("-------------------------------------------------------------------"))
                {
                    output = output + "\r\n";
                }
            }
            return output;
        }

    }
}