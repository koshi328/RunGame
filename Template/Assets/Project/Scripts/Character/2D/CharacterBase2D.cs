using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Character
{
	public class CharacterBase2D : CharacterBase
	{
		// キャッシュ ----------------------
		private Collider2D _collider;
		private Rigidbody2D _cacheRigidbody;
		public new Collider2D collider { get { return _collider ?? (_collider = GetComponent<Collider2D>()); } }
		public new Rigidbody2D rigidbody { get { return _cacheRigidbody ?? (_cacheRigidbody = GetComponent<Rigidbody2D>()); } }
		// ---------------------------------

		protected IMoveSystem _currentMoveSystem;
		public IMoveSystem currentMoveSystem { get { return _currentMoveSystem; } }

		public void ChangeMoveSystem(IMoveSystem system)
		{
			_currentMoveSystem = system;
		}

		public override void OnMove(in Vector2 direction)
		{
			if(_currentMoveSystem != null)
			{
				_currentMoveSystem.inputDirection = direction;
			}
		}

		public override void OnAvoidance(in Vector2 direction)
		{
			if(_currentMoveSystem != null)
			{
				_currentMoveSystem.avoidanceDirection = direction;
			}
		}

		public override void OnJump(float value)
		{
			if(_currentMoveSystem != null)
			{
				_currentMoveSystem.requestJump = true;
			}
		}

		public override void OnAction(IAction action)
		{
			if(_currentMoveSystem != null)
			{
				_currentMoveSystem.currentAction = action;
			}

		}

		public override void UpdateSelf()
		{
			if(_currentMoveSystem != null)
			{
				_currentMoveSystem.Update();
			}
		}
	}
}