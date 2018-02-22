using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ConsoleApplicationTest
{
    public delegate bool CallBack(int hwnd,int lParam);//定义委托函数类型
    class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency,int duration);
        [DllImport("user32.dll")]
        public static extern int EnumWindows(CallBack x,int y);
        static void Main(string[] args)
        {
            CallBack myCallBack = new CallBack((h, l) => {
                Console.WriteLine("Window handle is:");
                Console.WriteLine(h);
                return true; });
            EnumWindows(myCallBack,0);
            Beep(800,2000);
            Console.ReadLine();
        }
    }
}
