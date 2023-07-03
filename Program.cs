using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace WarThunderDailyRewardConsoleApp
{
    internal class Program
    {
        //3600000 1hour
        static int IDLESLEEP = 3600000; // 1 hour
        static int SLEEPMS = 10000; // 10 seconds
        static int UTCHOUR = 0;

        static void Main(string[] args)
        {
            Program p = new Program();
            CloseGame closeGame = new CloseGame();
            Process proc = new Process();

            bool hasCollectedReward = false;
            int counter = 0;
            // Check Registry
            if (!p.DoesRegistryExists())
            {
                p.SetStartUp(); // Registry Does Not Exists
                Debug.WriteLine("Registry Added");
            }

            while (true)
            {
                System.DateTime date = System.DateTime.Now.ToUniversalTime();
                int currentHour = date.Hour;

                Console.WriteLine(currentHour);

                if (counter >= 24)
                {
                    counter = 0;
                    hasCollectedReward = false;
                }

                if (currentHour >= UTCHOUR && !hasCollectedReward)
                    p.StartProcess(p, proc, closeGame, ref hasCollectedReward);
                
                System.Threading.Thread.Sleep(SLEEPMS);
                // Sleep FOR Hour
                counter++;
                // Check if the game is still open and close again if not
                while (p.GetGame() != IntPtr.Zero) 
                {
                    Console.WriteLine("Game Still not closed");
                    closeGame.RunCloseGameScript(p.GetGame());
                }

                Console.WriteLine("Going to Sleep for 1h");
                System.Threading.Thread.Sleep(IDLESLEEP);
            }

            // Close Game
            p.StartProcess(p, proc, closeGame, ref hasCollectedReward);
            // Schedule Task run war thunder
            // Program presses enter twice after 30 seconds
            // steam://rungameid/236390
            // Close War thunder application
            // Control Sequence close game
            // downArrow -> esc > 2xdownArrow -> enter -> leftarrow -> enter
        }
        private void StartProcess(Program p, Process proc, CloseGame closeGame, ref bool hasCollectedReward) 
        {
            Console.WriteLine("Start Collecting Rewards");
            // Start war thunder exec from steam
            proc.StartInfo.FileName = "D://Steam/steam.exe";
            proc.StartInfo.Arguments = "steam://rungameid/236390";

            proc.Start();

            // Wait for 30 seconds for game to open
            System.Threading.Thread.Sleep(SLEEPMS);

            // Get Game or 0
            IntPtr game = p.GetGame();
            

            // Focus War thunder on computer
            if (game != IntPtr.Zero) 
            {
                Console.WriteLine("Starting Close Game Script");
                closeGame.RunCloseGameScript(game);
                hasCollectedReward = true;
            }

        }
        private IntPtr GetGame() 
        {
            IntPtr game = IntPtr.Zero;

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
            return game;
        }
        private void SetStartUp() 
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Debug.WriteLine(strExeFilePath);


            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            rk.SetValue("WarThunderLoginApp", strExeFilePath);

        }
        private void RemoveStartUp() 
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            rk.DeleteValue("WarThunderLoginApp");
            Debug.WriteLine("Removed Registry");
        }
        private bool DoesRegistryExists() 
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            return (rk.GetValueNames().Contains("WarThunderLoginApp"));
        }
    }
}
