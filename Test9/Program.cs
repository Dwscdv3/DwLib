using System;
using System.Diagnostics;
using Dwscdv3.Expression;

namespace Test9 {
	static class Program {
		public static void Main(string[] args) {
			//Console.WriteLine("UInt64: " + ulong.MaxValue);
			//Console.WriteLine("Double: " + ((double)ulong.MaxValue).ToString("F0"));
			Stopwatch stopwatch = new Stopwatch();
			while (true) {
				string s = Console.ReadLine();
				stopwatch.Start();
				//Console.WriteLine("Int32: " + Int32Expression.Calculate(s));
				Console.WriteLine("Double: " + DoubleExpression.Calculate(s));
				Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");
				stopwatch.Reset();
			}
		}
	}
}