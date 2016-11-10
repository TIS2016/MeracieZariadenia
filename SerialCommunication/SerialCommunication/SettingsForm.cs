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
    public partial class SettingsForm : Form
    {
        private string _portName;
        private int _baudRate;

        public string PortName { get { return _portName; } }
        public int BaudRate { get { return _baudRate; } }

        public SettingsForm(List<COMPortInfo> portInfo, string currentPort, int currentBaudRate)
        {
            InitializeComponent();
            SetPortOptions(portInfo);
            if (portInfo.Count == 0)
            {
                warning.Visible = true;
            }
            
            cb_baudRates.SelectedIndex = cb_baudRates.FindString(currentBaudRate + "");
        }

        public void SetPortOptions(List<COMPortInfo> portsInfo)
        {
            foreach (COMPortInfo cp in portsInfo)
            {
                this.cb_Ports.Items.Add(cp.Name);
            }
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            _portName = cb_Ports.SelectedItem+"";
            _baudRate = Convert.ToInt32(cb_baudRates.SelectedItem+"");
            DialogResult = DialogResult.OK;
        }
        
        private void b_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
