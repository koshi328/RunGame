public static class ArrayExtension
{
	public static void Shuffle(this System.Array array)
	{
		for(int i = 0; i < array.Length; ++i)
		{
			var value = array.GetValue(i);
			var swapIndex = array.RandomIndex();
			array.SetValue(array.GetValue(swapIndex), i);
			array.SetValue(value, swapIndex);
		}
	}

	public static int RandomIndex(this System.Array array)
	{
		int len = array.Length;
		return RandomEx.Range(0, len);
	}
}

public struct Swap<T>
{
	T[] content;
	int mainIndex;

	public T Main { get { return content[MainIndex]; } set { content[MainIndex] = value; } }
	public T Sub { get { return content[SubIndex]; } set { content[SubIndex] = value; } }
	int MainIndex { get { return mainIndex;} }
	int SubIndex { get { return 1 - mainIndex; } }

	public Swap(T main, T sub)
	{
		content = new T[2];
		mainIndex = 0;

		Main = main;
		Sub = sub;
	}

	public void Change()
	{
		mainIndex = 1 - mainIndex;
	}
}