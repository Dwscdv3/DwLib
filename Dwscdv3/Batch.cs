using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Dwscdv3 {
	/// <summary>
	/// 包含批处理相关的静态方法的类。
	/// </summary>
	public static class Batch {
		public static void Output(byte[] b, string path) {
			if (path.Substring(path.Length - 4).ToLower() != ".bat") {
				path += ".bat";
			}
			FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
			fs.Write(b, 0, b.Length);
			fs.Flush();
			fs.Close();
		}
		public static void Output(string s, string path) {
			byte[] b = Encoding.UTF8.GetBytes(s);
			Output(b, path);
		}
		public static void Run(string path) {
			Process.Start(path);
		}
		//public static void GenerateAndRun(byte[] b, string path) {
		//	Generate(b, path);
		//	Run(path);
		//}
		//public static void GenerateAndRun(string s, string path) {
		//	GenerateAndRun(Encoding.UTF8.GetBytes(s), path);
		//}
	}
	public class BatchBuilder {
		string val = "";
		public int Length {
			get {
				return val.Length;
			}
		}
		public int Line {
			get {
				return System.Text.RegularExpressions.Regex.Matches(val, "\r\n").Count;
			}
		}
		/// <param name="echo">回显开关。</param>
		public BatchBuilder(bool echo) {
			WriteLine(echo ? "@echo on" : "@echo off");
		}
		public void Output(string path) {
			Batch.Output(val, path);
		}
		public void Output(string path, Encoding encoding) {
			Batch.Output(encoding.GetBytes(val), path);
		}
		/// <summary>
		/// 在临时文件夹中运行此脚本，运行后自动删除。
		/// </summary>
		public void TempRun() {
			val += "\r\ndel %0";
			string path = System.IO.Path.GetTempPath() + "\\" + System.Guid.NewGuid().ToString() + ".bat";
			Output(path);
			Batch.Run(path);
		}
		public void Write(string s) {
			val += s;
		}
		public void WriteLine(string s) {
			val += s + "\r\n";
		}
		public override string ToString() {
			return val;
		}
	}
	public static class SystemVariables {
		public static string Windows = "%windir%";
	}
}
