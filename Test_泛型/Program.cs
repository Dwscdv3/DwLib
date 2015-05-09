using System;
using Dwscdv3.Collections.Generic;

namespace Test_泛型 {
	struct testcase {
		public int i;
		public short s;
	}
	class Program {
		static void Main(string[] args) {
			List<testcase> l = new List<testcase>();
			for (short i = 0; i < 100; i++) {
				l.Add(new testcase { i = i * 2, s = i });
			} foreach (testcase s in l) {
				Console.WriteLine(string.Format("i is {0}, s is {1}", s.i, s.s));
			} Console.ReadKey(true);
		}
	}
}