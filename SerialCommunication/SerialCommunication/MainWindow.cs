using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
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

        SerialPort serialPort;
        public MainWindow()
        {
            InitializeComponent();
            portInfo = COMPortInfo.GetCOMPortsInfo();
            serialPort = new SerialPort();
            
            //Default Init
            BaudRate = 19200;
            PortName = com4Available() ? "COM4" : "";
        }
        
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm(portInfo, PortName, BaudRate);
            DialogResult result = settings.ShowDialog();
            if (result == DialogResult.OK)
            {
                PortName = settings.PortName;
                BaudRate = settings.BaudRate;
            }
        }

        private void b_startCom_Click(object sender, EventArgs e)
        {
            if (PortName == "")
            {
                MessageBox.Show("Prosim zvolte nastavenia portu.");
                return;
            }
            ToggleEnabledDisabled(PortStatus.OPEN);
            serialPort.PortName = PortName;
            serialPort.BaudRate = BaudRate;
            serialPort.Parity = Parity.Even;
            serialPort.StopBits = StopBits.One;
            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CHYBA; Port sa nepodarilo otvorit. " + ex.ToString(), "Chyba");
                ToggleEnabledDisabled(PortStatus.CLOSED);
                return;
            }
            StartCommunication();
        }
        
        private void b_endCom_Click(object sender, EventArgs e)
        {
            ToggleEnabledDisabled(PortStatus.CLOSED);
        }

        //TODO
        private void StartCommunication()
        {
            try
            { }
            catch (Exception e)
            { }
        }

        private bool com4Available()
            => portInfo.Where(p => p.Name == "COM4").ToList().Count > 0;

        #region Design
        private void ToggleEnabledDisabled(PortStatus s)
        {
            if (s == PortStatus.OPEN)
            {
                l_connected.ForeColor = Color.Green;
                led.BackColor = Color.Green;
                l_connected.Text = "Connected";
                b_startCom.Enabled = false;
                b_endCom.Enabled = true;

            }
            else if (s == PortStatus.CLOSED)
            {
                l_connected.ForeColor = Color.Red;
                led.BackColor = Color.Red;
                l_connected.Text = "Disconnected";
                b_startCom.Enabled = true;
                b_endCom.Enabled = false;
            }
        }

        enum PortStatus
        {
            OPEN,
            CLOSED
        }
        #endregion

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort.IsOpen) { serialPort.Close(); }
        }

    }
}
