using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace img2txt {
	class Program {
		static void Main(string[] args) {
			//string s = "";
			//for (int i = 0; i < 24; i++) {
			//	for (int j = 0; j < 48; j++) {
			//		s += "#";
			//	}
			//	s += "\r\n";
			//}
			//Console.WriteLine(s);
			DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
			FileInfo[] fis = di.GetFiles("basmall*.png");
			TextWriter tw = new StreamWriter(Directory.GetCurrentDirectory() + "\\ba.txt");
			foreach (FileInfo fi in fis) {
				Bitmap bmp = new Bitmap(Image.FromFile(fi.FullName));
				Color tmp = bmp.GetPixel(0, 0);
				bool isBlack = false;
				if (tmp.R < 128 && tmp.G < 128 && tmp.B < 128) {
					isBlack = true;
				}
				for (int i = 0; i < bmp.Height; i++) {
					for (int j = 0; j < bmp.Width; j++) {
						Color c = bmp.GetPixel(j, i);
						if (c.R < 128 || c.G < 128 || c.B < 128) {
							tw.Write("#");
						} else {
							tw.Write(" ");
						}
					}
					tw.WriteLine();
				}
			}
			tw.Flush();
			tw.Close();
		}
	}
}
