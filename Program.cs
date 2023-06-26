using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarThunderDailyRewardConsoleApp
{
    internal class Program
    {
        static int SLEEPMS = 10000;

        static void Main(string[] args)
        {
            CloseGame closeGame = new CloseGame();

            Process proc = new Process();
            // Start war thunder exec from steam
            proc.StartInfo.FileName = "D://Steam/steam.exe";
            proc.StartInfo.Arguments = "steam://rungameid/236390";

            Debug.WriteLine("Start process exit");
            proc.Start();

            // Wait for 30 seconds
            System.Threading.Thread.Sleep(SLEEPMS);

            IntPtr game = IntPtr.Zero;
            // Get War thunder process
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                if ((string)p.ProcessName == "aces") 
                {
                    Debug.WriteLine("War Thunder is open");
                    game = p.MainWindowHandle;
                    Debug.WriteLine(game);
                }
                //Debug.WriteLine(p.ProcessName);
            }
            // Focus War thunder on computer
            if (game != IntPtr.Zero) 
            {
                closeGame.RunCloseGameScript(game);
            }
            // Close Game
            
            // Schedule Task run war thunder
            // Program presses enter twice after 30 seconds
            // steam://rungameid/236390
            // Close War thunder application
            // Control Sequence close game
            // downArrow -> esc > 2xdownArrow -> enter -> leftarrow -> enter
        }
    }
}
