using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test10 {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("1. UInt16 to String\r\n2. String to Bytes");
			while (true) {
				switch (Console.ReadKey(true).KeyChar) {
					case '1':
						Console.Clear();
						Console.Title = "UInt16 to String";
						while (true) {
							string s = Console.ReadLine();
							int i;
							if (int.TryParse(s, out i)) {
								if (i >= 0 && i < 65536) {
									byte[] b = new byte[] { (byte)(i % 256), (byte)(i / 256) };
									Console.WriteLine(Encoding.Unicode.GetString(b));
								} else {
									Console.WriteLine("Number out of range. ");
								}
							} else {
								Console.WriteLine("Format incorrect. ");
							}
						}
					case '2':
						Console.Clear();
						Console.Title = "String to Bytes";
						while (true) {
							string s = Console.ReadLine();
							byte[] b = Encoding.Unicode.GetBytes(s);
							for (int i = 0; i < b.Length; i += 2) {
								Console.WriteLine(
									b[i].ToString() + " " + b[i + 1].ToString() + "\t " +
									(b[i] + b[i + 1] * 256).ToString() + "\t " +
									(b[i] + b[i + 1] * 256).ToString("X4") + "\t" +
									Encoding.Unicode.GetString(b, i, 2)
								);
							}
						}
					default:
						continue;
				}
			}
		}
	}
}