using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSystem
{
	public class Default_CameraController : ICameraController
	{
		Vector3 _default_position;
		Quaternion _default_rotation;
		public Default_CameraController(Vector3 position, Quaternion rotation)
		{
			_default_position = position;
			_default_rotation = rotation;
		}

		public void OnInitialize(Camera camera)
		{
			camera.transform.SetPositionAndRotation(_default_position, _default_rotation);
		}
		public void OnUpdate(Camera camera)
		{
			
		}
		public void OnFinalize(Camera camera)
		{

		}
	}
}