using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.IO.Ports;
using OpenHardwareMonitor;
using OpenHardwareMonitor.Hardware;

namespace ConsoleApp1
{
    class Program
    {
        static Computer thisComputer;
        static SerialPort port = new SerialPort("COM3", 9600);
        static double modifier = 1;

        static void Main()
        {
            try {
                port.Open(); // open port
            }
            catch
            {
                System.Threading.Thread.Sleep(90000); //try again
                port.Open(); // open port
            }
            thisComputer = new Computer() { CPUEnabled = true };
            thisComputer.Open(); //init and open computer
            Console.WriteLine("Hello World!");
            while (1 == 1)
            {
                Console.WriteLine("Modifier: {0}\n", modifier);
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
                           
                                if (Int32.Parse(sensor.Value.Value.ToString()) > 59){
                                    Console.WriteLine("Mans too hot!");
                                    int n = 1;
                                    while (n++ < (900 *modifier)) { // correleates to 1 min  of on time
                                        byte[] buffer = new byte[] { Convert.ToByte('1') };
                                        port.Write(buffer, 0, 1);
                                    }
                                    modifier = modifier * 1.6;
                                }
                                else
                                {
                                    modifier = modifier/1.1;
                                    if(modifier <= 1){
                                        modifier = 1;
                                    }
                                }
                                
                            }
                        }
                    }
                }
                System.Threading.Thread.Sleep(25000); 
            }
        }
    }
}
