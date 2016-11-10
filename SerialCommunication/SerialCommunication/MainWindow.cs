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
        SettingsForm settings;
        public MainWindow()
        {
            InitializeComponent();
            settings = new SettingsForm();

            l_connected.ForeColor = Color.Red;
            led.BackColor = Color.Red;
        }
        
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.Show();
        }
    }
}
