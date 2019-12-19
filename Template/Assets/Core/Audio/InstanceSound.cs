using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace Audio
{
	/// <summary>
	/// ワールド内で発生する音
	/// </summary>
	public class InstanceSound : MonoBehaviour
	{
		protected AudioSource _source;

		public bool IsActive { get; private set; }

		public static InstanceSound Create()
		{
			var obj = new GameObject("SE");
			var component = obj.AddComponent<InstanceSound>();
			var source = component._source = obj.AddComponent<AudioSource>();
			component.IsActive = false;

			source.outputAudioMixerGroup = AudioManager.Instance.GetGroup(AudioManager.SoundType.WorldSE);
			source.playOnAwake = false;
			source.loop = false;
			return component;
		}

		/// <summary>
		/// 再生
		/// </summary>
		public void Play(AudioClip clip, Vector3 position)
		{
			transform.position = position;
			gameObject.SetActive(true);
			_source.clip = clip;
			_source.Play();
		}

		/// <summary>
		/// 一時停止
		/// </summary>
		public void Pause()
		{
			_source.Pause();
		}

		/// <summary>
		/// 再開
		/// </summary>
		public void UnPause()
		{
			_source.UnPause();
		}

		public void UpdateSelf()
		{
			if(IsActive && _source.time >= _source.clip.length)
			{
				IsActive = false;
				gameObject.SetActive(false);
			}
		}
	}
}