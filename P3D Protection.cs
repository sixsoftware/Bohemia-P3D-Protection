using System;
using System.IO;
using System.Linq;

namespace fkjsdfhjkdsfbjdsf; // lol

internal class P3DProtect
{
	public P3DProtect(string filename)
	{
		byte[] array = File.ReadAllBytes(filename);
		byte[] pattern = new byte[5] { 114, 118, 109, 97, 116 };
		int num = IndexOfSequence(array, pattern, 0);
		int num2 = BitConverter.ToInt32(array, num + 6);
		if (num2 == 16)
		{
			array[num + 6] = 15;
			SaveFile(filename, array, num);
		}
	}

	private static void SaveFile(string path, byte[] array, int pos)
	{
		using FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
		fileStream.Write(array, 0, pos + 146);
		fileStream.Write(array, pos + 162, array.Length - pos - 162);
	}

	private static int IndexOfSequence(byte[] buffer, byte[] pattern, int startIndex)
	{
		int result = -1;
		int num = Array.IndexOf(buffer, pattern[0], startIndex);
		while (num >= 0 && num <= buffer.Length - pattern.Length)
		{
			byte[] array = new byte[pattern.Length];
			Buffer.BlockCopy(buffer, num, array, 0, pattern.Length);
			if (array.SequenceEqual(pattern))
			{
				result = num;
			}
			num = Array.IndexOf(buffer, pattern[0], num + 1);
		}
		return result;
	}
}
