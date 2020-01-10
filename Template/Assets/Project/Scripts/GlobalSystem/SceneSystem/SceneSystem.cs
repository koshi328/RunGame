using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystemManager : SingletonBehaviour<SceneSystemManager>
{
	SceneSystemAbstract current;

	bool nowLoading;

	public SceneSystemAbstract Current { get { return current; } }

	public void Initialize()
	{
		current = FirstScene();
		current?.OnEndLoad();
	}

	public IEnumerator ChangeScene(SceneSystemAbstract nextScene)
	{
		nowLoading = true;

		var operation = SceneManager.LoadSceneAsync(nextScene.SceneName, LoadSceneMode.Single);
		yield return nextScene.OnBeginLoad();
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

	public void OnUpdate()
	{
		if(current != null) current.OnUpdate();
	}

	public void OnLateUpdate()
	{
		if(current != null) current.OnLateUpdate();
	}

	/// <summary>
	/// 起動時のシーンはUnity側で生成されるので
	/// アクティブシーンから推測でシステムを割り当てる
	/// </summary>
	private SceneSystemAbstract FirstScene()
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

public abstract class SceneSystemAbstract
{
	public virtual string SceneName { get; }
	public virtual void OnUpdate() { }
	public virtual void OnLateUpdate() { }
	public virtual IEnumerator OnBeginLoad() { yield break; }
	public virtual void OnLoading(float progress) { }
	public virtual void OnEndLoad() { }
	public virtual void OnDiscard() { }
}