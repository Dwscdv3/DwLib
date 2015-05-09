using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Dwscdv3;

namespace Test7 {
	class Program {
		static void Main(string[] args) {
			Stopwatch s = new Stopwatch();
			s.Start();
			noip1();
			s.Stop();
			Console.WriteLine(s.ElapsedMilliseconds + "ms");
			Console.ReadKey(true);
		}

		private static void noip1() {
			DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());
			foreach (FileInfo f in d.GetFiles("*.in", SearchOption.TopDirectoryOnly)) {
				TextReader tr = new StreamReader(f.Open(FileMode.Open, FileAccess.Read));
				string[] ln1 = tr.ReadLine().Split(' '), ln2 = tr.ReadLine().Split(' '), ln3 = tr.ReadLine().Split(' ');
				tr.Close();
				int n = int.Parse(ln1[0]), na = int.Parse(ln1[1]), nb = int.Parse(ln1[2]);
				int[] patternA = new int[na], patternB = new int[nb];
				for (int i = 0; i < na; i++) {
					patternA[i] = int.Parse(ln2[i]);
				}
				for (int i = 0; i < nb; i++) {
					patternB[i] = int.Parse(ln3[i]);
				}
				int a = 0, b = 0, sa = 0, sb = 0;
				int[,] compare = {{0,2,1,1,2},  //0: draw
				/*compare[a, b]*/ {1,0,2,1,2},  //1: a win
								  {2,1,0,2,1},  //2: b win
								  {2,2,1,0,1},
								  {1,1,2,2,0}};
				for (int i = 0; i < n; i++) {
					switch (compare[patternA[i % na], patternB[i % nb]]) {
						case 1:
							sa++;
							break;
						case 2:
							sb++;
							break;
					}
				}
				string fname = f.ToString();
				TextWriter tw = new StreamWriter(fname.Substring(0, fname.Length - 2) + "out");
				tw.WriteLine(string.Format("{0} {1}", sa, sb));
				tw.Close();
			}
		}
	}
}
