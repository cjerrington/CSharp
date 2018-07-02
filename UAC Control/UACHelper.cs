using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace UAC_Control
{
    class UACHelper
    {
        private const string uacRegistryKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        private const string uacRegistryValue = "EnableLUA";
        private const string keyName = uacRegistryKey + "\\" + uacRegistryValue;

        public static bool IsUacEnabled
        {
            get
            {
                using (RegistryKey uacKey = Registry.LocalMachine.OpenSubKey(uacRegistryKey, false))
                {
                    bool result = uacKey.GetValue(uacRegistryValue).Equals(1);
                    return result;
                }
            }
        }

        public static void DisableUac()
        {
            RegistryKey uac = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);
            uac = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");

            uac.SetValue("EnableLUA", 0);
            uac.Close();
            MessageBox.Show("Disable done, please restart");
        }

        public static void EnableUac()
        {
            RegistryKey uac = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);

            uac = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");

            uac.SetValue("EnableLUA", 1);
            uac.Close();
            MessageBox.Show("Enable done, please restart");
        }
    }
}

