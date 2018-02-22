using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddInView;
using System.AddIn;
//第三步写插件
namespace FadeImageAddIn
{
    //[AddIn(插件名称,版本属性,Publisher发布者属性,插件说明属性)]
    [AddIn(
        "Fade Image Processor",
        Version="1.0.0.0",
        Publisher="PanQuanXing-06-02-2016",
        Description="Darkens the Picture"
        )]
    public class FadeImageProcessor:ImageProcessorAddInView
    {
        private AddInView.HostObject host;
        public override byte[] ProcessImageBytes(byte[] pixels)
        {
            int iteration = pixels.Length / 100;
            //throw new NotImplementedException();
            //此插件使用一个粗略的算法通过删除随机选择的像素的部分颜色使得图片部分区域变暗
            Random rand = new Random();
            int offset = rand.Next(0,10);
            int count=pixels.Length - 1 - offset;
            for (int i = 0; i <count ;i++ )
            {
                if ((i + offset) % 5 == 0)
                    pixels[i] = 0;
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
