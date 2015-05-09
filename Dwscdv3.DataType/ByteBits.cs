using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwscdv3.DataType
{
    public struct ByteBits
    {
        byte bits;

        /// <summary>
        /// 用指定的 Boolean 值初始化所有变量。
        /// </summary>
        public ByteBits(bool b)
        {
            bits = b ? (byte)0xff : (byte)0;
        }

        /// <summary>
        /// 用指定的 Byte 值初始化变量。例如，0x0d = 0b00001101，即第5, 6, 8个 Boolean 变量为 true。
        /// </summary>
        /// <param name="b"></param>
        public ByteBits(byte b)
        {
            bits = b;
            /*
            byte buffer = 0;
            for (byte i = 0; i < 8; i++)
            {
                if (this[i])
                {
                    buffer |= (byte)(1 << 7 - i);
                }
                else
                {
                    bits &= (byte)~(1 << 7 - i);
                }
            }
            bits = buffer;
            */
        }

        /// <summary>
        /// 用指定的 Boolean 数组给每个变量分别赋值。
        /// </summary>
        /// <param name="b"></param>
        public ByteBits(bool[] b)
        {
            if (b.Length == 8)
            {
                bits = 0;
                for (byte i = 0; i < 8; i++)
                {
                    this[i] = b[i];
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static implicit operator ByteBits(byte arg)
        {
            return new ByteBits(arg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">Byte 类型，范围0-7。</param>
        /// <returns></returns>
        public bool this[byte index]
        {
            get
            {
                if (index < 8)
                {
                    return (bits & (1 << 7 - index)) != 0;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (index < 8)
                {
                    if (value)
                    {
                        bits |= (byte)(1 << 7 - index);
                    }
                    else
                    {
                        bits &= (byte)~(1 << 7 - index);
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// 返回所有8个 Boolean 的值，分隔符为空格。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = "";
            for (byte b = 0; b < 8; b++)
            {
                s += this[b].ToString() + " ";
            }
            s += "\b";
            return s;
        }

        /// <summary>
        /// 返回一个包含所有8个 Boolean 的值的数组。
        /// </summary>
        /// <returns></returns>
        public bool[] ToBoolArray()
        {
            bool[] buffer = new bool[8];
            for (byte b = 0; b < 8; b++)
            {
                buffer[b] = this[b];
            }
            return buffer;
        }
    }
}
