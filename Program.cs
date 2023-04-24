using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WarThunderDailyRewardConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process proc = new Process();
            // Start war thunder exec from steam
            proc.StartInfo.FileName = "D://Steam/steam.exe";
            proc.StartInfo.Arguments = "steam://rungameid/236390";

            Debug.WriteLine("Start process exit");
            proc.Start();

            // Wait for 30 seconds
            System.Threading.Thread.Sleep(20000);

            // Get War thunder process
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                if ((string)p.ProcessName == "aces") 
                {
                    Debug.WriteLine("War Thunder is open");
                }
                //Debug.WriteLine(p.ProcessName);
            }

            // Schedule Task run war thunder

            // Program presses enter twice after 30 seconds
            // steam://rungameid/236390
            // Close War thunder application
        }
    }
}
