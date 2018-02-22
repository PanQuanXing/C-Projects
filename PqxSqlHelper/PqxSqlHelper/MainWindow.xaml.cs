using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace PqxSqlHelper
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int typeIndex;
        public int TYPEINDEX
        {
            get { return typeIndex; }
            set { typeIndex = value; }
        }
        private int tableIndex;
        public int TABLEINDEX
        {
            get { return tableIndex; }
            set { tableIndex = value; }
        }

        private BackgroundWorker backgroundWorker;
        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker = (BackgroundWorker)this.FindResource("backgroundWorker1");
            loadTypesFromExcel();
            typeComboBox.DataContext = this;
            tableItemComboBox.DataContext = this;
        }
        private void loadTypesFromExcel()
        {
            string path = Properties.Settings.Default.excelPath;         
            using (FileStream fs = File.OpenRead(Properties.Settings.Default.excelPath))
            {
                HSSFWorkbook wk = new HSSFWorkbook(fs);
                for (int i = 0; i < wk.NumberOfSheets; i++)
                {
                    typeComboBox.Items.Add(wk.GetSheetAt(i).SheetName);
                }
            }
            typeIndex = Properties.Settings.Default.typeIndex;
            tableIndex = Properties.Settings.Default.tableListIndex;
            fieldComboBox.Text = Properties.Settings.Default.searchFieldStr;
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tableItemComboBox.Items.Clear();
            using (FileStream fs = File.OpenRead(Properties.Settings.Default.excelPath))
            {
                HSSFWorkbook wk = new HSSFWorkbook(fs);
                if(typeIndex<0)
                    return;
                ISheet sheet = wk.GetSheetAt(typeIndex);
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    tableItemComboBox.Items.Add(sheet.GetRow(i).GetCell(1).ToString());
                }
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.typeIndex = typeIndex;
            Properties.Settings.Default.tableListIndex = tableIndex;
            Properties.Settings.Default.searchFieldStr = fieldComboBox.Text;
            Properties.Settings.Default.Save();
        }

        private void tableItemComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(typeIndex<0)
            tipsTextBlock.Text = "请先选择类型，再选择表名！";
        }

        private void tableItemComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            tipsTextBlock.Text = "";
        }
        //搜索按钮，点击事件
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(fieldComboBox.Text))
            {
                tipsTextBlock.Text = "请选择需要搜索的字段！";
                return;
            }
            tipsTextBlock.Text = "";
            string tableName,ssss;
            using (FileStream fs = File.OpenRead(Properties.Settings.Default.excelPath))
            {
                HSSFWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(typeIndex);
                tableName = sheet.GetRow(tableIndex).GetCell(0).ToString();
            }

            if (checkSettings())
            {
                showSettingsDialog();
            }
            else
            {
                try
                {
                    using (SqlConnection sqlCon = new SqlConnection(string.Format("server={0};user={1};pwd={2};database={3}",
                                                                                Properties.Settings.Default.serverIP, Properties.Settings.Default.sqlUserName,
                                                                                Properties.Settings.Default.sqlUserPassword, Properties.Settings.Default.dbName)))
                    {
                        if (string.IsNullOrEmpty(fieldTextBox.Text))
                            ssss = string.Format("Select top 400 * from {0}", tableName, fieldComboBox.Text);
                        else
                            ssss = string.Format("Select * from {0} Where {1} like '%{2}%'", tableName, fieldComboBox.Text, fieldTextBox.Text);
                
                        using (SqlDataAdapter sqlAda = new SqlDataAdapter(ssss, sqlCon))
                        {
                            sqlAda.SelectCommand.CommandType = System.Data.CommandType.Text;
                            sqlCon.Open();
                            DataTable dt = new DataTable();
                            dt.Clear();
                            sqlAda.Fill(dt);
                            //赋值给DataGrid ...
                            
                            dataGrid.ItemsSource = dt.DefaultView;
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    sqle = null;
                    showSettingsDialog();
                }
            }



        }
        //判断服务器ip、Sql用户名和密码是否为空.
        //Settings.settings下保存的字符串默认值为"".
        private bool checkSettings()
        {
            return string.IsNullOrEmpty(Properties.Settings.Default.serverIP)||string.IsNullOrEmpty(Properties.Settings.Default.sqlUserName)||string.IsNullOrEmpty(Properties.Settings.Default.sqlUserPassword)||string.IsNullOrEmpty(Properties.Settings.Default.dbName);
        }
        private void showSettingsDialog()
        {
            ConnectWindow toolWin = new ConnectWindow();
            toolWin.Owner = this;
            toolWin.ShowDialog();
        }

        private void fieldComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(tableItemComboBox.SelectedIndex<0)
            {
                tipsTextBlock.Text = "请先选择表名，再选择字段！";
            }
        }

        private void fieldComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            tipsTextBlock.Text = "";
        }

        private void setttingButton_Click(object sender, RoutedEventArgs e)
        {
            showSettingsDialog();
        }

        private static List<string> DataColumnCollectionNameToList(DataColumnCollection dcc)
        {
            List<string> result = new List<string>();
            foreach (DataColumn dc in dcc)
            {
                result.Add(dc.ColumnName);
            }
            return result;
        }

        private void tableItemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeIndex < 0)
            {
                tipsTextBlock.Text = "请先选择类型，再选择表名！";
                return;
            }
            tipsTextBlock.Text = "";
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            if (tableIndex < 0)
            {
                e.Cancel = false;
                return;
            }

            string tableName;

            using (FileStream fs = File.OpenRead(Properties.Settings.Default.excelPath))
            {
                HSSFWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(typeIndex);
                tableName = sheet.GetRow(tableIndex).GetCell(0).ToString();
            }
           
            if (checkSettings())
            {
                e.Cancel = true;
                //showSettingsDialog();
            }
            else
            {
                try
                {
                    using (SqlConnection sqlCon = new SqlConnection(string.Format("server={0};user={1};pwd={2};database={3}",
                                                                                Properties.Settings.Default.serverIP, Properties.Settings.Default.sqlUserName,
                                                                                Properties.Settings.Default.sqlUserPassword, Properties.Settings.Default.dbName)))
                    {
                        using (SqlDataAdapter sqlAda = new SqlDataAdapter(string.Format("Select * from {0} Where ID=OBJECT_ID('tb_name')", tableName),sqlCon))
                        {
                            sqlAda.SelectCommand.CommandType =System.Data.CommandType.Text;
                            sqlCon.Open();
                            DataTable dt = new DataTable();
                            dt.Clear();
                            sqlAda.Fill(dt);
                            e.Result =DataColumnCollectionNameToList(dt.Columns);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    sqle = null;
                    e.Cancel = true;
                }
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                tableIndex = -1;
                tableItemComboBox.Text = "";
                showSettingsDialog();
            }
            else if (e.Error != null)
            {
                // An error was thrown by the DoWork event handler.
                MessageBox.Show(e.Error.Message, "An Error Occurred");
            }
            else
            {
                fieldComboBox.DataContext = (List<string>)e.Result;
            }
        }

    }
}
/*
         private void button2_Click(object sender, EventArgs e)
        {
            string s = "";
            string str ="Select Name FROM SysColumns Where id=Object_Id('dataTable')";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read()){
                 s+=dr[0].ToString();
            }
            MessageBox.Show(s);
        }
 */
/*
 --举个例子，表名为zwj，字段为sp，查询sp字段中含有'所有'的语句为
select * from zwj where sp like '%所有%'
 
--表名为zwj，字段为sp，查询sp字段中含有'所'或'有'的语句为
select * from zwj where sp like '%所%' or sp like '%有%'
 */


//select * from xxx where * like '%xxx%'

/*
 查看所有表名：
select name from sysobjects where type='U'

查询表的所有字段名：
Select name from syscolumns Where ID=OBJECT_ID('表名')
 */
/*
   private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection("server=192.168.0.3;user=MyTest;pwd=FUCKyou++;database=HIS0720"))
            {
                //查找前300条："select top 300 * from BsUser"
                //查找名字中带周字的所有
                //Select * from 表名 Where ID=OBJECT_ID('表名')
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter("Select * from BsUser Where ID=OBJECT_ID('表名')", sqlCon))
                {
                    sqlAdapter.SelectCommand.CommandType = System.Data.CommandType.Text;
                    sqlCon.Open();
                    DataSet ds = new DataSet("HupyInfo");
                    List<string> list = new List<string>();
                    if (ds.Tables["HupyInfo"] != null)
                    {
                        ds.Tables[0].Clear();
                    }
                    sqlAdapter.Fill(ds, "HupyInfo");
                    list = DataColumnCollectionNameToList(ds.Tables[0].Columns);
                    comboBox1.DataContext = list;
                }
            }
        }


        public static List<string> DataColumnCollectionNameToList(DataColumnCollection dcc)
        {
            List<string> result = new List<string>();
            foreach (DataColumn dc in dcc)
            {
                result.Add(dc.ColumnName);
            }
            return result;
        }
 */