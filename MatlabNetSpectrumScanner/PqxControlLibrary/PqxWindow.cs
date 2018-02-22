using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PqxControlLibrary
{
    public class PqxWindow:Window
    {
        //定义构造函数
        public PqxWindow()
        {
            this.AllowsTransparency = true;
            this.DefaultStyleKey=typeof(PqxWindow);
            //最小化、最大化、缩放的修复
            WindowBehaviorHelper wbh = new WindowBehaviorHelper(this);
            wbh.RepairBahavior();
            this.MouseLeftButtonDown += new MouseButtonEventHandler(PqxWindow_MouseLeftButtonDown);
        }

        private void PqxWindow_MouseLeftButtonDown(object sender,MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
