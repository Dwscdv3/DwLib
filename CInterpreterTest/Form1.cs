using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CInterpreterTest {
	public partial class Form1 : Form {
		Dictionary<string, int> intVar = new Dictionary<string, int>();

		public Form1() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			TextReader tr = new StringReader(richTextBox1.Text);
			while (tr.Peek() != -1) {
				string ln = tr.ReadLine();

			}
			//if (Regex.Matches(richTextBox1.Text, "void main()").Count == 1) {
			//	StringReader sr = new StringReader(richTextBox1.Text);
			//	ReadUntil(sr, '{');
			//} else {
			//	textBox1.Text = "解释错误：应该有且仅有一个程序入口点\"void main()\"。";
			//}
		}
		/// <summary>
		/// 返回从当前字符开始直到遇到指定字符前的字符串。指定的字符不包含在返回结果中。
		/// </summary>
		string ReadUntil(StringReader sr, char c) {
			string s = "";
			while (sr.Peek() != c) {
				s += sr.Read();
			}
			sr.Read();
			return s;
		}
		/// <summary>
		/// 获取当前字符出现过几次。
		/// </summary>
		int CurrentCountOf(string input, int index) {
			string s = input.Substring(0, index);
			Regex r = new Regex(input[index].ToString());
			return r.Matches(s).Count;
		}
	}
}