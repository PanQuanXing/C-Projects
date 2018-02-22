
using ProjectManager;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace HerbalAnalysis
{
    /// <summary>
    /// ProjInitiWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProjInitiWindow : Window
    {
        private HerbalAnalysisProject haProj = new HerbalAnalysisProject() { Name="",ProjectFolderPath=""};
        private bool isNameInputCorrect=false;
        private bool isFolderPathInputCorrect = false;
        public HerbalAnalysisProject HaProj
        {
            get { return haProj; }
            set { haProj = value; }
        }
        public ProjInitiWindow()
        {
            InitializeComponent();
        }

        private void ProjPathScanBtn_Click(object sender, RoutedEventArgs e)
        {
            var strResult=WindowsHelper.ScanFilePath();
            if (strResult != "")
                haProj.ProjectFolderPath = strResult;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.DataContext = haProj;
        }

        private void OkCancel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == "OkBtn")
            {
                if (isNameInputCorrect&&isFolderPathInputCorrect)
                {
                    if (Directory.Exists(ProjPathTextBox.Text + "\\".ToString() + ProjNameTextBox.Text))
                    {
                        ShowWarningPopup("项目位置中已经存在项目名称的文件夹，请重新命名项目名称！", ProjNameTextBox);
                        return;
                    }
                    DialogResult = true;
                }
                else
                {
                    ShowWarningPopup("项目名称或项目位置不正确，请重新输入！", ProjNameTextBox);
                }
            }
            else if (btn.Name == "CancelBtn")
            {
                DialogResult = false;
            }
        }
        private void ProjNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isNameInputCorrect= IsProjNameCorrect();
        }
        private void ShowWarningPopup(string msg,TextBox tagTb)
        {
            WarningLabel.Content = msg;
            WarningPopup.IsOpen = true;
            if (tagTb!=null)
            {
                tagTb.Focus();
            }
        }
        private void ShowWarningPopup(string msg)
        {
            ShowWarningPopup(msg,null);
        }
        private bool IsProjNameCorrect()
        {
            if (haProj.IsProjectNameCorrect(ProjNameTextBox.Text))
            {
                if (WarningPopup.IsOpen)
                    WarningPopup.IsOpen = false;
                return true;
            }
            ShowWarningPopup("项目名称为空或包含下列符号:\\/:?\"<>|",ProjNameTextBox);
            return false;
        }
        private bool IsFolderPathCorrect()
        {
            if (!haProj.IsProjectFolderPathExist(ProjPathTextBox.Text))
            {
                ShowWarningPopup("所选项目保存位置不存在，请重新选择！", ProjPathTextBox);
                return false;
            }
            if (WarningPopup.IsOpen)
                WarningPopup.IsOpen = false;
            return true;
        }

        private void ProjPathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isFolderPathInputCorrect=IsFolderPathCorrect();
        }
    }
}
