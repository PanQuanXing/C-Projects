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
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(I18N.GetString("_File"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void menuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindowsManager.SaveWindowSize(this);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            MainWindowsManager.RestoreSize(this);
        }
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
            if (!Directory.Exists(folderPath))
                return;
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            treeFileStructure.Items.Clear();

            foreach (DirectoryInfo subDir in directoryInfo.GetDirectories())
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.Tag = subDir;
                newItem.Header = subDir.ToString();
                newItem.Items.Add("*");
                treeFileStructure.Items.Add(newItem);
            }

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
    }
}
