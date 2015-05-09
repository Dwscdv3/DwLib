using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dwscdv3.Console;
using Dwscdv3.Timers;
using Un4seen.Bass;

namespace BadApple {
	class Program {
		static WinMMTimer timer = new WinMMTimer(100);
		static int music;
		static ProgressBar p;
		static void Main(string[] args) {
			Console.WindowHeight = 38;
			System.Console.Title = "Bad Apple!!";
			BassNet.Registration("dwscdv3@qq.com", "2X93210140022");
			Bass.BASS_Init(-1, 48000, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
		Restart:
			ASCIIAnimation a = new ASCIIAnimation(35, 30, new StreamReader(Directory.GetCurrentDirectory() + "\\Bad Apple!!.txt"));
			music = Bass.BASS_StreamCreateFile(Directory.GetCurrentDirectory() + "\\Bad Apple!!.mp3", 0, 0, BASSFlag.BASS_DEFAULT);
			p = new ProgressBar(0, (int)Bass.BASS_ChannelGetLength(music));
			p.Line = 36;
			p.PercentPrecision = 2;
			timer.Tick += timer_Tick;
			timer.Start();
			Bass.BASS_ChannelPlay(music, false);
			a.Start();
			while (true) {
				switch (Console.ReadKey(true).Key) {
					case ConsoleKey.Spacebar:
						switch (a.Status) {
							case PlayerStatus.Playing:
								Bass.BASS_ChannelPause(music);
								a.Pause();
								break;
							case PlayerStatus.Paused:
								Bass.BASS_ChannelPlay(music, false);
								a.Start();
								break;
							case PlayerStatus.Stopped:
								Bass.BASS_StreamFree(music);
								a.Dispose();
								goto Restart;
						}
						break;
					case ConsoleKey.R:
						Bass.BASS_StreamFree(music);
						a.Dispose();
						goto Restart;
				}
			}
		}

		static void timer_Tick(object sender, EventArgs e) {
			p.Value = (int)Bass.BASS_ChannelGetPosition(music);
			Console.SetCursorPosition(0, 37);
			Console.Write("空格键播放/暂停，R键立即重新播放");
		}
	}
}
