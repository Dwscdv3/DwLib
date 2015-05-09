using System;
using Dwscdv3.ExtensionMethod;
using Dwscdv3.Random;

namespace Dwscdv3.Collections {
	/// <summary>
	/// 与排序相关的静态方法。
	/// </summary>
	public static class Sort {
		#region QuickSort/快速排序 @MoreWindows - 白话经典算法系列
		public static void QuickSort(int[] array) {
			QuickSort(array, 0, array.Length - 1);
		}
		static void QuickSort(int[] s, int l, int r) {
			if (l < r) {
				//Swap(s[l], s[(l + r) / 2]);		//[可选]将中间的这个数和第一个数交换
				int i = l, j = r, x = s[l];
				while (i < j) {
					while (i < j && s[j] >= x) {	// 从右向左找第一个小于x的数  
						j--;
					} if (i < j) {
						s[i++] = s[j];
					} while (i < j && s[i] < x) {		// 从左向右找第一个大于等于x的数  
						i++;
					} if (i < j) {
						s[j--] = s[i];
					}
				}
				s[i] = x;
				QuickSort(s, l, i - 1);
				QuickSort(s, i + 1, r);
			}
		}
		#endregion
		#region MergeSort/归并排序 @Dwscdv3
		#region MergeSort/归并排序(Int32)
		public static void MergeSort(int[] array) {
			if (array.Length == 1) {
				return;
			} else {
				int[] arg1 = split(array, false);
				int[] arg2 = split(array, true);
				MergeSort(arg1);
				MergeSort(arg2);
				addSentinel(ref arg1);
				addSentinel(ref arg2);
				int j = 0, k = 0;
				for (int i = 0; i < array.Length; i++) {
					if (arg1[j] <= arg2[k]) {
						array[i] = arg1[j];
						j++;
					} else {
						array[i] = arg2[k];
						k++;
					}
				}
			}
		}

		private static int[] split(int[] arg, bool theOther) {
			int half = arg.Length / 2;
			bool odd = false;
			if (arg.Length % 2 == 1) {
				odd = true;
			}
			if (!theOther) {
				int[] result = new int[half];
				for (int i = 0; i < half; i++) {
					result[i] = arg[i];
				}
				return result;
			} else {
				int[] result;
				if (odd) {
					result = new int[half + 1];
				} else {
					result = new int[half];
				}
				for (int i = half; i < arg.Length; i++) {
					result[i - half] = arg[i];
				}
				return result;
			}
		}

		private static void addSentinel(ref int[] arg) {
			Array.Resize<int>(ref arg, arg.Length + 1);
			arg[arg.Length - 1] = int.MaxValue;
		}
		#endregion
		#region MergeSort/归并排序(Int64)
		public static void MergeSort(long[] array) {
			if (array.Length == 1) {
				return;
			} else {
				long[] arg1 = split(array, false);
				long[] arg2 = split(array, true);
				MergeSort(arg1);
				MergeSort(arg2);
				addSentinel(ref arg1);
				addSentinel(ref arg2);
				int j = 0, k = 0;
				for (int i = 0; i < array.Length; i++) {
					if (arg1[j] <= arg2[k]) {
						array[i] = arg1[j];
						j++;
					} else {
						array[i] = arg2[k];
						k++;
					}
				}
			}
		}

		private static long[] split(long[] arg, bool theOther) {
			int half = arg.Length / 2;
			bool odd = false;
			if (arg.Length % 2 == 1) {
				odd = true;
			}
			if (!theOther) {
				long[] result = new long[half];
				for (int i = 0; i < half; i++) {
					result[i] = arg[i];
				}
				return result;
			} else {
				long[] result;
				if (odd) {
					result = new long[half + 1];
				} else {
					result = new long[half];
				}
				for (int i = half; i < arg.Length; i++) {
					result[i - half] = arg[i];
				}
				return result;
			}
		}

		private static void addSentinel(ref long[] arg) {
			Array.Resize<long>(ref arg, arg.Length + 1);
			arg[arg.Length - 1] = long.MaxValue;
		}
		#endregion
		#endregion
		#region HeapSort/堆排序 @零基础学算法
		static void HeapAdjust(int[] a, int s, int n) {
			int j, t;
			while (2 * s + 1 < n) {						//第s个结点有右子树
				j = 2 * s + 1;
				if ((j + 1) < n) {
					if (a[j] < a[j + 1])					//右左子树小于右子树，则需要比较右子树
						j++;						//序号增加1，指向右子树
				}
				if (a[s] < a[j]) {						//比较s与j为序号的数据
					t = a[s];							//交换数据 
					a[s] = a[j];
					a[j] = t;
					s = j;							//堆被破坏，需要重新调整
				} else {								//比较左右孩子均大则堆未破坏，不再需要调整
					break;
				}
			}
		}
		public static void HeapSort(int[] a) {
			int t, i, n = a.Length;
			for (i = n / 2 - 1; i >= 0; i--) {		//将a[0,n-1]建成大根堆
				HeapAdjust(a, i, n);
			}
			for (i = n - 1; i > 0; i--) {
				t = a[0];							//与第i个记录交换
				a[0] = a[i];
				a[i] = t;
				HeapAdjust(a, 0, i);				//将a[0]至a[i]重新调整为堆
			}
		}
		#endregion
		#region ShellSort/希尔排序 @零基础学算法
		public static void ShellSort(int[] array) {
			int i, j, x, n = array.Length, d = n / 2;
			while (d >= 1) {//循环至增量为1时结束
				for (i = d; i < n; i++) {
					x = array[i]; //获取序列中的下一个数据 
					j = i - d; //序列中前一个数据的序号 
					while (j >= 0 && array[j] > x) {//下一个数大于前一个数
						array[j + d] = array[j]; //将后一个数向前移动 
						j = j - d; //修改序号，继续向前比较 
					}
					array[j + d] = x; //保存数据 
				}
				d /= 2;  //缩小增量 
			}
		}
		#endregion
		#region InsertionSort/插入排序 @Dwscdv3
		/// <summary>
		/// 插入排序。
		/// </summary>
		public static void InsertionSort(int[] array) {
			for (int i = 0; i < array.Length; i++) {
				int j = 0;
				for (; j < i; j++) {
					if (array[i] <= array[j]) {
						break;
					}
				}
				int k = array[i];
				for (int l = i; l > j; l--) {
					array[l] = array[l - 1];
				}
				array[j] = k;
			}
		}

		/// <summary>
		/// 插入排序。
		/// </summary>
		public static void InsertionSort(long[] array) {
			for (int i = 0; i < array.Length; i++) {
				int j = 0;
				for (; j < i; j++) {
					if (array[i] <= array[j]) {
						break;
					}
				}
				long k = array[i];
				for (int l = i; l > j; l--) {
					array[l] = array[l - 1];
				}
				array[j] = k;
			}
		}
		#endregion
		#region SelectionSort/选择排序 @Dwscdv3
		/// <summary>
		/// 选择排序。
		/// </summary>
		public static void SelectionSort(int[] array) {
			for (int i = 0; i < array.Length; i++) {
				int min = int.MaxValue;
				int index = 0;
				for (int j = i; j < array.Length; j++) {
					if (array[j] < min) {
						min = array[j];
						index = j;
					}
				}
				array.Swap(i, index);
			}
		}

		/// <summary>
		/// 选择排序。
		/// </summary>
		public static void SelectionSort(long[] array) {
			for (int i = 0; i < array.Length; i++) {
				long min = long.MaxValue;
				int index = 0;
				for (int j = i; j < array.Length; j++) {
					if (array[j] < min) {
						min = array[j];
						index = j;
					}
				}
				array.Swap(i, index);
			}
		}
		#endregion
		#region BubbleSort/冒泡排序 @Dwscdv3
		/// <summary>
		/// 改进的冒泡排序。<para>此版本对于基本有序的序列效率较高。</para>
		/// </summary>
		public static void BubbleSort(int[] array) {
			int k, flag = array.Length;
			while (flag > 0) {
				k = flag;
				flag = 0;
				for (int i = 1; i < k; i++) {
					if (array[i - 1] > array[i]) {
						array.Swap(i - 1, i);
						flag = i;
					}
				}
			}
		}
		/// <summary>
		/// 最原始的冒泡排序。<para>此版本对于完全随机的序列效率较高。</para>
		/// </summary>
		public static void OriginalBubbleSort(int[] array) {
			for (int i = 0; i < array.Length; i++) {
				for (int j = 1; j < array.Length - i; j++) {
					if (array[j - 1] > array[j]) {
						array.Swap(j - 1, j);
					}
				}
			}
		}
		/// <summary>
		/// 改进的冒泡排序。<para>此版本对于基本有序的序列效率较高。</para>
		/// </summary>
		public static void BubbleSort(long[] array) {
			int k, flag = array.Length;
			while (flag > 0) {
				k = flag;
				flag = 0;
				for (int i = 1; i < k; i++) {
					if (array[i - 1] > array[i]) {
						array.Swap(i - 1, i);
						flag = i;
					}
				}
			}
		}
		/// <summary>
		/// 最原始的冒泡排序。<para>此版本对于完全随机的序列效率较高。</para>
		/// </summary>
		public static void OriginalBubbleSort(long[] array) {
			for (int i = 0; i < array.Length; i++) {
				for (int j = 1; j < array.Length - i; j++) {
					if (array[j - 1] > array[j]) {
						array.Swap(j - 1, j);
					}
				}
			}
		}
		#endregion
		#region Funny & Worthless @Dwscdv3

		/// <summary>
		/// 珠排序。
		/// </summary>
		public static void BeadSort(int[] arg) {
			int min = int.MaxValue;
			int max = int.MinValue;
			for (int i = 0; i < arg.Length; i++) {
				if (arg[i] < min) {
					min = arg[i];
				}
				if (arg[i] > max) {
					max = arg[i];
				}
			}
			byte[,] buffer = new byte[arg.Length, max - min + 1];
			for (int i = 0; i < arg.Length; i++) {
				for (int j = 0; j < arg[i] - min; j++) {
					buffer[i, j] = 1;
				}
			}
			bool isFinished = true;
			do {
				isFinished = true;
				for (int i = 0; i < arg.Length - 1; i++) {
					int j = 0;
					while (true) {
						if (buffer[i, j] == 1) {
							if (buffer[i + 1, j] == 0) {
								isFinished = false;
								buffer[i, j] = 0;
								buffer[i + 1, j] = 1;
							}
							j++;
						} else {
							break;
						}
					}
				}
			}
			while (!isFinished);
			int[] result = new int[arg.Length];
			for (int i = 0; i < arg.Length; i++) {
				int j = 0;
				while (true) {
					if (buffer[i, j] == 1) {
						j++;
					} else {
						break;
					}
				}
				result[i] = j + min;
			}
			arg = result;
		}

		//#region SleepSort/睡眠排序
		//static string SleepSortBuffer;
		//static int SleepSortCount;

		//public static int[] SleepSort(int[] arg)
		//{
		//	SleepSortBuffer = "";
		//	SleepSortCount = 0;
		//	for (int i = 0; i < arg.Length; i++)
		//	{
		//		Thread t = new Thread(SleepSortThread);
		//		t.Start(arg[i]);
		//	}
		//	Thread.Sleep(arg.Max());
		//	SleepSortBuffer += "\b";
		//	string[] str = SleepSortBuffer.Split(' ');
		//	int[] result = new int[arg.Length];
		//	for (int i = 0; i < arg.Length; i++)
		//	{
		//		result[i] = int.Parse(str[i]);
		//	}
		//	return result;
		//}

		//private static void SleepSortThread(object arg)
		//{
		//	Thread.Sleep((int)arg * 1);
		//	SleepSortBuffer += ((int)arg).ToString() + " ";
		//	SleepSortCount++;
		//}
		//#endregion

		/// <summary>
		/// Bogo排序：将一组数字随机打乱，并检查是否有序。若否，则重新打乱，直到有序为止。
		/// <para>警告：此排序算法纯属恶搞，请勿用于正常项目！</para>
		/// </summary>
		public static void BogoSort(int[] arg) {
			int[] result = (int[])arg.Clone();
			goto Start;
		Restart:
			NoRepeatRandom r = new NoRepeatRandom(arg.Length);
			for (int i = 0; i < arg.Length; i++) {
				result[i] = arg[r.Next()];
			}
		Start:
			for (int i = 0; i < arg.Length - 1; i++) {
				if (result[i] > result[i + 1]) {
					goto Restart;
				}
			}
			arg = result;
		}

		/// <summary>
		/// Bozo排序：交换任意两个数的位置，并检查是否有序。若否，则再次交换任意两个数的位置，直到有序为止。
		/// <para>Bogo排序的改进版。</para>
		/// <para>警告：此排序算法纯属恶搞，请勿用于正常项目！</para>
		/// </summary>
		public static void BozoSort(int[] arg) {
			System.Random r = new System.Random();
			while (!IsOrdered(arg)) {
				int index1 = r.Next(arg.Length), index2 = r.Next(arg.Length);
				int i = arg[index1];
				arg[index1] = arg[index2];
				arg[index2] = i;
			}
		}

		#endregion
		#region Assists
		/// <summary>
		/// 检查指定的数组是否有序。
		/// </summary>
		public static bool IsOrdered(int[] arg) {
			for (long i = 0; i < arg.Length - 1; i++) {
				if (arg[i] > arg[i + 1]) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 检查指定的数组是否有序。
		/// </summary>
		public static bool IsOrdered(long[] arg) {
			for (long i = 0; i < arg.Length - 1; i++) {
				if (arg[i] > arg[i + 1]) {
					return false;
				}
			}
			return true;
		}
		#endregion
	}
}
