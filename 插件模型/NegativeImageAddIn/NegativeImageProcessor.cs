using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn;

namespace NegativeImageAddIn
{
    [AddIn("图像负片插件",
        Version="1.0.0.0",
        Publisher="PanQuanXing-08-02-2016",
        Description="将图片的颜色反向，形成负片效果。")]
    public class NegativeImageProcessor:AddInView.ImageProcessorAddInView
    {
        private AddInView.HostObject host;
        public override byte[] ProcessImageBytes(byte[] pixels)
        {
            //throw new NotImplementedException();
            int iteration = pixels.Length / 100;
            for (int i = 0; i < pixels.Length - 2;i++ )
            {
                pixels[i]=(byte)(255-pixels[i]);//R
                pixels[i+1] = (byte)(255 - pixels[i+1]);//G
                pixels[i+2] = (byte)(255 - pixels[i+2]);//B
                if (i % iteration == 0)
                    host.ReportProgress(i/iteration);
            }
            return pixels;
        }

        public override void Initialize(AddInView.HostObject hostObj)
        {
            //throw new NotImplementedException();
            this.host = hostObj;
        }
    }
}
