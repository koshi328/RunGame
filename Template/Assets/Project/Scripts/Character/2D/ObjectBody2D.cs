using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
	public class ObjectBody2D
	{
		private static readonly int MAX_CONTACT = 5;
		private static readonly Vector2 up = Vector2.up;
		private static float slopes = 30;

		public bool IsHead { get; private set; }
		public bool IsLeft { get; private set; }
		public bool IsRight { get; private set; }
		public bool IsGround { get { return Ground.HasValue; } }
		public bool IsPreviousGround { get; private set; }
		public ContactPoint2D? Ground { get; private set; }
		
		Collider2D ref_collider;
		ContactPoint2D[] _contacts;

		GameObject _rideObject;
		Vector3 _previousRidePosition;

		public ObjectBody2D(Collider2D collider)
		{
			ref_collider = collider;
			_contacts = new ContactPoint2D[MAX_CONTACT];
		}

		public void Update()
		{
			IsPreviousGround = IsGround;
			IsHead = false;
			IsRight = false;
			IsLeft = false;
			Ground = null;

			int hitNum = ref_collider.GetContacts(_contacts);

			for(int i = 0; i < hitNum; ++i)
			{
				float angle = Vector2.SignedAngle(_contacts[i].normal, up);
				// 足元判定
				float absAngle = Mathf.Abs(angle);
				if(absAngle <= slopes)
				{
					Ground = _contacts[i];
				}
				else if(absAngle >= 170) // 頭上
				{
					IsHead = true;
				}

				if(!IsRight) IsRight = angle >= slopes && angle <= 170;
				if(!IsLeft) IsLeft = angle <= -slopes && angle >= -170;
			}
			if(IsGround)
			{
				if(_rideObject == null || _rideObject != Ground.Value.collider.gameObject)
				{
					_rideObject = Ground.Value.collider.gameObject;
					_previousRidePosition = _rideObject.transform.position;
				}
				else
				{
					_previousRidePosition = _rideObject.transform.position;
				}
			}
			else
			{
				_rideObject = null;
			}
		}

		public Vector2 GetDirection(float horizontal)
		{
			if(IsGround)
			{
				return Vector2.Perpendicular(Ground.Value.normal * (horizontal > 0 ? -1 : 1));
			}
			else
			{
				return new Vector2(horizontal, 0);
			}
		}

		public Vector2 GetRideMove()
		{
			if(!IsGround || _rideObject == null || _rideObject != Ground.Value.collider.gameObject) return Vector2.zero;
			return _rideObject.transform.position - _previousRidePosition;
		}
	}
}