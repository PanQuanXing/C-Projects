using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Interop;

namespace MatlabDonetWPF
{
    class MyControlHost : HwndHost
    {
        private IntPtr hookId = new IntPtr(3);
        private int msgHook;
        private IntPtr hwndControl;
        //private IntPtr hwndHost;
        private HandleRef hwndHost;
        private int hostHeight, hostWidth;
        private string matlabFigureName;
        public IntPtr HwndControl
        {
            get { return this.hwndControl; }
        }
        public HandleRef HwndHost
        {
            get { return this.hwndHost; }
        }
        internal const int WS_CHILD = 0x40000000,
           WS_VISIBLE = 0x10000000,
           LBS_NOTIFY = 0x00000001,
           HOST_ID = 0x00000002,
           LISTBOX_ID = 0x00000001,
           WS_VSCROLL = 0x00200000,
           WS_BORDER = 0x00800000,
           GWL_STYLE = -16,
           WS_DLGFRAME = 0x00400000,
           WM_CLOSE=0x10,
           WM_QUERYENDSESSION=0x11,
           WM_QUIT=0x12;
        public MyControlHost(double h,double w,string name)
        {
            this.msgHook = 0;
            this.hwndControl = IntPtr.Zero;
            //this.hwndHost = IntPtr.Zero;
            this.hostHeight = (int)h;
            this.hostWidth = (int)w;
            this.matlabFigureName = name;
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            //throw new NotImplementedException();
            int num = 0;
            while (num<10)
            {
                num++;
                if (hwndControl==IntPtr.Zero)
                {
                    //Thread.Sleep(400);
                    hwndControl = MatlabFigureHelper.FindWindow("SunAwtFrame",matlabFigureName);
                    Win32.User.SetWindowPos(hwndControl,Win32.User.HWND_NOTOPMOST,0,0,hostWidth,hostHeight,Win32.User.SWP_HIDEWINDOW);
                }
                else
                {
                    //MatlabFigureHelper.ShowWindow(hwndControl,MatlabFigureHelper._SW.SW_HIDE);
                    break;
                }
            }
            if (hwndControl == IntPtr.Zero)
            {
                MessageBox.Show("没有找到Figure窗体！");
                throw new NotImplementedException();
            }
            uint oldStyle = MatlabFigureHelper.GetWindowLong(hwndControl,GWL_STYLE);
            Win32.User.SetWindowLong(hwndControl, GWL_STYLE,(int)(oldStyle|Win32.User.WS_CHILD)&~Win32.User .WS_BORDER);//其中WS_DLGFRAME用于标识创建无标题窗体
            //Win32.User.SetWindowLong(hwndControl, GWL_STYLE, (int)(WS_DLGFRAME | Win32.User.WS_CHILD));
            //Win32.User.SetWindowLong(hwndControl, GWL_STYLE,Win32.User.WS_VISIBLE);
            MatlabFigureHelper.SetParent(hwndControl,hwndParent.Handle);
            //HookStart();
            //MatlabFigureHelper.UpdateWindow(hwndControl);
            hwndHost=new HandleRef(this,hwndControl);
            return hwndHost;
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            handled = false;
            return IntPtr.Zero;
        }
        protected override void DestroyWindowCore (HandleRef hwnd)
        {
            //throw new NotImplementedException();
            //MatlabFigureHelper.SendMessage(hwnd.Handle, WM_QUIT, 0, 0);
            //MatlabFigureHelper.DestroyWindow(hwnd.Handle);
            Win32.User.DestroyWindow(hwnd.Handle);
        }

        private int MyHookHandler(int nCode,int wParam,ref IntPtr lParam){
            MSG msg = (MSG)Marshal.PtrToStructure(lParam,typeof(MSG));
            if(msg.hwnd==hwndControl){
                Win32.User.SendMessage((IntPtr)hwndHost,msg.message,(int)msg.wParam,msg.lParam);
            }
            return Win32.User.CallNextHookEx(hookId,nCode,wParam,lParam);
        }
        private void HookStart()
        {
            if(msgHook==0)
            {
                Win32.User.HookProc hookProc = new Win32.   User.HookProc(MyHookHandler);
                msgHook = Win32.User.SetWindowsHookEx(hookId.ToInt32(), ref hookProc, IntPtr.Zero, Win32.Kernel.GetCurrentThreadId());
                if(msgHook==0)
                {
                    HookStop();
                    throw new Exception("SetWindowHook Failed!");
                }
            }
        }
        private void HookStop()
        {
            if (msgHook != 0)
            {
                Win32.User.UnhookWindowsHookEx((IntPtr)msgHook);
            }
        }
    }
}
