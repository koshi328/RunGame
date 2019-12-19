using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSystem
{
	public class ThirdPerson_CameraController : ICameraController
	{
		/// <memo>
		/// Example Setting
		/// Angle -10 ～ 45 (10)
		/// Pinch 3 ～ 10 (5)
		/// </memo>

		Vector2 defaultRotation = new Vector2(0, 0);

		Transform _root;
		Transform _tracker;
		Transform _target;
		Vector3 _offset;

		float _minAngle;
		float _maxAngle;
		Vector2 _angle;

		float _minPinch;
		float _maxPinch;
		float _pinch;

		protected Vector3 targetPosition { get { return _target ? _target.position : Vector3.zero; } }

		/// <memo>
		/// Options
		/// </memo>
		public bool UseOneAxisOnly { get; set; }
		public float LerpValue { get; set; }

		/// <summary>
		/// initialize
		/// </summary>
		public ThirdPerson_CameraController(float minPinch, float maxPinch, float defaultPinch, float minAngle, float maxAngle, float defaultAngle)
		{
			_minPinch = minPinch;
			_maxPinch = maxPinch;
			_pinch = defaultPinch;

			_minAngle = minAngle;
			_maxAngle = maxAngle;
			_angle.y = defaultAngle;
			UseOneAxisOnly = false;
			LerpValue = 1.0f;
		}

		public void SetTarget(Transform target, Vector3 offset)
		{
			_target = target;
			_offset = offset;
		}

		public void OnPinch(float pinch)
		{
			if(pinch > 0)
			{
				_pinch += pinch;
				if(_pinch >= _maxPinch) _pinch = _maxPinch;
				else if(_pinch <= _minPinch) _pinch = _minPinch;
				_tracker.SetLocalPositionZ(-_pinch);
			}
		}

		public void OnDrag(Vector2 rot)
		{
			if(rot.sqrMagnitude > float.MinValue)
			{
				if(UseOneAxisOnly)
				{
					if(rot.x > rot.y) rot.y = 0;
					else rot.x = 0;
				}
				_angle.x += rot.x;
				_angle.y -= rot.y;
				if(_angle.y >= _maxAngle) _angle.y = _maxAngle;
				else if(_angle.y <= _minAngle) _angle.y = _minAngle;
				_root.rotation = Quaternion.Euler(_angle.y, _angle.x, 0);
			}
		}

		public void OnInitialize(Camera camera)
		{
			_root = new GameObject("ThirdPersonCamera").transform;
			_tracker = new GameObject("Tracker").transform;
			_tracker.SetParent(_root);
			GameObject.DontDestroyOnLoad(_root.gameObject);
			_tracker.SetLocalPositionZ(-_pinch);
			_root.rotation = Quaternion.Euler(_angle.y, _angle.x, 0);
		}
		public void OnUpdate(Camera camera)
		{
			_root.position = targetPosition + _offset;
			camera.transform.position = Vector3.Lerp(camera.transform.position, _tracker.position, LerpValue);
			camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, _tracker.rotation, LerpValue);
		}
		public void OnFinalize(Camera camera)
		{
			GameObject.Destroy(_root.gameObject);
			_root = null;
			_tracker = null;
		}

	}
}