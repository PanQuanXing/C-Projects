using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;

namespace AddInView
{
    [AddInBase]
    public abstract class MyAddInView
    {
        public abstract string MyUpperAddIn(string str);
    }
}
