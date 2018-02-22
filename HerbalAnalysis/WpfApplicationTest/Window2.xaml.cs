using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplicationTest
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            FileInfo[] arrfi = null;
            ProjectManager.WindowsHelper.LoadPictrueFiles(false, false, out arrfi);
            if (arrfi != null)
            {
                for (int i = 0; i < arrfi.Length; ++i)
                {
                    arrfi[i].CopyTo("D:\\360Downloads" + "//" + i.ToString()  + arrfi[i].Extension, true);
                }
            }
        }
    }
}
