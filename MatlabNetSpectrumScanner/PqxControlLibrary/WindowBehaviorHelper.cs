using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace PqxControlLibrary
{
    class WindowBehaviorHelper
    {
        private const int WM_NCHITTEST = 0x0084;//测试消息
        private const int WM_GETMINMAXINFO = 0x0024;//大小变化消息

        private Window TargetWindow;//目标窗口
        private int CornerWidth=3;//拐角宽度
        private int BorderTransparentThickness=5;//阴影宽度
        private int BorderThickness = 4;//边框宽度
        private Point MousePoint = new Point();//鼠标坐标
        public enum HitTest : int//句柄测试
        {
            #region
            HTERROR = -2,
            HTTRANSPARENT = -1,
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTSIZE = HTGROWBOX,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTREDUCE = HTMINBUTTON,
            HTZOOM = HTMAXBUTTON,
            HTSIZEFIRST = HTLEFT,
            HTSIZELAST = HTBOTTOMRIGHT,
            HTOBJECT = 19,
            HTCLOSE = 20,
            HTHELP = 21
            #endregion
        }
        //定义构造函数
        public WindowBehaviorHelper(Window window)
        {
            this.TargetWindow = window;
        }
        //修复行为
        public void RepairBahavior()
        {
            if (null == TargetWindow)
                return;
            this.TargetWindow.SourceInitialized += delegate
            {
                IntPtr handle=(new WindowInteropHelper(TargetWindow)).Handle;
                HwndSource hwndSource = HwndSource.FromHwnd(handle);
                if (null != hwndSource)
                {
                    hwndSource.AddHook(WindowProc);
                }
            };
        }
        //定义消息循环队列
        private IntPtr WindowProc(IntPtr hwnd,int msg,IntPtr wParam,IntPtr lParam,ref bool handled) 
        {
            switch (msg)
            {
                case WM_NCHITTEST:
                    if (WindowState.Normal != TargetWindow.WindowState)
                        break;
                    this.MousePoint.X=(lParam.ToInt32() & 0xffff);
                    this.MousePoint.Y = (lParam.ToInt32() >> 16);

                    //window窗口左上角
                    if (this.MousePoint.X > this.TargetWindow.Left + this.BorderTransparentThickness
                        && this.MousePoint.X <= this.TargetWindow.Left + this.BorderTransparentThickness + this.CornerWidth
                        && this.MousePoint.Y > this.TargetWindow.Top + this.BorderTransparentThickness
                        && this.MousePoint.Y <= this.TargetWindow.Top + this.BorderTransparentThickness + this.CornerWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTTOPLEFT);
                    }
                    //window窗口左下角
                    else if (this.MousePoint.X > this.TargetWindow.Left + this.BorderTransparentThickness
                        && this.MousePoint.X <= this.TargetWindow.Left + this.BorderTransparentThickness + this.CornerWidth
                        && this.MousePoint.Y > this.TargetWindow.Top+ this.TargetWindow.ActualHeight - this.BorderTransparentThickness
                        && this.MousePoint.Y <= this.TargetWindow.Top+this.TargetWindow.ActualHeight - this.BorderTransparentThickness - this.CornerWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOMLEFT);
                    }
                    //window窗口右上角
                    else if(this.MousePoint.X < this.TargetWindow.Left + this.TargetWindow.ActualWidth - this.BorderTransparentThickness
                        && this.MousePoint.X >= this.TargetWindow.Left + this.TargetWindow.ActualWidth - this.BorderTransparentThickness - this.CornerWidth
                        && this.MousePoint.Y > this.TargetWindow.Top + this.BorderTransparentThickness
                        && this.MousePoint.Y <= this.TargetWindow.Top + this.BorderTransparentThickness + this.CornerWidth)
                    {
                        handled=true;
                        return new IntPtr((int)HitTest.HTTOPRIGHT);
                    }
                    //window窗口右下角
                    else if (this.MousePoint.X < this.TargetWindow.Left + this.TargetWindow.ActualWidth - this.BorderTransparentThickness
                        && this.MousePoint.X >= this.TargetWindow.Left + this.TargetWindow.ActualWidth - this.BorderTransparentThickness - this.CornerWidth
                        && this.MousePoint.Y < this.TargetWindow.Top + this.TargetWindow.ActualHeight - this.BorderTransparentThickness
                        && this.MousePoint.Y >= this.TargetWindow.Top + this.TargetWindow.ActualHeight - this.BorderTransparentThickness - this.CornerWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOMRIGHT);
                    }

                    //window窗口左侧
                    else if (this.MousePoint.X > this.TargetWindow.Left + this.BorderTransparentThickness
                        && this.MousePoint.X <= this.TargetWindow.Left + this.BorderTransparentThickness + this.BorderThickness
                        && this.MousePoint.Y > this.TargetWindow.Top + this.BorderTransparentThickness
                        && this.MousePoint.Y < this.TargetWindow.Top + this.TargetWindow.ActualHeight - this.BorderTransparentThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTLEFT);
                    }
                    //window窗口右侧
                    else if (this.MousePoint.X < this.TargetWindow.Left + this.TargetWindow.ActualWidth - this.BorderTransparentThickness
                        && this.MousePoint.X >= this.TargetWindow.Left + this.TargetWindow.ActualWidth - this.BorderTransparentThickness - this.BorderThickness
                        && this.MousePoint.Y > this.TargetWindow.Top + this.BorderTransparentThickness
                        && this.MousePoint.Y < this.TargetWindow.Top + this.TargetWindow.ActualHeight - this.BorderTransparentThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTRIGHT);
                    }
                    //window窗口上方
                    else if (this.MousePoint.X > this.TargetWindow.Left + this.BorderTransparentThickness
                        && this.MousePoint.X < this.TargetWindow.Left + this.TargetWindow.ActualWidth - this.BorderTransparentThickness
                        && this.MousePoint.Y > this.TargetWindow.Top + this.BorderTransparentThickness
                        && this.MousePoint.Y <= this.TargetWindow.Top + this.BorderTransparentThickness + this.BorderThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTTOP);
                    }
                    //window窗口下方
                    else if (this.MousePoint.X > this.TargetWindow.Left + this.BorderTransparentThickness
                        && this.MousePoint.X < this.TargetWindow.Left + this.TargetWindow.ActualWidth - this.BorderTransparentThickness
                        && this.MousePoint.Y < this.TargetWindow.Top + this.TargetWindow.ActualHeight - this.BorderTransparentThickness
                        && this.MousePoint.Y >= this.TargetWindow.Top + this.TargetWindow.ActualHeight - this.BorderTransparentThickness - this.BorderThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOM);
                    }
                    //其他消息
                    else
                    {
                        break;
                    }
                case WM_GETMINMAXINFO:
                    WMGetMinMaxInfo(hwnd,lParam);
                    handled=true;
                    break;
            }
            return IntPtr.Zero;
        }
        private void WMGetMinMaxInfo(IntPtr hwnd,IntPtr lParam)
        {
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left) - 3;
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top) - 3;
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left) + 6;
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top) + 6;
                mmi.ptMinTrackSize.x = (int)this.TargetWindow.MinWidth;
                mmi.ptMinTrackSize.y = (int)this.TargetWindow.MinHeight;
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);
        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        #region Nested type: MINMAXINFO
        [StructLayout(LayoutKind.Sequential)]
        internal struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }
        #endregion

        #region Nested type: MONITORINFO
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor;
            public RECT rcWork;
            public int dwFlags;
        }
        #endregion

        #region Nested type: POINT
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int x;
            public int y;
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        #endregion

        #region Nested type: RECT
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public static readonly RECT Empty;

            public int Width
            {
                get { return Math.Abs(right - left); }
            }
            public int Height
            {
                get { return bottom - top; }
            }

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(RECT rcSrc)
            {
                left = rcSrc.left;
                top = rcSrc.top;
                right = rcSrc.right;
                bottom = rcSrc.bottom;
            }

            public bool IsEmpty
            {
                get
                {
                    return left >= right || top >= bottom;
                }
            }

            public override string ToString()
            {
                if (this == Empty)
                {
                    return "RECT {Empty}";
                }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Rect))
                {
                    return false;
                }
                return (this == (RECT)obj);
            }

            public override int GetHashCode()
            {
                return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            }

            public static bool operator ==(RECT rect1, RECT rect2)
            {
                return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom);
            }

            public static bool operator !=(RECT rect1, RECT rect2)
            {
                return !(rect1 == rect2);
            }
        }
        #endregion
    }

}
