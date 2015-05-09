using System;
using Dwscdv3.Console;
using Dwscdv3.ExtensionMethod;
using Dwscdv3.Timers;

namespace Test6 {
	class Program {
		static ProgressBar p = new ProgressBar(0, 10000, true, false);
		static WinMMTimer t = new WinMMTimer(33);
		static int flag = 0;
		static string[] post = {
									"Turning to Zhuangbi mode...",
									"Failed. Turning to Doubi mode...",
									"Failed. Turning to 2B mode...",
									"Failed. Turning to SB mode..."
							   };
		//static Random r = new Random();
		static void Main(string[] args) {
			Console.CursorVisible = false;
			Console.CursorTop = 24;
			Console.Write("Do you want to turning to NB mode? (y/n): ");
			while (true) {
				ConsoleKeyInfo cki = Console.ReadKey(true);
				if (cki.KeyChar == 'y' || cki.KeyChar == 'Y') {
					for (int i = post.Length - 1; i > 0; i--) {
						post[i] = post[i - 1];
					}
					post[0] = "Turning to NB mode...";
					post[1] = "Failed. Turning to Zhuangbi mode...";
					break;
				} else if (cki.KeyChar == 'n' || cki.KeyChar == 'N') {
					break;
				}
			}
			p.Width = 25;
			p.PreName = "";
			p.PreNameWidth = 0;
			p.PostName = post[flag];
			p.PercentPrecision = 2;
			p.Line = 24;
			p.AutoRender = true;
			t.Tick += t_Tick;
			t.Start();
			Console.ReadKey(true);
		}

		static void t_Tick(object sender, EventArgs e) {
			if (p.Value + 1 > p.Maximum) {
				p.Value = 0;
				flag++;
				if (flag >= post.Length) {
					p.AutoRender = false;
					p.ClearLine();
					Console.Write("Successfully turned to SB mode. ");
				} else {
					p.PostName = post[flag];
				}
			} else {
				p.Value++;
			}
			//p.PostName = Math.Round(r.NextDouble() * 128 + 384, 2).ToString() + " KB/s";
		}
	}
}
