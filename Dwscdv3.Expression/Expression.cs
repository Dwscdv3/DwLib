using System;
using System.Collections.Generic;

namespace Dwscdv3.Expression {
	internal class Common {
		public static void Split(string s, string[] operators, List<string> l) {
			string[] tmp = s.Split(operators, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0, lastPos = 0; i < s.Length; i++) {
				if (i == s.Length - 1) {
					l.Add(s.Substring(lastPos, i - lastPos + 1));
					break;
				} foreach (string str in operators) {
					if (s.Substring(i, str.Length) == str) {
						l.Add(s.Substring(lastPos, i - lastPos));
						l.Add(s.Substring(i, str.Length));
						lastPos = i + str.Length;
					}
				}
			}
		}
	}
	public class DoubleExpression {
		public static double Calculate(string input) {
			string s = input.Replace("\r\n", "").Replace(" ", "");
			while (true) {
				#region 按照深度计算括号内子表达式
				int lBracketCurrent = 0, lBracketMax = 0, index = 0;
				for (int i = 0; i < s.Length; i++) {
					switch (s[i]) {
						case '(':
							lBracketCurrent++;
							if (lBracketCurrent > lBracketMax) {
								lBracketMax = lBracketCurrent;
								index = i;
							}
							break;
						case ')':
							lBracketCurrent--;
							break;
					}
				}
				#endregion
				#region 检查是否还有括号
				if (lBracketMax == 0) {
					break;
				}
				#endregion
				#region 检查括号匹配
				bool foundRBracket = false;
				for (int i = index + 1; i < s.Length; i++) {
					if (s[i] == ')') {
						foundRBracket = true;
						if (i == index + 1) {
							break;
						} else {
							double d = DoubleExpressionWithoutBrackets.Calculate(s.Substring(index + 1, i - index - 1));
							s = s.Remove(index, i - index + 1);
							s = s.Insert(index, d.ToString());
							break;
						}
					}
				}
				if (!foundRBracket) {
					throw new ArgumentException("括号数量不匹配。");
				}
				#endregion
			}
			return DoubleExpressionWithoutBrackets.Calculate(s);
		}
	}
	internal class DoubleExpressionWithoutBrackets {
		public static readonly string[] operators = { "+", "-", "*", "/", "%", "<=", ">=", "<", ">", "==", "!=", "&&", "||" };
		public static double Calculate(string input) {
			string s = input;
			#region 拆解表达式字符串
			List<string> l = new List<string>();
			Common.Split(s, operators, l);
			#endregion
			bool loop;
			#region * / %
			do {
				loop = false;
				for (int i = 0; i < l.Count; i++) {
					if (l[i] == "*") {
						double result = double.Parse(l[i - 1]) * double.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "/") {
						double result = double.Parse(l[i - 1]) / double.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "%") {
						double result = double.Parse(l[i - 1]) % double.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			#region + -
			do {
				loop = false;
				for (int i = 0; i < l.Count; i++) {
					if (l[i] == "+") {
						double result = double.Parse(l[i - 1]) + double.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "-") {
						double result = double.Parse(l[i - 1]) - double.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			#region < <= > >=
			do {
				loop = false;
				for (int i = 1; i < l.Count - 1; i++) {
					if (l[i] == "<") {
						double result = double.Parse(l[i - 1]) < double.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "<=") {
						double result = double.Parse(l[i - 1]) <= double.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == ">") {
						double result = double.Parse(l[i - 1]) > double.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == ">=") {
						double result = double.Parse(l[i - 1]) >= double.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			#region == !=
			for (int i = 0; i < l.Count; i++) {
				switch (l[i]) {
					case "True":
						l[i] = "1";
						break;
					case "False":
						l[i] = "0";
						break;
				}
			} do {
				loop = false;
				for (int i = 1; i < l.Count - 1; i++) {
					if (l[i] == "==") {
						double result = double.Parse(l[i - 1]) == double.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "!=") {
						double result = double.Parse(l[i - 1]) != double.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			#region &&
			do {
				loop = false;
				for (int i = 1; i < l.Count - 1; i++) {
					if (l[i] == "&&") {
						double result = IntToBool(l[i - 1]) && IntToBool(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			#region ||
			do {
				loop = false;
				for (int i = 1; i < l.Count - 1; i++) {
					if (l[i] == "||") {
						double result = IntToBool(l[i - 1]) || IntToBool(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			return double.Parse(l[0]);
		}
		static bool IntToBool(string s) {
			if (s == "0") {
				return false;
			} else {
				return true;
			}
		}
		static void Replace(List<string> l, double value, int index) {
			l.RemoveRange(index - 1, 3);
			l.Insert(index - 1, value.ToString());
		}
	}
	public class Int32Expression {
		public static int Calculate(string input) {
			string s = input.Replace("\r\n", "").Replace(" ", "");
			while (true) {
				#region 按照深度计算括号内子表达式
				int lBracketCurrent = 0, lBracketMax = 0, index = 0;
				for (int i = 0; i < s.Length; i++) {
					switch (s[i]) {
						case '(':
							lBracketCurrent++;
							if (lBracketCurrent > lBracketMax) {
								lBracketMax = lBracketCurrent;
								index = i;
							}
							break;
						case ')':
							lBracketCurrent--;
							break;
					}
				}
				#endregion
				#region 检查是否还有括号
				if (lBracketMax == 0) {
					break;
				}
				#endregion
				#region 检查括号匹配
				bool foundRBracket = false;
				for (int i = index + 1; i < s.Length; i++) {
					if (s[i] == ')') {
						foundRBracket = true;
						if (i == index + 1) {
							break;
						} else {
							int d = Int32ExpressionWithoutBrackets.Calculate(s.Substring(index + 1, i - index - 1));
							s = s.Remove(index, i - index + 1);
							s = s.Insert(index, d.ToString());
							break;
						}
					}
				}
				if (!foundRBracket) {
					throw new ArgumentException("括号数量不匹配。");
				}
				#endregion
			}
			return Int32ExpressionWithoutBrackets.Calculate(s);
		}
	}
	internal class Int32ExpressionWithoutBrackets {
		public static readonly string[] operators = { "+", "-", "*", "/", "%", "<=", ">=", "<", ">", "==", "!=", "&&", "||" };
		public static int Calculate(string input) {
			string s = input;
			#region 拆解表达式字符串
			List<string> l = new List<string>();
			Common.Split(s, operators, l);
			#endregion
			bool loop;
			#region * / %
			do {
				loop = false;
				for (int i = 0; i < l.Count; i++) {
					if (l[i] == "*") {
						int result = int.Parse(l[i - 1]) * int.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "/") {
						int result = int.Parse(l[i - 1]) / int.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "%") {
						int result = int.Parse(l[i - 1]) % int.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			#region + -
			do {
				loop = false;
				for (int i = 0; i < l.Count; i++) {
					if (l[i] == "+") {
						int result = int.Parse(l[i - 1]) + int.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "-") {
						int result = int.Parse(l[i - 1]) - int.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			#region < <= > >=
			do {
				loop = false;
				for (int i = 1; i < l.Count - 1; i++) {
					if (l[i] == "<") {
						int result = int.Parse(l[i - 1]) < int.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "<=") {
						int result = int.Parse(l[i - 1]) <= int.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == ">") {
						int result = int.Parse(l[i - 1]) > int.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == ">=") {
						int result = int.Parse(l[i - 1]) >= int.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			#region == !=
			for (int i = 0; i < l.Count; i++) {
				switch (l[i]) {
					case "True":
						l[i] = "1";
						break;
					case "False":
						l[i] = "0";
						break;
				}
			} do {
				loop = false;
				for (int i = 1; i < l.Count - 1; i++) {
					if (l[i] == "==") {
						int result = int.Parse(l[i - 1]) == int.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "!=") {
						int result = int.Parse(l[i - 1]) != int.Parse(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			#region &&
			do {
				loop = false;
				for (int i = 1; i < l.Count - 1; i++) {
					if (l[i] == "&&") {
						int result = IntToBool(l[i - 1]) && IntToBool(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			#region ||
			do {
				loop = false;
				for (int i = 1; i < l.Count - 1; i++) {
					if (l[i] == "||") {
						int result = IntToBool(l[i - 1]) || IntToBool(l[i + 1]) ? 1 : 0;
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			#endregion
			return int.Parse(l[0]);
		}
		static bool IntToBool(string s) {
			if (s == "0") {
				return false;
			} else {
				return true;
			}
		}
		static void Replace(List<string> l, int value, int index) {
			l.RemoveRange(index - 1, 3);
			l.Insert(index - 1, value.ToString());
		}
	}
	//public class BooleanExpression {
	//	static readonly string[] operators = { "<=", ">=", "<", ">", "==", "!=", "&&", "||" };
	//	static readonly string[] boolean = { "True", "False" };
	//	public static bool Calculate(string input) {
	//		string s = input.Replace("\r\n", "").Replace(" ", "");
	//		#region 拆解表达式字符串
	//		List<string> l = new List<string>();
	//		Common.Split(s, operators, l);
	//		#endregion
	//		#region 计算非布尔运算的结果
	//		for (int i = 0; i < l.Count; i++) {
	//			bool needParse = true;
	//			foreach (string str in operators) {
	//				if (l[i] == str) {
	//					needParse = false;
	//				}
	//			} foreach (string str in boolean) {
	//				if (l[i] == str) {
	//					needParse = false;
	//				}
	//			}
	//			if (needParse) {
	//				l[i] = FloatExpression.Calculate(l[i]).ToString();
	//			}
	//		}
	//		#endregion
	//		bool loop;
	//		#region < <= > >=
	//		do {
	//			loop = false;
	//			for (int i = 1; i < l.Count - 1; i++) {
	//				if (l[i] == "<") {
	//					bool result = double.Parse(l[i - 1]) < double.Parse(l[i + 1]);
	//					Replace(l, result, i);
	//					loop = true;
	//					break;
	//				} else if (l[i] == "<=") {
	//					bool result = double.Parse(l[i - 1]) <= double.Parse(l[i + 1]);
	//					Replace(l, result, i);
	//					loop = true;
	//					break;
	//				} else if (l[i] == ">") {
	//					bool result = double.Parse(l[i - 1]) > double.Parse(l[i + 1]);
	//					Replace(l, result, i);
	//					loop = true;
	//					break;
	//				} else if (l[i] == ">=") {
	//					bool result = double.Parse(l[i - 1]) >= double.Parse(l[i + 1]);
	//					Replace(l, result, i);
	//					loop = true;
	//					break;
	//				}
	//			}
	//		} while (loop);
	//		#endregion
	//		#region == !=
	//		for (int i = 0; i < l.Count; i++) {
	//			switch (l[i]) {
	//				case "True":
	//					l[i] = "1";
	//					break;
	//				case "False":
	//					l[i] = "0";
	//					break;
	//			}
	//		} do {
	//			loop = false;
	//			for (int i = 1; i < l.Count - 1; i++) {
	//				if (l[i] == "==") {
	//					bool result = double.Parse(l[i - 1]) == double.Parse(l[i + 1]);
	//					Replace(l, result, i);
	//					loop = true;
	//					break;
	//				} else if (l[i] == "!=") {
	//					bool result = double.Parse(l[i - 1]) != double.Parse(l[i + 1]);
	//					Replace(l, result, i);
	//					loop = true;
	//					break;
	//				}
	//			}
	//		} while (loop);
	//		#endregion
	//		#region &&
	//		do {
	//			loop = false;
	//			for (int i = 1; i < l.Count - 1; i++) {
	//				if (l[i] == "&&") {
	//					bool result = bool.Parse(l[i - 1]) && bool.Parse(l[i + 1]);
	//					Replace(l, result, i);
	//					loop = true;
	//					break;
	//				}
	//			}
	//		} while (loop);
	//		#endregion
	//		#region ||
	//		do {
	//			loop = false;
	//			for (int i = 1; i < l.Count - 1; i++) {
	//				if (l[i] == "||") {
	//					bool result = bool.Parse(l[i - 1]) || bool.Parse(l[i + 1]);
	//					Replace(l, result, i);
	//					loop = true;
	//					break;
	//				}
	//			}
	//		} while (loop);
	//		#endregion
	//		return bool.Parse(l[0]);
	//	}
	//	static void Replace(List<string> l, bool value, int index) {
	//		l.RemoveRange(index - 1, 3);
	//		l.Insert(index - 1, value.ToString());
	//	}
	//}
	/*
	/// <summary>
	/// 简单的数学表达式。支持四则运算、取模运算、幂运算和括号。
	/// </summary>
	public class SimpleExpression {
		//public struct SubExpression {
		//	int deep;
		//}
		public static double Calculate(string input) {
			string s = input;
			while (true) {
				int lBracketCurrent = 0, lBracketMax = 0, index = 0;
				for (int i = 0; i < s.Length; i++) {
					switch (s[i]) {
						case '(':
							lBracketCurrent++;
							if (lBracketCurrent > lBracketMax) {
								lBracketMax = lBracketCurrent;
								index = i;
							}
							break;
						case ')':
							lBracketCurrent--;
							break;
					}
				}
				if (lBracketMax == 0) {
					break;
				}
				bool foundRBracket = false;
				for (int i = index + 1; i < s.Length; i++) {
					if (s[i] == ')') {
						foundRBracket = true;
						if (i == index + 1) {
							break;
						} else {
							double d = NoBracketExpression.Calculate(s.Substring(index + 1, i - index - 1));
							s = s.Remove(index, i - index + 1);
							s = s.Insert(index, d.ToString());
							break;
						}
					}
				}
				if (!foundRBracket) {
					throw new ArgumentException("括号数量不匹配。");
				}
			}
			return NoBracketExpression.Calculate(s);
		}
	}
	/// <summary>
	/// 无括号的数学表达式。支持四则运算、取模运算和幂运算。<para>注意：本类的设计初衷是简化代码。不过若您的表达式没有括号，使用该类可以提高性能。</para>
	/// </summary>
	public class NoBracketExpression {
		public static double Calculate(string input) {
			string nosp = input.Replace(" ", "");
			foreach (char c in nosp) {
				switch (c) {
					case '(':
					case ')':
						throw new ArgumentException("表达式中不应含有括号。如果要计算带括号的表达式，请使用其它类的Calculate方法。");
				}
			}
			bool loop;
			List<string> l = new List<string>();
			string[] s = nosp.Split(new char[] { '+', '-', '*', '/', '^' });
			int lastPos = 0;
			for (int i = 0; i < nosp.Length; i++) {
				if (nosp[i] == '+' || nosp[i] == '-' || nosp[i] == '*' || nosp[i] == '/' || nosp[i] == '^') {
					l.Add(nosp.Substring(lastPos, i - lastPos));
					l.Add(nosp.Substring(i, 1));
					lastPos = i + 1;
				} else if (i == nosp.Length - 1) {
					l.Add(nosp.Substring(lastPos, i - lastPos + 1));
				}
			}
			do {
				loop = false;
				for (int i = 0; i < l.Count; i++) {
					if (l[i] == "^") {
						double result = Math.Pow(double.Parse(l[i - 1]), double.Parse(l[i + 1]));
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			do {
				loop = false;
				for (int i = 0; i < l.Count; i++) {
					if (l[i] == "*") {
						double result = double.Parse(l[i - 1]) * double.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "/") {
						double result = double.Parse(l[i - 1]) / double.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "%") {
						double result = double.Parse(l[i - 1]) % double.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			do {
				loop = false;
				for (int i = 0; i < l.Count; i++) {
					if (l[i] == "+") {
						double result = double.Parse(l[i - 1]) + double.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					} else if (l[i] == "-") {
						double result = double.Parse(l[i - 1]) - double.Parse(l[i + 1]);
						Replace(l, result, i);
						loop = true;
						break;
					}
				}
			} while (loop);
			return double.Parse(l[0]);
		}
		static void Replace(List<string> l, double value, int index) {
			l.RemoveRange(index - 1, 3);
			l.Insert(index - 1, value.ToString());
		}
	}
	*/
}