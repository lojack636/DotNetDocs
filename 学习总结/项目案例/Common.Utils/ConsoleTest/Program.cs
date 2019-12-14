using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Pseron>();
            var model = new Pseron
            {
                Id = 1,
                Name = "Aaron",
                
            };
            list.Add(model);
            var str = list.ObjectToJsonString();
            var str1 = str.JsonStringToObject<List<Pseron>>();
            Console.WriteLine(str);
        }
    }
    public class Pseron
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
    }
}
