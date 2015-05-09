using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwscdv3 {
	public static class Math {
		/// <summary>
		/// 返回指定自然数的阶乘。
		/// </summary>
		public static long Fact(long value) {
			if (value == 0 || value == 1) {
				return 1;
			}
			else if (value < 0) {
				throw new ArgumentOutOfRangeException("阶乘只能为自然数。");
			} else {
				long result = value;
				checked {
					for (int i = 0; i < value - 1; i++) {
						result *= value - i - 1;
					}
				}
				return result;
			}
		}
		/// <summary>
		/// 返回两个整数的最小公倍数。
		/// </summary>
		public static long Lcm(long a, long b) {
			if (a % b == 0) {
				return a;
			} else if (b % a == 0) {
				return b;
			} else {
				return a * b / Gcd(a, b);
			}
		}
		/// <summary>
		/// 返回两个整数的最大公约数。
		/// </summary>
		public static long Gcd(long a, long b) {
			long c = a % b;
			while (c != 0) {
				a = b;
				b = c;
				c = a % b;
			}
			return b;
		}
	}
}
