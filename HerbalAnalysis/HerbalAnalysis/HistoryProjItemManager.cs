using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HerbalAnalysis
{
    class HistoryProjItemManager
    {
        static string hp="HistoryProj";
        public static void  MovePoint()
        {
            
            Properties.Settings.Default.HistoryProjPoint = IncrementPoint();
            Properties.Settings.Default.Save();//保存一下
        }
        public static int IncrementPoint()
        {
            int point = Properties.Settings.Default.HistoryProjPoint;
            point += 1;
            if (point > 9)
                point = 0;
            return point;
        }
        public static void AddHistoryItem(string s)
        {
            int point = IncrementPoint();
            Properties.Settings.Default.HistoryProjPoint = point;
            Properties.Settings.Default[hp + point.ToString()] = s;
            Properties.Settings.Default.Save();
        }
        public static Queue GetHistoryItems()
        {
            string tempStr;
            int point = IncrementPoint() - 1,temp;
            Queue itemQueue = new Queue();
            for (int i = 0; i < 10; ++i)
            {
                temp = point - i;
                if (temp < 0)
                    temp += 10;
                tempStr = (string)Properties.Settings.Default[hp + temp.ToString()];
                if (tempStr != "")
                    itemQueue.Enqueue(tempStr);
            }
            return itemQueue;
        }
        public static void RemoveItem(string s)
        {
            for (int i=0;i<10;++i)
            {
                if (Properties.Settings.Default[hp + i.ToString()] as string == s)
                {
                    Properties.Settings.Default[hp + i.ToString()] = "";
                    Properties.Settings.Default.Save();
                }
            }
        }
    }
}
