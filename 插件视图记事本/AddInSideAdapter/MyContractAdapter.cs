using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;

namespace AddInSideAdapter
{
    [AddInAdapter]
    public class MyContractAdapter : ContractBase, Contracts.MyContract
    {
        private AddInView.MyAddInView view;

        public MyContractAdapter(AddInView.MyAddInView view)
        {
            this.view = view;
        }

        public string MyUpperAddIn(string str)
        {
            return view.MyUpperAddIn(str);
        }
    }
}

