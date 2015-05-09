using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwscdv3.Encoding {
	public static class CharacterReplace {

		#region ROT13
		/// <summary>
		/// 将字符串用ROT13编码或还原。
		/// </summary>
		public static string ROT13(string s) {
			string buffer = "";
			for (int i = 0; i < s.Length; i++) {
				switch (CharType(s[i])) {
					case 1:
					case 3:
						buffer += (char)(s[i] + (char)13);
						break;
					case 2:
					case 4:
						buffer += (char)(s[i] - (char)13);
						break;
					default:
						buffer += s[i];
						break;
				}
			}
			return buffer;
		}

		/// <summary>
		/// 将字节数组用ROT13编码或还原。
		/// </summary>
		public static byte[] ROT13(byte[] b) {
			byte[] buffer = new byte[b.Length];
			for (int i = 0; i < b.Length; i++) {
				switch (CharType(b[i])) {
					case 1:
					case 3:
						buffer[i] = (byte)(b[i] + 13);
						break;
					case 2:
					case 4:
						buffer[i] = (byte)(b[i] - 13);
						break;
					default:
						buffer[i] = b[i];
						break;
				}
			}
			return buffer;
		}

		/// <summary>
		/// <para>1 - 大写字母前半</para>
		/// <para>2 - 大写字母后半</para>
		/// <para>3 - 小写字母前半</para>
		/// <para>4 - 小写字母后半</para>
		/// <para>5 - 数字</para>
		/// <para>6 - 其它</para>
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		static int CharType(char c) {
			if (c >= 65 && c <= 77) { return 1; } else if (c >= 78 && c <= 90) { return 2; } else if (c >= 97 && c <= 109) { return 3; } else if (c >= 110 && c <= 122) { return 4; } else if (c >= 48 && c <= 57) { return 5; } else { return 6; }
		}

		static int CharType(byte b) {
			if (b >= 65 && b <= 77) { return 1; } else if (b >= 78 && b <= 90) { return 2; } else if (b >= 97 && b <= 109) { return 3; } else if (b >= 110 && b <= 122) { return 4; } else if (b >= 48 && b <= 57) { return 5; } else { return 6; }
		}
		#endregion

		/// <summary>
		/// 将字节数组内每个字节值反转。<para>用255减之。</para>
		/// </summary>
		public static byte[] Reverse(byte[] b) {
			byte[] buffer = new byte[b.Length];
			for (int i = 0; i < b.Length; i++) {
				buffer[i] = (byte)(255 - b[i]);
			}
			return buffer;
		}
	}
}
