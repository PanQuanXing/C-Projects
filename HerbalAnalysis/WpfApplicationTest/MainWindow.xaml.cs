using System;
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

namespace WpfApplicationTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProjectManager.HerbalAnalysisProject haPrj = new ProjectManager.HerbalAnalysisProject();
        private bool flags=false;
        private bool isInputCorrectFlag = false;
        private string nodeName;
        private int oldResult;
        private int startWL;
        private int endWL;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AppendButton_Click(object sender, RoutedEventArgs e)
        {
            int result;
            if (isInputCorrectFlag)
            {
                nodeName = NodeNameTextBox.Text;
                if (int.TryParse(NodeNameTextBox.Text,out result))
                {
                    if (oldResult == result)
                    {
                        nodeName = (oldResult + 1).ToString();
                        NodeNameTextBox.Text = nodeName;
                    }
                    oldResult = result;
                }
                else
                {
                    nodeName = NodeNameTextBox.Text;
                }
                //创建目录，并向TreeView添加

            }
            else
            {
                TipsTextBlock.Text="请输入合法的检测样品名称！";
            }

        }

        private void NodeNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (haPrj.IsProjectNameCorrect(NodeNameTextBox.Text))
            {
                isInputCorrectFlag = true;
                TipsTextBlock.Text = "";
            }
            else
            {
                isInputCorrectFlag = false;
                TipsTextBlock.Text = "检测样品名称为空或包含下列符号:\\/:?\"<>|";
            }
        }
        //限制TextBox粘贴命令
        private void WaveLengthTB_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            int result;
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                if (!int.TryParse((String)e.DataObject.GetData(typeof(String)),out result))
                { e.CancelCommand(); TipsTextBlock.Text = "只能粘贴数字！"; }
            }
            else { e.CancelCommand(); TipsTextBlock.Text = "只能粘贴数字！"; }
        }
        //限制TextBox只能输入数字
        private void WaveLengthTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //e.Key;无论输入什么键都是：ImeProcessed？？？所以用e.ImeProcessedKey
            Key key=e.ImeProcessedKey;
      if ((key >= Key.NumPad0 && key <= Key.NumPad9) ||
                (key >= Key.D0 && key <= Key.D9) ||
                e.Key == Key.Back ||
                 e.Key == Key.Left || e.Key == Key.Right)
            {
                TipsTextBlock.Text = "";
                //判断是否为组合键
                //if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                //{
                //    e.Handled = true;
                //}
                //e.Handled = true;
            }
            else
            {
                TipsTextBlock.Text = "只能输入数字！";
                e.Handled = true;
            }
        }

        private void NodeNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            flags = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(nodeName);
        }
    }
}
