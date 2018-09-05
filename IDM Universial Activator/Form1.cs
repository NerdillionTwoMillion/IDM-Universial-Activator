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

namespace IDM_Universial_Activator
{
    public partial class Form1 : Form
    {
        License license;
        Patch patch = new Patch();
        string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        string serial_key = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show("Firstn name Cannot be empty");
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
    }
}
