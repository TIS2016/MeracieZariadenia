using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialCommunication
{
    public partial class MainWindow : Form
    {
        List<COMPortInfo> portInfo;

        private string PortName { get; set; }
        private int BaudRate { get; set; }

        List<Data> DATA;

        /// <summary>
        /// Communication interval ( ms )
        /// </summary>
        private int CommInterval = 1000;
        
        SerialPort serialPort;
        public MainWindow()
        {
            InitializeComponent();
            portInfo = COMPortInfo.GetCOMPortsInfo();
            serialPort = new SerialPort();
            DATA = new List<Data>();
            
            //Default Init
            BaudRate = CONSTANTS.BaudRate;
            PortName = defaultCOMAvailable() ? CONSTANTS.DefaultCOM : String.Empty;

            ToggleEnabledDisabled(PortStatus.CLOSED);
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
        
        /// <summary>
        /// Starts communication loop. Periodically sends commands for probe1, probe2 data and timestamp. Stores this data.
        /// Visualizes the data in window. Then, (TODO) once per minute, stores the data to database.
        /// </summary>
        private void StartCommunication()
        {
            try
            {
                while (true)
                {
                    string sonda1 = ProcessResponse(CONSTANTS.Command.Sonda1Data, SendCommandAndGetResponse(CONSTANTS.Command.Sonda1Data));
                    string sonda2 = ProcessResponse(CONSTANTS.Command.Sonda2Data, SendCommandAndGetResponse(CONSTANTS.Command.Sonda2Data));
                    string timestamp = ProcessResponse(CONSTANTS.Command.Timestamp, SendCommandAndGetResponse(CONSTANTS.Command.Timestamp));

                    VisualizeProbeValues(sonda1, sonda2);
                    double tryparse;
                    Data sonda1Entry = new Data(1, (double.TryParse(sonda1, out tryparse) ? Convert.ToDouble(sonda1) : 0 ), timestamp);
                    Data sonda2Entry = new Data(2, (double.TryParse(sonda2, out tryparse) ? Convert.ToDouble(sonda2) : 0), timestamp);
                    DATA.Add(sonda1Entry);
                    DATA.Add(sonda2Entry);

                    Thread.Sleep(CommInterval);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An error has occured and caused the communication to terminate. " + e.ToString());
            }
        }

        private List<byte> SendCommandAndGetResponse(CONSTANTS.Command command)
        {
            byte[] request = CONSTANTS.DataCommandType[command];
            serialPort.Write(request, 0, request.Length);
            return RequestData();
        }

        private List<byte> RequestData()
        {
            List<byte> res = new List<byte>();
            int i = 0;
            while (true)
            {
                byte b = (byte)serialPort.ReadByte();
                if (b == CONSTANTS.StartByte)
                { continue; }

                else if (b == CONSTANTS.EndByte)
                { break; }

                else if (i > 1000)
                {
                    Console.WriteLine("Read TimeOut. Response may be incomplete or corrupt.");
                    break;
                }
                res.Add(b);
                i++;
            }
            return res;
        }

        private string ProcessResponse(CONSTANTS.Command c, List<byte> response)
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < response.Count; ++i)
            {
                res.Append((char)response[i]);
            }
            return res.ToString();
        }

        private void VisualizeProbeValues(string sonda1, string sonda2)
        {
            this.sonda1Value.Text = sonda1;
            this.sonda2Value.Text = sonda2;
        }

        private bool defaultCOMAvailable()
            => portInfo.Where(p => p.Name == CONSTANTS.DefaultCOM).ToList().Count > 0;

        #region Design

        /// <summary>
        /// Depending on port status, change some controls' visual.
        /// </summary>
        /// <param name="s"></param>
        private void ToggleEnabledDisabled(PortStatus s)
        {
            if (s == PortStatus.OPEN)
            {
                l_connected.ForeColor = CONSTANTS.Color_Connected;
                led.BackColor = CONSTANTS.Color_Connected;
                l_connected.Text = CONSTANTS.Connected;
                b_startCom.Enabled = false;
                b_endCom.Enabled = true;

            }
            else if (s == PortStatus.CLOSED)
            {
                l_connected.ForeColor = CONSTANTS.Color_Disconnected;
                led.BackColor = CONSTANTS.Color_Disconnected;
                l_connected.Text = CONSTANTS.Disconnected;
                b_startCom.Enabled = true;
                b_endCom.Enabled = false;
            }
        }

        internal enum PortStatus
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
