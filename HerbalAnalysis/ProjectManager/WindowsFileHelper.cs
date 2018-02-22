using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ProjectManager
{
    public class WindowsHelper
    {
        public static string ScanFilePath()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择项目保存路径：";
            if (folderBrowserDialog.ShowDialog()==DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }
            return "";
        }
        public static string[] LoadPictruesFilePath()
        {
            //创建打开文件对话框
            OpenFileDialog openDlg = new OpenFileDialog();
            //设置文件过滤器
            openDlg.Filter = "所有图像文件|*.bmp;*pcx;*.png;*jpg;*.gif;" +
                 "*tif;*ico;*drf;*cgm;*.wmf;*.eps;*emf|位图(*bmp;*jpg;*.png;...)|*.bmp;*.pcx;*.png;*.jpg;*gif;*.tif;*.ico|" +
                 "矢量图(*.wmf;*.eps;*.emf;...)|*.drf;*.cgm;*.cdr;*.wmf;*.eps;*.emf";
            //设置文件类型的顺序,从一开始
            openDlg.FilterIndex = 2;
            //设置对话框标题
            openDlg.Title = "请选择你要打开的图片";
            //记忆之前打开的文件夹
            openDlg.RestoreDirectory = true;
            //System.Windows.Forms;才有启用帮助按钮
            //openDlg.ShowHelp = true;
            //支持多选
            openDlg.Multiselect = true;
            if (DialogResult.OK == openDlg.ShowDialog())
            {             
                return openDlg.FileNames;
            }
            return null;
        }
        //按名称顺序排列
        public static void SortAsFileName(ref FileInfo[] arrFi)
        {
            Array.Sort(arrFi, delegate (FileInfo x, FileInfo y) { return x.Name.CompareTo(y.Name); });
        }
        //按名称倒序排列
        public static void SortRevserAsFileName(ref FileInfo[] arrFi)
        {
            Array.Sort(arrFi, delegate (FileInfo x, FileInfo y) { return y.Name.CompareTo(x.Name); });
        }
        //按创建时间顺序排列
        public static void SortAsFileCreationTime(ref FileInfo[] arrFi)
        {
            Array.Sort(arrFi, delegate (FileInfo x, FileInfo y) { return x.CreationTime.CompareTo(y.CreationTime); });
        }
        //按创建时间倒序排列
        public static void SortRevserAsFileCreationTime(ref FileInfo[] arrFi)
        {
            Array.Sort(arrFi, delegate (FileInfo x, FileInfo y) { return y.CreationTime.CompareTo(x.CreationTime); });
        }
        public static void LoadPictrueFiles(bool? isSortByTime,bool? isRevser,out FileInfo[] arrFi)
        {
            string[] res = LoadPictruesFilePath();
            FileInfo[] fileInfoes;
            
            if (res == null)
            {
                arrFi = null;
                return;
            }
            else
            {
                List<FileInfo> files = new List<FileInfo>();
                foreach (string s in res)
                {
                    if(File.Exists(s))
                        files.Add(new FileInfo(s));
                }
                fileInfoes = files.ToArray();
            }
            if (isSortByTime==true)
            {
                if (isRevser==true)
                    SortRevserAsFileCreationTime(ref fileInfoes);
                else
                    SortAsFileCreationTime(ref fileInfoes);
            }
            else
            {
                if (isRevser==true)
                    SortRevserAsFileName(ref fileInfoes);
                else
                    SortAsFileName(ref fileInfoes);
            }
            arrFi = fileInfoes;
        }
    }
}
