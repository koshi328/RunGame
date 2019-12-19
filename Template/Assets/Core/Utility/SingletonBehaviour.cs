using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	static private T m_instance;
	static public T Instance
	{
		get
		{
			if(m_instance == null) m_instance = Create();
			return m_instance;
		}
		private set { m_instance = value; }
	}

	static private T Create()
	{
		var component = new GameObject(typeof(T).Name).AddComponent<T>();
		GameObject.DontDestroyOnLoad(component.gameObject);
		return component;
	}
}