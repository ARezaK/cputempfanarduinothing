using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using OpenHardwareMonitor;
using OpenHardwareMonitor.Hardware;

namespace ConsoleApp1
{
    class Program
    {
        static Computer thisComputer;
        static SerialPort port = new SerialPort("COM3", 9600);

        static void Main()
        {
            port.Open(); // open port
            thisComputer = new Computer() { CPUEnabled = true };
            thisComputer.Open(); //init and open computer
            Console.WriteLine("Hello World!");
            while (1 == 1) { 
                foreach (var hardwareItem in thisComputer.Hardware)
                {
                    if (hardwareItem.HardwareType == HardwareType.CPU) //looking for cpu only
                    {
                        hardwareItem.Update();
                        foreach (IHardware subHardware in hardwareItem.SubHardware) //not sure what this is
                            subHardware.Update();

                        foreach (var sensor in hardwareItem.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Temperature)
                            {

                                Console.WriteLine("{0} Temperature = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                                if (Int32.Parse(sensor.Value.Value.ToString()) > 50){
                                    int n = 1;
                                    while (n++ < 1000) { 
                                        byte[] buffer = new byte[] { Convert.ToByte('1') };
                                        port.Write(buffer, 0, 1);
                                    }
                                }
                                System.Threading.Thread.Sleep(2000);
                            }
                        }
                    }
                }
            }
        }
    }
}
