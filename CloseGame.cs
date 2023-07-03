using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WarThunderDailyRewardConsoleApp
{
    // Using INPUT SIMULATOR PACKAGE
    internal class CloseGame
    {
        // Close War thunder application
        // Control Sequence close game
        // downArrow -> esc > 2xdownArrow -> enter -> leftarrow -> enter
        public CloseGame()
        {
        }

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData,
   UIntPtr dwExtraInfo);

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("user32.dll")]
        static extern IntPtr SetActiveWindow(IntPtr hWnd);

        public void RunCloseGameScript(IntPtr game) 
        {
            Console.WriteLine("Running Close Game Script");
            SetForegroundWindow(game);
            SetActiveWindow(game);
            double height = SystemParameters.FullPrimaryScreenHeight;
            double width = SystemParameters.FullPrimaryScreenWidth;

            Debug.WriteLine("Closing Game" );
            Debug.WriteLine(height);
            Debug.WriteLine(width);

            SetActiveWindow(game);
            Thread.Sleep(1000);

            // Close Update / Friend Referral Pop up
            Cursor.Position = new System.Drawing.Point(1504, 79); // Close Update X
            Thread.Sleep(2000);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new System.UIntPtr());
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new System.UIntPtr());
            Debug.WriteLine("ButtonPress");

            // Collect Reward
            //Cursor.Position = new System.Drawing.Point(963, 484); // Collect Reward
            //Thread.Sleep(2000);
            //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new System.UIntPtr());
            //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new System.UIntPtr());

            Cursor.Position = new System.Drawing.Point(1349, 746); // Close Reward
            Thread.Sleep(2000);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new System.UIntPtr());
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new System.UIntPtr());

            Cursor.Position = new System.Drawing.Point(1349, 746); // Close Reward
            Thread.Sleep(2000);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new System.UIntPtr());
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new System.UIntPtr());


            // Open Close Game Menu
            Cursor.Position = new System.Drawing.Point(88, 30); // 3 Dash Menu
            Thread.Sleep(2000);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new System.UIntPtr());
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new System.UIntPtr());

            Cursor.Position = new System.Drawing.Point(172, 117); // Move to Exit Game Button
            Thread.Sleep(3000);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new System.UIntPtr());
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new System.UIntPtr());


            // Exit game Pop up
            Cursor.Position = new System.Drawing.Point(895, 570); // Move to Exit Game Button
            Thread.Sleep(2000);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new System.UIntPtr());
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new System.UIntPtr());

            Debug.WriteLine(Cursor.Position);
            Console.WriteLine("Game Should Be Closed");
        }
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
    }
}
