using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.UI
{
	public class TouchPanel : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
	{
		private readonly float DoubleClickTime = 0.2f;
		private readonly float FlickDistance = 8.0f * 8.0f;
		float _prevClickTime;
		Vector2 _dragValue;
		int _firstId = -1;
		int _secondId = -1;

		public System.Action<Vector2> onDrag;
		public System.Action onClick;
		public System.Action onDoubleClick;
		public System.Action<float> onPinch;
		public System.Action<Vector2> onFlick;
		
		public void OnPointerClick(PointerEventData eventData)
		{
			if(_firstId != -1 || _secondId != -1) return;
			if(_prevClickTime + DoubleClickTime > Time.realtimeSinceStartup)
			{
				_prevClickTime = 0.0f;

				if(onDoubleClick != null)
					onDoubleClick.Invoke();
				return;
			}
			_prevClickTime = Time.realtimeSinceStartup;
			if(onClick != null)
			{
				onClick.Invoke();
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			// ピンチ処理
			if(_firstId != -1 && _secondId != -1)
			{
				if(onPinch != null)
					onPinch.Invoke(eventData.delta.magnitude);
				return;
			}

			_dragValue += eventData.delta;
			if(onDrag != null)
				onDrag.Invoke(eventData.delta);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if(_firstId == -1)
			{
				_firstId = eventData.pointerId;
				if(eventData.pointerId == -1) _firstId = 1;
			}
			else if(_secondId == -1)
			{
				_secondId = eventData.pointerId;
				if(eventData.pointerId == -1) _secondId = 2;
			}
			_dragValue += eventData.delta;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			_firstId = -1;
			_secondId = -1;
			_dragValue = Vector2.zero;

			if(eventData.delta.sqrMagnitude >= FlickDistance)
			{
				if(onFlick != null)
					onFlick.Invoke(eventData.delta.normalized);
			}
			if(onDrag != null)
				onDrag.Invoke(_dragValue);
		}
	}
}