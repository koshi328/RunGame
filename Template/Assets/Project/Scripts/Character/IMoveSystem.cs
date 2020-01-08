using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
	public interface IMoveSystem
	{
		Vector2 inputDirection { get; set; }
		bool requestJump { get; set; }
		Vector2 avoidanceDirection { get; set; }

		void Update();

#if UNITY_EDITOR
		void _OnInspector();
#endif
	}

	public static class Constants
	{
		public static readonly float Normal_Gravity = 0.98f;

		public static readonly float Max_FallingSpeed = -0.5f;
	}
}