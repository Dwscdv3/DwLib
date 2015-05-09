using System;
using System.Runtime.InteropServices;

namespace Dwscdv3.Timers {
	public class WinMMTimer : IDisposable {

		#region 字段
		static TimeCaps timeCaps;
		bool enabled = false;
		bool autoReset = false;
		int id;
		int interval;
		int resolution;
		TimeProc oneShotTimeProc;
		TimeProc periodicTimeProc;
		#endregion

		#region 属性
		public bool Enabled {
			get {
				return enabled;
			}
			set {
				if (value) {
					Start();
				} else {
					Stop();
				}
			}
		}
		/// <summary>
		/// 指定计时器的频率。为确保及时准确建议设置为 解析度(Resolution) 的10倍以上。
		/// </summary>
		public int Interval {
			get {
				return interval;
			}
			set {
				interval = value;
				if (enabled) {
					Stop(); Start();
				}
			}
		}
		/// <summary>
		/// 指定计时器的解析度。值越小，精度越高，但也将消耗更多的资源。
		/// </summary>
		public int Resolution {
			get {
				return resolution;
			}
			set {
				resolution = value;
				if (enabled) {
					Stop(); Start();
				}
			}
		}
		/// <summary>
		/// 指定计时器是否仅运行一次。
		/// </summary>
		public bool AutoReset {
			get {
				return autoReset;
			}
			set {
				autoReset = value;
				if (enabled) {
					Stop(); Start();
				}
			}
		}
		#endregion

		#region 方法
		static WinMMTimer() {
			timeGetDevCaps(ref timeCaps, Marshal.SizeOf(timeCaps));
		}
		/// <summary>
		/// 以默认值初始化 WinMMTimer 类新实例。Resolution 为最低可能值，Interval 为 Resolution * 100，AutoReset 为 false。
		/// </summary>
		public WinMMTimer() : this(100, 1, false) { }
		/// <summary>
		/// 以指定的值初始化 WinMMTimer 类新实例。
		/// </summary>
		/// <param name="interval">指定计时器的频率。</param>
		/// <param name="resolution">指定计时器的解析度。</param>
		/// <param name="autoReset">指定计时器是否仅运行一次。</param>
		public WinMMTimer(int interval = 100, int resolution = 1, bool autoReset = false) {
			this.interval = interval;
			this.resolution = resolution;
			this.autoReset = autoReset;
			oneShotTimeProc = new TimeProc(OneShotEventCallback);
			periodicTimeProc = new TimeProc(PeriodicEventCallback);
		}
		~WinMMTimer() {
			Stop();
		}
		public void Start() {
			if (!enabled) {
				id = autoReset ? timeSetEvent(interval, resolution, oneShotTimeProc, 0, autoReset ? 0 : 1)
							   : timeSetEvent(interval, resolution, periodicTimeProc, 0, autoReset ? 0 : 1);
				if (id == 0) {
					throw new Exception("Failed to start a timer. ");
				} else {
					enabled = true;
				}
			}
		}
		public void Stop() {
			if (enabled) {
				timeKillEvent(id);
				enabled = false;
			}
		}
		void OnTick(EventArgs e) {
			if (Tick != null) {
				Tick(this, e);
			}
		}
		void OneShotEventCallback(int id, int msg, int user, int param1, int param2) {
			this.OnTick(new EventArgs());
			this.Stop();
		}
		void PeriodicEventCallback(int id, int msg, int user, int param1, int param2) {
			this.OnTick(new EventArgs());
		}
		#endregion

		#region 平台调用
		[DllImport("winmm.dll")]
		static extern int timeGetDevCaps(ref TimeCaps caps, int sizeOfCaps);
		[DllImport("winmm.dll")]
		static extern int timeKillEvent(int id);
		[DllImport("winmm.dll")]
		static extern int timeSetEvent(int delay, int resolution, TimeProc proc, int user, int mode);
		#endregion

		#region 事件和委托
		delegate void TimeProc(int id, int msg, int user, int param1, int param2);
		public event EventHandler Tick;
		public event EventHandler Disposed;
		#endregion

		#region IDisposable 成员
		public void Dispose() {
			Stop();
			GC.SuppressFinalize(this);
			if (Disposed != null) {
				Disposed(this, new EventArgs());
			}
		}
		#endregion

	}
	[StructLayout(LayoutKind.Sequential)]
	internal struct TimeCaps {
		public int PeriodMin;
		public int PeriodMax;
	}
}