using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Audio
{
	/// <summary>
	/// 環境音
	/// Sceneにあらかじめ設置しておく
	/// </summary>
	public class EnvironmentSound : MonoBehaviour
	{
		[SerializeField]
		protected AudioSource _source;

		private void OnEnable()
		{
			if(_source == null)
			{
				Destroy(gameObject);
				return;
			}
			AudioManager.Instance.AddEnvironmentSound(this);
		}

		private void OnDisable()
		{
			AudioManager.Instance.RemoveEnvironmentSound(this);
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
	}
}