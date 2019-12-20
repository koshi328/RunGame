using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
	public class PlayerCharacter : CharacterBase2D
	{
		protected override void Awake()
		{
			base.Awake();

			var moveSystem = new MoveSystem_Normal2D(this);
			moveSystem.moveSpeed = 3.0f;
			moveSystem.maxJumpCount = 1;
			moveSystem.jumpPower = 0.5f;

			ChangeMoveSystem(moveSystem);
		}
	}
}