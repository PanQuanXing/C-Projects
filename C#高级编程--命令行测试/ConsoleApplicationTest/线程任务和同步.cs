using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ParallelLoopForTest();
            TasksUsingThreadPool();
            Console.ReadLine();
        }
        //用Parallel.For()方法循环
        static void ParallelLoopForTest()
        {
            ParallelLoopResult result = Parallel.For(0, 9, i =>
            {
                Console.WriteLine(String.Format("（{0}）任务编号：{1}，线程编号：{2}。", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId));
                Thread.Sleep(10);
            });
            Console.WriteLine("是否完成：{0}。", result.IsCompleted);
        }
        //1.使用线程池的任务
        static object tastMethodLock = new object();
        static void TaskMethod(object title)
        {
            lock (tastMethodLock)
            {
                Console.WriteLine(title);

                Console.WriteLine("任务ID：{0}，线程：{1}", Task.CurrentId == null ? "没有任务" : Task.CurrentId.ToString(), Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("是否是线程池的任务：{0}", Thread.CurrentThread.IsThreadPoolThread);
                Console.WriteLine("是否是在后台运行的线程：{0}", Thread.CurrentThread.IsBackground);
                Console.WriteLine();
            }
        }
        static void TasksUsingThreadPool()
        {
            TaskFactory tf=new TaskFactory();
            Task t1 = tf.StartNew(TaskMethod,"使用线程工厂！");

            Task t2 = Task.Factory.StartNew(TaskMethod,"生产一个任务！");

            Task t3 = new Task(TaskMethod,"Using a task constructor and start!");
            t3.Start();

            //Task t4 = Task.Run(()=>TaskMethod("Using the Run Method!"));
        }
    }
}
