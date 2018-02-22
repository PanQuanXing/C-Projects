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
        private static string mentionedDir;
        static void Main(string[] args)
        {
        START:
            Console.Write("请输入文件夹的全路径：");
            mentionedDir = Console.ReadLine();
            try
            {
                DirectoryInfo myDir = new DirectoryInfo(mentionedDir);
                if(myDir.Exists)
                {
                    DirectorySecurity dirSec = myDir.GetAccessControl();
                    foreach (FileSystemAccessRule fileRule in dirSec.GetAccessRules(true, true, typeof(NTAccount)))
                    {
                        Console.WriteLine(String.Format("{0} {1} {2} 访问 {3}",
                            mentionedDir,
                            fileRule.AccessControlType == AccessControlType.Allow ? "提供" : "拒绝",
                            fileRule.FileSystemRights,
                            fileRule.IdentityReference));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("错误的文件夹路径！");
                Console.WriteLine(e.Message);
                goto START;
            }
            Console.ReadLine();
        }
    }
}
