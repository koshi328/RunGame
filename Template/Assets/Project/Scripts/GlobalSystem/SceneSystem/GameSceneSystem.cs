using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class GameSceneSystem : ISceneSystem
{
	public const string name = "Sample";
	string ISceneSystem.SceneName { get { return name; } }

	CharacterBase player;
	CharacterPilot pilot;

	private void OnActiveScene()
	{
		var playerAsset = Resources.Load("Prefabs/Player");
		var playerObj = GameObject.Instantiate(playerAsset) as GameObject;
		player = playerObj.GetComponent<CharacterBase>();
		pilot = CharacterPilot.Create();
		pilot.Initialize(player);

		CameraSystem.CameraManager.Instance.ChangeMode(new CameraSystem.SideScroll_CameraController(playerObj.transform, new Vector3(0, 2, -10)));
	}
	
	void ISceneSystem.OnUpdate()
	{
		pilot?.UpdateSelf();
		player?.UpdateSelf();
	}

	void ISceneSystem.OnBeginLoad()
	{

	}

	void ISceneSystem.OnLoading(float progress)
	{

	}

	void ISceneSystem.OnEndLoad()
	{
		OnActiveScene();
	}

	void ISceneSystem.OnDiscard()
	{
		CameraSystem.CameraManager.Instance.ChangeMode(new CameraSystem.Default_CameraController(new Vector3(0, 2, -10), Quaternion.identity));
	}
}
