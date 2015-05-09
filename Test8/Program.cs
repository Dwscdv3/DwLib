using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dwscdv3.MathExpression;

namespace Test8 {
	class Program {
		static void Main(string[] args) {
			Stopwatch stopwatch = new Stopwatch();
			while (true) {
				string s = Console.ReadLine();
				stopwatch.Start();
				Console.WriteLine(SimpleExpression.Calculate(s));
				stopwatch.Stop();
				Console.WriteLine("用时：" + stopwatch.ElapsedMilliseconds + "ms");
				stopwatch.Reset();
			}
		}
	}
}
