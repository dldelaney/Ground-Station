namespace AvionicsTest3
{
    partial class AirspeedDisplay
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
            this.airSpeedIndicatorInstrumentControl1 = new AvionicsInstrumentControlDemo.AirSpeedIndicatorInstrumentControl();
            this.attitudeIndicatorInstrumentControl1 = new AvionicsInstrumentControlDemo.AttitudeIndicatorInstrumentControl();
            this.altimeterInstrumentControl1 = new AvionicsInstrumentControlDemo.AltimeterInstrumentControl();
            this.quitButton = new System.Windows.Forms.Button();
            this.speedApBox = new System.Windows.Forms.TextBox();
            this.pitchApBox = new System.Windows.Forms.TextBox();
            this.altApBox = new System.Windows.Forms.TextBox();
            this.bankApBox = new System.Windows.Forms.TextBox();
            this.speedApLabel = new System.Windows.Forms.Label();
            this.pitchApLabel = new System.Windows.Forms.Label();
            this.altApLabel = new System.Windows.Forms.Label();
            this.bankApLabel = new System.Windows.Forms.Label();
            this.headingIndicatorInstrumentControl1 = new AvionicsInstrumentControlDemo.HeadingIndicatorInstrumentControl();
            this.verticalSpeedIndicatorInstrumentControl1 = new AvionicsInstrumentControlDemo.VerticalSpeedIndicatorInstrumentControl();
            this.iBox = new System.Windows.Forms.NumericUpDown();
            this.pBox = new System.Windows.Forms.NumericUpDown();
            this.dBox = new System.Windows.Forms.NumericUpDown();
            this.sendPidButton = new System.Windows.Forms.Button();
            this.pidPickerDropdown = new System.Windows.Forms.ComboBox();
            this.headingApLabel = new System.Windows.Forms.Label();
            this.headingApBox = new System.Windows.Forms.TextBox();
            this.serialSelectDropdown = new System.Windows.Forms.ComboBox();
            this.serialConnectButton = new System.Windows.Forms.Button();
            this.serialDisconnectButton = new System.Windows.Forms.Button();
            this.joystickAquireButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBox)).BeginInit();
            this.SuspendLayout();
            // 
            // airSpeedIndicatorInstrumentControl1
            // 
            this.airSpeedIndicatorInstrumentControl1.Location = new System.Drawing.Point(525, 12);
            this.airSpeedIndicatorInstrumentControl1.Name = "airSpeedIndicatorInstrumentControl1";
            this.airSpeedIndicatorInstrumentControl1.Size = new System.Drawing.Size(161, 163);
            this.airSpeedIndicatorInstrumentControl1.TabIndex = 0;
            this.airSpeedIndicatorInstrumentControl1.Text = "airSpeedIndicatorInstrumentControl1";
            // 
            // attitudeIndicatorInstrumentControl1
            // 
            this.attitudeIndicatorInstrumentControl1.Location = new System.Drawing.Point(357, 12);
            this.attitudeIndicatorInstrumentControl1.Name = "attitudeIndicatorInstrumentControl1";
            this.attitudeIndicatorInstrumentControl1.Size = new System.Drawing.Size(162, 163);
            this.attitudeIndicatorInstrumentControl1.TabIndex = 1;
            this.attitudeIndicatorInstrumentControl1.Text = "attitudeIndicatorInstrumentControl1";
            // 
            // altimeterInstrumentControl1
            // 
            this.altimeterInstrumentControl1.Location = new System.Drawing.Point(191, 12);
            this.altimeterInstrumentControl1.Name = "altimeterInstrumentControl1";
            this.altimeterInstrumentControl1.Size = new System.Drawing.Size(160, 163);
            this.altimeterInstrumentControl1.TabIndex = 2;
            this.altimeterInstrumentControl1.Text = "altimeterInstrumentControl1";
            this.altimeterInstrumentControl1.Click += new System.EventHandler(this.altimeterInstrumentControl1_Click);
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(24, 761);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(75, 23);
            this.quitButton.TabIndex = 3;
            this.quitButton.Text = "QUIT";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // speedApBox
            // 
            this.speedApBox.Location = new System.Drawing.Point(537, 194);
            this.speedApBox.Name = "speedApBox";
            this.speedApBox.Size = new System.Drawing.Size(100, 20);
            this.speedApBox.TabIndex = 4;
            // 
            // pitchApBox
            // 
            this.pitchApBox.Location = new System.Drawing.Point(369, 194);
            this.pitchApBox.Name = "pitchApBox";
            this.pitchApBox.Size = new System.Drawing.Size(49, 20);
            this.pitchApBox.TabIndex = 5;
            // 
            // altApBox
            // 
            this.altApBox.Location = new System.Drawing.Point(207, 194);
            this.altApBox.Name = "altApBox";
            this.altApBox.Size = new System.Drawing.Size(78, 20);
            this.altApBox.TabIndex = 6;
            // 
            // bankApBox
            // 
            this.bankApBox.Location = new System.Drawing.Point(369, 233);
            this.bankApBox.Name = "bankApBox";
            this.bankApBox.Size = new System.Drawing.Size(64, 20);
            this.bankApBox.TabIndex = 7;
            // 
            // speedApLabel
            // 
            this.speedApLabel.AutoSize = true;
            this.speedApLabel.Location = new System.Drawing.Point(534, 178);
            this.speedApLabel.Name = "speedApLabel";
            this.speedApLabel.Size = new System.Drawing.Size(35, 13);
            this.speedApLabel.TabIndex = 8;
            this.speedApLabel.Text = "label1";
            // 
            // pitchApLabel
            // 
            this.pitchApLabel.AutoSize = true;
            this.pitchApLabel.Location = new System.Drawing.Point(366, 178);
            this.pitchApLabel.Name = "pitchApLabel";
            this.pitchApLabel.Size = new System.Drawing.Size(35, 13);
            this.pitchApLabel.TabIndex = 9;
            this.pitchApLabel.Text = "label2";
            // 
            // altApLabel
            // 
            this.altApLabel.AutoSize = true;
            this.altApLabel.Location = new System.Drawing.Point(204, 178);
            this.altApLabel.Name = "altApLabel";
            this.altApLabel.Size = new System.Drawing.Size(35, 13);
            this.altApLabel.TabIndex = 10;
            this.altApLabel.Text = "label3";
            // 
            // bankApLabel
            // 
            this.bankApLabel.AutoSize = true;
            this.bankApLabel.Location = new System.Drawing.Point(366, 217);
            this.bankApLabel.Name = "bankApLabel";
            this.bankApLabel.Size = new System.Drawing.Size(35, 13);
            this.bankApLabel.TabIndex = 11;
            this.bankApLabel.Text = "label1";
            // 
            // headingIndicatorInstrumentControl1
            // 
            this.headingIndicatorInstrumentControl1.Location = new System.Drawing.Point(692, 12);
            this.headingIndicatorInstrumentControl1.Name = "headingIndicatorInstrumentControl1";
            this.headingIndicatorInstrumentControl1.Size = new System.Drawing.Size(163, 163);
            this.headingIndicatorInstrumentControl1.TabIndex = 12;
            this.headingIndicatorInstrumentControl1.Text = "headingIndicatorInstrumentControl1";
            // 
            // verticalSpeedIndicatorInstrumentControl1
            // 
            this.verticalSpeedIndicatorInstrumentControl1.Location = new System.Drawing.Point(12, 12);
            this.verticalSpeedIndicatorInstrumentControl1.Name = "verticalSpeedIndicatorInstrumentControl1";
            this.verticalSpeedIndicatorInstrumentControl1.Size = new System.Drawing.Size(164, 163);
            this.verticalSpeedIndicatorInstrumentControl1.TabIndex = 13;
            this.verticalSpeedIndicatorInstrumentControl1.Text = "verticalSpeedIndicatorInstrumentControl1";
            // 
            // iBox
            // 
            this.iBox.Location = new System.Drawing.Point(12, 234);
            this.iBox.Name = "iBox";
            this.iBox.Size = new System.Drawing.Size(77, 20);
            this.iBox.TabIndex = 14;
            this.iBox.ValueChanged += new System.EventHandler(this.iBox_ValueChanged);
            // 
            // pBox
            // 
            this.pBox.InterceptArrowKeys = false;
            this.pBox.Location = new System.Drawing.Point(12, 208);
            this.pBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(77, 20);
            this.pBox.TabIndex = 15;
            this.pBox.ValueChanged += new System.EventHandler(this.pBox_ValueChanged);
            // 
            // dBox
            // 
            this.dBox.Location = new System.Drawing.Point(12, 261);
            this.dBox.Name = "dBox";
            this.dBox.Size = new System.Drawing.Size(77, 20);
            this.dBox.TabIndex = 16;
            this.dBox.ValueChanged += new System.EventHandler(this.dBox_ValueChanged);
            // 
            // sendPidButton
            // 
            this.sendPidButton.Location = new System.Drawing.Point(12, 288);
            this.sendPidButton.Name = "sendPidButton";
            this.sendPidButton.Size = new System.Drawing.Size(75, 23);
            this.sendPidButton.TabIndex = 18;
            this.sendPidButton.Text = "Send it";
            this.sendPidButton.UseVisualStyleBackColor = true;
            this.sendPidButton.Click += new System.EventHandler(this.sendPidButton_Click);
            // 
            // pidPickerDropdown
            // 
            this.pidPickerDropdown.FormattingEnabled = true;
            this.pidPickerDropdown.Location = new System.Drawing.Point(12, 183);
            this.pidPickerDropdown.Name = "pidPickerDropdown";
            this.pidPickerDropdown.Size = new System.Drawing.Size(121, 21);
            this.pidPickerDropdown.TabIndex = 19;
            this.pidPickerDropdown.SelectedIndexChanged += new System.EventHandler(this.pidPickerDropdown_SelectedIndexChanged);
            // 
            // headingApLabel
            // 
            this.headingApLabel.AutoSize = true;
            this.headingApLabel.Location = new System.Drawing.Point(702, 178);
            this.headingApLabel.Name = "headingApLabel";
            this.headingApLabel.Size = new System.Drawing.Size(35, 13);
            this.headingApLabel.TabIndex = 20;
            this.headingApLabel.Text = "label1";
            // 
            // headingApBox
            // 
            this.headingApBox.Location = new System.Drawing.Point(705, 194);
            this.headingApBox.Name = "headingApBox";
            this.headingApBox.Size = new System.Drawing.Size(100, 20);
            this.headingApBox.TabIndex = 21;
            // 
            // serialSelectDropdown
            // 
            this.serialSelectDropdown.FormattingEnabled = true;
            this.serialSelectDropdown.IntegralHeight = false;
            this.serialSelectDropdown.Location = new System.Drawing.Point(95, 207);
            this.serialSelectDropdown.Name = "serialSelectDropdown";
            this.serialSelectDropdown.Size = new System.Drawing.Size(88, 21);
            this.serialSelectDropdown.TabIndex = 23;
            // 
            // serialConnectButton
            // 
            this.serialConnectButton.Location = new System.Drawing.Point(95, 232);
            this.serialConnectButton.Name = "serialConnectButton";
            this.serialConnectButton.Size = new System.Drawing.Size(88, 23);
            this.serialConnectButton.TabIndex = 24;
            this.serialConnectButton.Text = "Connect!";
            this.serialConnectButton.UseVisualStyleBackColor = true;
            this.serialConnectButton.Click += new System.EventHandler(this.serialConnectButton_Click);
            // 
            // serialDisconnectButton
            // 
            this.serialDisconnectButton.Location = new System.Drawing.Point(95, 261);
            this.serialDisconnectButton.Name = "serialDisconnectButton";
            this.serialDisconnectButton.Size = new System.Drawing.Size(88, 23);
            this.serialDisconnectButton.TabIndex = 25;
            this.serialDisconnectButton.Text = "Disconnect";
            this.serialDisconnectButton.UseVisualStyleBackColor = true;
            this.serialDisconnectButton.Click += new System.EventHandler(this.serialDisconnectButton_Click);
            // 
            // joystickAquireButton
            // 
            this.joystickAquireButton.Location = new System.Drawing.Point(12, 530);
            this.joystickAquireButton.Name = "joystickAquireButton";
            this.joystickAquireButton.Size = new System.Drawing.Size(107, 23);
            this.joystickAquireButton.TabIndex = 26;
            this.joystickAquireButton.Text = "Aquire Joystick";
            this.joystickAquireButton.UseVisualStyleBackColor = true;
            this.joystickAquireButton.Click += new System.EventHandler(this.aquireJoystickButton_Click);
            // 
            // AirspeedDisplay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(888, 562);
            this.Controls.Add(this.joystickAquireButton);
            this.Controls.Add(this.serialDisconnectButton);
            this.Controls.Add(this.serialConnectButton);
            this.Controls.Add(this.serialSelectDropdown);
            this.Controls.Add(this.headingApBox);
            this.Controls.Add(this.headingApLabel);
            this.Controls.Add(this.pidPickerDropdown);
            this.Controls.Add(this.sendPidButton);
            this.Controls.Add(this.dBox);
            this.Controls.Add(this.pBox);
            this.Controls.Add(this.iBox);
            this.Controls.Add(this.verticalSpeedIndicatorInstrumentControl1);
            this.Controls.Add(this.headingIndicatorInstrumentControl1);
            this.Controls.Add(this.bankApLabel);
            this.Controls.Add(this.altApLabel);
            this.Controls.Add(this.pitchApLabel);
            this.Controls.Add(this.speedApLabel);
            this.Controls.Add(this.bankApBox);
            this.Controls.Add(this.altApBox);
            this.Controls.Add(this.pitchApBox);
            this.Controls.Add(this.speedApBox);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.altimeterInstrumentControl1);
            this.Controls.Add(this.attitudeIndicatorInstrumentControl1);
            this.Controls.Add(this.airSpeedIndicatorInstrumentControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AirspeedDisplay";
            this.Tag = "";
            this.Text = "Ground Station";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AvionicsInstrumentControlDemo.AirSpeedIndicatorInstrumentControl airSpeedIndicatorInstrumentControl1;
        private AvionicsInstrumentControlDemo.AttitudeIndicatorInstrumentControl attitudeIndicatorInstrumentControl1;
        private AvionicsInstrumentControlDemo.AltimeterInstrumentControl altimeterInstrumentControl1;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.TextBox speedApBox;
        private System.Windows.Forms.TextBox pitchApBox;
        private System.Windows.Forms.TextBox altApBox;
        private System.Windows.Forms.TextBox bankApBox;
        private System.Windows.Forms.Label speedApLabel;
        private System.Windows.Forms.Label pitchApLabel;
        private System.Windows.Forms.Label altApLabel;
        private System.Windows.Forms.Label bankApLabel;
        private AvionicsInstrumentControlDemo.HeadingIndicatorInstrumentControl headingIndicatorInstrumentControl1;
        private AvionicsInstrumentControlDemo.VerticalSpeedIndicatorInstrumentControl verticalSpeedIndicatorInstrumentControl1;
        private System.Windows.Forms.NumericUpDown iBox;
        private System.Windows.Forms.NumericUpDown pBox;
        private System.Windows.Forms.NumericUpDown dBox;
        private System.Windows.Forms.Button sendPidButton;
        private System.Windows.Forms.ComboBox pidPickerDropdown;
        private System.Windows.Forms.Label headingApLabel;
        private System.Windows.Forms.TextBox headingApBox;
        private System.Windows.Forms.ComboBox serialSelectDropdown;
        private System.Windows.Forms.Button serialConnectButton;
        private System.Windows.Forms.Button serialDisconnectButton;
        private System.Windows.Forms.Button joystickAquireButton;
    }
}

