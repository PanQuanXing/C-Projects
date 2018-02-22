using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var dealer = new NameDealer();
            var xiaoming = new Consumer("XiaoMing");
            WeakNameInfoEventManager.AddListener(dealer,xiaoming);

            dealer.NewName("XiaoHua");

            var xiaohong = new Consumer("XiaoHong");
            WeakNameInfoEventManager.AddListener(dealer,xiaohong);

            dealer.NewName("XiaoXingXing");

            WeakNameInfoEventManager.RemoveListener(dealer,xiaoming);

            dealer.NewName("Red Bull");

            Console.ReadLine();
        }
    }
    public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs;
    public class NameInfoEventArgs : EventArgs
    {
        public string Name { get; private set; }
        public NameInfoEventArgs(string name)
        {
            this.Name = name;
        }
    }
    public class NameDealer
    {
        private event EventHandler<NameInfoEventArgs> newNameInfo;
        public event EventHandler<NameInfoEventArgs> NewNameInfo
        {
            add { newNameInfo += value; }
            remove { newNameInfo -= value; }
        }
        public void NewName(string name)
        {
            Console.WriteLine(String.Format("NameDealer,new name {0}.",name));
            RaiseNewNameInfo(name);
        }
        protected virtual void RaiseNewNameInfo(string name)
        {
            EventHandler<NameInfoEventArgs> newNameInfoCopy = newNameInfo;
            if (newNameInfoCopy != null)
            {
                newNameInfoCopy(this,new NameInfoEventArgs(name));
            }
        }
    }
    //定义一个弱事件管理类
    //WeakEventManager在程序集WindowsBase的名称空间System.Windows中定义
    public class WeakNameInfoEventManager : WeakEventManager
    {
        public static WeakNameInfoEventManager CurrentManager
        {
            get
            {
                var manager = WeakEventManager.GetCurrentManager(typeof(WeakNameInfoEventManager)) as WeakNameInfoEventManager;
                if (manager == null)
                {
                    manager = new WeakNameInfoEventManager();
                    WeakEventManager.SetCurrentManager(typeof(WeakNameInfoEventManager), manager);
                }
                return manager;
            }
        }
        public static void AddListener(object sender,IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(sender,listener);
        }
        public static void RemoveListener(object sender,IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(sender,listener);
        }
        protected void NameDealer_NewNameInfo(object sender,NameInfoEventArgs e)
        {
            DeliverEvent(sender,e);
        }
        protected override void StartListening(object source)
        {
            (source as NameDealer).NewNameInfo += NameDealer_NewNameInfo;
        }
        protected override void StopListening(object source)
        {
            (source as NameDealer).NewNameInfo -= NameDealer_NewNameInfo;
        }
    }
    //定义弱事件侦听器类：
    //弱事件侦听器需要为实现IWeakEventListener接口。这个接口定义了ReceiveWeakEvent(Type,object)方法，触发事件时，从弱事件管理器中调用这个方法。
    public class Consumer : IWeakEventListener
    {
        private string name;
        public Consumer() { }
        public Consumer(string name)
        {
            this.name = name;
        }
        public void NewNameIsHere(object sender, NameInfoEventArgs e)
        {
            Console.WriteLine(String.Format("{0}:name {1} is new!",name,e.Name));
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            NewNameIsHere(sender,e as NameInfoEventArgs);
            return true;
        }
    }
}
