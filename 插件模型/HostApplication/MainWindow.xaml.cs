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
using Microsoft.Win32;
using System.Drawing;
using System.AddIn;
using HostView;
using System.AddIn.Hosting;
using System.Windows.Threading;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;

namespace HostApplication
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string curFileName;
        private AutomationHost automationHost;
        private HostView.ImageProcessorHostView addin;         

        public MainWindow()
        {
            InitializeComponent();
        }
        //窗体加载时，查找（发现）可用的插件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string path = Environment.CurrentDirectory;
            AddInStore.Update(path);
            IList<AddInToken> tokens = AddInStore.FindAddIns(typeof(HostView.ImageProcessorHostView),path);
            addInList.ItemsSource = tokens;
           
            automationHost = new AutomationHost(myProgressBar);
        }
        //BitmapSource转Bitmap方法一：
        public static Bitmap bitmapSourceToBitmap(BitmapSource bitmapSource)
        {
            Bitmap bitmap;
            using(MemoryStream outStream =new MemoryStream()){
                //BitmapEncoder bitmapEncoder = new BmpBitmapEncoder();//用这个,到保存这一步有error
                //BitmapEncoder bitmapEncoder = new JpegBitmapDecoder();//参数不知道
                BitmapEncoder bitmapEncoder = new PngBitmapEncoder();
                bitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                bitmapEncoder.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }
        //BitmapSource转Bitmap方法二：
        public static void bitmapSourceToBitmap(BitmapSource bitmapSource,out Bitmap bitmap)
        {
            //将转换的image格式化
            FormatConvertedBitmap formatSrc = new FormatConvertedBitmap();
            formatSrc.BeginInit();
            formatSrc.Source = bitmapSource;
            formatSrc.DestinationFormat = System.Windows.Media.PixelFormats.Bgra32;
            formatSrc.EndInit();
            //复制到Bitmap中去
            bitmap = new Bitmap(formatSrc.PixelWidth,formatSrc.PixelHeight,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(System.Drawing.Point.Empty,bitmap.Size),System.Drawing.Imaging.ImageLockMode.WriteOnly,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            formatSrc.CopyPixels(System.Windows.Int32Rect.Empty,data.Scan0,data.Height*data.Stride,data.Stride);
            bitmap.UnlockBits(data);
        }
        //注意：BitmapSource继承自ImageSource
        //从bitmap转ImageSource
        public static ImageSource bitmapToImageSource(Bitmap bitmap)
        {
            IntPtr bmap = bitmap.GetHbitmap();
            ImageSource imgs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            return imgs;
        }
        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            //创建打开文件对话框
            OpenFileDialog openDlg = new OpenFileDialog();
            //设置文件过滤器
            openDlg.Filter = "所以文件|*.*;|所有图像文件|*.bmp;*pcx;*.png;*jpg;*.gif;" +
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
            if (true == openDlg.ShowDialog())
            {
                //读取当前选择的文件名
                curFileName = openDlg.FileName;
                //使用Image.FromFile创建图像对象
                try
                {
                    //curBitmap = (Bitmap)System.Drawing.Image.FromFile(curFileName);
                    this.myImage.Source = new BitmapImage(new Uri(curFileName,UriKind.Absolute));
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
               // this.myImage.Source = MainWindow.bitmapToImageSource(curBitmap);
            }
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (null == myImage.Source)
            {
                return;
            }
            /*
            BitmapImage bimg = (myImage.Source) as BitmapImage; error:BitmapSource to BitmapImage ，BitmapImage会为null。
            Stream imgStr = bimg.StreamSource;
            Bitmap curBitmap = new Bitmap(imgStr);
            */
            //使用方法一：
            //Bitmap curBitmap = MainWindow.bitmapSourceToBitmap(myImage.Source as BitmapSource);
            //使用方法二：
            Bitmap curBitmap;
            MainWindow.bitmapSourceToBitmap(myImage.Source as BitmapSource,out curBitmap);

            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "BMP文件(*.bmp)|*.bmp|Gif文件(*.gif)|*.gif|JPEG文件(*.jpg)|*.jpg|PNG文件(*.png)|*.png|图标文件(*.ico)|*.ico";
            if (true == saveDlg.ShowDialog())
            {
                string fileName = saveDlg.FileName;
                string fileExtensionName = fileName.Substring(fileName.LastIndexOf(".") + 1);
                switch (fileExtensionName)
                {
                    case "bmp":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case "ico":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Icon);
                        break;
                    default:
                        break;
                }
            }
        }
        private class AutomationHost : HostView.HostObject
        {
            private ProgressBar progressBar;
            public AutomationHost(ProgressBar p)
            {
                this.progressBar = p;
            }


            public override void ReportProgress(int progressPercent)
            {
                //throw new NotImplementedException();
                progressBar.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart)delegate()
                    {
                        progressBar.Value = progressPercent;
                    });
            }
        }
        private void addInList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmdProcessorImage.IsEnabled = addInList.SelectedIndex != -1;
        }

        private void cmdProcessorImage_Click(object sender, RoutedEventArgs e)
        {
            BitmapSource source = (BitmapSource)myImage.Source;
            if (source != null)
            {
                int stride = source.PixelWidth * source.Format.BitsPerPixel / 8;
                stride += (stride % 4) * 4;
                int arrySize = stride * source.PixelHeight * source.Format.BitsPerPixel / 8;
                byte[] originalPixels = new byte[arrySize];
                source.CopyPixels(originalPixels, stride, 0);

                AddInToken token = (AddInToken)addInList.SelectedItem;
                addin = token.Activate<HostView.ImageProcessorHostView>(AddInSecurityLevel.Internet);
                addin.Initialize(automationHost);
                //写法一：(ThreadStart)delegate(){}
                //Thread thread = new Thread(()=>{
                //    byte[] changedPixels = addin.ProcessImageBytes(originalPixels);

                //    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                //        (ThreadStart)delegate()
                //    {
                //        BitmapSource newSource = BitmapSource.Create(source.PixelWidth, source.PixelHeight, source.DpiX, source.DpiY, source.Format, source.Palette, changedPixels, stride);
                //        myImage.Source = newSource;
                //        myProgressBar.Value = 0;

                //        addin = null;
                //    });
                //});


                //写法二：使用lamda表达式  (ThreadStart)(()=>{})
                Thread thread = new Thread(() => {
                    byte[] changedPixels = addin.ProcessImageBytes(originalPixels);

                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        (ThreadStart)(()=>
                        {
                            BitmapSource newSource = BitmapSource.Create(source.PixelWidth, source.PixelHeight, source.DpiX, source.DpiY, source.Format, source.Palette, changedPixels, stride);
                            myImage.Source = newSource;
                            myProgressBar.Value = 0;

                            addin = null;
                        }));
                });
                thread.Start();
            }
        }
    }
}
