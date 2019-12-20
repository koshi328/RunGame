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

		protected virtual void Awake()
		{

		}

		public abstract void OnMove(in Vector2 direction);

		public abstract void OnAvoidance(in Vector2 direction);

		public abstract void OnJump(float value);

		public abstract void OnAction(IAction action);

		public abstract void UpdateSelf();

		private void Update()
		{
			UpdateSelf();
		}
	}

	/// <summary>
	/// キャラクターが実行するアクションのインターフェース
	/// </summary>
	public interface IAction
	{

	}
}