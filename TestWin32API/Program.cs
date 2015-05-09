using System;
using System.Timers;
using Dwscdv3.Win32API;

namespace TestWin32API {
	class Program {
		static Timer t = new Timer(10);
		static User32.RECT rect;

		static void Main(string[] args) {
			rect = new User32.RECT {
				top = 0,
				left = 0,
				right = 1,
				bottom = 1
			};

			t.Elapsed += t_Elapsed;
			t.Start();

			Console.ReadKey(true);
		}

		static void t_Elapsed(object sender, ElapsedEventArgs e) {
			User32.Cursor.ClipCursor(ref rect);
		}
	}
}
