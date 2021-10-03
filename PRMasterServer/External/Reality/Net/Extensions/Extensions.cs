using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Reality.Net.Extensions
{
	public static class Extensions
	{
		private sealed class Class177 : IDisposable, IEnumerable, IEnumerator, IEnumerable<int>, IEnumerator<int>
		{
			private int int_0;

			private int int_1;

			private int int_2;

			public string string_0;

			public string string_1;

			public string string_2;

			public string string_3;

			public int int_3;

			int IEnumerator<int>.Current
			{
				[DebuggerHidden]
				get
				{
					return int_0;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return int_0;
				}
			}

			[DebuggerHidden]
			public Class177(int int_4)
			{
				int_1 = int_4;
				int_2 = Thread.CurrentThread.ManagedThreadId;
			}

			[DebuggerHidden]
			IEnumerator<int> IEnumerable<int>.GetEnumerator()
			{
				Class177 @class;
				if (Thread.CurrentThread.ManagedThreadId == int_2 && int_1 == -2)
				{
					int_1 = 0;
					@class = this;
				}
				else
				{
					@class = new Class177(0);
				}
				@class.string_0 = string_1;
				@class.string_2 = string_3;
				return @class;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<int>)this).GetEnumerator();
			}

			private bool MoveNext()
			{
				switch (int_1)
				{
				case 0:
					int_1 = -1;
					if (string.IsNullOrEmpty(string_2))
					{
						throw new ArgumentException("the string to find may not be empty", "value");
					}
					int_3 = 0;
					goto IL_0066;
				case 1:
					{
						int_1 = -1;
						int_3 += string_2.Length;
						goto IL_0066;
					}
					IL_0066:
					int_3 = string_0.IndexOf(string_2, int_3);
					if (int_3 != -1)
					{
						int_0 = int_3;
						int_1 = 1;
						return true;
					}
					break;
				}
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			void IDisposable.Dispose()
			{
			}
		}

		private static readonly object object_0 = new object();

		public static string GetString(this Random rand, int length)
		{
			char[] array = new char[length];
			lock (object_0)
			{
				for (int i = 0; i < length; i++)
				{
					array[i] = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"[rand.Next(62)];
				}
			}
			return new string(array);
		}

		public static string GetString(this Random rand, int length, string chars)
		{
			char[] array = new char[length];
			lock (object_0)
			{
				for (int i = 0; i < length; i++)
				{
					array[i] = chars[rand.Next(chars.Length)];
				}
			}
			return new string(array);
		}

		public static string ToMD5(this string string_0)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.ASCII.GetBytes(string_0);
			byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		public static string ToSha1(this string string_0)
		{
			SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			byte[] bytes = Encoding.ASCII.GetBytes(string_0);
			byte[] array = sHA1CryptoServiceProvider.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		public static IEnumerable<int> IndexesOf(this string str, string value)
		{
			Class177 @class = new Class177(-2);
			@class.string_1 = str;
			@class.string_3 = value;
			return @class;
		}

		public static double ToEpoch(this DateTime dt)
		{
			return (dt.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
		}

		public static int ToEpochInt(this DateTime dt)
		{
			return (int)(dt.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
		}

		public static double ToEpochMilliseconds(this DateTime dt)
		{
			return (dt.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;
		}

		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
		{
			Random random = new Random();
			List<T> list2 = new List<T>(list);
			int num = list2.Count;
			while (num > 1)
			{
				num--;
				int index = random.Next(num + 1);
				T value = list2[index];
				list2[index] = list2[num];
				list2[num] = value;
			}
			return list2;
		}
	}
}
