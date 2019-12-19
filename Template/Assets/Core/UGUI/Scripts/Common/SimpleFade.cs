using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.UI
{
	public class SimpleFade : ManagedPanel
	{
		private static SimpleFade _instance;

		public override ManagedPanel CreatePanel(Transform parent)
		{
			gameObject.AddComponent<CanvasGroup>();

			var image = gameObject.AddComponent<UnityEngine.UI.Image>();
			image.color = Color.black;
			image.rectTransform.anchorMin = new Vector2(0, 0);
			image.rectTransform.anchorMax = new Vector2(1, 1);
			image.rectTransform.offsetMin = new Vector2(0, 0);
			image.rectTransform.offsetMax = new Vector2(1, 1);
			image.raycastTarget = false;

			transform.SetParent(parent, false);
			return this;
		}

		private static SimpleFade Create()
		{
			var component = new GameObject("FadeView").AddComponent<SimpleFade>();
			UIManager.Instance.Register(component, UIManager.Layer.Overlay);
			_instance = component;
			return component;
		}

		private static Coroutine Begin(float duration, bool fadeIn)
		{
			if(_instance == null)
			{
				Create();
			}
			if(fadeIn)
			{
				UIUtil.Visible(_instance.gameObject, false);
				return _instance.StartCoroutine(UIUtil.Display(null, _instance.gameObject, duration));
			}
			else
			{
				UIUtil.Visible(_instance.gameObject, true);
				return _instance.StartCoroutine(UIUtil.Hide(null, _instance.gameObject, duration));
			}
		}

		public static Coroutine FadeIn(float duration = 0.5f)
		{
			return Begin(duration, true);
		}

		public static Coroutine FadeOut(float duration = 0.5f)
		{
			return Begin(duration, false);
		}

		public override void OnDiscard()
		{
			Destroy(_instance.gameObject);
			_instance = null;
		}
	}
}