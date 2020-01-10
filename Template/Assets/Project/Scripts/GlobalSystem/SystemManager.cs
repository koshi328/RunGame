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
		sceneSystemManager = SceneSystemManager.Instance;
		sceneSystemManager.Initialize();
	}

	private void Update()
	{
		cameraManager.Run();
		sceneSystemManager.OnUpdate();
	}

	private void LateUpdate()
	{
		sceneSystemManager.OnLateUpdate();
	}

	public Coroutine ChangeScene(SceneSystemAbstract sceneSystem)
	{
		return StartCoroutine(sceneSystemManager.ChangeScene(sceneSystem));
	}
}
