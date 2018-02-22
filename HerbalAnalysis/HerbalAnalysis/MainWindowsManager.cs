using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace HerbalAnalysis
{
    class MainWindowsManager
    {
        public static void SaveWindowSize(Window win)
        {
            Properties.Settings.Default.MainWindowPosition = win.RestoreBounds;
            Properties.Settings.Default.Save();
        }
        public static void RestoreSize(Window win)
        {
            Rect rect = Properties.Settings.Default.MainWindowPosition;
            if (rect == default(Rect))
            {
                win.Width = SystemParameters.WorkArea.Width / 2;
                win.Height = SystemParameters.WorkArea.Height / 2;
                win.Left = (SystemParameters.WorkArea.Width - win.Width) / 2;
                win.Top = (SystemParameters.WorkArea.Height - win.Height) / 2;
                return;
            }
            win.Top = rect.Top;
            win.Left = rect.Left;
            if (win.SizeToContent == SizeToContent.Manual)
            {
                win.Width = rect.Width;
                win.Height = rect.Height;
            }
        }
    }
}
