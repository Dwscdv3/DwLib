using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dwscdv3.Data.TextDB {
	public class TextDB {
		string path = null;
		bool safeMode = false;
		MemoryStream table = null;

		public string Path {
			get {
				return path;
			}
			set {
				path = value;
			}
		}
		/// <summary>
		/// 指定写入文件时是否创建副本并覆盖原文件。
		/// </summary>
		public bool SafeMode {
			get {
				return safeMode;
			}
			set {
				safeMode = value;
			}
		}

		/// <summary>
		/// 使用指定的路径创建新的 TextDB 实例。
		/// </summary>
		public TextDB(string path) {
			this.path = path;
			load();
		}

		todo

		private void load() {
			byte[] buffer = null;
			using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite)) {
				fs.Read(buffer, 0, buffer.Length);
			} table = new MemoryStream(buffer);
		}
	}
}