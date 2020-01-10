using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
	public class PlayerCharacter : CharacterBase2D
	{
		MoveSystem_SideRun2D moveSystem;
		
		public bool IsGameOver { get; set; }

		protected float gameOverTime = 0.0f;
		protected Vector3 previousPosition;

		protected void Awake()
		{
			IsGameOver = false;

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

		public override void UpdateSelf()
		{
			base.UpdateSelf();

			if((previousPosition - transform.position).sqrMagnitude < Time.deltaTime)
			{
				gameOverTime += Time.deltaTime;
			}
			else
			{
				gameOverTime = 0.0f;
			}

			if(gameOverTime >= Time.fixedDeltaTime * 10)
			{
				IsGameOver = true;
			}

			previousPosition = transform.position;
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