using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;
using System.AddIn.Contract;

namespace Contracts
{
    [AddInContract]
    public interface MyContract : IContract
    {
        string MyUpperAddIn(string str);
    }
}
