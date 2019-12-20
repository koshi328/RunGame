using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneSystem : ISceneSystem
{
	public const string name = "Title";
	string ISceneSystem.SceneName { get { return name; } }

	private void OnActiveScene()
	{

	}

	void ISceneSystem.OnUpdate()
	{
		Debug.Log("T:Update");

		if(Input.GetKeyDown(KeyCode.Space))
		{
			SystemManager.Instance.ChangeScene(new GameSceneSystem());
		}
	}

	void ISceneSystem.OnBeginLoad()
	{

	}

	void ISceneSystem.OnLoading(float progress)
	{
		Debug.Log("T:Loading");
	}

	void ISceneSystem.OnEndLoad()
	{
		OnActiveScene();
	}

	void ISceneSystem.OnDiscard()
	{
		Debug.Log("T:Discard");
	}
}
