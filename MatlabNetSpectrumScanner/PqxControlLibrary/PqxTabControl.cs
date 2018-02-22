using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace PqxControlLibrary
{
    public class PqxTabControl:TabControl
    {
        static PqxTabControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PqxTabControl),new FrameworkPropertyMetadata(typeof(PqxTabControl)));
        }
    }
}
