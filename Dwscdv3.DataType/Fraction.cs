using System;

namespace Dwscdv3.DataType {
	/// <summary>
	/// 表示分数。分子和分母为64位带符号整数。
	/// </summary>
	public struct Fraction {

		#region 字段和属性
		long numerator, denominator;
		bool autoReduction;
		/// <summary>
		/// 获取或设置分子。
		/// </summary>
		public long Numerator { get { return numerator; } set { numerator = value; } }
		/// <summary>
		/// 获取或设置分母。
		/// </summary>
		public long Denominator { get { return denominator; } set { denominator = value; } }
		/// <summary>
		/// 获取或设置是否应自动约分。
		/// </summary>
		public bool AutoReduction { get { return autoReduction; } set { autoReduction = value; } }
		#endregion

		#region 构造函数
		/// <summary>
		/// 以指定的参数初始化新实例。
		/// </summary>
		/// <param name="numerator">分子</param>
		/// <param name="denominator">分母</param>
		/// <param name="autoReduction">自动约分</param>
		public Fraction(long numerator = 0, long denominator = 1, bool autoReduction = true) {
			this.numerator = numerator;
			this.denominator = denominator;
			this.autoReduction = autoReduction;
			AutoReduce();
		}
		public Fraction(string value, bool autoReduction = true) {
			string[] nums = value.Split('/');
			int i = int.Parse(nums[0]), j = int.Parse(nums[1]);
			numerator = i;
			denominator = j;
			this.autoReduction = autoReduction;
			AutoReduce();
		}
		#endregion

		#region 实例方法
		/// <summary>
		/// 对此分数进行约分。
		/// </summary>
		public void Reduce() {
			long a = Dwscdv3.Math.Gcd(numerator, denominator);
			numerator /= a;
			denominator /= a;
		}
		private void AutoReduce() {
			if (autoReduction) {
				Reduce();
			}
		}
		public override bool Equals(object obj) {
			if (obj is Fraction) {
				return ToDecimal() == ((Fraction)obj).ToDecimal();
			} else if (obj is long) {
				return ToDecimal() == (long)obj;
			} else {
				throw new ArgumentException();
			}
		}
		#endregion

		#region 静态成员
		public static readonly Fraction One = new Fraction(1);
		public static readonly Fraction Zero = new Fraction();
		/// <summary>
		/// 返回指定分数的指定次幂。
		/// </summary>
		/// <param name="f">要乘幂的分数。</param>
		/// <param name="i">指定幂的整数。</param>
		public static Fraction Pow(Fraction f, int pow) {
			if (pow > 0) {
				long x = f.numerator, y = f.denominator;
				for (int i = 0; i < pow - 1; i++) {
					f.numerator *= x;
					f.denominator *= y;
				}
				return f;
			} else if (pow < 0) {
				long x = f.numerator, y = f.denominator;
				for (int i = 0; i < (-pow) - 1; i++) {
					f.numerator *= x;
					f.denominator *= y;
				}
				long buf = f.numerator;
				f.numerator = f.denominator;
				f.denominator = buf;
				return f;
			} else {
				return new Fraction(1, 1, f.autoReduction);
			}
		}
		#endregion

		#region 类型转换
		public bool ToBoolean() {
			return numerator == 0 ? false : true;
		}
		public decimal ToDecimal() {
			return (decimal)numerator / (decimal)denominator;
		}
		public double ToDouble() {
			return (double)numerator / (double)denominator;
		}
		/// <summary>
		/// 返回表示此分数的数组。索引0为分子，索引1为分母。
		/// </summary>
		public long[] ToArray() {
			return new long[] { numerator, denominator };
		}
		/// <summary>
		/// 返回分数的常规表示形式。
		/// </summary>
		public override string ToString() {
			return string.Format("{0}/{1}", numerator.ToString(), denominator.ToString());
		}
		/// <summary>
		/// 返回分数用小数表示的形式。
		/// </summary>
		/// <param name="highPrecision">
		/// <para>false: 使用 double 类型计算以提高性能</para>
		/// <para>true: 使用 decimal 类型计算以提高精确度。</para>
		/// </param>
		public string ToDecimalString(bool highPrecision = false) {
			if (highPrecision) {
				return ((decimal)numerator / (decimal)denominator).ToString();
			} else {
				return ((double)numerator / (double)denominator).ToString();
			}
		}
		#endregion

		#region 运算符重载
		public static Fraction operator -(Fraction a) {
			a.numerator = -a.numerator;
			return a;
		}
		public static Fraction operator +(Fraction a, Fraction b) {
			long lcm = Dwscdv3.Math.Lcm(a.denominator, b.denominator);
			a.numerator *= lcm / a.denominator; a.denominator = lcm;
			b.numerator *= lcm / b.denominator; b.denominator = lcm;
			a.numerator += b.numerator;
			a.AutoReduce(); return a;
		}
		public static Fraction operator -(Fraction a, Fraction b) {
			long lcm = Dwscdv3.Math.Lcm(a.denominator, b.denominator);
			a.numerator *= lcm / a.denominator; a.denominator = lcm;
			b.numerator *= lcm / b.denominator; b.denominator = lcm;
			a.numerator -= b.numerator;
			a.AutoReduce(); return a;
		}
		public static Fraction operator *(Fraction a, Fraction b) {
			a.numerator *= b.numerator;
			a.denominator *= b.denominator;
			a.AutoReduce(); return a;
		}
		public static Fraction operator /(Fraction a, Fraction b) {
			a.numerator *= b.denominator;
			a.denominator *= b.numerator;
			a.AutoReduce(); return a;
		}
		public static bool operator ==(Fraction a, Fraction b) {
			return a.ToDecimal() == b.ToDecimal();
		}
		public static bool operator !=(Fraction a, Fraction b) {
			return a.ToDecimal() != b.ToDecimal();
		}
		public static bool operator <(Fraction a, Fraction b) {
			return a.ToDecimal() < b.ToDecimal();
		}
		public static bool operator >(Fraction a, Fraction b) {
			return a.ToDecimal() > b.ToDecimal();
		}
		public static bool operator <=(Fraction a, Fraction b) {
			return a.ToDecimal() <= b.ToDecimal();
		}
		public static bool operator >=(Fraction a, Fraction b) {
			return a.ToDecimal() >= b.ToDecimal();
		}
		#endregion

		#region 显式和隐式转换
		public static implicit operator Fraction(long l) {
			return new Fraction(l, 1);
		}
		public static implicit operator Fraction(string s) {
			string[] nums = s.Split('/');
			return new Fraction(long.Parse(nums[0]), long.Parse(nums[1]));
		}
		public static explicit operator bool(Fraction a) {
			return !(a.numerator == 0);
		}
		public static explicit operator byte(Fraction a) {
			return (byte)(a.numerator / a.denominator);
		}
		public static explicit operator sbyte(Fraction a) {
			return (sbyte)(a.numerator / a.denominator);
		}
		public static explicit operator short(Fraction a) {
			return (short)(a.numerator / a.denominator);
		}
		public static explicit operator ushort(Fraction a) {
			return (ushort)(a.numerator / a.denominator);
		}
		public static explicit operator int(Fraction a) {
			return (int)(a.numerator / a.denominator);
		}
		public static explicit operator uint(Fraction a) {
			return (uint)(a.numerator / a.denominator);
		}
		public static explicit operator long(Fraction a) {
			return a.numerator / a.denominator;
		}
		public static explicit operator ulong(Fraction a) {
			return (ulong)(a.numerator / a.denominator);
		}
		public static explicit operator float(Fraction a) {
			return (float)a.numerator / (float)a.denominator;
		}
		public static explicit operator double(Fraction a) {
			return (double)a.numerator / (double)a.denominator;
		}
		public static explicit operator decimal(Fraction a) {
			return (decimal)a.numerator / (decimal)a.denominator;
		}
		#endregion

	}
}