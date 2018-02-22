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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;


namespace PqxDesignSearch
{
    /// <summary>
    /// EditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditWindow : Window
    {
        private IWorkbook wk = null;
        private ObservableCollection<string> waysList = null;
        private int lastRowNum;
        public EditWindow()
        {

            LoadExcel();
            InitializeComponent();
            waysListBox.DataContext = waysList;
        }

        private void LoadExcel()
        {
            lastRowNum = Properties.Settings.Default.lastRowNum;//读取系统中存储的值
            waysList = new ObservableCollection<string>();
            using (FileStream fs = File.OpenRead(Properties.Settings.Default.wayExcelPath))
            {
                wk = new HSSFWorkbook(fs);
            }
            ISheet sheet = wk.GetSheetAt(0);
            for (int i = 1; i <= lastRowNum; i++)
            {
                waysList.Add(sheet.GetRow(i).GetCell(0).ToString());
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //窗体关闭的时候，对编辑后的Excel进行保存
            using (FileStream fs = File.OpenWrite(Properties.Settings.Default.wayExcelPath))
            {
                wk.Write(fs);
                wk.Close();
            }
            Properties.Settings.Default.lastRowNum = lastRowNum;
            Properties.Settings.Default.Save();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(themeTextBox.Text) && string.IsNullOrEmpty(sentenceTextBox.Text))
            {
                MessageBox.Show(this, "主题或者语句字符串不能为空！", "错误：", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            ISheet sheet = wk.GetSheetAt(0);
            lastRowNum += 1;
            IRow row = sheet.CreateRow(lastRowNum);
            row.CreateCell(0).SetCellValue(themeTextBox.Text);
            row.CreateCell(1).SetCellValue(sentenceTextBox.Text);
            waysList.Add(themeTextBox.Text);
        }

        private void waysListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (waysListBox.SelectedIndex < 0)
                return;
            //从1行开始保存，所以需要将SelectedIndex+1。
            IRow row = wk.GetSheetAt(0).GetRow(waysListBox.SelectedIndex + 1);
            themeTextBox.Text = row.GetCell(0).ToString();
            sentenceTextBox.Text = row.GetCell(1).ToString();
        }
        /*
            总结以上四条得到ref和out使用时的区别是：
            ①：ref指定的参数在函数调用时候必须初始化，不能为空的引用。而out指定的参数在函数调用时候可以不初始化；
            ②：out指定的参数在进入函数时会清空自己，必须在函数内部赋初值。而ref指定的参数不需要。
        */
        //NOPI删除EXCEl中特定行
        private void sheetRemoveRow(ISheet sheet, int rowIndex)
        {
            int lastNum = sheet.LastRowNum;
            if (rowIndex < 0)
            {
                return;
            }
            rowIndex += 1;
            if (rowIndex > lastNum)//如果是删除最后一行就直接remove掉所有cell
            {
                sheet.RemoveRow(sheet.GetRow(rowIndex - 1));
            }
            else
            {
                sheet.ShiftRows(rowIndex, lastNum, -1);
            }
        }


        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            //清空输入
            themeTextBox.Text = "";
            sentenceTextBox.Text = "";
            if (waysListBox.SelectedIndex < 0)
                return;
            sheetRemoveRow(wk.GetSheetAt(0), waysListBox.SelectedIndex + 1);
            lastRowNum -= 1;
            waysList.RemoveAt(waysListBox.SelectedIndex);
            waysListBox.SelectedIndex = -1;//ComBox的索引置0
        }
    }
}
