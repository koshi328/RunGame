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

			inputDirection.x = Input.GetAxisRaw("Horizontal");
			inputDirection.y = Input.GetAxisRaw("Vertical");

			m_character.OnMove(inputDirection);

			if(Input.GetKeyDown(KeyCode.Space))
			{
				m_character.OnJump(1.0f);
			}
		}
	}
}