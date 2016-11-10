using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialCommunication
{
    public partial class MainWindow : Form
    {
        List<COMPortInfo> portInfo;

        string PortName { get; set; }
        int BaudRate { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            portInfo = COMPortInfo.GetCOMPortsInfo();

            l_connected.ForeColor = Color.Red;
            led.BackColor = Color.Red;
        }
        
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm(portInfo, PortName, BaudRate);
            DialogResult result = settings.ShowDialog();
            if (result == DialogResult.OK)
            {
                PortName = settings.PortName;
                BaudRate = settings.BaudRate;
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("Cancel");
            }
        }

        private void b_startCom_Click(object sender, EventArgs e)
        {

        }
    }
}
