using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace PqxControlLibrary
{
    public class PqxProgressBar:ProgressBar
    {
        public static readonly DependencyProperty MyTextFontSize;
        public static readonly DependencyProperty MyTextContent;
        public static readonly DependencyProperty MyTextForeColor;
        public static readonly DependencyProperty MyBackgroundBrush;
        public static readonly DependencyProperty MyBackgroundStroke;
        public static readonly DependencyProperty MyTrackBrush;
        public static readonly DependencyProperty MyRadiusX;
        public static readonly DependencyProperty MyRadiusY;
        public double TextFontSize
        {
            get { return (double)base.GetValue(MyTextFontSize); }
            set { base.SetValue(MyTextFontSize,value); }
        }
        public Brush TextForeColor
        {
            get { return base.GetValue(MyTextForeColor) as Brush; }
            set { base.SetValue(MyTextForeColor,value); }
        }
        public string TextContent
        {
            get { return base.GetValue(MyTextContent) as string; }
            set { base.SetValue(MyTextContent,value); }
        }
        public Brush BackgroundBrush
        {
            get { return base.GetValue(MyBackgroundBrush) as Brush; }
            set { base.SetValue(MyBackgroundBrush,value); }
        }
        public Brush BackgroundStroke
        {
            get { return base.GetValue(MyBackgroundStroke) as Brush; }
            set { base.SetValue(MyBackgroundStroke,value); }
        }
        public Brush TrackBrush
        {
            get { return base.GetValue(MyTrackBrush) as Brush; }
            set { base.SetValue(MyTrackBrush,value ); }
        }
        public double TheRadiusX
        {
            get { return (double)base.GetValue(MyRadiusX); }
            set { base.SetValue(MyRadiusX, value); }
        }
        public double TheRadiusY
        {
            get { return (double)base.GetValue(MyRadiusY); }
            set { base.SetValue(MyRadiusY,value); }
        }
        static PqxProgressBar()
        {
            PqxProgressBar.MyTextFontSize = DependencyProperty.Register("TextFontSize",typeof(double),typeof(PqxProgressBar),new PropertyMetadata(null));
            PqxProgressBar.MyTextForeColor = DependencyProperty.Register("TextForeColor",typeof(Brush),typeof(PqxProgressBar),new PropertyMetadata(null));
            PqxProgressBar.MyTextContent = DependencyProperty.Register("TextContent",typeof(string),typeof(PqxProgressBar),new PropertyMetadata(null));
            PqxProgressBar.MyBackgroundBrush = DependencyProperty.Register("BackgroundBrush",typeof(Brush),typeof(PqxProgressBar),new PropertyMetadata(null));
            PqxProgressBar.MyBackgroundStroke = DependencyProperty.Register("BackgroudStroke",typeof(Brush),typeof(PqxProgressBar),new PropertyMetadata(null));
            PqxProgressBar.MyTrackBrush = DependencyProperty.Register("TrackBrush",typeof(Brush),typeof(PqxProgressBar),new PropertyMetadata(null));
            PqxProgressBar.MyRadiusX = DependencyProperty.Register("TheRadiusX",typeof(double),typeof(PqxProgressBar),new PropertyMetadata(null));
            PqxProgressBar.MyRadiusY=DependencyProperty.Register("TheRadiusY",typeof(double),typeof(PqxProgressBar),new PropertyMetadata(null));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PqxProgressBar), new FrameworkPropertyMetadata(typeof(PqxProgressBar)));
        }
        public PqxProgressBar()
        {
            TextContent = "PqxProgressBar";
            TextForeColor = Brushes.White;
            BackgroundBrush = Brushes.LightBlue;
            BackgroundStroke = Brushes.Gray;
            TrackBrush = Brushes.Blue;
            TheRadiusX = 0;
            TheRadiusY = 0;
            TextFontSize = 10;
        }
    }
}
