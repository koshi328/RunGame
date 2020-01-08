using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapController : MonoBehaviour
{
	public enum ChipType
	{
		None,
		Floor,
		Wall,
		Coin,
		BigCoin,
	}

	List<GameObject> data;

	GameObject[] chipAsset;

	public float endLine;

	public static MapController Create()
	{
		var obj = new GameObject("MapController");
		var component = obj.AddComponent<MapController>();
		return component;
	}

	private void Awake()
	{
		chipAsset = new GameObject[]
		{
			null,
			Resources.Load("Prefabs/Stage/Floor") as GameObject,
			Resources.Load("Prefabs/Stage/Wall") as GameObject,
			Resources.Load("Prefabs/Stage/Coin") as GameObject,
			Resources.Load("Prefabs/Stage/Coin") as GameObject,
		};

		data = new List<GameObject>();
	}

	public void Load(string fileName)
	{
		Clear();
		var asset = LoadAsset(fileName);
		using(var open = new System.IO.MemoryStream(asset))
		{
			using(var reader = new System.IO.StreamReader(open))
			{
				endLine = 0;
				for(int i = 0; !reader.EndOfStream; ++i)
				{
					var line = reader.ReadLine().Split(',');
					for(int j = 0; j < line.Length; ++j)
					{
						var index = int.Parse(line[j]);
						if((ChipType)index == ChipType.None) continue;
						var chip = Instantiate(chipAsset[index]);
						chip.transform.position = new Vector2(j, -i);
						data.Add(chip);

						if(endLine < j) endLine = j;
					}
				}
				endLine -= 5;
			}
		}
	}

	public void CheckLoop(Character.PlayerCharacter player)
	{
		if(!player) return;
		var position = player.rigidbody.position;
		if(position.x < endLine) return;
		position.x -= endLine;
		player.rigidbody.position = position;
	}

	public void Clear()
	{
		foreach(var info in data)
		{
			Destroy(info);
		}
		data.Clear();
	}

	protected byte[] LoadAsset(string fileName)
	{
		//return Resources.Load(path) as TextAsset;
		var path = (Application.streamingAssetsPath) + "/" + (fileName);
		return System.IO.File.ReadAllBytes(path);
	}
}