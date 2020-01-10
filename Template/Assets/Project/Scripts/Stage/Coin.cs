using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MapChipBase
{
	public override void Initialize()
	{
		gameObject.SetActive(true);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		gameObject.SetActive(false);
	}
}
