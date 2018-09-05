using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;

namespace IDM_Universial_Activator
{
    class Patch
    {
        RegistryKey downloadManager;
        
        public string ModifyRegEntry(License license)
        {
            try
            {
                // Open Reg key of IDM
                downloadManager = Registry.CurrentUser.OpenSubKey("Software\\DownloadManager", true);
            }
            catch(Exception e)
            {
                return "An error occurred! " + e.ToString();
            }

            try
            {
                // Set key/value pairs if they dont exist
                    downloadManager.SetValue("FName", license.FirstName);               
                
                    downloadManager.SetValue("LName", license.LastName);              
                
                    downloadManager.SetValue("Serial", license.Serial);
            }
            catch(Exception e)
            {
                return "An error occurred! " + e.ToString();
            }

            if (!VerifyChanges(license))
            {
                return "Keys already exist in registry. No changes made.";
            }
                
            
            downloadManager.Close();

            return "Patching Successful!";
        }

        public Boolean VerifyChanges(License license)
        {
            if (downloadManager.GetValue("FName").ToString() != license.FirstName)
                return false;
            if (downloadManager.GetValue("LName").ToString() != license.LastName)           
                return false;           
            if (downloadManager.GetValue("Serial").ToString() != license.Serial)          
                return false;           
            return true;
        }
    }
}
