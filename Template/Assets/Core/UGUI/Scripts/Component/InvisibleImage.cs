using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameSystem.UI
{
	public class InvisibleImage : Image
	{
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			toFill.Clear();
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(InvisibleImage))]
	public class InvisibleImageProperty : Editor
	{
		public override void OnInspectorGUI()
		{

		}
	}
#endif

}