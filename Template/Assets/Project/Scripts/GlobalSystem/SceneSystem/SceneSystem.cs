using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ISceneSystem
{
	string SceneName { get; }
	void OnUpdate();
	void OnBeginLoad();
	void OnLoading(float progress);
	void OnEndLoad();
	void OnDiscard();
}

public class SceneSystemManager
{
	ISceneSystem current;

	bool nowLoading;

	public SceneSystemManager()
	{
		current = FirstScene();
		current?.OnEndLoad();
	}

	public IEnumerator ChangeScene(ISceneSystem nextScene)
	{
		nowLoading = true;

		var operation = SceneManager.LoadSceneAsync(nextScene.SceneName, LoadSceneMode.Single);
		nextScene.OnBeginLoad();
		while(!operation.isDone)
		{
			nextScene.OnLoading(operation.progress);
			yield return null;
		}
		nextScene.OnEndLoad();
		if(current != null) current.OnDiscard();
		current = nextScene;

		nowLoading = false;
	}

	public void Update()
	{
		if(current != null) current.OnUpdate();
	}

	/// <summary>
	/// 起動時のシーンはUnity側で生成されるので
	/// アクティブシーンから推測でシステムを割り当てる
	/// </summary>
	private ISceneSystem FirstScene()
	{
		string name = SceneManager.GetActiveScene().name;
		switch(name)
		{
			case GameSceneSystem.name: return new GameSceneSystem();
			case TitleSceneSystem.name: return new TitleSceneSystem();
		}
		return null;
	}
}