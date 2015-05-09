using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Dwscdv3.Timers;

namespace WinFormTest1 {
	public partial class Form1 : Form {
		WinMMTimer t1 = new WinMMTimer(10, 1, false);
		WinMMTimer t2 = new WinMMTimer(10, 1, false);
		//System.Timers.Timer timer = new System.Timers.Timer(1);
		Stopwatch stopwatch = new Stopwatch();

		public Form1() {
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;
			t1.Tick += t1_Tick;
			t2.Tick += t2_Tick;
			stopwatch.Start();
			t1.Start();
			t2.Start();
		}
		void t1_Tick(object sender, EventArgs e) {
			Thread t = new Thread(a);
			t.Start();
		}
		void t2_Tick(object sender, EventArgs e) {
			Thread t = new Thread(b);
			t.Start();
		}
		void a() {
			textBox1.Text += "0";
		}
		void b() {
			textBox1.Text += "1";
		}
	}
}
