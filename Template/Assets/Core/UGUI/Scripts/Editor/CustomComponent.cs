using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;
using GameSystem.UI;

namespace UIEditor
{
	public static class CustomComponent
	{
		private static readonly Vector2 buttonSize = new Vector2(200, 80);

		private static GameObject CreateUIElementRoot(string name, Vector2 size)
		{
			GameObject child = new GameObject(name);
			RectTransform rectTransform = child.AddComponent<RectTransform>();
			rectTransform.sizeDelta = size;
			return child;
		}
		public static GameObject CreateLongPressButton()
		{
			GameObject buttonRoot = CreateUIElementRoot("LongPressButton", buttonSize);

			GameObject childText = new GameObject("Text");
			childText.transform.SetParent(buttonRoot.transform);

			Image image = buttonRoot.AddComponent<Image>();
			image.sprite = null;
			image.type = Image.Type.Sliced;
			image.color = Color.white;

			Button bt = buttonRoot.AddComponent<LongPressButton>();

			Text text = childText.AddComponent<Text>();
			text.text = "Button";
			text.alignment = TextAnchor.MiddleCenter;
			text.color = Color.black;
			text.fontSize = 32;

			RectTransform textRectTransform = childText.GetComponent<RectTransform>();
			textRectTransform.anchorMin = Vector2.zero;
			textRectTransform.anchorMax = Vector2.one;
			textRectTransform.sizeDelta = Vector2.zero;

			return buttonRoot;
		}
	}

	public static class CustomComponentCaller
	{
		[MenuItem("GameObject/UI/Extension/LongPressButton")]
		public static void CreateLongPressButton()
		{
			var obj = CustomComponent.CreateLongPressButton();
			obj.transform.SetParent(UITools.GetCanvasTransform(), false);
		}
	}
}