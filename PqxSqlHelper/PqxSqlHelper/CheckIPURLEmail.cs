using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PqxSqlHelper
{
    class CheckIPURLEmail
    {
        //@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$"
        /*
         IP中每个十进制数，“|”号划分了5种情况：①0~9   ②10~99   ③100~199   ④200~249   ⑤250~255
         {n}表示恰好匹配了n次。  \.){3}表示每个十进制数加'.'重复三次。最后加上最后一个十进制数即完成对IP的匹配。

            当然这是较简单的写法，也可以简写成：
                @"^(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})(1\d{2})|(2[0-4]\d)|(25[0-5]))$ " 
                任意数字重复1-2次，及0~99 ，其他均是对第一个表达式进行了缩写。C#中务必加上首尾的^和$，否则上述方法匹配-1.1.1.1这样的IP也会返回True。
         */
        public static bool ValidateIPAddress(string ipAddress)
        {
            Regex validipregex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
            //string.Trim()方法去掉字符串前后面的空白字符
            return (ipAddress != "" && validipregex.IsMatch(ipAddress.Trim())) ? true : false;
        }


        //匹配URL的正则分析
        /*
         关于该正则表达式的说明：
        ①：该正则表达式匹配的字符串必须以http://、https://、ftp://开头；
        ②：该正则表达式能匹配URL或者IP地址；（如：http://www.baidu.com 或者 http://192.168.1.1）
        ③：该正则表达式能匹配到URL的末尾，即能匹配到子URL；（如能匹配：http://www.baidu.com/s?wd=a&rsv_spt=1&issp=1&rsv_bp=0&ie=utf-8&tn=baiduhome_pg&inputT=1236）
        ④：该正则表达式能够匹配端口号；(URL部分转载自点击打开链接)
         */
        public static bool ValidateURL(string ipAddress)
        {
            Regex validipregex = new Regex(@"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)? ");
            //string.Trim()方法去掉字符串前后面的空白字符
            return (ipAddress != "" && validipregex.IsMatch(ipAddress.Trim())) ? true : false;
        }


        //匹配邮箱的正则分析：
        //\w匹配任何字母或数字  \W匹配除字母和数字外任何字符   \S匹配任何非空字符（除空格，换行，制表符等） 
        //*表示匹配0或多次   +表示1或多次    .匹配任何除了\n以外的字符    ?匹配0次或1次
        public static bool ValidateEmail(string ipAddress)
        {
            Regex validipregex = new Regex(@"(\w+\.) * \w+@(\w+\.)+[A-Za-z]+");
            //string.Trim()方法去掉字符串前后面的空白字符
            return (ipAddress != "" && validipregex.IsMatch(ipAddress.Trim())) ? true : false;
        }
    }
}
