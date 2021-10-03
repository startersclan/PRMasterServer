using System;

namespace Reality.Net.GameSpy.Servers
{
	public class GSEncoding
	{
		public static string GetMasterServerHostname(string gamename)
		{
			return "ms.prm.so";
		}

		public static byte[] CompilePacket(string gamename, byte[] validate, string fields, int type, string filter = "")
		{
			string text = string.Format("\0\u0001\u0003\0\0\0\0{0}\0{0}\0{1}{2}\0{3}\0\0\0\0{4}", gamename, DataFunctions.BytesToString(validate), filter, fields, smethod_10(type));
			int num = text.Length + 2;
			text = $"{Convert.ToChar(num >> 8)}{smethod_10(num)}{text}";
			return DataFunctions.StringToBytes(text);
		}

		public static byte[] GenerateValidationKey()
		{
			long num = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).Ticks / 10000L;
			byte[] array = new byte[8];
			for (int i = 0; i < 8; i++)
			{
				do
				{
					num = (num * 214013L + 2531011L) & 0x7FL;
				}
				while (num < 33L || num >= 127L);
				array[i] = (byte)num;
			}
			return array;
		}

		public static byte[] Decode(byte[] key, byte[] validate, byte[] data, long size)
		{
			if (key != null && validate != null && data != null && size >= 0L)
			{
				return smethod_0(key, validate, data, size, null);
			}
			return null;
		}

		private static byte[] smethod_0(byte[] byte_0, byte[] byte_1, byte[] byte_2, long long_0, GSEncodingData gsencodingData_0)
		{
			byte[] array = new byte[261];
			byte[] byte_3 = ((gsencodingData_0 != null) ? gsencodingData_0.EncodingKey : array);
			if (gsencodingData_0 == null || gsencodingData_0.Start == 0L)
			{
				byte_2 = smethod_2(ref byte_3, ref byte_0, byte_1, ref byte_2, ref long_0, ref gsencodingData_0);
				if (byte_2 == null)
				{
					return null;
				}
			}
			if (gsencodingData_0 == null)
			{
				smethod_6(ref byte_3, ref byte_2, long_0);
				return byte_2;
			}
			if (gsencodingData_0.Start != 0L)
			{
				byte[] byte_4 = new byte[long_0 - gsencodingData_0.Offset];
				Array.ConstrainedCopy(byte_2, (int)gsencodingData_0.Offset, byte_4, 0, (int)(long_0 - gsencodingData_0.Offset));
				long num = smethod_6(ref byte_3, ref byte_4, long_0 - gsencodingData_0.Offset);
				Array.ConstrainedCopy(byte_4, 0, byte_2, (int)gsencodingData_0.Offset, (int)(long_0 - gsencodingData_0.Offset));
				gsencodingData_0.Offset += num;
				byte[] array2 = new byte[long_0 - gsencodingData_0.Start];
				Array.ConstrainedCopy(byte_2, (int)gsencodingData_0.Start, array2, 0, (int)(long_0 - gsencodingData_0.Start));
				return array2;
			}
			return null;
		}

		public static byte[] Encode(byte[] key, byte[] validate, byte[] data, long size)
		{
			byte[] array = new byte[size + 23L];
			byte[] array2 = new byte[23];
			if (key != null && validate != null && data != null && size >= 0L)
			{
				int num = key.Length;
				int num2 = validate.Length;
				int num3 = new Random().Next();
				for (int i = 0; i < array2.Length; i++)
				{
					num3 = num3 * 214013 + 2531011;
					array2[i] = (byte)((num3 ^ key[i % num] ^ validate[i % num2]) % 256);
				}
				array2[0] = 235;
				array2[1] = 0;
				array2[2] = 0;
				array2[8] = 228;
				for (long num4 = size - 1L; num4 >= 0L; num4--)
				{
					array[array2.Length + num4] = data[num4];
				}
				Array.Copy(array2, array, array2.Length);
				size += array2.Length;
				long long_ = size;
				byte[] array3 = smethod_1(key, validate, array, long_, null);
				byte[] array4 = new byte[array3.Length + array2.Length];
				Array.Copy(array2, 0, array4, 0, array2.Length);
				Array.Copy(array3, 0, array4, array2.Length, array3.Length);
				return array4;
			}
			return null;
		}

		private static byte[] smethod_1(byte[] byte_0, byte[] byte_1, byte[] byte_2, long long_0, GSEncodingData gsencodingData_0)
		{
			byte[] array = new byte[261];
			byte[] byte_3 = ((gsencodingData_0 != null) ? gsencodingData_0.EncodingKey : array);
			if (gsencodingData_0 == null || gsencodingData_0.Start == 0L)
			{
				byte_2 = smethod_2(ref byte_3, ref byte_0, byte_1, ref byte_2, ref long_0, ref gsencodingData_0);
				if (byte_2 == null)
				{
					return null;
				}
			}
			if (gsencodingData_0 == null)
			{
				smethod_7(ref byte_3, ref byte_2, long_0);
				return byte_2;
			}
			if (gsencodingData_0.Start != 0L)
			{
				byte[] byte_4 = new byte[long_0 - gsencodingData_0.Offset];
				Array.ConstrainedCopy(byte_2, (int)gsencodingData_0.Offset, byte_4, 0, (int)(long_0 - gsencodingData_0.Offset));
				long num = smethod_7(ref byte_3, ref byte_4, long_0 - gsencodingData_0.Offset);
				Array.ConstrainedCopy(byte_4, 0, byte_2, (int)gsencodingData_0.Offset, (int)(long_0 - gsencodingData_0.Offset));
				gsencodingData_0.Offset += num;
				byte[] array2 = new byte[long_0 - gsencodingData_0.Start];
				Array.ConstrainedCopy(byte_2, (int)gsencodingData_0.Start, array2, 0, (int)(long_0 - gsencodingData_0.Start));
				return array2;
			}
			return null;
		}

		private static byte[] smethod_2(ref byte[] byte_0, ref byte[] byte_1, byte[] byte_2, ref byte[] byte_3, ref long long_0, ref GSEncodingData gsencodingData_0)
		{
			long num = (byte_3[0] ^ 0xEC) + 2;
			byte[] byte_4 = new byte[8];
			if (long_0 < 1L)
			{
				return null;
			}
			if (long_0 < num)
			{
				return null;
			}
			long num2 = byte_3[num - 1L] ^ 0xEA;
			if (long_0 < num + num2)
			{
				return null;
			}
			Array.Copy(byte_2, byte_4, 8);
			byte[] array = new byte[long_0 - num];
			Array.ConstrainedCopy(byte_3, (int)num, array, 0, (int)(long_0 - num));
			smethod_3(ref byte_0, ref byte_1, ref byte_4, array, num2);
			Array.ConstrainedCopy(array, 0, byte_3, (int)num, (int)(long_0 - num));
			num += num2;
			if (gsencodingData_0 == null)
			{
				byte[] array2 = new byte[long_0 - num];
				Array.ConstrainedCopy(byte_3, (int)num, array2, 0, (int)(long_0 - num));
				byte_3 = array2;
				long_0 -= num;
			}
			else
			{
				gsencodingData_0.Offset = num;
				gsencodingData_0.Start = num;
			}
			return byte_3;
		}

		private static void smethod_3(ref byte[] byte_0, ref byte[] byte_1, ref byte[] byte_2, byte[] byte_3, long long_0)
		{
			long num = byte_1.Length;
			for (long num2 = 0L; num2 <= long_0 - 1L; num2++)
			{
				byte_2[(byte_1[num2 % num] * num2) & 7L] = (byte)(byte_2[(byte_1[num2 % num] * num2) & 7L] ^ byte_2[num2 & 7L] ^ byte_3[num2]);
			}
			long long_ = 8L;
			smethod_4(ref byte_0, ref byte_2, ref long_);
		}

		private static void smethod_4(ref byte[] byte_0, ref byte[] byte_1, ref long long_0)
		{
			long long_ = 0L;
			long long_2 = 0L;
			if (long_0 >= 1L)
			{
				for (long num = 0L; num <= 255L; num++)
				{
					byte_0[num] = (byte)num;
				}
				for (long num = 255L; num >= 0L; num--)
				{
					byte b = (byte)smethod_5(byte_0, num, byte_1, long_0, ref long_, ref long_2);
					byte b2 = byte_0[num];
					byte_0[num] = byte_0[b];
					byte_0[b] = b2;
				}
				byte_0[256] = byte_0[1];
				byte_0[257] = byte_0[3];
				byte_0[258] = byte_0[5];
				byte_0[259] = byte_0[7];
				byte_0[260] = byte_0[long_ & 0xFFL];
			}
		}

		private static long smethod_5(byte[] byte_0, long long_0, byte[] byte_1, long long_1, ref long long_2, ref long long_3)
		{
			long num = 0L;
			long num2 = 1L;
			if (long_0 == 0L)
			{
				return 0L;
			}
			if (long_0 > 1L)
			{
				do
				{
					num2 = (num2 << 1) + 1L;
				}
				while (num2 < long_0);
			}
			long num3;
			do
			{
				long_2 = byte_0[long_2 & 0xFFL] + byte_1[long_3];
				long_3++;
				if (long_3 >= long_1)
				{
					long_3 = 0L;
					long_2 += long_1;
				}
				num++;
				num3 = ((num <= 11L) ? (long_2 & num2) : (long_2 & (num2 % long_0)));
			}
			while (num3 > long_0);
			return num3;
		}

		private static long smethod_6(ref byte[] byte_0, ref byte[] byte_1, long long_0)
		{
			for (long num = 0L; num < long_0; num++)
			{
				byte_1[num] = smethod_8(ref byte_0, byte_1[num]);
			}
			return long_0;
		}

		private static long smethod_7(ref byte[] byte_0, ref byte[] byte_1, long long_0)
		{
			for (long num = 0L; num < long_0; num++)
			{
				byte_1[num] = smethod_9(ref byte_0, byte_1[num]);
			}
			return long_0;
		}

		private static byte smethod_8(ref byte[] byte_0, byte byte_1)
		{
			int num = byte_0[256];
			int num2 = byte_0[257];
			int num3 = byte_0[num];
			byte_0[256] = (byte)((num + 1) % 256);
			byte_0[257] = (byte)((num2 + num3) % 256);
			num = byte_0[260];
			num2 = byte_0[257];
			num2 = byte_0[num2];
			num3 = byte_0[num];
			byte_0[num] = (byte)num2;
			num = byte_0[259];
			num2 = byte_0[257];
			num = byte_0[num];
			byte_0[num2] = (byte)num;
			num = byte_0[256];
			num2 = byte_0[259];
			num = byte_0[num];
			byte_0[num2] = (byte)num;
			num = byte_0[256];
			byte_0[num] = (byte)num3;
			num2 = byte_0[258];
			num = byte_0[num3];
			num3 = byte_0[259];
			num2 = (num2 + num) % 256;
			byte_0[258] = (byte)num2;
			num = num2;
			num3 = byte_0[num3];
			num2 = byte_0[257];
			num2 = byte_0[num2];
			num = byte_0[num];
			num3 = (num3 + num2) % 256;
			num2 = byte_0[260];
			num2 = byte_0[num2];
			num3 = (num3 + num2) % 256;
			num2 = byte_0[num3];
			num3 = byte_0[256];
			num3 = byte_0[num3];
			num = (num + num3) % 256;
			num3 = byte_0[num2];
			num2 = byte_0[num];
			byte_0[260] = byte_1;
			num3 = (num3 ^ num2 ^ byte_1) % 256;
			byte_0[259] = (byte)num3;
			return (byte)num3;
		}

		private static byte smethod_9(ref byte[] byte_0, byte byte_1)
		{
			int num = byte_0[256];
			int num2 = byte_0[257];
			int num3 = byte_0[num];
			byte_0[256] = (byte)((num + 1) % 256);
			byte_0[257] = (byte)((num2 + num3) % 256);
			num = byte_0[260];
			num2 = byte_0[257];
			num2 = byte_0[num2];
			num3 = byte_0[num];
			byte_0[num] = (byte)num2;
			num = byte_0[259];
			num2 = byte_0[257];
			num = byte_0[num];
			byte_0[num2] = (byte)num;
			num = byte_0[256];
			num2 = byte_0[259];
			num = byte_0[num];
			byte_0[num2] = (byte)num;
			num = byte_0[256];
			byte_0[num] = (byte)num3;
			num2 = byte_0[258];
			num = byte_0[num3];
			num3 = byte_0[259];
			num2 = (num2 + num) % 256;
			byte_0[258] = (byte)num2;
			num = num2;
			num3 = byte_0[num3];
			num2 = byte_0[257];
			num2 = byte_0[num2];
			num = byte_0[num];
			num3 = (num3 + num2) % 256;
			num2 = byte_0[260];
			num2 = byte_0[num2];
			num3 = (num3 + num2) % 256;
			num2 = byte_0[num3];
			num3 = byte_0[256];
			num3 = byte_0[num3];
			num = (num + num3) % 256;
			num3 = byte_0[num2];
			num2 = byte_0[num];
			num3 = (num3 ^ num2 ^ byte_1) % 256;
			byte_0[260] = (byte)num3;
			byte_0[259] = byte_1;
			return (byte)num3;
		}

		private static char smethod_10(long long_0)
		{
			return (char)(long_0 % 256L);
		}

		private static uint smethod_11(char char_0)
		{
			return char_0;
		}
	}
}
