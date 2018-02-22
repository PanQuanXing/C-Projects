using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = @"int .NET 4.5 is not installed. AutoCAD Electrical 2016 cannot be installed without this component. See your system administrator for more information. ----pin in ping";
            string pattern = @"\bin[a-z]*";
            FindTest(ref text,ref pattern);
            Console.WriteLine();
            string text2 = @"http:// what was it http://wwww.wrox.com --oh yes http://www.wrox.com http://baidu.com:8080";
            string pattern2 = @"\b(\S+)://([^:]+)(?::(\S+))?\b";
            FindURI(ref text2,ref pattern2);
            Console.ReadLine();
        }
        static void FindTest(ref string text,ref string pattern)
        {
            MatchCollection matches = Regex.Matches(text,pattern,RegexOptions.IgnoreCase);
            PrintMatches(ref text,ref matches);
        }
        static void PrintMatches(ref string text,ref MatchCollection matches)
        {
            Console.WriteLine("原始字符串是：\n\n"+text+"\n");
            Console.WriteLine("匹配的字符串一共有"+matches.Count.ToString()+"个：");

            foreach(Match nextMatch in matches)
            {
                
                int index = nextMatch.Index;
                string result = nextMatch.ToString();
                int charsBefore = index < 5 ? index : 5;
                int formEnd = text.Length - index - result.Length;
                int charsAfter = formEnd < 5 ? formEnd : 5;
                int charForDisplay = charsBefore + charsAfter + result.Length;

                Console.WriteLine("字符串的位置：{0}，\t字符串是：{1}\t，\t{2}",index,result,text.Substring(index-charsBefore,charForDisplay));
                 
                //Console.WriteLine(nextMatch.ToString());
            }
        }
        //经常使用的匹配URI(<protocol>://<address>:<port>)
        static void FindURI(ref string text,ref string pattern)
        {
            MatchCollection matches = Regex.Matches(text,pattern,RegexOptions.IgnoreCase);
            foreach(Match nextMatch in matches)
            {
                Console.WriteLine("Index:" + nextMatch.Index.ToString() + "\tString:" + nextMatch.ToString());
            }
        }
    }
}
