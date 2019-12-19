using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
	public class AudioManager : SingletonBehaviour<AudioManager>
	{
		public enum SoundType
		{
			WorldSE,
			UiSE,
			BGM,
			Environment,
		}

		private AudioMixer _audioMixer;
		private AudioMixerGroup _worldSeGroup;
		private AudioMixerGroup _uiSeGroup;
		private AudioMixerGroup _bgmGroup;
		private AudioMixerGroup _environmentGroup;

		private AudioSource[] _bgmSource;
		private AudioSource _uiSeSource;
		private List<InstanceSound> _worldSeList;
		private List<EnvironmentSound> _environmentList;

		private int MainBgmIndex = 0;
		private int SubBgmIndex { get { return 1 - MainBgmIndex; } }

		public AudioMixerGroup GetGroup(SoundType type)
		{
			switch(type)
			{
				case SoundType.WorldSE: return _worldSeGroup;
				case SoundType.UiSE: return _uiSeGroup;
				case SoundType.BGM: return _bgmGroup;
				case SoundType.Environment: return _environmentGroup;
			}
			return null;
		}

		public void Awake()
		{
			_audioMixer = Resources.Load("Audio/AudioMixer") as AudioMixer;

			_worldSeList = new List<InstanceSound>();
			_environmentList = new List<EnvironmentSound>();

			_bgmSource = new AudioSource[2];
			for(int i = 0;i < 2;++i)
			{
				var tmp = _bgmSource[i] = gameObject.AddComponent<AudioSource>();
				tmp.playOnAwake = false;
				tmp.loop = true;
			}

			_uiSeSource = gameObject.AddComponent<AudioSource>();
			_uiSeSource.playOnAwake = false;
			_uiSeSource.loop = false;

			// 完全一致にしないと予期しないGroupを取得する可能性がある
			_worldSeGroup = _audioMixer.FindMatchingGroups("WorldSE")[0];
			_uiSeGroup = _audioMixer.FindMatchingGroups("UiSE")[0];
			_bgmGroup = _audioMixer.FindMatchingGroups("BGM")[0];
			_environmentGroup = _audioMixer.FindMatchingGroups("Environment")[0];
		}

		public void AddEnvironmentSound(EnvironmentSound sound)
		{
			_environmentList.Add(sound);
		}

		public void RemoveEnvironmentSound(EnvironmentSound sound)
		{
			_environmentList.Remove(sound);
		}

		public void PlayBGM(AudioClip clip, bool fade = false)
		{
			StopAllCoroutines();
			StartCoroutine(_PlayBGM(clip, fade));
		}
		private IEnumerator _PlayBGM(AudioClip clip, bool fade = false)
		{
			var sub = _bgmSource[SubBgmIndex];
			var main = _bgmSource[MainBgmIndex];
			sub.Stop();
			sub.clip = clip;
			sub.volume = 0.0f;
			main.volume = 1.0f;
			sub.Play();
			if(fade)
			{
				do
				{
					main.volume -= Time.deltaTime;
					sub.volume += Time.deltaTime;
					yield return null;
				} while(main.volume <= 0.0f);
			}
			else
			{
				main.volume = 0.0f;
				sub.volume = 1.0f;
			}

			MainBgmIndex = SubBgmIndex;
		}

		public void PlaySE(AudioClip clip, Vector3 position)
		{
			InstanceSound instance = null;
			for(int i = 0; i < _worldSeList.Count; ++i)
			{
				if(!_worldSeList[i].IsActive)
				{
					instance = _worldSeList[i];
					break;
				}
			}
			if(instance == null)
			{
				instance = InstanceSound.Create();
				_worldSeList.Add(instance);
			}

			instance.Play(clip, position);
		}

		public void PlayUiSE(AudioClip clip)
		{
			_uiSeSource.clip = clip;
			_uiSeSource.Play();
		}

		public void Pause()
		{
			foreach(var worldSe in _worldSeList) { worldSe.Pause(); }
			foreach(var env in _environmentList) { }

		}

		public void UnPause()
		{
			foreach(var worldSe in _worldSeList) { worldSe.UnPause(); }
			foreach(var env in _environmentList) { }
		}
	}
}