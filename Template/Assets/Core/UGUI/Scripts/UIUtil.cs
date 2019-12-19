using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.UI
{
	public static class UIUtil
	{

		public static T SimpleCreateUI<T>(UIManager.Layer layer) where T : MonoBehaviour, IPanelController
		{
			var controller = new GameObject(typeof(T).Name).AddComponent<T>();
			UIManager.Instance.Register(controller, layer);
			return controller;
		}

		public static T CreateUI<T>(string path,Transform parent) where T : MonoBehaviour
		{
			var ui = GameObject.Instantiate(Resources.Load(path)) as GameObject;
			if(parent != null)
			{
				ui.transform.SetParent(parent);
			}
			return ui.GetComponent<T>();
		}
	}
}