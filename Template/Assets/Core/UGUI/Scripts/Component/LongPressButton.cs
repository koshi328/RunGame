using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
#endif

namespace GameSystem.UI
{
	[AddComponentMenu("UI/LongPressButton", 30)]
	public class LongPressButton : Button, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
	{
		[SerializeField]
		private ButtonClickedEvent _onStartLongPress = new ButtonClickedEvent();

		[SerializeField]
		private ButtonClickedEvent _onEndLongPress = new ButtonClickedEvent();

		[SerializeField]
		private ButtonClickedEvent _onPressing = new ButtonClickedEvent();

		public ButtonClickedEvent onStartLongPress
		{
			get { return _onStartLongPress; }
			set { _onStartLongPress = value; }
		}

		public ButtonClickedEvent onEndLongPress
		{
			get { return _onEndLongPress; }
			set { _onEndLongPress = value; }
		}

		public ButtonClickedEvent onPressing
		{
			get { return _onPressing; }
			set { _onPressing = value; }
		}
		
		public float ThresholdTime_LongPress { get { return _thresholdTime_LongPress; } set { _thresholdTime_LongPress = value; } }

		private float _thresholdTime_LongPress = 0.3f;
		private bool _isLongPressd;

		public override void OnPointerClick(PointerEventData eventData)
		{
			StopAllCoroutines();
			if(_isLongPressd == true)
			{
				_isLongPressd = false;
				return;
			}
			base.OnPointerDown(eventData);
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			StartCoroutine(StartLongPress());
			base.OnPointerDown(eventData);
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			EndLongPress();
			base.OnPointerUp(eventData);
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			EndLongPress();
			base.OnPointerExit(eventData);
		}

		IEnumerator StartLongPress()
		{
			var wait = new WaitForSeconds(ThresholdTime_LongPress);
			yield return wait;
			_isLongPressd = true;
			if(_onStartLongPress != null)
			{
				_onStartLongPress.Invoke();
			}
		}

		private void EndLongPress()
		{
			StopAllCoroutines();
			if(_isLongPressd == true)
			{
				if(_onEndLongPress != null)
				{
					_onEndLongPress.Invoke();
				}
				_isLongPressd = false;
			}
		}

		private void Update()
		{
			if(_isLongPressd)
			{
				if(_onPressing != null)
				{
					_onPressing.Invoke();
				}
			}
		}
	}

#if UNITY_EDITOR
	[CanEditMultipleObjects, CustomEditor(typeof(LongPressButton), true)]
	public class ButtonExEditor : ButtonEditor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			this.serializedObject.Update();
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("_onStartLongPress"), true);
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("_onEndLongPress"), true);
			this.serializedObject.ApplyModifiedProperties();
		}
	}
#endif
}