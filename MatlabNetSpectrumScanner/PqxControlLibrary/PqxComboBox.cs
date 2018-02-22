using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace PqxControlLibrary
{
    public class PqxComboBox:ComboBox
    {
        static PqxComboBox()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PqxComboBox), new FrameworkPropertyMetadata(typeof(ComboBox)));
        }

    }
}
