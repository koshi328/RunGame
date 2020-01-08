using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Character
{
	public class CharacterBase2D : CharacterBase
	{
		[SerializeField]
		Animator _animator;

		int SlidingHash;

		// キャッシュ ----------------------
		protected BoxCollider2D _collider;
		protected Rigidbody2D _cacheRigidbody;
		protected SpriteRenderer _avatarSprite;
		public new BoxCollider2D collider { get { return _collider ?? (_collider = GetComponent<BoxCollider2D>()); } }
		public new Rigidbody2D rigidbody { get { return _cacheRigidbody ?? (_cacheRigidbody = GetComponent<Rigidbody2D>()); } }
		public SpriteRenderer avatarSprite { get { return _avatarSprite ?? (_avatarSprite = GetComponentInChildren<SpriteRenderer>()); } }
		// ---------------------------------

		
		protected virtual void Start()
		{
			rigidbody.velocity = Vector2.zero;

			SlidingHash = Animator.StringToHash("sliding");
		}
		
		public void SlidingPose(bool enable)
		{
			_animator.SetBool(SlidingHash, enable);
		}
	}
}