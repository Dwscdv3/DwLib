using System;

namespace Dwscdv3.Simulator.Assembly {
	public class Hardware {
		//const char[] CharSet = {
		//	'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 
		//	'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 
		//	'<', '>', '[', ']', 
		//	'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 
		//	'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 
		//	'(', ')', '{', '}', 
		//	'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
		//	',', '.', '?', '!', '#', '$', '%', '&', 
		//	'+', '-', '*', '/', '=', 
		//	'\\', 
		//	'\'', '"', 
		//};

		short VideoX, VideoY;

		public short A, B;		//寄存器
		public byte[] RAM;		//内存
		public byte[] VRAM;		//视频内存
		public byte[] Storage;	//存储

		public Hardware(short RAMSize = 4096, short VideoWidth = 80, short VideoHeight = 25, short StorageSize = 0, string StorageFilePath = null) {
			RAM = new byte[RAMSize];
			VideoX = VideoWidth;
			VideoY = VideoHeight;
			VRAM = new byte[VideoWidth * VideoHeight];
			if (!string.IsNullOrWhiteSpace(StorageFilePath)) {
				Storage = new byte[StorageSize];
			}
		}
	}
}