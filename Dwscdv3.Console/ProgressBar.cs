using System;
using System.Text;

namespace Dwscdv3.Console {
	/// <summary>
	/// 控制台用的进度条。
	/// </summary>
	public class ProgressBar {
		#region 属性
		private bool autoRender = true;
		/// <summary>
		/// 获取或设置在修改属性后是否应重新渲染进度条。
		/// </summary>
		public bool AutoRender {
			get {
				return autoRender;
			}
			set {
				autoRender = value;
				if (value) {
					Render();
				}
			}
		}
		private int value;
		/// <summary>
		/// 获取或设置进度条当前进度的值。
		/// </summary>
		public int Value {
			get {
				return value;
			}
			set {
				if (value <= maximum) {
					this.value = value;
					percent = (double)this.Value / maximum * 100;
					if (autoRender) {
						Render();
					}
				} else {
					throw new ArgumentOutOfRangeException("进度不能超过最大值。");
				}

			}
		}
		private int maximum;
		/// <summary>
		/// 获取或设置进度条进度的最大值。
		/// </summary>
		public int Maximum {
			get {
				return maximum;
			}
			set {
				if (value >= this.value) {
					maximum = value;
					percent = (double)this.Value / maximum * 100;
					if (autoRender) {
						Render();
					}
				} else {
					throw new ArgumentOutOfRangeException("最大值不应小于进度。");
				}
			}
		}
		private int width = 50;
		/// <summary>
		/// 获取或设置进度条的宽度（竖杠所占字符数）。
		/// </summary>
		public int Width {
			get {
				return width;
			}
			set {
				ClearLine();
				width = value;
				if (autoRender) {
					Render();
				}
			}
		}
		private double percent;
		/// <summary>
		/// 获取或设置当前进度的百分比。
		/// </summary>
		public double Percent {
			get {
				return percent;
			}
			set {
				if (value <= 100) {
					percent = value;
					this.value = (int)(value * (double)maximum / 100);
					if (autoRender) {
						Render();
					}
				} else {
					throw new ArgumentOutOfRangeException("百分比不能大于100%。");
				}
			}
		}
		private bool showPercent;
		/// <summary>
		/// 获取或设置进度条是否应显示当前进度百分比。
		/// </summary>
		public bool ShowPercent {
			get {
				return showPercent;
			}
			set {
				ClearLine();
				showPercent = value;
				if (autoRender) {
					Render();
				}
			}
		}
		private int persentPrecision = 0;
		/// <summary>
		/// 获取或设置百分比的小数位数。
		/// </summary>
		public int PercentPrecision {
			get {
				return persentPrecision;
			}
			set {
				ClearLine();
				persentPrecision = value;
				if (autoRender) {
					Render();
				}
			}
		}
		private int line = 0;
		/// <summary>
		/// 获取或设置本进度条显示于控制台的哪一行中。
		/// </summary>
		public int Line {
			get {
				return line;
			}
			set {
				ClearLine();
				line = value;
				if (autoRender) {
					Render();
				}
			}
		}
		private int preNameWidth = 0;
		/// <summary>
		/// 获取或设置进度条的前缀名称区域宽度（以字符为单位）。
		/// </summary>
		public int PreNameWidth {
			get {
				return preNameWidth;
			}
			set {
				ClearLine();
				preNameWidth = value;
				if (autoRender) {
					Render();
				}
			}
		}
		private string preName = "";
		/// <summary>
		/// 获取或设置进度条的前缀名称。
		/// </summary>
		public string PreName {
			get {
				return preName;
			}
			set {
				preName = value;
				if (autoRender) {
					Render();
				}
			}
		}
		private bool showPostName = false;
		/// <summary>
		/// 获取或设置进度条的后缀名称区域宽度（以字符为单位）。
		/// </summary>
		public bool ShowPostName {
			get {
				return showPostName;
			}
			set {
				ClearLine();
				showPostName = value;
				if (autoRender) {
					Render();
				}
			}
		}
		private string postName = "";
		/// <summary>
		/// 获取或设置进度条的后缀名称。
		/// </summary>
		public string PostName {
			get {
				return postName;
			}
			set {
				ClearLine();
				postName = value;
				if (string.IsNullOrEmpty(value)) {
					ShowPostName = false;
				} else {
					ShowPostName = true;
				}
				if (autoRender) {
					Render();
				}
			}
		}
		#endregion
		#region 构造函数
		/// <summary>
		/// 以指定的值初始化 ProgressBar 类的新实例。
		/// </summary>
		/// <param name="value">指定进度条当前进度的值。</param>
		/// <param name="maximum">指定进度条进度的最大值。</param>
		/// <param name="showPercent">指定是否应显示当前进度的百分比。</param>
		/// <param name="autoRender">指定在修改属性后是否应重新渲染进度条。</param>
		public ProgressBar(int value = 0, int maximum = 100, bool showPercent = true, bool autoRender = true) {
			AutoRender = false;
			Maximum = maximum;
			Value = value;
			ShowPercent = showPercent;
			Width = 50;
			PreNameWidth = 0;
			Line = 0;
			AutoRender = autoRender;
		}
		/// <summary>
		/// 以指定的值初始化 ProgressBar 类的新实例。
		/// </summary>
		/// <param name="percent">指定进度条当前进度的百分比。</param>
		/// <param name="maximum">指定进度条进度的最大值。</param>
		/// <param name="showPercent">指定是否应显示当前进度的百分比。</param>
		/// <param name="autoRender">指定在修改属性后是否应重新渲染进度条。</param>
		public ProgressBar(double percent = 0, int maximum = 100, bool showPercent = true, bool autoRender = true) {
			AutoRender = false;
			Maximum = maximum;
			Percent = percent;
			ShowPercent = showPercent;
			Width = 50;
			PreNameWidth = 0;
			Line = 0;
			AutoRender = autoRender;
		}
		#endregion
		#region 方法
		/// <summary>
		/// 立即渲染进度条。
		/// </summary>
		public void Render() {
			ClearLine();
			RenderPreName();
			RenderProgress();
			RenderPercent();
			RenderPostName();
		}
		private void RenderPreName() {
			if (PreName.Length > PreNameWidth) {
				System.Console.Write(PreName.Substring(0, PreNameWidth));
			} else {
				System.Console.Write(PreName);
				for (int i = 0; i < PreNameWidth - PreName.Length; i++) {
					System.Console.Write(" ");
				}
			}
		}
		private void RenderProgress() {
			System.Console.Write("[");
			int progress = (int)(Math.Round(Percent) * Width / 100);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < progress; i++) {
				sb.Append("|");

			}
			System.Console.Write(sb);
			sb = new StringBuilder();
			for (int i = 0; i < Width - progress; i++) {
				sb.Append(" ");
			}
			System.Console.Write(sb);
		}
		private void RenderPercent() {
			if (ShowPercent) {
				System.Console.Write("] - " + Math.Round(Percent, PercentPrecision) + "%");
			} else {
				System.Console.Write("]");
			}
		}
		private void RenderPostName() {
			if (ShowPostName) {
				System.Console.Write(" - " + PostName);
			}
		}
		/// <summary>
		/// 清空进度条所在行的内容。
		/// </summary>
		public void ClearLine() {
			System.Console.CursorTop = Line;
			System.Console.Write("\r");
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < PreNameWidth + Width + 2 + 
				(ShowPostName ? 3 + PostName.Length : 0) + 
				(ShowPercent ? 7 + (PercentPrecision > 0 ? PercentPrecision + 1 : 0) : 0); i++) {
				sb.Append(" ");
			}
			System.Console.Write(sb);
			System.Console.CursorTop = Line;
			System.Console.Write("\r");
		}
		#endregion
	}
}
