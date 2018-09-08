using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace IDM_Universial_Activator
{
    public partial class Form1 : Form
    {
        License license;
        Patch patch = new Patch();
        CodeIsle.LibIpsNet.Patcher Patcher = new CodeIsle.LibIpsNet.Patcher();
        string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        string serial_key = null;

        // Holds the path to the patch file.
        private string patchFile = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PatchEXE();
            textBoxLog.AppendText("Validating input\n");
            if (!Validate())
            {
                return;
            }

            textBoxLog.AppendText("Creating license object\n");
            license = new License
            {
                FirstName = textBoxFName.Text,
                LastName = textBoxLName.Text,
                Serial = serial_key
            };

            textBoxLog.AppendText("Adding license to registry\n");
            textBoxLog.AppendText("Patching complete\n");
            MessageBox.Show(patch.ModifyRegEntry(license)); 
        }

        public Boolean Validate()
        {
            // Check first and last names
            if(textBoxFName.Text == null || textBoxFName.Text == "")
            {
                MessageBox.Show("First name Cannot be empty");
                return false;
            }
            if(textBoxLName.Text == null || textBoxLName.Text == "")
            {
                MessageBox.Show("Last name Cannot be empty");
                return false;
            }
            if(serial_key == null)
            {
                MessageBox.Show("Generate a serial key");
                return false;
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxLog.AppendText("Generating serial key\n");
            Random rand = new Random();
            string key = "";

            for(int i = 0; i <= 19; i++)
            {
               
                key += characters[rand.Next(0, characters.Length)];
            }

            // Insert hyphens
            key = key.Insert(5, "-");
            key = key.Insert(11, "-");
            key = key.Insert(17, "-");

            serial_key = key;
            textBoxSerial.Text = key;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void labelLog_Click(object sender, EventArgs e)
        {

        }

        #region IPS Patching Code
        private static void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
                w.Write(r.ReadBytes((int)s.Length));
        }

        private void PatchEXE()
        {
            textBoxLog.AppendText("Killing IDMan.exe" + "\n");
            try
            {
                foreach (var process in Process.GetProcessesByName("IDMan"))
                    process.Kill();
                foreach (var process in Process.GetProcessesByName("IEMonitor"))
                    process.Kill();
            }
            catch { }

            textBoxLog.AppendText("Extracting patch from embedded resources..." + "\n");
            if (!File.Exists(Application.StartupPath + @"\IDMan.ips"))
                Extract("IDM_Universial_Activator", Application.StartupPath, "Resources", "IDMan.ips");
            else
                textBoxLog.AppendText("File already exists, using existing file...\n");

            textBoxLog.AppendText(@"Using default IDMan.exe install location C:\Program Files (x86)\Internet Download Manager" + "\n");
            string BackupFile = GetUniqueFilePath(@"C:\Program Files (x86)\Internet Download Manager\IDMan.bak");
            try
            {

                File.Copy(@"C:\Program Files (x86)\Internet Download Manager\IDMan.exe", BackupFile);
                Patcher.Patch(Application.StartupPath + @"\IDMan.ips", @"C:\Program Files (x86)\Internet Download Manager\IDMan.exe", @"C:\Program Files (x86)\Internet Download Manager\IDMan.exe");
                textBoxLog.AppendText("File has been patched\n");
            }
            catch(Exception ex)
            {
                textBoxLog.AppendText("Patching failed. Check exception for more details...\n");
                MessageBox.Show(ex.ToString());
            }
            File.Delete(Application.StartupPath + @"\IDMan.ips");
        }

        public static string GetUniqueFilePath(string filepath)
        {
            if (File.Exists(filepath))
            {
                string folder = Path.GetDirectoryName(filepath);
                string filename = Path.GetFileNameWithoutExtension(filepath);
                string extension = Path.GetExtension(filepath);
                int number = 1;

                Match regex = Regex.Match(filepath, @"(.+) \((\d+)\)\.\w+");

                if (regex.Success)
                {
                    filename = regex.Groups[1].Value;
                    number = int.Parse(regex.Groups[2].Value);
                }

                do
                {
                    number++;
                    filepath = Path.Combine(folder, string.Format("{0} ({1}){2}", filename, number, extension));
                }
                while (File.Exists(filepath));
            }

            return filepath;
        }
        #endregion
    }
}
