using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;

namespace HostSideAdapter
{
    [HostAdapter]
    public class MyHostAdapter : HostView.MyHostView
    {
        private Contracts.MyContract contract;
        private ContractHandle contractHandle;

        public MyHostAdapter(Contracts.MyContract contract)
        {
            this.contract = contract;
            contractHandle = new ContractHandle(contract);
        }

        public override string MyUpperAddIn(string str)
        {
            return contract.MyUpperAddIn(str);
        }
    }
}
