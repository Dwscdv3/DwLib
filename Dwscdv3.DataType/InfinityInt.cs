using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwscdv3.DataType {
	//struct InfinityInt {
	//	ulong hi, lo;
	//	ulong High { get { return hi; } set { hi = value; } }
	//	ulong Low { get { return lo; } set { lo = value; } }
	//	public InfinityInt(ulong lo) : this(0, lo) { }
	//	public InfinityInt(ulong hi, ulong lo) { this.hi = hi; this.lo = lo; }
	//	public string ToString(string format) {
	//		switch (format[0]) {
	//			case 'x':
	//				return internalToString(format);
	//			case 'X':
	//				return internalToString(format);
	//			default:
	//				throw new ArgumentException("参数格式不正确。");
	//		}
	//	}
	//	private string internalToString(string format) {
	//		string caps = format[0].ToString();
	//		if (format.Length > 1) {
	//			int length = int.Parse(format.Substring(1));
	//			if (length > 32 || length < 1) {
	//				throw new ArgumentOutOfRangeException("有效范围: 1 - 32");
	//			} else {
	//				return (hi.ToString(caps + "16") + lo.ToString(caps + "16")).TrimStart('0');
	//			}
	//		} else {
	//			if (hi == 0) {
	//				return lo.ToString(caps);
	//			} else {
	//				return hi.ToString(caps) + lo.ToString(caps + "16");
	//			}
	//		}
	//	}
	//}
}
