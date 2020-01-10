using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class GameSceneSystem : SceneSystemAbstract
{
	private enum GameState
	{
		Wait,
		Play,
		GameOver,
	}

	public const string name = "Sample";
	public override string SceneName { get { return name; } }

	private PlayerCharacter player;
	private CharacterPilot pilot;
	private MapController mapController;

	private float elapsedTime;
	private float speedRate;
	private GameState gameState;

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
		gameState = GameState.Wait;
		player.SetMoveSpeed(0.0f);

		yield return new WaitForSeconds(2.0f);

		player.SetMoveSpeed(8.0f);
		gameState = GameState.Play;
	}

	private IEnumerator GameOverFlow()
	{
		player.SetMoveSpeed(0.0f);
		yield return new WaitForSeconds(2.0f);
		CameraSystem.CameraManager.Instance.ChangeMode(new CameraSystem.Default_CameraController(new Vector3(0, 2, -10), Quaternion.identity));
		SystemManager.Instance.ChangeScene(new TitleSceneSystem());
	}

	public override void OnLateUpdate()
	{
		if(gameState == GameState.GameOver)
		{
			return;
		}

		if(gameState == GameState.Play)
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

			if(player != null)
			{
				player.UpdateSelf();
				if(player.IsGameOver)
				{
					gameState = GameState.GameOver;
					SystemManager.Instance.StartCoroutine(GameOverFlow());
				}
			}
		}
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
