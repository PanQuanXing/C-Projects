using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;

namespace AddInSideAdapter
{
    [AddInAdapter]
    public class ImageProcessorViewToContractAdapter:ContractBase,Contract.IImageProcessorContract
    {
        private AddInView.ImageProcessorAddInView view;
        public ImageProcessorViewToContractAdapter(AddInView.ImageProcessorAddInView v)
        {
            this.view = v;
        }
        public byte[] ProcessorImageBytes(byte[] pixels)
        {
            //throw new NotImplementedException();
            return view.ProcessImageBytes(pixels);
        }


        public void Initialize(Contract.IHostObjectContract hostObj)
        {
            //throw new NotImplementedException();
            view.Initialize(new HostObjectContractToViewAddInAdapter(hostObj));
        }
    }
    public class HostObjectContractToViewAddInAdapter : AddInView.HostObject
    {
        private Contract.IHostObjectContract contract;
        private ContractHandle handle;
        public HostObjectContractToViewAddInAdapter(Contract.IHostObjectContract c)
        {
            this.contract = c;
            this.handle = new ContractHandle(c);
        }
        public override void ReportProgress(int progressPercent)
        {
            //throw new NotImplementedException();
            contract.ReportProgress(progressPercent);
        }
    }
}
