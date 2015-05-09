using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Dwscdv3.DataType;
using Dwscdv3.ExtensionMethod;
using Dwscdv3.Timers;

namespace Test3
{
    class Program {
		static int i = 0;
		static WinMMTimer timer = new WinMMTimer(1, 1, false);
		static Stopwatch stopwatch = new Stopwatch();

        static void Main(string[] args)
        {
			timer.Tick += Timer_Tick;
			stopwatch.Start();
			timer.Start();
			//让Main方法进入死循环以确保程序不会退出。
			while (true) {
				Thread.Sleep(0);
			}
        }

		static void Timer_Tick(object sender, EventArgs e) {
			if (i++ >= 10) {
				stopwatch.Stop();
				Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");
				i = 0;
			}
		}
    }
}
