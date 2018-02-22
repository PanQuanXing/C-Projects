using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostView
{
    //宿主视图是一个紧密镜像协定接口的抽象类
    public abstract class ImageProcessorHostView
    {
        public abstract byte[] ProcessImageBytes(byte[] pixels);
        public abstract void Initialize(HostObject host);
    }
    public abstract class HostObject
    {
        public abstract void ReportProgress(int progressPercent);
    }
}
