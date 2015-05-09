using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwscdv3.Language {
	public class Interpreter {
		Dictionary<string, int> Int32Var = new Dictionary<string, int>();
		Dictionary<string, double> DoubleVar = new Dictionary<string, double>();
		List<Word> Statement;
		string[] DataType = { "void",
							  "int",
							  "double",
							  "char" };
		internal Interpreter(List<Word> statement) {
			Statement = statement;
		}
		public void Run() {
			int pos = -1;
			for (int i = 0; i < Statement.Count - 3; i++) {
				foreach (string s in DataType) {
					if (Statement[i].Value == s || Statement[i + 1].Value == "main") {
						pos = i + 2;
						break;
					}
				}
				if (pos != -1) {
					break;
				}
			}
			if (pos == -1) {
				throw new Exception("未找到main()函数。");
			}
			for (; ; ) {
				switch (Statement[pos].Type) {
					case WordType.Keyword:
						
						break;
					case WordType.Variable:

						break;
					case WordType.Operator:
						
						break;
					case WordType.Assignment:

						break;
				}
			}
		}
	}
}