using System;
using System.Text;

namespace Reality.Net.GameSpy.Servers
{
	public class DataFunctions
	{
		public static byte[] CopyBytes(byte[] data, int start, int length, bool reverse = false)
		{
			byte[] array = new byte[length];
			Array.Copy(data, start, array, 0, length);
			if (reverse)
			{
				Array.Reverse(array);
			}
			return array;
		}

		public static byte[] StringToBytes(string data)
		{
			return Encoding.GetEncoding("ISO-8859-1").GetBytes(data);
		}

		public static string BytesToString(byte[] data)
		{
			return Encoding.GetEncoding("ISO-8859-1").GetString(data);
		}
	}
}
