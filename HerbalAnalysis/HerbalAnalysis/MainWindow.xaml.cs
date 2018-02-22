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
using LanguageLocalization;
using System.IO;
using ProjectManager;
using System.Collections;

namespace HerbalAnalysis
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private HerbalAnalysisProject haProj;
        private bool isNeedUpdata=true;
        private bool isInputCorrectFlag = false;
        private string nodeName;
        private int oldResult;
        private HerbalAnalysisProject haPrj = new HerbalAnalysisProject();

        public MainWindow()
        {
            InitializeComponent();
            loadTexts();
            BuildFileTree(@"D:\matlabTest");
        }
        private void loadTexts()
        {
            this.Title = I18N.GetString("HerbalAnalysisHelper");
            menuItemFile.Header = I18N.GetString("_File");
            menuItemNew.Header = I18N.GetString("_New Project");
            menuItemOpen.Header = I18N.GetString("_Open Project");
            menuItemAdd.Header = I18N.GetString("Ad_d");
            menuItemClose.Header = I18N.GetString("_Close");
            menuItemRecentProject.Header = I18N.GetString("Recent Pro_ject");
            menuItemExit.Header = I18N.GetString("E_xit");

            menuItemEdit.Header = I18N.GetString("_Edit");
            menuItemUndo.Header = I18N.GetString("_Undo");
            menuItemCut.Header = I18N.GetString("Cu_t");
            menuItemCopy.Header = I18N.GetString("_Copy");
            menuItemPaste.Header = I18N.GetString("_Paste");
            menuItemDelete.Header = I18N.GetString("_Delete");

            menuItemView.Header = I18N.GetString("_View");
            menuItemProjResManager.Header = I18N.GetString("_Project Resources Manager");
            menuItemProjAttrPage.Header = I18N.GetString("Project Attributes Pa_ge");

            menuItemAbout.Header = I18N.GetString("_About");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void menuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
        //新建项目命令
        private void NewPqxProjCommand(object sender, ExecutedRoutedEventArgs e)
        {
            ProjInitiWindow piw = new ProjInitiWindow();
            bool? isOk = piw.ShowDialog();
            if (isOk == true)
            {
                haProj = piw.HaProj;
                Directory.CreateDirectory(haProj.GetActualProjFolderPath());
                //File.Create(haProj.GetMainProjFilePath()).Close();
                string[] projData = { DateTime.Now.ToString(), haProj.GetActualProjFolderPath() + "\\".ToString() + haProj.Name + ".xml".ToString() };
                string mainProjFilePath = haProj.GetMainProjFilePath();
                File.WriteAllLines(mainProjFilePath,projData);
                HistoryProjItemManager.AddHistoryItem(mainProjFilePath);
                isNeedUpdata = true;
                MessageBox.Show("确定"+haProj.ToString());
            }else if (isOk == false)
            {
                MessageBox.Show("取消");
            }

        }
        //窗体关闭执行的一些操作
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindowsManager.SaveWindowSize(this);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            MainWindowsManager.RestoreSize(this);
        }
        //加载最近项目菜单
        private void menuItemFile_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isNeedUpdata)
            {
                Queue historyQueue = HistoryProjItemManager.GetHistoryItems();
                menuItemRecentProject.ItemsSource = historyQueue;//采用数据绑定
                isNeedUpdata = false;
            }
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(!File.Exists((sender as TextBlock).Text))
            {
                HistoryProjItemManager.RemoveItem((sender as TextBlock).Text);
                isNeedUpdata = true;
            }
        }
        private void BuildFileTree(string folderPath)
        {
            //if (!Directory.Exists(folderPath))
            //    return;
            //DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            //treeFileStructure.Items.Clear();

            //foreach (DirectoryInfo subDir in directoryInfo.GetDirectories())
            //{
            //    TreeViewItem newItem = new TreeViewItem();
            //    newItem.Tag = subDir;
            //    newItem.Header = subDir.ToString();
            //    newItem.Items.Add("*");
            //    treeFileStructure.Items.Add(newItem);
            //}

        }

        private void OpenPqxProjCommand(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void treeFileStructure_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.OriginalSource;
            item.Items.Clear();
            if (item.Tag is DirectoryInfo)
            {
                DirectoryInfo dir = item.Tag as DirectoryInfo;
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeViewItem newItem = new TreeViewItem();
                    newItem.Tag = subDir;
                    newItem.Header = subDir.ToString();
                    newItem.Items.Add("*");
                    item.Items.Add(newItem);
                }
                foreach (FileInfo subFile in dir.GetFiles())
                {
                    TreeViewItem newItem = new TreeViewItem();
                    newItem.Tag = subFile;
                    newItem.Header = subFile.ToString();
                    item.Items.Add(newItem);
                }
            }
        }
        //添加按钮的Click事件
        private void AppendButton_Click(object sender, RoutedEventArgs e)
        {
            int result;
            if (isInputCorrectFlag)
            {
                nodeName = NodeNameTextBox.Text;
                if (int.TryParse(NodeNameTextBox.Text, out result))
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
                int startWl, spaceWl;
                if(int.TryParse(WaveLengthStartTB.Text,out startWl) && int.TryParse(WaveLengthSpaceTB.Text,out spaceWl))
                {
                    TestingSample ts = new TestingSample();
                    ts.StartWL = startWl;
                    ts.SpaceWL = spaceWl;
                    ts.PicturesPath=Directory.CreateDirectory(haPrj.GetActualProjFolderPath()+"\\".ToString()+nodeName);
                    CopyPictures(ref ts);
                    haPrj.TestsSamples.Add(ts);
                }
                else
                {
                    TipsTextBlock.Text = "开始波长和步长必须是整数！";
                }
            }
            else
            {
                TipsTextBlock.Text = "请输入合法的检测样品名称！";

            }
        }
        //复制图片到指定的文件夹
        private void CopyPictures(ref TestingSample ts)
        {
            FileInfo[] arrfi = null;
            ProjectManager.WindowsHelper.LoadPictrueFiles(IsSortTimeCheckBox.IsChecked, IsReverseCheckBox.IsChecked, out arrfi);
            if (arrfi!=null)
            {
                for (int i=0;i<arrfi.Length;++i)
                {
                    arrfi[i].CopyTo(ts.PicturesPath.Name+"//"+i.ToString()+arrfi[i].Extension,true);
                }
            }
        }
        //检测是否输入正确的目录名称
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
                if (!int.TryParse((String)e.DataObject.GetData(typeof(String)), out result))
                { e.CancelCommand(); TipsTextBlock.Text = "只能粘贴数字！"; }
            }
            else { e.CancelCommand(); TipsTextBlock.Text = "只能粘贴数字！"; }
        }
        //限制TextBox只能输入数字
        private void WaveLengthTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //e.Key;无论输入什么键都是：ImeProcessed？？？所以用e.ImeProcessedKey
            Key key = e.ImeProcessedKey;
            if ((key >= Key.NumPad0 && key <= Key.NumPad9) ||
                      (key >= Key.D0 && key <= Key.D9) ||
                      e.Key == Key.Back ||
                       e.Key == Key.Left || e.Key == Key.Right)
            {
                TipsTextBlock.Text = "";
            }
            else
            {
                TipsTextBlock.Text = "只能输入数字！";
                e.Handled = true;
            }
        }
    }
}
