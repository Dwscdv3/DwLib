using System;
using System.Diagnostics;
using Dwscdv3.Random;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Double:\t" + ((double)ulong.MaxValue).ToString("f0"));
            //Console.WriteLine("UInt64:\t" + ulong.MaxValue);
            Console.WriteLine("测试 - 不重复随机数");
            Console.Write("请输入下界和上界（有符号32位整数），用半角逗号隔开：");
            string[] input = Console.ReadLine().Split(',');
            Stopwatch s = new Stopwatch();
            s.Start();
            NoRepeatRandom r = new NoRepeatRandom(int.Parse(input[0]), int.Parse(input[1]));
            for (int i = 0; i < r.Total; i++)
            {
                Console.Write(r.Next() + " ");
            }
            s.Stop();
            Console.WriteLine(string.Format("\r\n数量：{0}个，总用时：{1}ms，平均用时{2}ms", r.Total, s.ElapsedMilliseconds, s.ElapsedMilliseconds / r.Total));
        }
    }
}
