using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using SharpDX.DirectInput;


namespace AvionicsTest3
{
    public partial class AirspeedDisplay : Form // using avionics insturments from https://www.codeproject.com/Articles/27411/C-Avionic-Instrument-Controls
    {
        DirectInput directInput = new DirectInput();
        Guid joystickGuid = Guid.Empty;
        Joystick joystick;
        const int joystickMaxValue = 65535;
        SerialPort serialPort = new SerialPort();
        float[,] PIDs = new float[6,3];
        bool[] joystickButtons = new bool[12];
        float[] joystickAxis = new float[3];// a percentage between 0 and 1 related to how much the joystick has moved in that axis
        string serialReadSave = "";
        const int generalBufferSize = 10;
        const int insturmentSideBufferSize = 5;
        const int insturmentTopBufferSize = 5;
        const int insturmentSideOffset = 5;
        const int apLabelFont = 50;
        const int apBoxFont = 40;
        const int pidBoxFont = 20;
        const int buttonFont = 30;
        const int aquireJoystickButtonFont = 12;
        long serialOpenedMillis = -1;
        bool joystickExists = false;
        string planeState = "manualControl";

        public AirspeedDisplay()
        {
            InitializeComponent();
            serialPort.PortName = "COM1";
            serialPort.BaudRate = 115200;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            joystickInit();

            drawWindowInit();
            initSerialPorts();
            loadLastPIDs();

            Timer timer = new Timer();
            timer.Interval = (10); // 10ms
            timer.Tick += new EventHandler(loopDeLoop);
            timer.Start();
        }
        private void loopDeLoop(object sender, EventArgs e) {
            
            updateJoystickValues();
            compileSerialDataToSend();
            readDataFromSerial();

        }
        public void delay(int millis)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.ElapsedMilliseconds < millis) { }
        }
        private void quitButton_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen) {
                serialPort.Close();
            }
            Application.Exit();
        }
        private void joystickInit() {
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid = deviceInstance.InstanceGuid;
            }

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty) {
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                {
                    joystickGuid = deviceInstance.InstanceGuid;
                }
            }
            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                MessageBox.Show("No joystick/gamepad found", "Joystick Error", MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    joystick = new Joystick(directInput, joystickGuid);
                    Console.WriteLine("Found Joystick with GUID: {0}", joystickGuid);
                    Console.WriteLine(joystick.Capabilities.ButtonCount.ToString() + " buttons available.");
                    joystick.Acquire();
                    joystickExists = true;
                }
                catch (Exception e1) {
                    MessageBox.Show(e1.Message, "Joystick Error", MessageBoxButtons.OK);
                }
            }
        }
        private void pidPickerDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = pidPickerDropdown.SelectedIndex;
            if(selectedIndex != -1)
            {
                pBox.Value = (decimal)PIDs[selectedIndex,0];
                iBox.Value = (decimal)PIDs[selectedIndex,1];
                dBox.Value = (decimal)PIDs[selectedIndex,2];
            }
        }
        private void pBox_ValueChanged(object sender, EventArgs e)
        {
            if (pidPickerDropdown.SelectedIndex != -1) {
                PIDs[pidPickerDropdown.SelectedIndex,0] = (float)pBox.Value;
            }
        }
        private void iBox_ValueChanged(object sender, EventArgs e)
        {
            if (pidPickerDropdown.SelectedIndex != -1)
            {
                PIDs[pidPickerDropdown.SelectedIndex,1] = (float)iBox.Value;
            }
        }
        private void dBox_ValueChanged(object sender, EventArgs e)
        {
            if (pidPickerDropdown.SelectedIndex != -1)
            {
                PIDs[pidPickerDropdown.SelectedIndex,2] = (float)dBox.Value;
            }
        }
        private void sendPidButton_Click(object sender, EventArgs e)
        {
            //save all settings before sending
            CurrentData currentData = new CurrentData();
            //have to do each one individually because two-dimentional arrays are wierd.
            for (int i = 0; i < 3; i++)
            {
                currentData.altPID[i] = PIDs[0,i];
                currentData.pitchPID[i] = PIDs[1, i];
                currentData.bankPID[i] = PIDs[2, i];
                currentData.speedPID[i] = PIDs[3, i];
                currentData.headingPID[i] = PIDs[4, i];
                currentData.runwayLineupPID[i] = PIDs[5, i];
            }

            var writer = new XmlSerializer(typeof(CurrentData));
            var wfile = new StreamWriter("currentData.xml");
            writer.Serialize(wfile, currentData);
            wfile.Close();

            //encode and send the values to the serial port
            if (serialPort.IsOpen) {
                //send only selected PIDs
                if (pidPickerDropdown.SelectedIndex != -1) {
                    string str = "CP";
                    switch (pidPickerDropdown.SelectedIndex) { 
                        case 0: str += "A"; break;
                        case 1: str += "P"; break;
                        case 2: str += "B"; break;
                        case 3: str += "S"; break;
                        case 4: str += "H"; break;
                        case 5: str += "R"; break;
                        default:
                        break;
                    }
                    string strSave = str; //the first part of the message will be the same throughout all 3 messages
                    str += "p";
                    str += (float)pBox.Value;
                    str += strSave;
                    str += "i";
                    str += (float)iBox.Value;
                    str += strSave;
                    str += "d";
                    str += (float)dBox.Value;
                    Console.WriteLine(str);
                    serialPort.Write(str);
                }
            }

        }
        private void loadLastPIDs() {

            //check for settings on startup and load them
            if (File.Exists("currentData.xml"))
            {
                XmlSerializer reader = new XmlSerializer(typeof(CurrentData));
                StreamReader file = new StreamReader("currentData.xml");
                CurrentData readData = (CurrentData)reader.Deserialize(file);
                file.Close();
                // Apperantly you can't copy an entire array when using two-dimentional arrays
                for (int i = 0; i < 3; i++)
                {
                    PIDs[0, i] = readData.altPID[i];
                    PIDs[1, i] = readData.pitchPID[i];
                    PIDs[2, i] = readData.bankPID[i];
                    PIDs[3, i] = readData.speedPID[i];
                    PIDs[4, i] = readData.headingPID[i];
                    PIDs[5, i] = readData.runwayLineupPID[i];
                }
            }
        }
        private void drawWindowInit() {

            //-------FORM DISPLAY----------
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            //place the insturments so they look nice (each taking up 1/5 of the screen (with buffers))
            if (true)
            {
                verticalSpeedIndicatorInstrumentControl1.Height = this.Height / 3;
                verticalSpeedIndicatorInstrumentControl1.Width = verticalSpeedIndicatorInstrumentControl1.Height;
                verticalSpeedIndicatorInstrumentControl1.Location = new Point(insturmentSideOffset, insturmentTopBufferSize);

                altimeterInstrumentControl1.Height = this.Height / 3;
                altimeterInstrumentControl1.Width = altimeterInstrumentControl1.Height;
                altimeterInstrumentControl1.Location = new Point(verticalSpeedIndicatorInstrumentControl1.Location.X + verticalSpeedIndicatorInstrumentControl1.Width + insturmentSideBufferSize, insturmentTopBufferSize);

                attitudeIndicatorInstrumentControl1.Height = this.Height / 3;
                attitudeIndicatorInstrumentControl1.Width = attitudeIndicatorInstrumentControl1.Height;
                attitudeIndicatorInstrumentControl1.Location = new Point(altimeterInstrumentControl1.Location.X + altimeterInstrumentControl1.Width + insturmentSideBufferSize, insturmentTopBufferSize);

                airSpeedIndicatorInstrumentControl1.Height = this.Height / 3;
                airSpeedIndicatorInstrumentControl1.Width = airSpeedIndicatorInstrumentControl1.Height;
                airSpeedIndicatorInstrumentControl1.Location = new Point(attitudeIndicatorInstrumentControl1.Location.X + attitudeIndicatorInstrumentControl1.Width + insturmentSideBufferSize, insturmentTopBufferSize);

                headingIndicatorInstrumentControl1.Height = this.Height / 3;
                headingIndicatorInstrumentControl1.Width = headingIndicatorInstrumentControl1.Height;
                headingIndicatorInstrumentControl1.Location = new Point(airSpeedIndicatorInstrumentControl1.Location.X + airSpeedIndicatorInstrumentControl1.Width + insturmentSideBufferSize, insturmentTopBufferSize);
            }
            //place the labels below the insturments
            if (true)
            {
                speedApLabel.Location = new Point(airSpeedIndicatorInstrumentControl1.Location.X, airSpeedIndicatorInstrumentControl1.Location.Y + airSpeedIndicatorInstrumentControl1.Height + generalBufferSize);
                speedApLabel.Font = new Font("Microsoft Sans Serif", apLabelFont);
                speedApLabel.Text = "Speed";

                pitchApLabel.Location = new Point(attitudeIndicatorInstrumentControl1.Location.X, attitudeIndicatorInstrumentControl1.Location.Y + attitudeIndicatorInstrumentControl1.Height + generalBufferSize);
                pitchApLabel.Font = new Font("Microsoft Sans Serif", apLabelFont);
                pitchApLabel.Text = "Pitch";

                // bankApLabel location is in autopilot textboxes because it was easier to set it up there, sorry :/

                altApLabel.Location = new Point(altimeterInstrumentControl1.Location.X, altimeterInstrumentControl1.Location.Y + altimeterInstrumentControl1.Height + generalBufferSize);
                altApLabel.Font = new Font("Microsoft Sans Serif", apLabelFont);
                altApLabel.Text = "Altitude";

                headingApLabel.Location = new Point(headingIndicatorInstrumentControl1.Location.X, headingIndicatorInstrumentControl1.Location.Y + headingIndicatorInstrumentControl1.Height + generalBufferSize);
                headingApLabel.Font = new Font("Microsoft Sans Serif", apLabelFont);
                headingApLabel.Text = "Heading";
            }
            //place the autopilot textboxes
            if (true)
            {
                speedApBox.Location = new Point(speedApLabel.Location.X, speedApLabel.Location.Y + speedApLabel.Size.Height + generalBufferSize);
                speedApBox.Size = new Size(airSpeedIndicatorInstrumentControl1.Size.Width, speedApLabel.Size.Height);
                speedApBox.Font = new Font("Microsoft Sans Serif", apBoxFont);
                speedApBox.Multiline = false;

                pitchApBox.Location = new Point(pitchApLabel.Location.X, pitchApLabel.Location.Y + pitchApLabel.Size.Height + generalBufferSize);
                pitchApBox.Size = new Size(attitudeIndicatorInstrumentControl1.Size.Width, pitchApLabel.Size.Height);
                pitchApBox.Font = new Font("Microsoft Sans Serif", apBoxFont);
                pitchApBox.Multiline = false;

                bankApLabel.Location = new Point(pitchApBox.Location.X, pitchApBox.Location.Y + pitchApBox.Size.Height + generalBufferSize);
                bankApLabel.Font = new Font("Microsoft Sans Serif", apLabelFont);
                bankApLabel.MaximumSize = new Size(attitudeIndicatorInstrumentControl1.Size.Width, pitchApBox.Size.Height);
                bankApLabel.Text = "Bank";

                bankApBox.Location = new Point(bankApLabel.Location.X, bankApLabel.Location.Y + bankApLabel.Height + generalBufferSize);
                bankApBox.Size = new Size(attitudeIndicatorInstrumentControl1.Size.Width, bankApLabel.Size.Height);
                bankApBox.Font = new Font("Microsoft Sans Serif", apBoxFont);
                bankApBox.Multiline = false;

                altApBox.Location = new Point(altApLabel.Location.X, altApLabel.Location.Y + altApLabel.Height + generalBufferSize);
                altApBox.Size = new Size(altimeterInstrumentControl1.Size.Width, altApLabel.Size.Height);
                altApBox.Font = new Font("Microsoft Sans Serif", apBoxFont);
                altApBox.Multiline = false;

                headingApBox.Location = new Point(headingApLabel.Location.X, headingApLabel.Location.Y + headingApLabel.Height + generalBufferSize);
                headingApBox.Size = new Size(headingIndicatorInstrumentControl1.Size.Width, headingApLabel.Size.Height);
                headingApBox.Font = new Font("Microsoft Sans Serif", apBoxFont);
                headingApBox.Multiline = false;
            }
            //setup the PID picker
            if (true)
            {
                pidPickerDropdown.Location = new Point(generalBufferSize, verticalSpeedIndicatorInstrumentControl1.Height + generalBufferSize);
                pidPickerDropdown.Width = verticalSpeedIndicatorInstrumentControl1.Width - generalBufferSize;
                pidPickerDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
                pidPickerDropdown.Font = new Font("Microsoft Sans Serif", 30);
                pidPickerDropdown.Items.Add("Altitude");
                pidPickerDropdown.Items.Add("Pitch");
                pidPickerDropdown.Items.Add("Bank");
                pidPickerDropdown.Items.Add("Speed");
                pidPickerDropdown.Items.Add("Heading");
                pidPickerDropdown.Items.Add("Runway Lineup");
            }
            //set the PID value boxes
            if (true)
            {
                pBox.Location = new Point(pidPickerDropdown.Location.X, pidPickerDropdown.Location.Y + pidPickerDropdown.Height + generalBufferSize);
                pBox.Font = new Font("Microsoft Sans Serif", pidBoxFont);
                pBox.Width = pidPickerDropdown.Width / 2;
                pBox.DecimalPlaces = 4;
                pBox.InterceptArrowKeys = false;
                pBox.Maximum = 10000;
                pBox.Minimum = -10000;
                pBox.Increment = 0.0001M;
                pBox.Value = 0;
                iBox.Location = new Point(pBox.Location.X, pBox.Location.Y + pBox.Height + generalBufferSize);
                iBox.Font = new Font("Microsoft Sans Serif", pidBoxFont);
                iBox.Width = pBox.Width;
                iBox.DecimalPlaces = 4;
                iBox.InterceptArrowKeys = false;
                iBox.Maximum = 10000;
                iBox.Minimum = -10000;
                iBox.Increment = 0.0001M;
                dBox.Location = new Point(iBox.Location.X, iBox.Location.Y + iBox.Height + generalBufferSize);
                dBox.Font = new Font("Microsoft Sans Serif", pidBoxFont);
                dBox.Width = iBox.Width;
                dBox.DecimalPlaces = 4;
                dBox.InterceptArrowKeys = false;
                dBox.Maximum = 10000;
                dBox.Minimum = -10000;
                dBox.Increment = 0.0001M;
                sendPidButton.Location = new Point(dBox.Location.X, dBox.Location.Y + dBox.Height + generalBufferSize);
                sendPidButton.Size = new Size(dBox.Width, dBox.Height + 10);
                sendPidButton.Font = new Font("Microsoft Sans Serif", pidBoxFont);
            }
            //set the serial dropdown and connect/disconnect buttons
            if (true)
            {
                serialSelectDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
                serialSelectDropdown.Font = new Font("Microsoft Sans Serif", 20);
                serialSelectDropdown.Location = new Point(pBox.Location.X + pBox.Size.Width + generalBufferSize, pBox.Location.Y);
                serialSelectDropdown.Size = new Size((verticalSpeedIndicatorInstrumentControl1.Location.X + verticalSpeedIndicatorInstrumentControl1.Size.Width) - (pBox.Location.X + pBox.Size.Width + (2 * generalBufferSize)), pBox.Size.Height);
                serialConnectButton.Location = new Point(iBox.Location.X + iBox.Size.Width + generalBufferSize, iBox.Location.Y);
                serialConnectButton.Size = new Size((verticalSpeedIndicatorInstrumentControl1.Location.X + verticalSpeedIndicatorInstrumentControl1.Size.Width) - (iBox.Location.X + iBox.Size.Width + (2 * generalBufferSize)), iBox.Size.Height);
                serialConnectButton.Font = new Font("Microsoft Sans Serif", buttonFont / 2);
                serialDisconnectButton.Location = new Point(dBox.Location.X + dBox.Size.Width + generalBufferSize, dBox.Location.Y);
                serialDisconnectButton.Size = new Size((verticalSpeedIndicatorInstrumentControl1.Location.X + verticalSpeedIndicatorInstrumentControl1.Size.Width) - (dBox.Location.X + dBox.Size.Width + (2 * generalBufferSize)), dBox.Size.Height);
                serialDisconnectButton.Font = new Font("Microsoft Sans Serif", buttonFont / 2);
            }
            
            //set the joystick connection button
            joystickAquireButton.Size = new Size(verticalSpeedIndicatorInstrumentControl1.Size.Width/2, pBox.Height);
            joystickAquireButton.Location = new Point(generalBufferSize, this.Size.Height - joystickAquireButton.Size.Height - generalBufferSize); 
            joystickAquireButton.Font = new Font("Microsoft Sans Serif", aquireJoystickButtonFont);

            //place the exit
            quitButton.Height = 20;
            quitButton.Width = 45;
            quitButton.Location = new Point(this.Width - quitButton.Width, 0);

            //other/misc
            sendPidButton.Font = new Font("Microsoft Sans Serif", buttonFont*7/8);
            //TODO - add return home, autoland, and manual override buttons
            //TODO - add clicking on the respective dial to activate the autopilot
            //TODO - add 'lights' for checking if autopilot is enabled (background of label?)

        }
        private void serialConnectButton_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                try
                {
                    if (serialSelectDropdown.SelectedIndex != -1)
                    {
                        serialPort.PortName = serialSelectDropdown.SelectedItem.ToString();
                        serialPort.Open();
                        Console.WriteLine("Serial port opened with name: " + serialPort.PortName);
                        serialOpenedMillis = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Serial Port Error", MessageBoxButtons.OK);
                    serialConnectButton.BackColor = Color.Red;
                    serialDisconnectButton.BackColor = Color.Red;
                }
            }
            if (serialPort.IsOpen)
            {
                serialConnectButton.BackColor = Color.Green;
                serialDisconnectButton.BackColor = Color.Green;
            }
        }
        private void serialDisconnectButton_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen) {
                serialPort.Close();
                serialConnectButton.BackColor = Color.Red;
                serialDisconnectButton.BackColor = Color.Red;
                Console.WriteLine("Serial port closed!");
                Console.WriteLine("Serial port opened for: " + ((DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()-(long)serialOpenedMillis)/1000.0) + " seconds");
            }

            serialSelectDropdown.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                serialSelectDropdown.Items.Add(port.ToString());
            }
        }
        private void initSerialPorts() {
            //get all ports in a list
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                serialSelectDropdown.Items.Add(port.ToString());
            }
        }
        private void updateJoystickValues() {
            if (joystickExists)
            {
                try
                {
                    var state = joystick.GetCurrentState();
                    joystickExists = true;
                    joystickButtons = state.Buttons;
                    joystickAxis[0] = state.X / (float)joystickMaxValue;
                    joystickAxis[1] = state.Y / (float)joystickMaxValue;
                    joystickAxis[2] = (joystickMaxValue - state.Z) / (float)joystickMaxValue;

                    //label1.Text = "";
                    //print a percentage of how "pushed" the joystick is
                    //label1.Text += "Right: " + joystickAxis[0] + "\n";
                    //label1.Text += "Down:" + joystickAxis[1] + "\n";
                    //label1.Text += "Throttle: " + joystickAxis[2] + "\n";

                    for (int i = 0; i < joystickButtons.Length; i++)
                    {
                        if (joystickButtons[i] == true)
                        {
                            //label1.Text += "B" + i.ToString() + "\n";
                        }
                    }
                    joystickAquireButton.BackColor = Color.Green;
                }
                catch (Exception exp)
                {
                    Console.WriteLine("Joystick Exception: " + exp);
                    if (joystickExists)
                    {
                        joystickAquireButton.BackColor = Color.Red;
                    }
                    joystickExists = false;
                }
            }

        }
        private void aquireJoystickButton_Click(object sender, EventArgs e)
        {
            joystickInit();
        }
        private void compileSerialDataToSend() {

                if (planeState == "manualControl")
                {
                    string str = "CMR";// command, manual control, roll
                    str += (float)joystickAxis[0];// x-axis
                    checksumAndSendToSerial(str);
                    str = "CMP";// command, manual control, pitch
                    str += (float)joystickAxis[1];// y-axis
                    checksumAndSendToSerial(str);
                    str = "CMT";//command, manual control, throttle
                    str += (float)joystickAxis[2];// throttle
                    checksumAndSendToSerial(str);
                }
                else if (planeState == "") { 
                    
                }
        }
        private void readDataFromSerial() {
            if (serialPort.IsOpen)
            {
                while (serialPort.BytesToRead > 0) {
                    serialReadSave += (char)serialPort.ReadChar();
                }

                while (serialReadSave.IndexOf('~') > -1 && serialReadSave.IndexOf(']') > -1)//if there is a message in the buffer
                {// while loop so we can parse multiple messages in one function call
                    // check checksum
                    int msgStart = serialReadSave.IndexOf('~');
                    int numStart = serialReadSave.IndexOf('[');
                    int msgEnd = serialReadSave.IndexOf(']');
                    string msg = serialReadSave.Substring(msgStart + 1, numStart - msgStart - 1);
                    int msgTotal = Int16.Parse(serialReadSave.Substring(numStart + 1, msgEnd - numStart - 1));
                    int calculatedTotal = 0;
                    for (int i = 0; i < msg.Length; i++)
                    {
                        calculatedTotal = (byte)msg[i];
                    }
                    if (calculatedTotal == msgTotal)
                    {
                        //if correct, send to insturment panel and write data to file
                        if (msg[0] == 'D')// this isn't a switch statement only because individual items in a switch statement can't be collapsed
                        {
                            switch (msg[1])
                            {
                                case 'S':
                                    airSpeedIndicatorInstrumentControl1.SetAirSpeedIndicatorParameters(Int16.Parse(msg.Substring(2)));// TODO: when updated insturment pictures, parse to float, multiply by factor, cast to int
                                    break;
                                case 'G':
                                    // TODO: parse data and set insturment
                                    break;
                                case 'L':
                                    // TODO: send to file
                                    // Feature? - add GMaps to screen to show location in real time
                                    break;
                                case 'A':
                                    altimeterInstrumentControl1.SetAlimeterParameters(Int16.Parse(msg.Substring(2)));// TODO: when updated insturment pictures, parse to float, multiply by factor, cast to int
                                    break;
                                case 'H':
                                    headingIndicatorInstrumentControl1.SetHeadingIndicatorParameters(Int16.Parse(msg.Substring(2)));// TODO: when updated insturment pictures, parse to float, multiply by factor, cast to int
                                    break;
                                case 'V':
                                    verticalSpeedIndicatorInstrumentControl1.SetVerticalSpeedIndicatorParameters(Int16.Parse(msg.Substring(2)));// TODO: when updated insturment pictures, parse to float, multiply by factor, cast to int
                                    break;
                            }
                        }
                        else if (msg[0] == 'A')
                        {

                        }
                        writeToFile(msg, "correctData.txt");

                    }
                    else
                    {
                        //if incorrect, write to different file and don't send to insturments (light up indicator for incorrect checksums)
                        writeToFile(msg, "incorrectData.txt");
                        // TODO: light up indicator for incorrect checksum
                    }

                    serialReadSave = serialReadSave.Substring(msgEnd + 1);//cut the message from the buffer
                }
                        
                    // if no more bytes, save message and return
            }
            
            
            

        }
        private void altimeterInstrumentControl1_Click(object sender, EventArgs e)
        {
            //TODO - enable altitude PID
            altApLabel.BackColor = Color.Green;
        }
        private void checksumAndSendToSerial(string msg) {
            //fill remainder of chars with ] until 30 chars total
            string str = "~";
            str += msg;
            int total = 0;
            for(int i = 0; i < msg.Length; i++) {
                total += (byte)msg[i];
            }
            msg += "[";
            msg += total.ToString();
            msg += "]";
            for(int i = msg.Length; i < 30; i++)
            {
                //add ']'s until at 30 chars
                msg += "]";
            }
            if (serialPort.IsOpen)
            {
                serialPort.Write(msg);
            }
        }
        private void writeToFile(string msg, string filename) {
            using (StreamWriter file = new StreamWriter(filename, true))
            {
                file.Write(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());// millis since epoch (midnight Jan 1, 1970)
                file.Write(",");
                file.WriteLine(msg);
            }
        }
        
    }
}
