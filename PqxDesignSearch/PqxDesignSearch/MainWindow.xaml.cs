using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using PqxSecret;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int lastRowNum;
        public MainWindow()
        {
            InitializeComponent();
            LoadExcel();
            waysComboBox.SelectedIndex = Properties.Settings.Default.currentWayIndex;//恢复原来选择的选项
        }
        private void LoadExcel()
        {
            lastRowNum = Properties.Settings.Default.lastRowNum;//读取系统中存储的值,在主Windows中不能修改其值。
            using (FileStream fs = File.OpenRead(Properties.Settings.Default.wayExcelPath))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(0);
                for (int i = 1; i <= lastRowNum; i++)
                {
                    waysComboBox.Items.Add(sheet.GetRow(i).GetCell(0).ToString());
                }
                wk.Close();
            }
        }
        private void showSettingsDialog()
        {
            SettingWindow toolWin = new SettingWindow();
            toolWin.Owner = this;
            toolWin.ShowDialog();
        }
        //判断服务器ip、Sql用户名和密码是否为空.
        //Settings.settings下保存的字符串默认值为"".
        private bool checkSettings()
        {
            return string.IsNullOrEmpty(Properties.Settings.Default.serverIP) || string.IsNullOrEmpty(Properties.Settings.Default.sqlUserName) || string.IsNullOrEmpty(Properties.Settings.Default.sqlUserPassword) || string.IsNullOrEmpty(Properties.Settings.Default.dbName);
        }
        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            showSettingsDialog();
        }

        private void editWayButton_Click(object sender, RoutedEventArgs e)
        {
            waysComboBox.Items.Clear();
            EditWindow toolWin = new EditWindow();
            toolWin.Owner = this;
            toolWin.ShowDialog();
            LoadExcel();
        }

        private void exactButton_Click(object sender, RoutedEventArgs e)//精确搜索
        {
            megTextBlock.Text = "";
            if (string.IsNullOrEmpty(fieldTextBox.Text))
            {
                return;
            }

            if (waysComboBox.SelectedIndex < 0)
            {
                MessageBox.Show(this, "请先选择搜索方式！", "警告：", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            StringBuilder wayStr = new StringBuilder();
            using (FileStream fs = File.OpenRead(Properties.Settings.Default.wayExcelPath))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                wayStr.Append(wk.GetSheetAt(0).GetRow(waysComboBox.SelectedIndex + 1).GetCell(1).ToString());
                wk.Close();
            }
            //获取到的wayStr格式为“...where .... like”形式

            if (checkSettings())//检查是否保存了数据库连接
            {
                showSettingsDialog();
            }
            else
            {
                try
                {
                    using (SqlConnection sqlCon = new SqlConnection(string.Format("server={0};user={1};pwd={2};database={3}",
                                                                                DES.DecryptDES(Properties.Settings.Default.serverIP), DES.DecryptDES(Properties.Settings.Default.sqlUserName),
                                                                                DES.DecryptDES(Properties.Settings.Default.sqlUserPassword), Properties.Settings.Default.dbName)))
                    {
                        wayStr.Append(string.Format(" '{0}'", fieldTextBox.Text));


                        using (SqlDataAdapter sqlAda = new SqlDataAdapter(wayStr.ToString(), sqlCon))
                        {
                            sqlAda.SelectCommand.CommandType = System.Data.CommandType.Text;
                            sqlCon.Open();
                            DataTable dt = new DataTable();
                            dt.Clear();
                            sqlAda.Fill(dt);
                            //赋值给DataGrid ...

                            this.gridView1.ItemsSource = dt.DefaultView;
                            megTextBlock.Text = string.Format("一共有{0}例。", dt.Rows.Count);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    showSettingsDialog();
                }
            }
        }

        private void fuzzyButton_Click(object sender, RoutedEventArgs e)//模糊搜索
        {
            megTextBlock.Text = "";
            if (string.IsNullOrEmpty(fieldTextBox.Text))
            {
                return;
            }

            if (waysComboBox.SelectedIndex < 0)
            {
                MessageBox.Show(this, "请先选择搜索方式！", "警告：", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            StringBuilder wayStr = new StringBuilder();
            using (FileStream fs = File.OpenRead(Properties.Settings.Default.wayExcelPath))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                wayStr.Append(wk.GetSheetAt(0).GetRow(waysComboBox.SelectedIndex + 1).GetCell(1).ToString());
                wk.Close();
            }
            //获取到的wayStr格式为“...where .... like”形式

            if (checkSettings())//检查是否保存了数据库连接
            {
                showSettingsDialog();
            }
            else
            {
                try
                {
                    using (SqlConnection sqlCon = new SqlConnection(string.Format("server={0};user={1};pwd={2};database={3}",
                                                                                DES.DecryptDES(Properties.Settings.Default.serverIP), DES.DecryptDES(Properties.Settings.Default.sqlUserName),
                                                                                DES.DecryptDES(Properties.Settings.Default.sqlUserPassword), Properties.Settings.Default.dbName)))
                    {
                        wayStr.Append(string.Format(" '%{0}%'", fieldTextBox.Text));


                        using (SqlDataAdapter sqlAda = new SqlDataAdapter(wayStr.ToString(), sqlCon))
                        {
                            sqlAda.SelectCommand.CommandType = System.Data.CommandType.Text;
                            sqlCon.Open();
                            DataTable dt = new DataTable();
                            dt.Clear();
                            sqlAda.Fill(dt);
                            //赋值给DataGrid ...

                            this.gridView1.ItemsSource = dt.DefaultView;
                            megTextBlock.Text = string.Format("一共有{0}例。", dt.Rows.Count);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    showSettingsDialog();
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.currentWayIndex=waysComboBox.SelectedIndex;//保存选择的选项
            Properties.Settings.Default.Save();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.IsActive)
            {
                if (Properties.Settings.Default.IsUserCode)
                {
                    editWayButton.IsEnabled = false;
                    this.Title = "SqlServer查询助手(用户版)—电白区中医院信息科（作者：潘全星-2017-11-07）";
                }
                else
                {
                    this.Title = "SqlServer查询助手(管理版)—电白区中医院信息科（作者：潘全星-2017-11-07）";
                }
            }
            else
            {
                this.Title = "SqlServer查询助手(未激活，请联系作者！)—电白区中医院信息科（作者：潘全星-2017-11-07）";
                mainGrid.IsEnabled = false;
            }
        }
    }
}
