# cputempfanarduinothing

Request higher execution level
https://stackoverflow.com/a/37892024

Add a reference to the OpenHardwareMonitor dll
https://stackoverflow.com/a/12992312

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenHardwareMonitor;
using OpenHardwareMonitor.Hardware;

namespace CPUTemperatureMonitor
{
    public partial class Form1 : Form
    {

        Computer thisComputer;
        SerialPort port = new SerialPort("COM3", 9600);

        public Form1()
        {

            InitializeComponent();
            
            port.Open();

            thisComputer = new Computer() { CPUEnabled = true };

            thisComputer.Open();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            String temp = "";

            foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();

                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {

                            temp += String.Format("{0} Temperature = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                            byte[] buffer = new byte[] { Convert.ToByte('A') };
                            port.Write(buffer, 0, 1);
                        }
                    }
                }
            }

            textBox1.Text = temp;

        }
    }
}
