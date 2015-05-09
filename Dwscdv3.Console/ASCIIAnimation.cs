using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dwscdv3.Timers;

namespace Dwscdv3.Console {
	public enum PlayerStatus { Playing, Paused, Stopped }
	public class ASCIIAnimation {
		public PlayerStatus Status { get; set; }
		public int Height { get; set; }

		WinMMTimer timer;
		TextReader tr;
		StreamReader sr;

		public ASCIIAnimation(int height, int fps, StreamReader sr) {
			Height = height;
			timer = new WinMMTimer(1000 / fps);
			timer.Tick += frame;
			tr = sr;
			this.sr = sr;
			System.Console.CursorVisible = false;
		}
		~ASCIIAnimation() {
			tr.Close();
			sr.Close();
		}
		public void Dispose() {
			GC.SuppressFinalize(this);
			Stop();
			tr.Close();
			sr.Close();
		}
		public void Start() {
			timer.Start();
			Status = PlayerStatus.Playing;
		}
		public void Pause() {
			timer.Stop();
			Status = PlayerStatus.Paused;
		}
		public void Stop() {
			timer.Stop();
			tr = sr;
			System.Console.Clear();
			Status = PlayerStatus.Stopped;
		}
		void frame(object sender, EventArgs e) {
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < Height; i++) {
				if (tr.Peek() != -1) {
					sb.Append(tr.ReadLine());
					sb.Append("\r\n");
					//System.Console.WriteLine(tr.ReadLine());
				} else {
					Stop();
					break;
				}
			}
			System.Console.SetCursorPosition(0, 0);
			System.Console.Write(sb.ToString());
		}
	}
}
