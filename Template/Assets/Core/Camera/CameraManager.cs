using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSystem
{
	public class CameraManager : SingletonBehaviour<CameraManager>
	{
		ICameraController m_stash;
		ICameraController m_current;

		List<ITransformEffect> m_transformEffect;

		protected Camera targetCamera;

		public Vector3 CameraDirection { get { return Camera.main.transform.forward; } }

		/// <summary>
		/// カメラの状態によって移動ベクトルを補正する
		/// </summary>
		public void AdjustMoveDirection(ref Vector3 moveDirection)
		{
			var camDir = CameraDirection;
			camDir.y = 0;
			camDir.Normalize();
			var right = new Vector3(camDir.z, 0, -camDir.x);

			var result = camDir * moveDirection.z + right * moveDirection.x;
			moveDirection.x = result.x;
			moveDirection.y = result.y;
			moveDirection.z = result.z;
		}

		public void Awake()
		{
			m_transformEffect = new List<ITransformEffect>();
		}

		public void AddEffect(ITransformEffect effect)
		{
			m_transformEffect.Add(effect);
		}

		public void ChangeMode(ICameraController controller)
		{
			if(targetCamera == null)
			{
				targetCamera = Camera.main;
			}
			m_current?.OnFinalize(targetCamera);
			m_current = controller;
			m_current?.OnInitialize(targetCamera);
		}

		public void Stash(ICameraController controller)
		{
			m_stash = m_current;
			ChangeMode(controller);
		}

		public void Pop()
		{
			if(m_stash == null) return;
			ChangeMode(m_stash);
		}

		public void Run()
		{
			if(m_current == null) return;

			if(targetCamera == null)
			{
				targetCamera = Camera.main;
			}
			m_current.OnUpdate(targetCamera);
			m_transformEffect.RemoveAll((eff) =>
			{
				return eff.UpdateOrRemove(targetCamera);
			});
		}
	}


	public interface ICameraController
	{
		void OnInitialize(Camera camera);
		void OnUpdate(Camera camera);
		void OnFinalize(Camera camera);
	}

	public interface ITransformEffect
	{
		bool UpdateOrRemove(Camera camera);
	}


	public class ShakeEffect : ITransformEffect
	{
		public enum Magnitude
		{
			Small,
			Midlle,
			Large
		}

		float m_magnitude;
		float m_duration;
		float m_startTime;

		AnimationCurve m_curve;

		public ShakeEffect(Magnitude magnitude, int intensity, float duration)
		{
			m_magnitude = GetMagnitudeValue(magnitude);
			m_duration = duration;

			m_curve = new AnimationCurve();
			var once = duration / intensity;
			float total = 0.0f;
			for(int i = 1; i < intensity + 1; ++i)
			{
				m_curve.AddKey(total, 0.0f);
				total += once * 0.25f;
				m_curve.AddKey(total, -1.0f);
				total += once * 0.5f;
				m_curve.AddKey(total, 1.0f);
				total += once * 0.25f;
				m_curve.AddKey(total, 0.0f);
			}
			m_startTime = Time.realtimeSinceStartup;
		}

		public bool UpdateOrRemove(Camera camera)
		{
			float elapsed = Time.realtimeSinceStartup - m_startTime;
			float progress = elapsed / m_duration;
			float valueX = m_curve.Evaluate(elapsed) * m_magnitude * (1 - progress);
			float valueY = m_curve.Evaluate(elapsed / 2) * m_magnitude * (1 - progress);
			camera.transform.position += camera.transform.rotation * new Vector3(valueX, valueY, 0);
			return progress >= 1.0f;
		}

		static protected float GetMagnitudeValue(Magnitude magnitude)
		{
			switch(magnitude)
			{
				case Magnitude.Small: return 0.5f;
				case Magnitude.Midlle: return 1.0f;
				case Magnitude.Large: return 1.5f;
			}
			return 0.0f;
		}
	}
}