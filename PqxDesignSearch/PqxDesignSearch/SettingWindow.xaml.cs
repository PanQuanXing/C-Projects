using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PqxSecret;

namespace PqxDesignSearch
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        private Regex rx;
        public SettingWindow()
        {
            InitializeComponent();
            rx = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
            loadSettingData();
        }
        private void loadSettingData()
        {
            ipPasswordBox.Password = DES.DecryptDES(Properties.Settings.Default.serverIP);
            userNamePasswordBox.Password = DES.DecryptDES(Properties.Settings.Default.sqlUserName);
            passwordBox.Password = DES.DecryptDES(Properties.Settings.Default.sqlUserPassword);
            remCheckBox.IsChecked = Properties.Settings.Default.isCheckedState;
            dbNameTextBox.Text =Properties.Settings.Default.dbName;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder tipSB = new StringBuilder();
            bool flag = true;
            tipSB.Append("");
            if (!CheckIPURLEmail.ValidateIPAddress(ipPasswordBox.Password))
            {
                flag = false;
                tipSB.Append("1.请输入合法IP!\n");
            }
            if (string.IsNullOrEmpty(userNamePasswordBox.Password))
            {
                flag = false;
                tipSB.Append("2.用户名不能为空。\n");
            }
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                flag = false;
                tipSB.Append("3.密码不能为空。\n");
            }
            if (string.IsNullOrEmpty(dbNameTextBox.Text))
            {
                flag = false;
                tipSB.Append("4.数据库名不能为空。\n");
            }
            tipsTextBlock.Text = tipSB.ToString();
            if (flag)
            {
                this.Close();
            }


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.serverIP =DES.DecryptDES( ipPasswordBox.Password);
            Properties.Settings.Default.sqlUserName = DES.DecryptDES(userNamePasswordBox.Password);
            Properties.Settings.Default.sqlUserPassword = DES.EncryptDES(passwordBox.Password);
            Properties.Settings.Default.isCheckedState = (bool)remCheckBox.IsChecked;
            Properties.Settings.Default.dbName =dbNameTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void ipPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (rx.IsMatch(ipPasswordBox.Password))
            {
                tipsTextBlock.Text = "";
            }
            else
            {
                tipsTextBlock.Text = "请输入合法IP!";
            }
        }

    }
}
