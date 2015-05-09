using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dwscdv3.Language {
	public enum WordType {
		Keyword,			//关键字
		Variable,			//变量
		//Expression,			//表达式
		InternalFunction,	//内部函数
		Function,			//函数
		FunctionCall,		//函数调用
		Argument,			//参数
		Semicolon,			//分号
		Operator,			//运算符
		Assignment,			//赋值
		Bracket,			//圆括号
		Brace				//花括号
	}
	public struct Word {
		public string Value;
		public WordType Type;
		public Word(string value, WordType type) {
			Value = value;
			Type = type;
		}
	}
	public class Scanner {
		const string[] Keyword = { "var" };
		const string[] InternalFunction = { "output",
											"input" };
		const string[] Operator = { "^",
									//
									"*",
									"/",
									"%",
									//
									"+",
									"-",
									//
									"<",
									">",
									"<=",
									">=",
									"==",
									//
									"," };
		const string Assignment = "=";
		const string[] Bracket = { "(",
								   ")" };
		const string[] Brace = { "{",
								 "}" };
		const string Semicolon = ";";
		readonly List<string> Separator = new List<string>();

		TextReader tr;
		List<Word> result = new List<Word>();
		string ln;

		public Scanner(StreamReader s) {
			Separator.AddRange(Keyword);
			Separator.AddRange(InternalFunction);
			Separator.AddRange(Operator);
			Separator.Add(Assignment);
			Separator.AddRange(Bracket);
			Separator.AddRange(Brace);
			Separator.Add(Semicolon);
		}
		public void Scan() {
			while (tr.Peek() != -1) {
				for (NextFunction(); !IsEndOfFile(); NextFunction()) {
					for (ln = NextSentence(); !IsEndOfFunction(); ln = NextSentence()) {
						ln = ln.Replace("\r\n", "");
						ln = ln.Replace(" ", "");
						List<string> ls = Split(ln, Separator);
						foreach (string s in ls) {
							foreach (string s2 in Keyword) {
								if (s == s2) {
									result.Add(new Word(s, WordType.Keyword));
									break;
								}
							} foreach (string s2 in Operator) {
								if (s == s2) {
									result.Add(new Word(s, WordType.Operator));
									break;
								}
							} foreach (string s2 in InternalFunction) {
								if (s == s2) {
									result.Add(new Word(s, WordType.InternalFunction));
									break;
								}
							} if (s == Assignment) {
								result.Add(new Word(s, WordType.Assignment));
							} foreach (string s2 in Bracket) {
								if (s == s2) {
									result.Add(new Word(s, WordType.Bracket));
								}
							} foreach (string s2 in Brace) {
								if (s == s2) {
									result.Add(new Word(s, WordType.Brace));
								}
							} if (s == Semicolon) {
								result.Add(new Word(s, WordType.Semicolon));
							}
						}


						//if (ln.Substring(0, 3) == "var") {
						//	result.Add(new Word("var", WordType.Keyword));
						//	result.Add(new Word(ReadUntil(ln, '=', 3), WordType.Variable));
						//	result.Add(new Word("=", WordType.Assignment));
						//	result.Add(new Word(ReadUntil(ln, ';', ln.IndexOf('=') + 1), WordType.Expression));
						//	//result.Add(new Word(";", WordType.Semicolon));
						//} else if (true) {

						//} else if (true) {

						//}
					}
				}
			}
		}
		private List<string> Split(string input, List<string> separator) {
			int pos = 0;
			List<string> result = new List<string>();
			while (pos < input.Length) {
				for (int i = 0; i < separator.Count; i++) {
					if (pos + separator[i].Length <= input.Length) {
						if (ln.Substring(pos, separator[i].Length) == separator[i]) {
							if (i - pos > 1) {
								result.Add(input.Substring(pos + 1, i - pos - 1));
							}
							result.Add(separator[i]);
							pos = i;
							break;
						}
					}
				}
			}
			return result;
		}
		private bool IsEndOfFile() {
			if (tr.Peek() == -1) {
				return true;
			} else {
				return false;
			}
		}
		private void NextFunction() {
			//Regex re = new Regex("[a-zA-Z][a-zA-Z0-9]*([a-zA-Z0-9]*)");
		}
		private string NextSentence() {
			string s = "";
			do {
				s += (char)(tr.Read());
			} while (s[s.Length - 1] != ';' || s[s.Length - 1] != '}');
			return s;
		}
		private bool IsEndOfFunction() {
			if (ln[ln.Length - 1] == '}') {
				return true;
			} else {
				return false;
			}
		}
		bool IsVariable(string s) {
			s = s.Replace(" ", "");
			string name = ReadUntil(s, '=', 0);
			foreach (Word w in result) {
				if (w.Value == name) {
					return true;
				}
			}
			return false;
		}
		string ReadUntil(string input, char endChar, int index) {
			string s = "";
			bool found = false;
			for (int i = index; i < input.Length; i++) {
				if (input[i] != endChar) {
					s += input[i];
				} else {
					found = true;
					break;
				}
			}
			if (!found) {
				throw new Exception("解释错误。请检查语句。");
			}
			return s;
		}
	}
}