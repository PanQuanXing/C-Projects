using System;
using System.AddIn.Hosting;
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

namespace NotePlus
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<AddInToken> addIns;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            string path = Environment.CurrentDirectory;
            AddInStore.Update(path);
            addIns = AddInStore.FindAddIns(typeof(HostView.MyHostView), path);
            tsm.ItemsSource = addIns;
            //ItemCollection items = tsm.Items;

            //foreach (var i in items)
            //{
            //    Type t = i.GetType();
            //    string msg = i.ToString();
            //}

            // int i=items.Count;
            //MenuItem m = (MenuItem)items[0];
        }

        private void StackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb=(TextBlock)((sender as StackPanel).Children)[0];
            for (int i=0;i< addIns.Count;++i)
            {
                AddInToken at = addIns[i] as AddInToken;
                if (at.Name==tb.Text)
                {
                    HostView.MyHostView addin = at.Activate<HostView.MyHostView>(AddInSecurityLevel.Internet);
                    string changedString = addin.MyUpperAddIn(textBox.Text);
                    textBox.Text = changedString;
                }
            }
        }
    }
}
