using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
	public abstract class CharacterBase : MonoBehaviour
	{
		// キャッシュ ----------------------
		private Transform m_cacheTransform;
		public new Transform transform { get { return m_cacheTransform ?? (m_cacheTransform = gameObject.transform); } }
		// ---------------------------------

		protected IAction currentAction;

		protected IMoveSystem _currentMoveSystem;
		public IMoveSystem currentMoveSystem { get { return _currentMoveSystem; } }

		public virtual void ChangeMoveSystem(IMoveSystem system)
		{
			_currentMoveSystem = system;
		}

		public virtual void OnMove(in Vector2 direction)
		{
			if(_currentMoveSystem != null)
			{
				_currentMoveSystem.inputDirection = direction;
			}
		}

		public virtual void OnAvoidance(in Vector2 direction)
		{
			if(_currentMoveSystem != null)
			{
				_currentMoveSystem.avoidanceDirection = direction;
			}
		}

		public virtual void OnJump(float value)
		{
			if(_currentMoveSystem != null)
			{
				_currentMoveSystem.requestJump = true;
			}
		}

		public virtual void OnAction(IAction action)
		{
			currentAction?.End(this);
			currentAction = action;
			currentAction?.Start(this);
		}

		public virtual void UpdateSelf()
		{
			if(_currentMoveSystem != null)
			{
				_currentMoveSystem.Update();
			}

			if(currentAction != null)
			{
				if(!currentAction.Execute(this))
				{
					currentAction.End(this);
				}
			}
		}
	}

	/// <summary>
	/// キャラクターが実行するアクションのインターフェース
	/// </summary>
	public interface IAction
	{
		void Start(CharacterBase character);
		bool Execute(CharacterBase character);
		void End(CharacterBase character);
	}
}