using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneSystem : SceneSystemAbstract
{
	public const string name = "Title";
	public override string SceneName { get { return name; } }

	private bool alreadyCallChange;

	private void OnActiveScene()
	{
		alreadyCallChange = false;
	}

	public override void OnUpdate()
	{
		if(alreadyCallChange) return;
		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
		{
			alreadyCallChange = true;
			SystemManager.Instance.ChangeScene(new GameSceneSystem());
		}
	}

	public override void OnLoading(float progress)
	{

	}

	public override IEnumerator OnBeginLoad()
	{
		yield return GameSystem.UI.SimpleFade.FadeIn();
	}

	public override void OnEndLoad()
	{
		GameSystem.UI.SimpleFade.FadeOut();
		OnActiveScene();
	}

	public override void OnDiscard()
	{

	}
}
