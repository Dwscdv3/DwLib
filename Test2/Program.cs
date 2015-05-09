using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Dwscdv3.Collections;
using Dwscdv3.Random;
using Microsoft.Win32;

namespace Test2 {
	class Program {
		static unsafe void Main(string[] args) {
			Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();
			NoRepeatRandom r = new NoRepeatRandom(-50000, 50000); int[] array = new int[r.Total];
			for (int i = 0; i < r.Total; i++) {
				array[i] = r.Next();
			}
			stopwatch.Stop(); long genTime = stopwatch.ElapsedMilliseconds;
			//输出排序前数组
			//stopwatch.Reset(); stopwatch.Start();
			//int[] result = Sort.BeadSort(array);
			//stopwatch.Stop(); long sortTime = stopwatch.ElapsedMilliseconds;
			//for (int i = 0; i < result.Length; i++)
			//{
			//	Console.Write(result[i] + " ");
			//}
			//Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
			stopwatch.Reset(); stopwatch.Start();
			//根据源代码显示，.NET框架自带排序使用快排，4.5版本后预测到快排效率低时会改用插排或堆排。
			//Array.Sort(array);							//框架自带排序：           138ms / 1000000个
			//												//                        12ms /  100000个
			//												//
			//Sort.QuickSort(array);						//　　快速排序：           136ms / 1000000个
			//												//                        11ms /  100000个
			//Sort.HeapSort(array);							//　　　堆排序：           246ms / 1000000个
			//												//　　　　　　　            16ms /  100000个
			//Sort.ShellSort(array);						//　　希尔排序：           363ms / 1000000个
			//												//　　　　　　　            27ms /  100000个
			//Sort.MergeSort(array);						//　　归并排序：           445ms / 1000000个
			//												//                        58ms /  100000个
			//												//
			//Sort.InsertionSort(array);					//　　插入排序：          5884ms /  100000个
			//												//                        57ms /   10000个
			//Sort.SelectionSort(array);					//　　选择排序：          5881ms /  100000个
			//												//						  61ms /   10000个
			//Sort.BubbleSort(array);//	这货	为什	么这	么慢	……	//　　冒泡排序：         25569ms /  100000个
			//												//　　　　　　　          1019ms /   10000个
			//												//　　　　　　　            11ms /    1000个
			//很小众的算法，用CPU计算慢出翔						//
			//Sort.BeadSort(array);							//　　　珠排序：   >16min(broke) /   10000个
			//												//　　　　　　　          6650ms /    1000个
			//这两个是卖萌的，不要指望得出统计数据了……			//
			//Sort.BogoSort(array);							//
			//Sort.BozoSort(array);							//
			stopwatch.Stop(); long sort2Time = stopwatch.ElapsedMilliseconds;
			//输出排序后数组
			//for (int i = 0; i < array.Length; i++) {
			//	Console.Write(array[i] + " ");
			//}
			Console.WriteLine(string.Format("\n数量: {2}个, 生成: {0}ms, 排序: {1}ms", genTime, sort2Time, array.Length));
			Console.WriteLine("检查数列状态...");
			Console.WriteLine(Sort.IsOrdered(array) ? "状态：有序" : "状态：无序");
		}
	}
}
