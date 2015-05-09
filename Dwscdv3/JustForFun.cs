using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;

namespace Dwscdv3 {
	public class JustForFun {
		/// <summary>
		/// 使当前线程进入死循环。
		/// </summary>
		public static void InfinityLoop() {
			while (true) { }
		}
		/// <summary>
		/// 使CPU使用率达到100%。
		/// </summary>
		public static void CPUKiller() {
			Thread[] threads = new Thread[GetProcessorCount()];
			for (int i = 0; i < threads.Length; i++) {
				threads[i] = new Thread(InfinityLoop);
				threads[i].Start();
			}
		}
		public static void CPUUsage(float f) {
			Thread[] threads = new Thread[GetProcessorCount()];
			for (int i = 0; i < threads.Length; i++) {
				threads[i] = new Thread(SingleThreadCPUUsage);
				threads[i].Start(f);
			}
		}
		public static void SingleThreadCPUUsage(object arg) {
			PerformanceCounter p = new PerformanceCounter("Processor", "% Processor Time", "_Total");
			float f = (float)arg;
			while (true) {
				if (p.NextValue() > f) {
					Console.Write("         \r" + p.NextValue());
					Thread.Sleep(10);
				}
			}
		}
		static int GetProcessorCount() {
			ManagementClass mc = new ManagementClass(new ManagementPath("Win32_Processor"));
			foreach (ManagementObject mo in mc.GetInstances()) {
				return (int)((uint)(mo.Properties["NumberOfLogicalProcessors"].Value));
			}
			return 1;
		}
		/// <summary>
		/// 关闭计算机。
		/// </summary>
		/// <param name="countDown">关机前的倒计时，单位：秒。</param>
		public static void Shutdown(int countDown = 0) {
			if (countDown > 315360000) {
				throw new ArgumentOutOfRangeException("倒计时有效范围是0-315360000");
			}
			BatchBuilder bb = new BatchBuilder(false);
			bb.WriteLine("start shutdown /s /t " + countDown);
			bb.TempRun();
		}
		/// <summary>
		/// 重新启动计算机。
		/// </summary>
		/// <param name="countDown">重启前的倒计时，单位：秒。</param>
		public static void Restart(int countDown = 0) {
			if (countDown > 315360000) {
				throw new ArgumentOutOfRangeException("倒计时有效范围是0-315360000");
			}
			BatchBuilder bb = new BatchBuilder(false);
			bb.WriteLine("start shutdown /r /t " + countDown);
			bb.TempRun();
		}
	}
}
