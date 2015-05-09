using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Dwscdv3.Win32API {
	public static class User32 {
		public struct RECT {
			public int left;
			public int top;
			public int right;
			public int bottom;
		}
		
		public static class Cursor {
			/// <summary>
			/// 获取鼠标指针位置。
			/// </summary>
			[DllImport("user32.dll")]
			public static extern int GetCursorPos(ref Point p);
			/// <summary>
			/// 设置鼠标指针位置。
			/// </summary>
			[DllImport("user32.dll")]
			public static extern int SetCursorPos(int X, int Y);
			/// <summary>
			/// 限制鼠标指针的活动范围。
			/// </summary>
			[DllImport("user32.dll")]
			public static extern bool ClipCursor(ref RECT lpRect);

			public static Point GetCursorPos() {
				Point p = new Point();
				GetCursorPos(ref p);
				return p;
			}
			public static void MoveRelative(int x, int y) {
				Point p = GetCursorPos();
				SetCursorPos(p.X + x, p.Y + y);
			}
		}
		public static class Mouse {
			public enum MouseFlags {
				Move = 0x0001,
				LeftDown = 0x0002,
				LeftUp = 0x0004,
				RightDown = 0x0008,
				RightUp = 0x0010,
				MiddleDown = 0x0020,
				MiddleUp = 0x0040,
				XDown = 0x0080,
				XUp = 0x0100,
				Wheel = 0x0800,
				HWheel = 0x1000,
				Absolute = 0x8000
			}

			/// <summary>
			/// 模拟鼠标动作。
			/// </summary>
			/// <param name="dx">Flag: Absolute</param>
			/// <param name="dy">Flag: Absolute</param>
			/// <param name="dwData">Flag: Wheel, HWheel, XDown, XUp</param>
			[DllImport("user32.dll")]
			public static extern int mouse_event(MouseFlags dwFlags, int dx, int dy, int dwData, uint dwExtraInfo);
		}
		public static class Window {
			public enum ExtendedWindowStylesFlags {
				ACCEPTFILES			= 0x00000010,
				APPWINDOW			= 0x00040000,
				CLIENTEDGE			= 0x00000200,
				COMPOSITED			= 0x02000000,
				CONTEXTHELP			= 0x00000400,
				CONTROLPARENT		= 0x00010000,
				DLGMODALFRAME		= 0x00000001,
				LAYERED				= 0x00080000,
				LAYOUTRTL			= 0x00400000,
				LEFT				= 0x00000000,
				LEFTSCROLLBAR		= 0x00004000,
				LTRREADING			= 0x00000000,
				MDICHILD			= 0x00000040,
				NOACTIVATE			= 0x08000000,
				NOINHERITLAYOUT		= 0x00100000,
				NOPARENTNOTIFY		= 0x00000004,
				NOREDIRECTIONBITMAP	= 0x00200000,
				OVERLAPPEDWINDOW	= (WINDOWEDGE
									  |CLIENTEDGE),
				PALETTEWINDOW		= (WINDOWEDGE
									  |TOOLWINDOW
									  |TOPMOST),
				RIGHT				= 0x00001000,
				RIGHTSCROLLBAR		= 0x00000000,
				RTLREADING			= 0x00002000,
				STATICEDGE			= 0x00020000,
				TOOLWINDOW			= 0x00000080,
				TOPMOST				= 0x00000008,
				TRANSPARENT			= 0x00000020,
				WINDOWEDGE			= 0x00000100
			}
			public enum GetWindowLongFlags {
				EXSTYLE = -20,
				HINSTANCE = -6,
				ID = -12,
				STYLE = -16,
				USERDATA = -21,
				WNDPROC = -4
			}
			[DllImport("user32.dll")]
			public static extern int GetWindowLong(IntPtr hWnd, GetWindowLongFlags nIndex);
			[DllImport("user32.dll")]
			public static extern int SetWindowLong(IntPtr hWnd, GetWindowLongFlags nIndex, ExtendedWindowStylesFlags dwNewLong);
		}
		public static class WindowsMessage {
			[DllImport("user32.dll")]
			public static extern object SendMessage(IntPtr hWnd, uint Msg, object wParam, object lParam);
			[DllImport("user32.dll")]
			public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
			[DllImport("user32.dll")]
			public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
			[DllImport("user32.dll")]
			public static extern bool PostMessage(IntPtr hWnd, uint Msg, object wParam, object lParam);
			[DllImport("user32.dll")]
			public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
			[DllImport("user32.dll")]
			public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
		}
	}
}