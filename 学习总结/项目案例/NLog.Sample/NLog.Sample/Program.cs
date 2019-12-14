using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NLog.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start......");

            //Logger.Write("测试内容......");
            for (int i = 0; i < 50; i++)
            {
                Thread.Sleep(1000);
                Logger.Write("当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            Console.WriteLine("End......");
            Console.ReadKey();
        }
    }
}
