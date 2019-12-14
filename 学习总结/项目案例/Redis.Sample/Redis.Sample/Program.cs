using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var redisUtil = new RedisUtils.RedisUtil();

            var key = Guid.NewGuid().ToString();

            //var s = redisUtil.StringSet(key, "123");
            //Console.WriteLine(s ? "OK" : "No");

            var token = Environment.MachineName;
            var s = redisUtil.LockTake(key, token, TimeSpan.FromMinutes(1));
            if (s)
            {
                try
                {
                    Console.WriteLine("Working..........");
                }
                finally
                {
                    redisUtil.LockRelease(key, token);
                }
            }

            Console.WriteLine("End....");
            Console.ReadKey();
        }
    }
}
