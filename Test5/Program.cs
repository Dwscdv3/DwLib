using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Numerics;
using System.Text;
using System.Threading;
using Dwscdv3.Timers;

namespace Test5 {
	class Program {
		static bool b = false;
		static bool add = true;
		static WinMMTimer t1 = new WinMMTimer(99);
		static WinMMTimer t2 = new WinMMTimer(1);
		static WinMMTimer t3 = new WinMMTimer(200);
		static void Main(string[] args) {
			//PerformanceCounter p = new PerformanceCounter("Processor", "% Processor Time", "_Total");
			Thread[] threads = new Thread[GetProcessorCount()];
			for (int i = 0; i < threads.Length; i++) {
				threads[i] = new Thread(Loop);
				threads[i].Start();
			}
			t1.Tick += On;
			t2.Tick += Off;
			t3.Tick += ChangeCPUUsage;
			t1.Start();
			t3.Start();
		}

		static int GetProcessorCount() {
			ManagementClass mc = new ManagementClass(new ManagementPath("Win32_Processor"));
			foreach (ManagementObject mo in mc.GetInstances()) {
				return (int)((uint)(mo.Properties["NumberOfLogicalProcessors"].Value));
			}
			return 1;
		}

		private static void Loop() {
			while (true) {
				while (b) { }
				Thread.Sleep(1);
			}
		}
		static void On(object sender, EventArgs e) {
			t1.Stop();
			b = true;
			t2.Start();
		}
		static void Off(object sender, EventArgs e) {
			t2.Stop();
			b = false;
			t1.Start();
		}
		static void ChangeCPUUsage(object sender, EventArgs e) {
			Console.Write("                    \r理论的CPU使用率: " + t2.Interval + "%");
			if (t2.Interval <= 1) {
				add = true;
			} else if (t2.Interval >= 99) {
				add = false;
			}
			if (add) {
				t1.Interval--;
				t2.Interval++;
			} else {
				t1.Interval++;
				t2.Interval--;
			}
		}
	}
}