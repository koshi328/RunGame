using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameSystem.UI
{
	public partial class UIManager : SingletonBehaviour<UIManager>
	{
		protected Canvas m_overlayCanvas;
		protected CanvasScaler m_scaler;
		protected GraphicRaycaster m_raycaster;
		protected EventSystem m_eventSystem;
		protected StandaloneInputModule m_inputModule;
		
		/// <summary>
		/// 初回アクセス時実行
		/// </summary>
		public void Awake()
		{
			var canvas = new GameObject("OverlayCanvas");
			var eventSystem = new GameObject("EventSystem");
			canvas.transform.SetParent(transform);
			eventSystem.transform.SetParent(transform);

			m_overlayCanvas = canvas.AddComponent<Canvas>();
			m_scaler = canvas.AddComponent<CanvasScaler>();
			m_raycaster = canvas.AddComponent<GraphicRaycaster>();
			m_eventSystem = eventSystem.AddComponent<EventSystem>();
			m_inputModule = eventSystem.AddComponent<StandaloneInputModule>();

			// OverlayCanvas化
			m_overlayCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
			// スケールパラメータ
			m_scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			m_scaler.referenceResolution = new Vector2(1920, 1080);
			m_scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
			m_scaler.matchWidthOrHeight = 1.0f;

			LayerManager_Initialize();
		}
	}
}