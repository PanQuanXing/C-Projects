using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn;
using System.Text.RegularExpressions;

namespace FadeImageAddIn
{
    [AddIn("字母大写", Version = "1.0.0.0", Publisher = "潘全星",Description = "Upper case all letters.")]
    public class MyUpperProcessor : AddInView.MyAddInView
    {
        public override string MyUpperAddIn(string str)
        {
            //throw new NotImplementedException();
            StringBuilder strResult = new StringBuilder("");
            char[] chs = str.ToCharArray();
            Regex r = new Regex("[a-z]");
            foreach (char s in chs)
            {
                if (r.IsMatch(s.ToString()))
                {
                    strResult.Append(s.ToString().ToUpper());
                }
                else
                {
                    strResult.Append(s.ToString());
                }
            }
            return strResult.ToString();
        }
    }
}