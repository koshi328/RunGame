using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class GameSceneSystem : SceneSystemAbstract
{
	public const string name = "Sample";
	public override string SceneName { get { return name; } }

	private PlayerCharacter player;
	private CharacterPilot pilot;
	private MapController mapController;

	private float elapsedTime;
	private float speedRate;
	private bool isGameStart;

	private void OnActiveScene()
	{
		mapController = MapController.Create();
		mapController.Load("Map/map001.txt");

		var playerAsset = Resources.Load("Prefabs/Player");
		var playerObj = GameObject.Instantiate(playerAsset) as GameObject;
		player = playerObj.GetComponent<PlayerCharacter>();
		pilot = CharacterPilot.Create();
		pilot.Initialize(player);

		CameraSystem.CameraManager.Instance.ChangeMode(new CameraSystem.SideScroll_CameraController(playerObj.transform, new Vector3(0, 2, -10)));

		SceneSystemManager.Instance.StartCoroutine(StartFlow());
	}

	private IEnumerator StartFlow()
	{
		isGameStart = false;
		player.SetMoveSpeed(0.0f);

		yield return new WaitForSeconds(2.0f);

		player.SetMoveSpeed(8.0f);
		isGameStart = true;
	}

	public override void OnLateUpdate()
	{
		if(isGameStart)
		{
			elapsedTime += Time.deltaTime;

			mapController?.CheckLoop(player);
			pilot?.UpdateSelf();

			if(speedRate < 2.0f)
			{
				speedRate = 1.0f + (elapsedTime / 10.0f * 0.1f);
			}

			if(Time.timeScale != speedRate)
			{
				Time.timeScale = speedRate;
			}
		}

		player?.UpdateSelf();
	}

	public override void OnEndLoad()
	{
		OnActiveScene();
	}

	public override void OnDiscard()
	{
		CameraSystem.CameraManager.Instance.ChangeMode(new CameraSystem.Default_CameraController(new Vector3(0, 2, -10), Quaternion.identity));
	}
}
