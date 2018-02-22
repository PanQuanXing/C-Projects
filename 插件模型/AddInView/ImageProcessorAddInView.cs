using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;
//第二步定义插件视图
namespace AddInView
{
    //插件视图用于提供镜像协定程序集的抽象类，并且被用于插件一方。
    [AddInBase]
    public abstract class ImageProcessorAddInView
    {
        public abstract byte[] ProcessImageBytes(byte[] pixels);
        public abstract void Initialize(HostObject hostObj);
    }
    public abstract class HostObject
    {
        public abstract void ReportProgress(int progressPercent);
    }
}