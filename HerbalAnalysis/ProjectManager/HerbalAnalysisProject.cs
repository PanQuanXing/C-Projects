using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ProjectManager
{
    public class HerbalAnalysisProject:INotifyPropertyChanged
    {
        private string name;
        private string projectFolderPath;
        private List<TestingSample> testsSamples;
        private byte analysisStep=0;
        public List<TestingSample> TestsSamples
        {
            get { return testsSamples; }
            set { testsSamples = value; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public byte AnalysisStep { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                if (name!=value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string ProjectFolderPath
        {
            get { return projectFolderPath; }
            set
            {
                if (projectFolderPath != value)
                {
                    projectFolderPath = value;
                    OnPropertyChanged("ProjectFolderPath");
                }
                    
            }
        }
        public string GetActualProjFolderPath()
        {
            return projectFolderPath + "\\".ToString() + name;
        }
        public string GetMainProjFilePath()
        {
            return projectFolderPath + "\\".ToString() + name + "\\".ToString() + name + ".pqxProj";
        }
        public bool IsProjMainFolderPathExist()
        {
            return Directory.Exists(GetActualProjFolderPath());
        }
        //封装PropertyChangeEventArgs事件
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public bool IsProjectNameCorrect(string n)
        {
            if (n != null&&n!="")
            {
                Regex reg = new Regex(@"[\\/:\*\?""<>|]");
                //bool bbb= reg.Match(n).Success;
                return !reg.Match(n).Success;
            }
            return false;
        }
        public bool IsProjectNameCorrect()
        {
            return IsProjectNameCorrect(name);
        }
        //用于判断项目的主文件夹所在的路径是否存在
        public bool IsProjectFolderPathExist(string pfp)
        {
            if (pfp != null&&pfp!="")
            {
                return Directory.Exists(pfp);
            }
            return false;
        }
        public bool IsProjectFolderPathExist()
        {
            return IsProjectFolderPathExist(projectFolderPath);
        }
    }
}
