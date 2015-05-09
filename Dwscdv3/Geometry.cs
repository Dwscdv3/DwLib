using System;

namespace Dwscdv3 {
	[Serializable]
	public struct Point {
		public double X;
		public double Y;
		//public static Point Absolute(Point p) {
		//	return new Point {
		//		X = System.Math.Abs(p.X),
		//		Y = System.Math.Abs(p.Y)
		//	};
		//}
		//public static Point operator +(Point x, Point y) {
		//	return new Point { X = x.X + y.X, Y = x.Y + y.Y };
		//}
		//public static Point operator -(Point x, Point y) {
		//	return new Point { X = x.X - y.X, Y = x.Y - y.Y };
		//}
		//public static Point operator *(Point x, Point y) {
		//	return new Point { X = x.X * y.X, Y = x.Y * y.Y };
		//}
		//public static Point operator /(Point x, Point y) {
		//	return new Point { X = x.X / y.X, Y = x.Y / y.Y };
		//}
		//public static Point operator *(Point x, double y) {
		//	return new Point { X = x.X * y, Y = x.Y * y };
		//}
		//public static Point operator /(Point x, double y) {
		//	return new Point { X = x.X / y, Y = x.Y / y };
		//}
	}
	public class Geometry {
		/// <summary>
		/// 计算两点之间的距离。
		/// </summary>
		public static double Distance(Point x, Point y) {
			return Pythagorean(y.X - x.X, y.Y - x.Y);
		}
		/// <summary>
		/// 毕达哥拉斯定理（又名勾股定理）。
		/// </summary>
		public static double Pythagorean(double x, double y) {
			return System.Math.Sqrt(x * x + y * y);
		}
		//public static double ToAngle(Point x, Point o, Point y) {
		//	//Point v1 = x - o, v2 = y - o;
		//	//Point N = new Point { X = -x.Y, Y = x.X };
		//	//return System.Math.Atan2(v2 * N, v2 * v1);
		//	//double a = Distance(x, y), b = Distance(x, o), c = Distance(y, o);
		//	//return (b * b + c * c - a * a) / 2 * b * c;
		//}
	}
}