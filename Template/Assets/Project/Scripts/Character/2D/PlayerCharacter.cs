using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
	public class PlayerCharacter : CharacterBase2D
	{
		MoveSystem_SideRun2D moveSystem;
		protected void Awake()
		{
			moveSystem = new MoveSystem_SideRun2D(this);
			moveSystem.moveSpeed = 0.0f;
			moveSystem.maxJumpCount = 1;
			moveSystem.jumpPower = 10.0f;

			ChangeMoveSystem(moveSystem);
		}

		public void SetMoveSpeed(float speed)
		{
			moveSystem.moveSpeed = speed;
		}
	}

#if UNITY_EDITOR
	[UnityEditor.CustomEditor(typeof(PlayerCharacter))]
	public class PlayerCharacterEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var info = (CharacterBase2D)target;
			DrawDefaultInspector();
			if(info.currentMoveSystem != null)
			{
				info.currentMoveSystem._OnInspector();
			}
		}
	}
#endif
}