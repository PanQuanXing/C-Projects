using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace PqxControlLibrary
{
    public class PqxTabItem:TabItem
    {
        public static readonly DependencyProperty MoverBrushProperty;
        public static readonly DependencyProperty EnterBrushProperty;
        public Brush MoverBrush
        {
            get { return base.GetValue(PqxTabItem.MoverBrushProperty) as Brush; }
            set { base.SetValue(PqxTabItem.EnterBrushProperty,value); }
        }
        public Brush EnterBrush 
        {
            get { return base.GetValue(PqxTabItem.EnterBrushProperty) as Brush;}
            set { base.SetValue(PqxTabItem.EnterBrushProperty,value); }
        }
        static PqxTabItem()
        {
            PqxTabItem.MoverBrushProperty = DependencyProperty.Register("MoverBrush",typeof(Brush),typeof(PqxTabItem),new PropertyMetadata(null));
            PqxTabItem.EnterBrushProperty = DependencyProperty.Register("EnterBrush",typeof(Brush),typeof(PqxTabItem),new PropertyMetadata(null));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PqxTabItem),new FrameworkPropertyMetadata(typeof(PqxTabItem)));
        }
        public PqxTabItem()
        {
            base.Header = "PqxTabItem";
            base.Background = Brushes.LightBlue;
        }
    }
}
