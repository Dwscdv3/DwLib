using System;
using System.Collections.Generic;

namespace Dwscdv3.Random {
	/// <summary>
	/// 表示不重复的伪随机数生成器。
	/// </summary>
	public class NoRepeatRandom {
		System.Random r;
		List<int> ar;
		/// <summary>
		/// 获取该伪随机数序列内元素的总数量。
		/// </summary>
		public int Total { get { return ar.Count; } }
		int left;
		/// <summary>
		/// 获取该随机数序列内元素的剩余数量。
		/// </summary>
		public int Left { get { return left; } }
		/// <summary>
		/// 使用与时间相关的默认种子值，初始化 Dwscdv3.Math.NoRepeatRandom 类的新实例。
		/// </summary>
		/// <param name="Amount">生成的不重复伪随机数的数量。默认下界为0。</param>
		public NoRepeatRandom(int Amount) {
			r = new System.Random();
			ar = InitializeArray(0, Amount);
		}
		/// <summary>
		/// 使用与时间相关的默认种子值，初始化 Dwscdv3.Math.NoRepeatRandom 类的新实例。
		/// </summary>
		/// <param name="Min">生成的不重复伪随机数的下界（伪随机数可取该下界值）。</param>
		/// <param name="Max">生成的不重复伪随机数的上界（伪随机数不可取该上界值）。</param>
		public NoRepeatRandom(int Min, int Max) {
			r = new System.Random();
			ar = InitializeArray(Min, Max);
		}
		/// <summary>
		/// 返回一个未出现过的伪随机数。
		/// </summary>
		public int Next() {
			if (left <= 0) {
				throw new Exception("随机数序列已用尽。请初始化一个新实例。");
			} else {
				int index = r.Next(left);
				int result = (int)ar[index];
				ar[index] = ar[left - 1];
				left--;
				return result;
			}
		}
		/// <summary>
		/// 从数列中移除指定的数值。
		/// </summary>
		public void Except(int i) {
			if (Left < Total) {
				throw new Exception("排除功能只能在输出数字前使用。");
			} if (i < ar[0] && i > ar[ar.Count - 1]) {
				throw new Exception("指定的数字不在序列中。");
			}
			int index0 = ar[0];
			ar[i - index0] = ar[--left];
			ar.RemoveAt(ar.Count - 1);
		}
		/// <summary>
		/// 从数列中移除指定范围内的数值。
		/// </summary>
		public void ExceptRange(int start, int length) {
			for (int i = 0; i < length; i++) {
				Except(start + i);
			}
		}
		List<int> InitializeArray(int Min, int Max) {
			if (Min >= Max) {
				throw new Exception("上界应大于下界。");
			} else {
				List<int> buffer = new List<int>();
				for (int i = 0; i < Max - Min; i++) {
					buffer.Add(Min + i);
				}
				left = buffer.Count;
				return buffer;
			}
		}
	}
}
