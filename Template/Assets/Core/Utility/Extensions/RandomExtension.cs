
public static class RandomEx
{
	static System.Random m_instance = new System.Random();

	public static int Range(int min, int max)
	{
		return m_instance.Next(min, max);
	}
}
