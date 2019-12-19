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

		private static CanvasGroup GetGroup(this GameObject obj)
		{
			return obj.GetOrAddComponent<CanvasGroup>();
		}

		public static void Enable(GameObject obj, bool enable)
		{
			var group = obj.GetGroup();
			group.alpha = enable ? 1.0f : 0.5f;
			group.blocksRaycasts = enable;
		}

		/// <summary>
		/// 表示切替
		/// </summary>
		public static void Visible(GameObject obj, bool visible)
		{
			var group = obj.GetGroup();
			group.alpha = visible ? 1.0f : 0.0f;
			group.blocksRaycasts = visible;
		}

		/// <summary>
		/// 隠す
		/// </summary>
		public static IEnumerator Hide(System.Action complete, GameObject obj, float duration)
		{
			var group = obj.GetGroup();

			yield return Fade(false, obj, duration);
			group.blocksRaycasts = false;

			complete?.Invoke();
		}

		/// <summary>
		/// 見せる
		/// </summary>
		public static IEnumerator Display(System.Action complete, GameObject obj, float duration)
		{
			var group = obj.GetGroup();

			group.blocksRaycasts = false;
			yield return Fade(true, obj, duration);
			group.blocksRaycasts = true;

			complete?.Invoke();
		}

		/// <summary>
		/// 止める
		/// </summary>
		public static IEnumerator Stop(System.Action complete, GameObject obj, float duration)
		{
			yield return Hide(complete, obj, duration);
			obj.SetActive(false);
		}

		/// <summary>
		/// 起動
		/// </summary>
		public static IEnumerator Awake(System.Action complete, GameObject obj, float duration)
		{
			obj.SetActive(true);
			yield return Display(complete, obj, duration);
		}

		/// <summary>
		/// フェード
		/// </summary>
		public static IEnumerator Fade(bool fadeIn, GameObject obj, float fadeSec)
		{
			uTools.TweenAlpha tween;

			if(fadeIn)
			{
				tween = uTools.TweenAlpha.Begin(obj, 0.0f, 1.0f, fadeSec);
			}
			else
			{
				tween = uTools.TweenAlpha.Begin(obj, 1.0f, 0.0f, fadeSec);
			}

			yield return new WaitWhile(() => tween && tween.enabled);
		}
	}
}