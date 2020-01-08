using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSystem
{
	public class SideScroll_CameraController : ICameraController
	{
		Transform _target;
		Vector3 _offset;
		public SideScroll_CameraController(Transform target, Vector3 offset)
		{
			_target = target;
			_offset = offset;
		}

		public void OnInitialize(Camera camera)
		{
		}
		public void OnUpdate(Camera camera)
		{
			var pos = _offset;
			pos.x = _target.transform.position.x;
			camera.transform.position = pos;
		}

		public void OnFinalize(Camera camera)
		{

		}
	}
}