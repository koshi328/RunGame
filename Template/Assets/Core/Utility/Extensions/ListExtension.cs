public static class ListExtension
{
	public static void Shuffle(this System.Collections.IList list)
	{
		for(int i = 0; i < list.Count; ++i)
		{
			var value = list[i];
			var swapIndex = list.RandomIndex();
			list[i] = list[swapIndex];
			list[swapIndex] = value;
		}
	}

	public static int RandomIndex(this System.Collections.IList list)
	{
		int len = list.Count;
		return RandomEx.Range(0, len);
	}

	public static void AddValues(this System.Collections.IList list, params object[] info)
	{
		for(int i = 0; i < info.Length; ++i) list.Add(info[i]);
	}
}