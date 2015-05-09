using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Dwscdv3.Cryptography
{
	public class DwCode {
		/// <summary>
		/// 警告：调用此方法后实参容量将被扩容为 width * 3 的倍数。
		/// </summary>
		public static Bitmap GetBitmap(byte[] b, int width) {
			bool needFix = b.Length % (width * 3) != 0;
			int height = needFix ? b.Length / (width * 3) + 1 : b.Length / (width * 3);
			Bitmap bmp = new Bitmap(width, height);
			if (needFix) {
				Array.Resize<byte>(ref b, ((b.Length / (width * 3)) + 1) * width * 3);
			}
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					bmp.SetPixel(j, i, Color.FromArgb(b[i * width * 3 + j * 3], 
													  b[i * width * 3 + j * 3 + 1], 
													  b[i * width * 3 + j * 3 + 2]));
				}
			}
			return bmp;
		}

		public static byte[] GetBytes(Bitmap bmp) {
			byte[] b = new byte[bmp.Width * bmp.Height * 3];
			for (int i = 0; i < bmp.Height; i++) {
				for (int j = 0; j < bmp.Width; j++) {
					Color c = bmp.GetPixel(j, i);
					b[i * bmp.Width * 3 + j * 3] = c.R;
					b[i * bmp.Width * 3 + j * 3 + 1] = c.G;
					b[i * bmp.Width * 3 + j * 3 + 2] = c.B;
				}
			}
			return b;
		}
	}
}
