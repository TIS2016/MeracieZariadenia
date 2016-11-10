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
        public SettingsForm()
        {
            InitializeComponent();
            SetPortOptions();
        }

        public void SetPortOptions()
        {
            List<COMPortInfo> portsInfo = COMPortInfo.GetCOMPortsInfo();
            foreach (COMPortInfo cp in portsInfo)
            {
                this.cb_Ports.Items.Add(cp.Name);
            }
        }
    }
}
