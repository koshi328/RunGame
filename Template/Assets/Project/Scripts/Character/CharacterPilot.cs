using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
	public class CharacterPilot : MonoBehaviour
	{
		CharacterBase m_character;

		Vector2 inputDirection;

		public static CharacterPilot Create()
		{
			var obj = new GameObject("Pilot");
			var pilot = obj.AddComponent<CharacterPilot>();
			return pilot;
		}

		public void Initialize(CharacterBase character)
		{
			m_character = character;
		}

		public void UpdateSelf()
		{
			if(m_character == null) return;

			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				m_character.OnJump(1.0f);
			}

			if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				m_character.OnAction(new SlidingAction());
			}
		}
	}

	public class SlidingAction : IAction
	{
		void IAction.Start(CharacterBase character)
		{
			var target = (CharacterBase2D)character;
			target.SlidingPose(true);
		}

		bool IAction.Execute(CharacterBase character)
		{
			if(!Input.GetKey(KeyCode.DownArrow))
			{
				return false;
			}
			return true;
		}

		void IAction.End(CharacterBase character)
		{
			var target = (CharacterBase2D)character;
			target.SlidingPose(false);
		}
	}
}