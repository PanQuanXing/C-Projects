using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class TestingSample:INotifyPropertyChanged
    {
        private DirectoryInfo picturesPath;
        private int startWL;
        private int spaceWL;
        public DirectoryInfo PicturesPath
        {
            get { return picturesPath; }
            set
            {
                picturesPath = value;
                OnPropertyChanged("PicturesPath");
            }
        }
        public int StartWL
        {
            get { return startWL; }
            set { startWL = value; }
        }
        public int SpaceWL
        {
            get { return spaceWL; }
            set { spaceWL = value; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
