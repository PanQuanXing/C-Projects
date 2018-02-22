using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

using MatlabDonetWPF;
using System.Windows.Interop;
using System.Threading;
namespace SpectrumScanner
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private FigureTests.FourFigureTest fft;
        public MainWindow()
        {
            InitializeComponent();
            fft = new FigureTests.FourFigureTest();
            fft.figure1Test();
            fft.figure2Test();
        }
        private void ListBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            //fft.figure1Test();
            hh=new MyControlHost(border1.ActualHeight,border1.ActualWidth,"Figure 1");
            hh.MessageHook += new HwndSourceHook(ControlMsgFilter);
            Win32.User.SetFocus(hh.Handle);
            border1.Child = hh;
            layoutDocument.IsActive = true;

        }
        private void ListBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            //fft.figure2Test();
            //border2.Child = new MyControlHost(border2.ActualHeight, border2.ActualWidth, "Figure 2");
            IntPtr hwndControl = MatlabFigureHelper.FindWindow("SunAwtFrame","Figure 1");
            Win32.User.SetWindowPos(hwndControl, Win32.User.HWND_NOTOPMOST, 0, 0, (int)border1.ActualWidth, (int)border1.ActualHeight, Win32.User.SWP_HIDEWINDOW);
        }

        private void ListBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            fft.figure3Test();
            border3.Child = new MyControlHost(border3.ActualHeight, border3.ActualWidth, "Figure 3");
        }
        private HwndHost hh;
        private void ListBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            fft.figure4Test();
            //IntPtr hwndControl = MatlabFigureHelper.FindWindow("SunAwtFrame", "Figure 4");
            //uint oldStyle = MatlabFigureHelper.GetWindowLong(hwndControl, Win32.User.GWL_STYLE);
            //Win32.User.SetWindowLong(hwndControl,Win32.User.GWL_STYLE, (int)(oldStyle | Win32.User.WS_CHILD) & ~Win32.User.WS_BORDER);
            border4.Child = new MyControlHost(border4.ActualHeight, border4.ActualWidth, "Figure 4");
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private IntPtr ControlMsgFilter(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            handled = false;
            MessageBox.Show(msg.ToString());
            //if (msg == Win32.User.WA_CLICKACTIVE)
            //    MessageBox.Show(((uint)wParam.ToInt32()>>16&0xFFFF).ToString());
            return IntPtr.Zero;
        }

    }
}
