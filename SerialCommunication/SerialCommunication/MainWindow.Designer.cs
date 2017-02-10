namespace SerialCommunication
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.l_freq = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freq = new System.Windows.Forms.NumericUpDown();
            this.l_sec = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.sonda1Value = new System.Windows.Forms.Label();
            this.sonda2Value = new System.Windows.Forms.Label();
            this.currVals = new System.Windows.Forms.Label();
            this.l_sonda2 = new System.Windows.Forms.Label();
            this.l_sonda1 = new System.Windows.Forms.Label();
            this.b_startCom = new System.Windows.Forms.Button();
            this.b_endCom = new System.Windows.Forms.Button();
            this.led = new System.Windows.Forms.TextBox();
            this.l_connected = new System.Windows.Forms.Label();
            this._systemtrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tb_Logs = new System.Windows.Forms.TextBox();
            this.chb_showLogs = new System.Windows.Forms.CheckBox();
            this._commInterval = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.freq)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // l_freq
            // 
            this.l_freq.AutoSize = true;
            this.l_freq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.l_freq.Location = new System.Drawing.Point(12, 39);
            this.l_freq.Name = "l_freq";
            this.l_freq.Size = new System.Drawing.Size(208, 20);
            this.l_freq.TabIndex = 1;
            this.l_freq.Text = "Communication Frequency";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(627, 29);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(90, 25);
            this.optionsToolStripMenuItem.Text = "Advanced";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // freq
            // 
            this.freq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.freq.Location = new System.Drawing.Point(16, 70);
            this.freq.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.freq.Name = "freq";
            this.freq.ReadOnly = true;
            this.freq.Size = new System.Drawing.Size(91, 26);
            this.freq.TabIndex = 3;
            this.freq.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.freq.ValueChanged += new System.EventHandler(this.freq_ValueChanged);
            // 
            // l_sec
            // 
            this.l_sec.AutoSize = true;
            this.l_sec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.l_sec.Location = new System.Drawing.Point(113, 78);
            this.l_sec.Name = "l_sec";
            this.l_sec.Size = new System.Drawing.Size(84, 20);
            this.l_sec.TabIndex = 4;
            this.l_sec.Text = "second(s)";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.sonda1Value);
            this.panel.Controls.Add(this.sonda2Value);
            this.panel.Controls.Add(this.currVals);
            this.panel.Controls.Add(this.l_sonda2);
            this.panel.Controls.Add(this.l_sonda1);
            this.panel.Location = new System.Drawing.Point(267, 31);
            this.panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(348, 193);
            this.panel.TabIndex = 5;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // sonda1Value
            // 
            this.sonda1Value.BackColor = System.Drawing.SystemColors.HighlightText;
            this.sonda1Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sonda1Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sonda1Value.Location = new System.Drawing.Point(19, 65);
            this.sonda1Value.Name = "sonda1Value";
            this.sonda1Value.Size = new System.Drawing.Size(241, 32);
            this.sonda1Value.TabIndex = 6;
            // 
            // sonda2Value
            // 
            this.sonda2Value.BackColor = System.Drawing.SystemColors.HighlightText;
            this.sonda2Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sonda2Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sonda2Value.Location = new System.Drawing.Point(19, 134);
            this.sonda2Value.Name = "sonda2Value";
            this.sonda2Value.Size = new System.Drawing.Size(241, 32);
            this.sonda2Value.TabIndex = 5;
            // 
            // currVals
            // 
            this.currVals.AutoSize = true;
            this.currVals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.currVals.Location = new System.Drawing.Point(15, 9);
            this.currVals.Name = "currVals";
            this.currVals.Size = new System.Drawing.Size(121, 20);
            this.currVals.TabIndex = 4;
            this.currVals.Text = "Current Values";
            // 
            // l_sonda2
            // 
            this.l_sonda2.AutoSize = true;
            this.l_sonda2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.l_sonda2.Location = new System.Drawing.Point(19, 113);
            this.l_sonda2.Name = "l_sonda2";
            this.l_sonda2.Size = new System.Drawing.Size(67, 20);
            this.l_sonda2.TabIndex = 3;
            this.l_sonda2.Text = "Probe 2";
            // 
            // l_sonda1
            // 
            this.l_sonda1.AutoSize = true;
            this.l_sonda1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.l_sonda1.Location = new System.Drawing.Point(17, 41);
            this.l_sonda1.Name = "l_sonda1";
            this.l_sonda1.Size = new System.Drawing.Size(67, 20);
            this.l_sonda1.TabIndex = 1;
            this.l_sonda1.Text = "Probe 1";
            // 
            // b_startCom
            // 
            this.b_startCom.Location = new System.Drawing.Point(16, 114);
            this.b_startCom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.b_startCom.Name = "b_startCom";
            this.b_startCom.Size = new System.Drawing.Size(120, 36);
            this.b_startCom.TabIndex = 6;
            this.b_startCom.Text = "Start";
            this.b_startCom.UseVisualStyleBackColor = true;
            this.b_startCom.Click += new System.EventHandler(this.b_startCom_Click);
            // 
            // b_endCom
            // 
            this.b_endCom.Enabled = false;
            this.b_endCom.Location = new System.Drawing.Point(16, 158);
            this.b_endCom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.b_endCom.Name = "b_endCom";
            this.b_endCom.Size = new System.Drawing.Size(120, 34);
            this.b_endCom.TabIndex = 7;
            this.b_endCom.Text = "End";
            this.b_endCom.UseVisualStyleBackColor = true;
            this.b_endCom.Click += new System.EventHandler(this.b_endCom_Click);
            // 
            // led
            // 
            this.led.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.led.Cursor = System.Windows.Forms.Cursors.Default;
            this.led.Enabled = false;
            this.led.Location = new System.Drawing.Point(468, 235);
            this.led.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.led.Multiline = true;
            this.led.Name = "led";
            this.led.ReadOnly = true;
            this.led.Size = new System.Drawing.Size(20, 20);
            this.led.TabIndex = 8;
            // 
            // l_connected
            // 
            this.l_connected.AutoSize = true;
            this.l_connected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.l_connected.Location = new System.Drawing.Point(493, 235);
            this.l_connected.Name = "l_connected";
            this.l_connected.Size = new System.Drawing.Size(89, 20);
            this.l_connected.TabIndex = 9;
            this.l_connected.Text = "Connected";
            // 
            // _systemtrayIcon
            // 
            this._systemtrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this._systemtrayIcon.BalloonTipText = "FHT6020 communication running";
            this._systemtrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_systemtrayIcon.Icon")));
            this._systemtrayIcon.Text = "FHT6020 communication";
            this._systemtrayIcon.Visible = true;
            this._systemtrayIcon.BalloonTipClicked += new System.EventHandler(this._systemtrayIcon_Click);
            this._systemtrayIcon.BalloonTipClosed += new System.EventHandler(this._systemtrayIcon_BalloonTipClosed);
            this._systemtrayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this._systemtrayIcon_Click);
            this._systemtrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._systemtrayIcon_Click);
            // 
            // tb_Logs
            // 
            this.tb_Logs.Location = new System.Drawing.Point(12, 261);
            this.tb_Logs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_Logs.Multiline = true;
            this.tb_Logs.Name = "tb_Logs";
            this.tb_Logs.ReadOnly = true;
            this.tb_Logs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_Logs.Size = new System.Drawing.Size(603, 134);
            this.tb_Logs.TabIndex = 11;
            this.tb_Logs.Visible = false;
            // 
            // chb_showLogs
            // 
            this.chb_showLogs.AutoSize = true;
            this.chb_showLogs.Location = new System.Drawing.Point(16, 235);
            this.chb_showLogs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chb_showLogs.Name = "chb_showLogs";
            this.chb_showLogs.Size = new System.Drawing.Size(96, 20);
            this.chb_showLogs.TabIndex = 12;
            this.chb_showLogs.Text = "Show Logs";
            this.chb_showLogs.UseVisualStyleBackColor = true;
            this.chb_showLogs.CheckedChanged += new System.EventHandler(this.chb_showLogs_CheckedChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(627, 263);
            this.Controls.Add(this.chb_showLogs);
            this.Controls.Add(this.tb_Logs);
            this.Controls.Add(this.l_connected);
            this.Controls.Add(this.led);
            this.Controls.Add(this.b_endCom);
            this.Controls.Add(this.b_startCom);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.l_sec);
            this.Controls.Add(this.freq);
            this.Controls.Add(this.l_freq);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainWindow";
            this.Text = "FHT 6020 Communication";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.freq)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label l_freq;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown freq;
        private System.Windows.Forms.Label l_sec;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label currVals;
        private System.Windows.Forms.Label l_sonda2;
        private System.Windows.Forms.Label l_sonda1;
        private System.Windows.Forms.Button b_startCom;
        private System.Windows.Forms.Button b_endCom;
        private System.Windows.Forms.TextBox led;
        private System.Windows.Forms.Label l_connected;
        private System.Windows.Forms.Label sonda1Value;
        private System.Windows.Forms.Label sonda2Value;
        private System.Windows.Forms.NotifyIcon _systemtrayIcon;
        private System.Windows.Forms.TextBox tb_Logs;
        private System.Windows.Forms.CheckBox chb_showLogs;
        private System.Windows.Forms.Timer _commInterval;
    }
}

