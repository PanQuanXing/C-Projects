using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;

namespace HostSideAdapter
{
    [HostAdapter]
    public class ImageProcessorContractToViewHostAdapter:HostView.ImageProcessorHostView
    {
        private Contract.IImageProcessorContract contract;
        private ContractHandle contractHandler;
        public ImageProcessorContractToViewHostAdapter(Contract.IImageProcessorContract c)
        {
            this.contract = c;
            contractHandler = new ContractHandle(c);
        }
        public override byte[] ProcessImageBytes(byte[] pixels)
        {
            //throw new NotImplementedException();
            return contract.ProcessorImageBytes(pixels);
        }

        public override void Initialize(HostView.HostObject host)
        {
            //throw new NotImplementedException();
            HostObjectViewToContractHostAdapter hostAdapter = new HostObjectViewToContractHostAdapter(host);
            contract.Initialize(hostAdapter);
        }
    }
    public class HostObjectViewToContractHostAdapter : ContractBase, Contract.IHostObjectContract
    {
        private HostView.HostObject view;
        public HostObjectViewToContractHostAdapter(HostView.HostObject v)
        {
            this.view = v;
        }
        public void ReportProgress(int progressPercent)
        {
            view.ReportProgress(progressPercent);
        }
    }
}
