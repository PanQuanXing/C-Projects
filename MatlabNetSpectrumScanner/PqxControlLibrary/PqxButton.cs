using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace PqxControlLibrary
{
    public class PqxButton : Button
    {
        public static readonly DependencyProperty MouseMoverColorProperty;
        public static readonly DependencyProperty MousePressColorProperty;
        public static readonly DependencyProperty CornerRadiusProperty;
        public Brush MouseMoverColor
        {
            get { return (Brush)base.GetValue(MouseMoverColorProperty); }
            set { base.SetValue(MouseMoverColorProperty, value); }
        }
        public Brush MouseEnterColor
        {
            get { return (Brush)base.GetValue(MousePressColorProperty); }
            set { base.SetValue(MousePressColorProperty, value); }
        }
        public CornerRadius BtnCornerRadius
        {
            get { return (CornerRadius)base.GetValue(CornerRadiusProperty); }
            set { base.SetValue(CornerRadiusProperty, value); }
        }
        static PqxButton()
        {
            PqxButton.MouseMoverColorProperty = DependencyProperty.Register("MouseMoverColor", typeof(Brush), typeof(PqxButton), new PropertyMetadata(null));
            PqxButton.MousePressColorProperty = DependencyProperty.Register("MouseEnterColor", typeof(Brush), typeof(PqxButton), new PropertyMetadata(null));
            PqxButton.CornerRadiusProperty = DependencyProperty.Register("BtnCornerRadius", typeof(CornerRadius), typeof(PqxButton), new PropertyMetadata(null));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PqxButton), new FrameworkPropertyMetadata(typeof(PqxButton)));
        }
        public PqxButton()
        {
            base.Content = "PqxButton";
            base.Background = Brushes.LightBlue;
        }
    }
}