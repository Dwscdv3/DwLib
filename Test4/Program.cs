using System;
using System.Timers;

namespace Test4 {
	class Program {
		//static WinMMTimer t = new WinMMTimer(1000);
		static Timer t = new Timer(1000);
		static uint a = 0;
		static bool b = true;

		static void Main(string[] args) {
			//t.Tick += t_Tick;
			t.Elapsed += t_Tick;
			t.Start();
			while (true) {
				while (b) {
					a++;
				}
				Console.WriteLine(a);
				a = 0;
				b = true;
			}
		}

		static void t_Tick(object sender, EventArgs e) {
			b = false;
		}
	}
}
