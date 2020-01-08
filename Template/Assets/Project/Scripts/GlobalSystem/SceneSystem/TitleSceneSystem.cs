using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneSystem : SceneSystemAbstract
{
	public const string name = "Title";
	public override string SceneName { get { return name; } }

	private void OnActiveScene()
	{

	}

	public override void OnUpdate()
	{
		Debug.Log("T:Update");

		if(Input.GetKeyDown(KeyCode.Space))
		{
			SceneSystemManager.Instance.ChangeScene(new GameSceneSystem());
		}
	}

	public override void OnLoading(float progress)
	{
		Debug.Log("T:Loading");
	}

	public override void OnEndLoad()
	{
		OnActiveScene();
	}

	public override void OnDiscard()
	{
		Debug.Log("T:Discard");
	}
}
