namespace SerialCommunication
{
    partial class SettingsForm
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
            this.cb_Ports = new System.Windows.Forms.ComboBox();
            this.cb_baudRates = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.b_ok = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.warning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cb_Ports
            // 
            this.cb_Ports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Ports.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_Ports.Location = new System.Drawing.Point(32, 61);
            this.cb_Ports.Name = "cb_Ports";
            this.cb_Ports.Size = new System.Drawing.Size(121, 24);
            this.cb_Ports.TabIndex = 0;
            // 
            // cb_baudRates
            // 
            this.cb_baudRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_baudRates.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_baudRates.Items.AddRange(new object[] {
            "19200",
            "9600"});
            this.cb_baudRates.Location = new System.Drawing.Point(249, 61);
            this.cb_baudRates.Name = "cb_baudRates";
            this.cb_baudRates.Size = new System.Drawing.Size(121, 24);
            this.cb_baudRates.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(28, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Set Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(245, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Set Baud Rate:";
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point(67, 128);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(121, 34);
            this.b_ok.TabIndex = 4;
            this.b_ok.Text = "OK";
            this.b_ok.UseVisualStyleBackColor = true;
            this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
            // 
            // b_cancel
            // 
            this.b_cancel.Location = new System.Drawing.Point(208, 128);
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size(121, 34);
            this.b_cancel.TabIndex = 5;
            this.b_cancel.Text = "Cancel";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler(this.b_cancel_Click);
            // 
            // warning
            // 
            this.warning.AutoSize = true;
            this.warning.ForeColor = System.Drawing.Color.Red;
            this.warning.Location = new System.Drawing.Point(29, 88);
            this.warning.Name = "warning";
            this.warning.Size = new System.Drawing.Size(220, 16);
            this.warning.TabIndex = 6;
            this.warning.Text = "No usable port found on this device.";
            this.warning.Visible = false;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 183);
            this.Controls.Add(this.warning);
            this.Controls.Add(this.b_cancel);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_baudRates);
            this.Controls.Add(this.cb_Ports);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Advanced Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_Ports;
        private System.Windows.Forms.ComboBox cb_baudRates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button b_ok;
        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.Label warning;
    }
}