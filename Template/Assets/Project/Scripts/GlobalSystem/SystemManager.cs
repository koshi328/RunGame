using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using Character;
using CameraSystem;
using GameSystem.UI;

public class SystemManager : SingletonBehaviour<SystemManager>
{
	#region 起動
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	public static void __Initialize__()
	{
		SystemManager.Instance.Initialize();
	}
	#endregion

	AudioManager audioManager;
	UIManager uiManager;
	CameraManager cameraManager;
	SceneSystemManager sceneSystemManager;

	private void Initialize()
	{
		audioManager = AudioManager.Instance;
		uiManager = UIManager.Instance;
		cameraManager = CameraManager.Instance;
		sceneSystemManager = new SceneSystemManager();
	}

	private void Update()
	{
		cameraManager.Run();
		sceneSystemManager.Update();
	}

	public void ChangeScene(ISceneSystem nextScene)
	{
		StartCoroutine(sceneSystemManager.ChangeScene(nextScene));
	}
}
