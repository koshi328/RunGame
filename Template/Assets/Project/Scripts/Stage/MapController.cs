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

	List<MapChipBase> data;

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

		data = new List<MapChipBase>();
	}

	public void Load(string fileName)
	{
		Clear();
		var asset = LoadAsset(fileName);
		using(var open = new System.IO.MemoryStream(asset))
		{
			using(var reader = new System.IO.StreamReader(open))
			{
				int baseHeight = -1;
				endLine = 0;
				for(int i = 0; !reader.EndOfStream; ++i)
				{
					++baseHeight;
					var line = reader.ReadLine().Split(',');
					for(int j = 0; j < line.Length; ++j)
					{
						var index = int.Parse(line[j]);
						if((ChipType)index == ChipType.None) continue;
						var chip = Instantiate(chipAsset[index]);
						chip.transform.position = new Vector2(j, -i);
						data.Add(chip.GetComponent<MapChipBase>());

						if(endLine < j) endLine = j;
					}
				}

				var begin = Instantiate(chipAsset[(int)ChipType.Floor]);
				begin.transform.position = new Vector3(-10, -baseHeight, 0);
				begin.transform.localScale = new Vector3(20, 1, 1);

				var end = Instantiate(chipAsset[(int)ChipType.Floor]);
				end.transform.position = new Vector3(endLine + 10, -baseHeight, 0);
				end.transform.localScale = new Vector3(20, 1, 1);

				endLine += 3;
			}
		}
	}

	protected void InitializeMap()
	{
		foreach(var chip in data)
		{
			chip.Initialize();
		}
	}

	public void CheckLoop(Character.PlayerCharacter player)
	{
		if(!player) return;
		var position = player.transform.position;
		if(position.x < endLine) return;
		position.x -= endLine;
		player.transform.position = position;

		InitializeMap();
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

public class MapChipBase : MonoBehaviour
{
	public virtual void Initialize()
	{

	}
}