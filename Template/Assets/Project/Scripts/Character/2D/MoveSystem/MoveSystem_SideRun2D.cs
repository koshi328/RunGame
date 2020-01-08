using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
	public class MoveSystem_SideRun2D : IMoveSystem
	{
		public int maxJumpCount { get; set; }
		public float moveSpeed { get; set; }
		public float jumpPower { get; set; }

		public Vector2 inputDirection { get; set; }
		public bool requestJump { get; set; }
		public Vector2 avoidanceDirection { get; set; }

		protected PlayerCharacter character;
		protected ObjectBody2D body;
		
		protected int jumpCount;

		public MoveSystem_SideRun2D(PlayerCharacter character)
		{
			this.character = character;
			body = new ObjectBody2D(character.collider);
		}

		public void Update()
		{
			var position = character.rigidbody.position;

			body.Update();

			if(jumpCount != 0 && body.IsGround && character.rigidbody.velocity.y <= 0)
			{
				jumpCount = 0;
			}

			// ジャンプ処理
			if(requestJump && jumpCount < maxJumpCount)
			{
				++jumpCount;
				position.y += Time.deltaTime;
				character.rigidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
			}
			requestJump = false;

			position.x += moveSpeed * Time.deltaTime;
			character.rigidbody.position = position;
		}

#if UNITY_EDITOR
		void IMoveSystem._OnInspector()
		{
			UnityEditor.EditorGUILayout.LabelField("ジャンプ回数");
			maxJumpCount = UnityEditor.EditorGUILayout.IntSlider(maxJumpCount, 0, 5);
			UnityEditor.EditorGUILayout.LabelField("ジャンプ力");
			jumpPower = UnityEditor.EditorGUILayout.Slider(jumpPower, 0, 50);
			UnityEditor.EditorGUILayout.LabelField("移動速度");
			moveSpeed = UnityEditor.EditorGUILayout.Slider(moveSpeed, -100, 100);
		}
#endif
	}
}