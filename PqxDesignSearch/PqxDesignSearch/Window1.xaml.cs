using PqxSecret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PqxDesignSearch
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.UserCode = MD5.EncryptMD5("TX9XD-98N7V-6WMQ6-BX7FG-H8Q99");
            Properties.Settings.Default.AdministratorCode = MD5.EncryptMD5("2F77B-TNFGY-69QQF-B8YKP-D69TJ");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //测试1:
            //MessageBox.Show(string.Format("UserCode:{0} \n AdministratorCode:{1}",Properties.Settings.Default.UserCode,Properties.Settings.Default.AdministratorCode));
            if (string.Compare(MD5.EncryptMD5(textBox1.Text), Properties.Settings.Default.UserCode) == 0)
            {
                Properties.Settings.Default.IsActive = true;
                Properties.Settings.Default.IsUserCode = true;
                textBlock1.Foreground = System.Windows.Media.Brushes.Green;
                textBlock1.Text = "你已经成功激活用户版。\n请关闭此窗口后重启应用！";
            }
            else if (string.Compare(MD5.EncryptMD5(textBox1.Text), Properties.Settings.Default.AdministratorCode) == 0)
            {
                Properties.Settings.Default.IsActive = true;
                Properties.Settings.Default.IsUserCode = false;
                textBlock1.Foreground = System.Windows.Media.Brushes.Green;
                textBlock1.Text = "你已经成功激活管理员版。\n请关闭此窗口后重启应用！";
            }
            else
            {
                textBlock1.Foreground = System.Windows.Media.Brushes.Red;
                textBlock1.Text = "请输入正确的激活码！";
            }
            Properties.Settings.Default.Save();
            button1.IsEnabled = false;
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = textBox1.Text;
            if (string.Compare(MD5.EncryptMD5(str),Properties.Settings.Default.UserCode) == 0)
            {
                textBlock1.Foreground = System.Windows.Media.Brushes.Green;
                textBlock1.Text = "你已经正确输入用户版激活码。\n请按回车键进行激活！";
            }
            else if (string.Compare(MD5.EncryptMD5(str), Properties.Settings.Default.AdministratorCode) == 0)
            {
                textBlock1.Foreground = System.Windows.Media.Brushes.Green;
                textBlock1.Text = "你已经正确输入管理员版激活码。\n请按回车键进行激活！";
            }
            else
            {
                textBlock1.Foreground = System.Windows.Media.Brushes.Red;
                textBlock1.Text = "请输入正确的激活码！";
            }
        }
    }
}
