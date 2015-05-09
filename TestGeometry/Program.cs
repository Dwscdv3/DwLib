using System;
using Dwscdv3;

namespace TestGeometry {
	class Program {
		static void Main(string[] args) {
			Point o = new Point { X = 0, Y = 0 };
			Point a = new Point { X = 6, Y = 0 };
			Point b = new Point { X = 6, Y = 6 };
			Console.WriteLine(Geometry.ToAngle(a, o, b));
			
		}
	}
}