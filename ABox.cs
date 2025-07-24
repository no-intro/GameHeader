using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace GameHeader
{
    public partial class ABox : Form
    {
        public ABox()
        {
            InitializeComponent();
            // get Assembly fields
            string title = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyTitleAttribute), false)).Title;
            string company = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company;
            string description = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyDescriptionAttribute), false)).Description;
            //string version = ((AssemblyVersionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyVersionAttribute), false)).Version;
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
            // labels
            this.Text = string.Format("About {0}", title);
            this.labelProductName.Text = title;
            this.labelDescription.Text = description;
            this.labelVersion.Text = string.Format("Version {0}", version);
            this.labelCompanyName.Text = company;
        }
    }
}