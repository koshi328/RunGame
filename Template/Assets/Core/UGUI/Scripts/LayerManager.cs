using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.UI
{
	/// <summary>
	/// LayerManager
	/// UIに階層を作りUIの登録を管理する
	/// UIManagerの機能として統合している
	/// </summary>
	public partial class UIManager
	{
		public enum Layer
		{
			Background,
			Control,
			Overlay,
		}

		private class LayerInfo
		{
			public GameObject root { get; }
			public Layer layer { get; }
			public List<IPanelController> content { get; }
			public LayerInfo(GameObject root, Layer layer)
			{
				this.root = root;
				this.layer = layer;
				content = new List<IPanelController>();
			}
		}

		LayerInfo[] m_layerInfos;

		private void LayerManager_Initialize()
		{
			var num = System.Enum.GetValues(typeof(Layer)).Length;
			m_layerInfos = new LayerInfo[num];
			for(int i = 0; i < num; ++i)
			{
				Layer layer = (Layer)i;
				var root = new GameObject(layer.ToString(), typeof(RectTransform));
				root.transform.SetParent(m_overlayCanvas.transform, false);
				var rect = root.GetComponent<RectTransform>();
				rect.anchoredPosition = new Vector2(0, 0);
				rect.anchorMax = new Vector2(1, 1);
				rect.anchorMin = new Vector2(0, 0);
				rect.offsetMax = new Vector2(1, 1);
				rect.offsetMin = new Vector2(0, 0);
				m_layerInfos[i] = new LayerInfo(root, layer);
				root.SetActive(false);
			}
		}

		private LayerInfo GetInfo(Layer layer)
		{
			return m_layerInfos[(int)layer];
		}
		
		public void Register(IPanelController controller, Layer layer)
		{
			var info = GetInfo(layer);
			info.content.Add(controller);
			if(info.content.Count > 0)
			{
				info.root.SetActive(true);
			}

			controller.OnRegister(layer, info.root.transform);
		}

		public void Discard(IPanelController controller, Layer layer)
		{
			var info = GetInfo(layer);
			info.content.Remove(controller);
			if(info.content.Count <= 0)
			{
				info.root.SetActive(false);
			}
		}

		public void Discard(Layer layer)
		{
			var info = GetInfo(layer);
			foreach(var con in info.content)
			{
				con.OnDiscard();
			}
			info.content.Clear();
			info.root.SetActive(false);
		}

		public void AllDiscard()
		{
			foreach(var infos in m_layerInfos)
			{
				Discard(infos.layer);
			}
		}
	}

	public interface IPanel
	{
		void OnPanelDiscard();
	}

	public interface IPanelController
	{
		void OnRegister(UIManager.Layer layer, Transform parent);
		void OnDiscard();
	}

	public abstract class PanelController<T> : MonoBehaviour, IPanelController where T : IPanel
	{
		protected T panel;
		protected UIManager.Layer layer;

		void IPanelController.OnRegister(UIManager.Layer layer, Transform parent)
		{
			this.layer = layer;
			panel = CreatePanel(parent);
		}

		public abstract T CreatePanel(Transform parent);

		public virtual void OnDiscard()
		{
			Destroy(gameObject);
			panel.OnPanelDiscard();
		}

		public virtual void Close()
		{
			UIManager.Instance.Discard(this, layer);
		}
	}

	public abstract class ManagedPanel : PanelController<ManagedPanel> ,IPanel
	{
		public virtual void OnPanelDiscard() { }
	}
	
	/*
	* 使い方サンプル
	 */
	#region
	/*
	public class ManagedPanel_Sample : ManagedPanel
	{
		[SerializeField]
		protected UnityEngine.UI.Image image;

		private static string path = "";

		public static ManagedPanel_Sample Create()
		{
			var controller = new GameObject("ManagedPanel_Sample").AddComponent<ManagedPanel_Sample>();
			UIManager.Instance.Register(controller, UIManager.Layer.Control);
			return controller;
		}

		public override ManagedPanel CreatePanel(Transform parent)
		{
			var ui = GameObject.Instantiate(Resources.Load(path)) as GameObject;
			return ui.GetComponent<ManagedPanel>();
		}

		private void Awake()
		{
			image.color = Color.red;
		}
	}

	public class Panel_Sample : MonoBehaviour, IPanel
	{
		private static string path = "";

		public static Panel_Sample Create()
		{
			var ui = GameObject.Instantiate(Resources.Load(path)) as GameObject;
			return ui.GetComponent<Panel_Sample>();
		}

		void IPanel.OnPanelDiscard()
		{
			Destroy(gameObject);
		}
	}


	public class PanelController_Sample : PanelController<Panel_Sample>
	{
		public static PanelController_Sample Create()
		{
			var controller = new GameObject("PanelController_Sample").AddComponent<PanelController_Sample>();
			UIManager.Instance.Register(controller, UIManager.Layer.Control);
			return controller;
		}

		public override Panel_Sample CreatePanel(Transform parent)
		{
			return Panel_Sample.Create();
		}
	}
	*/
	#endregion
}