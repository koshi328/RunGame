using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
	public class MoveSystem_Normal2D : IMoveSystem
	{
		public int maxJumpCount { get; set; }
		public float moveSpeed { get; set; }
		public float jumpPower { get; set; }


		public Vector2 inputDirection { get; set; }
		public bool requestJump { get; set; }
		public Vector2 avoidanceDirection { get; set; }
		public IAction currentAction { get; set; }

		protected CharacterBase2D character;
		protected ObjectBody2D body;

		protected int jumpCount;
		protected float fallingSpeed;
		protected Vector2 velocity;

		public MoveSystem_Normal2D(CharacterBase2D character)
		{
			fallingSpeed = 0.0f;

			this.character = character;
			body = new ObjectBody2D(character.collider);
		}

		public void Update()
		{
			body.Update();
			bool isGround = body.IsGround;
			bool prevGround = body.IsPreviousGround;

			// ジャンプ処理
			if(requestJump && jumpCount < maxJumpCount)
			{
				requestJump = false;
				fallingSpeed = jumpPower;
				if(isGround)
				{
					++jumpCount;
				}
			}

			// 移動処理
			if(!prevGround)
			{
				if(isGround) // 着地
				{
					jumpCount = 0;
					fallingSpeed = 0;
				}
				else // 空中
				{
					if(body.IsHead)
					{
						fallingSpeed = -Constants.Normal_Gravity * Time.deltaTime;
					}

					if(fallingSpeed >= Constants.Max_FallingSpeed)
					{
						fallingSpeed -= Constants.Normal_Gravity * Time.deltaTime;
						if(fallingSpeed <= Constants.Max_FallingSpeed) fallingSpeed = Constants.Max_FallingSpeed;
					}

					if(body.IsRight || body.IsLeft)
					{
						velocity.x = 0.0f;
					}
				}
			}
			else
			{
				if(isGround) // 立っている
				{

				}
				else // 離地
				{
					++jumpCount;
				}
			}

			if(isGround)
			{
				velocity.x = 0.0f;
				velocity.y = 0.0f;
				if(inputDirection.x > float.Epsilon || inputDirection.x < -float.Epsilon)
				{
					velocity = body.GetDirection(inputDirection.x) * moveSpeed * Time.deltaTime;
				}
			}
			else
			{
				velocity.x += inputDirection.x * 0.1f * Time.deltaTime;
				velocity.y = 0;
			}

			var position = character.rigidbody.position;
			position.x += velocity.x;
			position.y += velocity.y + fallingSpeed;
			character.rigidbody.position = position;
		}

#if UNITY_EDITOR
		void IMoveSystem._OnInspector()
		{
			UnityEditor.EditorGUILayout.LabelField("ジャンプ回数");
			maxJumpCount = UnityEditor.EditorGUILayout.IntSlider(maxJumpCount, 0, 5);
			UnityEditor.EditorGUILayout.LabelField("ジャンプ力");
			jumpPower = UnityEditor.EditorGUILayout.Slider(jumpPower, 0, 10);
			UnityEditor.EditorGUILayout.LabelField("移動速度");
			moveSpeed = UnityEditor.EditorGUILayout.Slider(moveSpeed, 0, 10);
		}
#endif
	}
}