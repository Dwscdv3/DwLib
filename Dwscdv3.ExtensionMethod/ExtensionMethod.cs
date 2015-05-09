using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwscdv3.ExtensionMethod
{
    public static class PrimitiveTypeExt
    {
        #region Reverse(十进制反转)
        public static sbyte Reverse(this sbyte arg)
        {
            checked
            {
                sbyte result = 0;
                for (; arg != 0; arg /= 10)
                {
                    result = (sbyte)(result * 10 + arg % 10);
                }
                return result;
            }
        }

        public static byte Reverse(this byte arg)
        {
            checked
            {
                byte result = 0;
                for (; arg != 0; arg /= 10)
                {
                    result = (byte)(result * 10 + arg % 10);
                }
                return result;
            }
        }

        public static short Reverse(this short arg)
        {
            checked
            {
                short result = 0;
                for (; arg != 0; arg /= 10)
                {
                    result = (short)(result * 10 + arg % 10);
                }
                return result;
            }
        }

        public static ushort Reverse(this ushort arg)
        {
            checked
            {
                ushort result = 0;
                for (; arg != 0; arg /= 10)
                {
                    result = (ushort)(result * 10 + arg % 10);
                }
                return result;
            }
        }

        public static int Reverse(this int arg)
        {
            checked
            {
                int result = 0;
                for (; arg != 0; arg /= 10)
                {
                    result = result * 10 + arg % 10;
                }
                return result;
            }
        }

        public static uint Reverse(this uint arg)
        {
            checked
            {
                uint result = 0;
                for (; arg != 0; arg /= 10)
                {
                    result = result * 10 + arg % 10;
                }
                return result;
            }
        }

        public static long Reverse(this long arg)
        {
            checked
            {
                long result = 0;
                for (; arg != 0; arg /= 10)
                {
                    result = result * 10 + arg % 10;
                }
                return result;
            }
        }

        public static ulong Reverse(this ulong arg)
        {
            checked
            {
                ulong result = 0;
                for (; arg != 0; arg /= 10)
                {
                    result = result * 10 + arg % 10;
                }
                return result;
            }
        }
        #endregion

        /*
        #region 隐式转换 - Boolean to Any
        public static implicit operator sbyte(bool arg)
        {
            return arg ? (sbyte)1 : (sbyte)0;
        }

        public static implicit operator byte(bool arg)
        {
            return arg ? (byte)1 : (byte)0;
        }

        public static implicit operator short(bool arg)
        {
            return arg ? (short)1 : (short)0;
        }

        public static implicit operator ushort(bool arg)
        {
            return arg ? (ushort)1 : (ushort)0;
        }

        public static implicit operator int(bool arg)
        {
            return arg ? 1 : 0;
        }

        public static implicit operator uint(bool arg)
        {
            return arg ? (uint)1 : (uint)0;
        }

        public static implicit operator long(bool arg)
        {
            return arg ? 1 : 0;
        }

        public static implicit operator ulong(bool arg)
        {
            return arg ? (ulong)1 : (ulong)0;
        }
        #endregion
        */
    }

    public static class RandomExt
    {
        #region Random.NextLong(生成64位整数随机数 - 本方法有问题，因Double类型精度比Int64低)
        /// <summary>
        /// 返回非负随机数。
        /// </summary>
        /// <returns></returns>
        public static long NextLong(this System.Random r)
        {
            return (long)(r.NextDouble() * long.MaxValue);
        }

        /// <summary>
        /// 返回一个小于所指定最大值的非负随机数。
        /// </summary>
        /// <param name="maxValue">要生成的随机数的上界（随机数不能取该上界值）。<paramref name="maxValue"/> 必须大于等于0。</param>
        /// <returns></returns>
        public static long NextLong(this System.Random r, long maxValue)
        {
            if (maxValue > 0)
            {
                return (long)(r.NextDouble() * maxValue);
            }
            else
            {
                throw new ArgumentException("上界应大于0。如果您要生成负数，请使用该方法的其它重载版本。", "maxValue");
            }
        }

        /// <summary>
        /// 返回一个指定范围内的随机数。
        /// </summary>
        /// <param name="minValue">返回的随机数的下界（随机数可取该下界值）。</param>
        /// <param name="maxValue">返回的随机数的上界（随机数不能取该上界值）。<paramref name="maxValue"/> 必须大于等于 <paramref name="minValue"/>。</param>
        /// <returns></returns>
        public static long NextLong(this System.Random r, long minValue, long maxValue)
        {
            if (maxValue > minValue)
            {
                return (long)(r.NextDouble() * (maxValue - minValue) + minValue);
            }
            else
            {
                throw new ArgumentException("上界应大于下界。");
            }
        }
        #endregion
    }

	public static class ArrayExt {
		#region Swap
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this bool[] a, int b, int c) {
			bool d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this sbyte[] a, int b, int c) {
			sbyte d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this byte[] a, int b, int c) {
			byte d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this short[] a, int b, int c) {
			short d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this ushort[] a, int b, int c) {
			ushort d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this int[] a, int b, int c) {
			int d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this uint[] a, int b, int c) {
			uint d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this long[] a, int b, int c) {
			long d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this ulong[] a, int b, int c) {
			ulong d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this float[] a, int b, int c) {
			float d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this double[] a, int b, int c) {
			double d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this decimal[] a, int b, int c) {
			decimal d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		/// <summary>
		/// 交换数组中两个索引的位置。
		/// </summary>
		public static void Swap(this object[] a, int b, int c) {
			object d = a[b];
			a[b] = a[c];
			a[c] = d;
		}
		#endregion

		#region Insert
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		public static void Insert(this bool[] array, bool value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<bool>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		public static void Insert(this sbyte[] array, sbyte value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<sbyte>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		public static void Insert(this byte[] array, byte value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<byte>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		/// 
		public static void Insert(this short[] array, short value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<short>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		/// 
		public static void Insert(this ushort[] array, ushort value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<ushort>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		/// 
		public static void Insert(this int[] array, int value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<int>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		/// 
		public static void Insert(this uint[] array, uint value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<uint>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		public static void Insert(this long[] array, long value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<long>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		public static void Insert(this ulong[] array, ulong value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<ulong>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		public static void Insert(this float[] array, float value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<float>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		public static void Insert(this double[] array, double value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<double>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		public static void Insert(this decimal[] array, decimal value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<decimal>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		/// <summary>
		/// 在指定索引处插入新项。插入位置以后的项向后移动 1 个索引。
		/// </summary>
		/// <param name="value">指定插入项的值。</param>
		/// <param name="insertIndex">指定要插入的索引位置。</param>
		/// <param name="resize">指定是否应扩大数组容量。如果不扩大，数组的最后一个元素将丢失。</param>
		public static void Insert(this object[] array, object value, int insertIndex, bool resize = true) {
			if (resize) {
				Array.Resize<object>(ref array, array.Length + 1);
			}
			for (int i = array.Length - 1; i > insertIndex; i--) {
				array[i] = array[i - 1];
			}
			array[insertIndex] = value;
		}
		#endregion
	}
}
