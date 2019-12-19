using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : new()
{
	static private T m_instance;
	static public T Instance
	{
		get
		{
			if(m_instance == null) m_instance = new T();
			return m_instance;
		}
		private set { m_instance = value; }
	}
}
