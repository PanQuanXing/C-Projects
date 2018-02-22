using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyVector v1 = new MyVector(11.13,22.27,4.35);
            MyVector v2 = new MyVector(23,45,67);
            Console.WriteLine("\nIn IJK format:\nv1 is {0,30:IJK}\nv2 is {1,30:IJK}",v1,v2);
            Console.WriteLine("\nIn Default format:\nv1 is {0,30}\nv2 is {1,30}", v1, v2);
            Console.WriteLine("\nIn VE format:\nv1 is {0,30:VE}\nv2 is {1,30:VE}", v1, v2);
            Console.WriteLine("\nNorms are:\nv1 is {0,30:N}\nv2 is {1,30:N}", v1, v2);
            Console.ReadLine();
        }
    }
    public struct MyVector:IFormattable
    {
        public double x, y, z;
        //error:结构体没有不带参数的构造函数public MyVector() { }
        public MyVector(double x,double y,double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public double Norm()
        {
            return x * x + y * y + z * z;
        }
        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
                return this.ToString();
            string formatUpper = format.ToUpper();
            switch (format)
            {
                case "N":
                    return String.Format("||{0}||", this.Norm().ToString());
                case "VE":
                    return String.Format("({0:E},{1:E},{2:E})",x,y,z);//此处若是x、y、z.ToString()就不能输出科学计数法
                case "IJK":
                    return String.Format("{0}i+{1}j+{2}z",x.ToString(),y.ToString(),z.ToString());
                default:
                    return this.ToString();
            }
        }
    }
}
