using ProjectManager;
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
using System.Windows.Shapes;

namespace WpfApplicationTest
{
    /// <summary>
    /// WindowTreeViewTest2.xaml 的交互逻辑
    /// </summary>
    public partial class WindowTreeViewTest2 : Window
    {
        private HerbalAnalysisProject haPrj;
        public WindowTreeViewTest2()
        {
            haPrj = new HerbalAnalysisProject();
            List<TestingSample> nodes = new List<TestingSample>()
            {
                new TestingSample { PicturesPath=new System.IO.DirectoryInfo(@"C:\Users\潘全星\Documents\360截图")},
                new TestingSample {PicturesPath=new System.IO.DirectoryInfo(@"D:\matlabTest\1\1") },
                new TestingSample {PicturesPath=new System.IO.DirectoryInfo(@"D:\matlabTest\1\2") }
            };
            haPrj.TestsSamples = nodes;
            InitializeComponent();
            this.TreeView.ItemsSource = haPrj.TestsSamples;
        }
        private void item_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item.Header is TestingSample)
            {
                item.Items.Clear();
                DirectoryInfo subDir = (item.Header as TestingSample).PicturesPath;
                try
                {
                    foreach (FileInfo file in subDir.GetFiles())
                    {
                        TreeViewItem newItem = new TreeViewItem() { Tag = file, Header = file.ToString() };
                        item.Items.Add(newItem);
                    }
                }
                catch
                {
                }
            }
        }

        private void item_Collapsed(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            item.Items.Clear();
            item.Items.Add("*");
        }
    }
}
