using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
//第一步定义协定
namespace Contract
{
    //插件管线的起点--插件接口，创建协定程序集
    [AddInContract]//使用AddInContract特性修饰协定
    public interface IImageProcessorContract:IContract
    {
        byte[] ProcessorImageBytes(byte[] pixels);//接收图像字节数组，进行处理后，返回图像字节数组
        void Initialize(IHostObjectContract hostObj);
    }
    //与宿主进行交互--宿主接口
    //与插件接口一样，宿主接口必须继承IContract接口。与插件接口不同，不用AddInContract修饰,因为它不是由插件实现
    public interface IHostObjectContract : IContract
    {
        void ReportProgress(int progressPercent);
    }
}
