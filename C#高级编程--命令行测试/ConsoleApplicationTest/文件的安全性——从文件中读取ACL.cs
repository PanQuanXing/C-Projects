using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ConsoleApplicationTest
{
    class Program
    {
        private static string myFilePath;
        static void Main(string[] args)
        {
            START:
            Console.Write("请输入文件的全路径：");
            myFilePath = Console.ReadLine();
            try
            {
                using (FileStream myFile = new FileStream(myFilePath, FileMode.Open, FileAccess.Read))
                {
                    FileSecurity fileSec = myFile.GetAccessControl();//获取文件的ACL
                    foreach (FileSystemAccessRule fileRule in fileSec.GetAccessRules(true, true, typeof(NTAccount)))
                    {
                        Console.WriteLine(String.Format("{0} {1} {2} 访问 {3}",
                            myFilePath,
                            fileRule.AccessControlType==AccessControlType.Allow?"提供":"拒绝",
                            fileRule.FileSystemRights,
                            fileRule.IdentityReference));
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("错误的文件路径！");
                Console.WriteLine(e.Message);
                goto START;
            }
            Console.ReadLine();
        }
    }
}
