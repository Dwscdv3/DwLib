using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dwscdv3.ExtensionMethod;

namespace Test_扩展方法 {
	class Program {
		static void Main(string[] args) {
			int[] arr = { 0, 1 };
			arr.Swap(0, 1);
			Console.WriteLine(arr[0] + ", " + arr[1]);
			
		}
	}
}
