using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SerialCommunication
{
    public partial class MainWindow : Form
    {
        List<COMPortInfo> portInfo;
        
        private string PortName { get; set; }
        private int BaudRate { get; set; }
        private bool _endCommRequest = false;
        List<Data> DATA;

        Database _database;

        private static BackgroundWorker commWorker;

        List<string> _logs = new List<string>();

        SerialPort serialPort;
        public MainWindow()
        {
            InitializeComponent();
            
            InitializeBGWorker();

            portInfo = COMPortInfo.GetCOMPortsInfo();
            serialPort = new SerialPort();
            DATA = new List<Data>();
            
            _database = new Database(this);

            //Default Init
            BaudRate = CONSTANTS.BaudRate;
            PortName = defaultCOMAvailable() ? CONSTANTS.DefaultCOM : String.Empty;
            ToggleEnabledDisabled(PortStatus.CLOSED);

            this.WindowState = FormWindowState.Minimized;
            TryConnecting();
        }
        #region Thread init

        private void InitializeBGWorker()
        {
            commWorker = new BackgroundWorker();
            commWorker.WorkerSupportsCancellation = true;
            commWorker.DoWork += RequestProbeData;
        }
        
        #endregion

        private void MainWindow_Load(object sender, EventArgs e)
        {
        }
        
        
        private void b_startCom_Click(object sender, EventArgs e)
        {
            TryConnecting();
        }

        private void TryConnecting()
        {
            _endCommRequest = false;
            if (PortName == "")
            {
                Log("Prosim zvolte nastavenia portu.");
                NotifyErrorOnSystemTray();
                return;
            }
            ToggleEnabledDisabled(PortStatus.OPEN);
            serialPort.PortName = PortName;
            serialPort.BaudRate = BaudRate;
            serialPort.Parity = Parity.Even;
            serialPort.DataBits = 7;
            serialPort.StopBits = StopBits.One;
            try
            {
                //opens the port
                serialPort.Open();
                Log("Port open. Communication started successfully.");
                //request probe data
                commWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Log("CHYBA; Port sa nepodarilo otvorit. " + ex.ToString(), Color.Red);
                NotifyErrorOnSystemTray();
                ToggleEnabledDisabled(PortStatus.CLOSED);
                return;
            }
        }
        
        private void b_endCom_Click(object sender, EventArgs e)
        {
            ToggleEnabledDisabled(PortStatus.CLOSED);
            _endCommRequest = true;
            CONSTANTS.ElapsedSeconds = 0;
            if (commWorker.IsBusy)
            {
                commWorker.CancelAsync();
            }
            serialPort.Close();
        }

        /// <summary>
        /// Starts communication loop. Periodically gets called from .Sends commands for probe1, probe2 data and timestamp. Stores this data.
        /// Visualizes the data in window. Then, once per minute, stores the data to database.
        /// </summary>
        private void RequestProbeData(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    //if request to cancel communication was invoked,
                    //terminate and return
                    if (commWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (CONSTANTS.ElapsedSeconds >= CONSTANTS.DbInsertPeriod)
                    {
                        try
                        {
                            InsertDataToDB();
                        }
                        catch (Exception ex)
                        {
                            Log("ERROR: DB insert failed.");
                        }
                        //return;
                    }

                    string s1 = ProcessResponse(SendCommandAndGetResponse(CONSTANTS.Command.Sonda1Data));
                    string s2 = ProcessResponse(SendCommandAndGetResponse(CONSTANTS.Command.Sonda2Data));
                    string timestamp = ProcessResponse(SendCommandAndGetResponse(CONSTANTS.Command.Timestamp));
                    VisualizeProbeValues(s1, s2);

                    double sonda1 = ExtractDoubleValue(s1);
                    double sonda2 = ExtractDoubleValue(s2);

                    Data sondaEntry = new Data(sonda1, sonda2, timestamp);
                    DATA.Add(sondaEntry);

                    CONSTANTS.ElapsedSeconds += CONSTANTS.CommInterval / 1000;

                    Thread.Sleep(CONSTANTS.CommInterval);
                }
            }
            catch (Exception ex)
            {
                if (!_endCommRequest)
                {
                    Log("An error has occured and caused the communication to terminate. " + ex.ToString(), Color.Red);
                    NotifyErrorOnSystemTray();
                }
            }
        }

        #region OLD
        /*
                /// <summary>
                /// Starts communication loop. Periodically gets called from .Sends commands for probe1, probe2 data and timestamp. Stores this data.
                /// Visualizes the data in window. Then, once per minute, stores the data to database.
                /// </summary>
                private void RequestProbeData(object sender, DoWorkEventArgs e)
                {
                    try
                    {
                        //if request to cancel communication was invoked,
                        //terminate and return
                        if (commWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        if (CONSTANTS.ElapsedSeconds >= CONSTANTS.DbInsertPeriod)
                        {
                            //pause timer
                            _commInterval.Enabled = false;
                            try
                            {
                                InsertDataToDB();
                            }
                            catch (Exception ex)
                            {
                                Log("ERROR: DB insert failed.");
                            }
                            //re-enable timer & return
                            _commInterval.Enabled = true;
                            return;
                        }

                        string s1 = ProcessResponse(SendCommandAndGetResponse(CONSTANTS.Command.Sonda1Data));
                        string s2 = ProcessResponse(SendCommandAndGetResponse(CONSTANTS.Command.Sonda2Data));
                        string timestamp = ProcessResponse(SendCommandAndGetResponse(CONSTANTS.Command.Timestamp));
                        VisualizeProbeValues(s1, s2);

                        double sonda1 = ExtractDoubleValue(s1);
                        double sonda2 = ExtractDoubleValue(s2);

                        Data sondaEntry = new Data(sonda1, sonda2, timestamp);
                        DATA.Add(sondaEntry);

                        CONSTANTS.ElapsedSeconds += CONSTANTS.CommInterval/1000;
                    }
                    catch (Exception ex)
                    {
                        if (!_endCommRequest)
                        {
                            Log("An error has occured and caused the communication to terminate. " + ex.ToString(), Color.Red);
                            NotifyErrorOnSystemTray();
                        }
                    }
                }*/
        #endregion
        private double ExtractDoubleValue(string s)
        {
            try
            {
                s = s.Remove(0, 5); //removes the registry name "01RM " from the string;
                string[] rest = s.Split(' ');//if the string was correct, rest[0] should have the value.
                double value = double.Parse(rest[0], CultureInfo.InvariantCulture);
                Log("Parse success: " + value);
                return value;
            }
            catch (Exception e)
            {
                Log("Error parsing registry value; " + e.ToString());
                return 0;
            }
        }

        private void InsertDataToDB()
        {
            CONSTANTS.ElapsedSeconds = 0;
            if (_database.InsertOnTable(DATA))
            {
                Log(DATA.Count + " Data successfully uploaded to database.");
            }
            DATA.Clear();
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

        private string ProcessResponse(List<byte> response)
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
                freq.Enabled = false;
                optionsToolStripMenuItem.Enabled = false;

            }
            else if (s == PortStatus.CLOSED)
            {
                l_connected.ForeColor = CONSTANTS.Color_Disconnected;
                led.BackColor = CONSTANTS.Color_Disconnected;
                l_connected.Text = CONSTANTS.Disconnected;
                b_startCom.Enabled = true;
                b_endCom.Enabled = false;
                freq.Enabled = true;
                optionsToolStripMenuItem.Enabled = true;
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
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                _database.CloseConn();
            }
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

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                _systemtrayIcon.Visible = true;
                _systemtrayIcon.ShowBalloonTip(300);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                _systemtrayIcon.Visible = false;
            }
        }
        
        private void freq_ValueChanged(object sender, EventArgs e)
        {
            int sec = (int)freq.Value;
            if (sec < 1) { sec = 1; }
            if (sec > 10) { sec = 10; }

            CONSTANTS.CommInterval = sec * 1000;
            freq.Value = sec;
        }

        private void chb_showLogs_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_showLogs.Checked)
            {
                tb_Logs.Visible = true;
                this.Height = this.Height + tb_Logs.Height;
            }
            else
            {
                tb_Logs.Visible = false;
                this.Height = this.Height - tb_Logs.Height;
            }
        }

        public void Log(string message, Color? c = null)
        {
            Color txtColor = c ?? Color.Black;
            _logs.Add("**** " + DateTime.UtcNow + " : " + message);

            if (_logs.Count > CONSTANTS.LogCount)
            {
                _logs.RemoveAt(0);
            }
            tb_Logs.Text = String.Join(Environment.NewLine, _logs);
            scrollLogsDown();
        }

        public void NotifyErrorOnSystemTray()
        {
            _systemtrayIcon.Visible = true;
            _systemtrayIcon.BalloonTipText = "An error has occured: for more information, click here and see Logs. ";
            _systemtrayIcon.ShowBalloonTip(500);
            //then sets the text bck to default in _systemtrayIcon_BalloonTipClosed
        }

        /// <summary>
        /// h4x0r
        /// </summary>
        private void scrollLogsDown()
        {
            tb_Logs.AppendText(" ");
        }

        private void _systemtrayIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            _systemtrayIcon.BalloonTipText = CONSTANTS.TrayIconDefaultText;
        }

        private void _systemtrayIcon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void _systemtrayIcon_Click(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
