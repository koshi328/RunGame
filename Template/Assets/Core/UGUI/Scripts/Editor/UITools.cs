using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public static class UITools
{
	public static Transform SelectionObject()
	{
		var selection = UnityEditor.Selection.activeGameObject;
		if(selection != null)
		{
			return selection.transform;
		}
		return null;
	}

	public static Transform GetCanvasTransform()
	{
		var selection = SelectionObject();
		if(selection != null)
		{
			var canvas = selection.GetComponentInParent<Canvas>();
			if(canvas != null)
			{
				return selection;
			}
		}
		var findCanvas = GameObject.FindObjectOfType<Canvas>();
		if(findCanvas != null)
		{
			return findCanvas.transform;
		}
		return CreateTemplateCanvas().transform;
	}

	[MenuItem("GameObject/UI/Extension/DefaultUI")]
	public static Canvas CreateTemplateCanvas()
	{
		var root = new GameObject("UIRoot");

		var canvasObj = new GameObject("Canvas");
		var canvas = canvasObj.AddComponent<Canvas>();
		var scaler = canvasObj.AddComponent<CanvasScaler>();
		canvasObj.AddComponent<GraphicRaycaster>();

		scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		scaler.referenceResolution = new Vector2(1920, 1080);
		scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
		scaler.matchWidthOrHeight = 1.0f;

		var eventSystem = GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
		if(eventSystem == null)
		{
			var obj = new GameObject("EventSystem",typeof(EventSystem));
			obj.AddComponent<StandaloneInputModule>();
			obj.transform.SetParent(root.transform);
		}

		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvasObj.transform.SetParent(root.transform);
		canvasObj.transform.SetSiblingIndex(0);

		EditorUtility.SetDirty(canvasObj);
		return canvas;
	}
}
