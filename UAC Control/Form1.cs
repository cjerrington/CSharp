using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace UAC_Control
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            lblUAC.Text = UACHelper.IsUacEnabled ? "Enabled" : "Disabled";
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            if (IsAdministrator())
            {
                UACHelper.EnableUac();
            }
            else
            {
                MessageBox.Show("User must be an administrator to run this option", "UAC Control", 0 , MessageBoxIcon.Error);
            }
            
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            if (IsAdministrator())
            {
                UACHelper.DisableUac();
            }
            else
            {
                MessageBox.Show("User must be an administrator to run this option", "UAC Control", 0, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsAdministrator())
            {
                StartShutDown("-f -r -t 0");
            }
            else
            {
                MessageBox.Show("User must be an administrator to run this option", "UAC Control", 0, MessageBoxIcon.Error);
            }
        }

        private static void StartShutDown(string param)
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = "cmd";
            proc.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Arguments = "/C shutdown " + param;
            Process.Start(proc);
        }

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
