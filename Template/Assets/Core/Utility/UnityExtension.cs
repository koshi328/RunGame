using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class UnityExtension
{
	public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
	{
		var component = obj.GetComponent<T>();
		if(!component)
		{
			component = obj.AddComponent<T>();
		}
		return component;
	}

	public static void SetPositionX(this Transform transform, float value)
	{
		Vector3 temp = transform.position;
		temp.x = value;
		transform.position = temp;
	}

	public static void SetPositionY(this Transform transform, float value)
	{
		Vector3 temp = transform.position;
		temp.y = value;
		transform.position = temp;
	}

	public static void SetPositionZ(this Transform transform, float value)
	{
		Vector3 temp = transform.position;
		temp.z = value;
		transform.position = temp;
	}
	
		public static void SetLocalPositionX(this Transform transform, float value)
	{
		Vector3 temp = transform.localPosition;
		temp.x = value;
		transform.localPosition = temp;
	}

	public static void SetLocalPositionY(this Transform transform, float value)
	{
		Vector3 temp = transform.localPosition;
		temp.y = value;
		transform.localPosition = temp;
	}

	public static void SetLocalPositionZ(this Transform transform, float value)
	{
		Vector3 temp = transform.localPosition;
		temp.z = value;
		transform.localPosition = temp;
	}
}

#if !UNITY_EDITOR
public class Debug
{
	public static bool developerConsoleVisible { get { return UnityEngine.Debug.developerConsoleVisible; } set { UnityEngine.Debug.developerConsoleVisible = value; } }
	public static ILogger unityLogger { get { return UnityEngine.Debug.unityLogger; } }
	public static bool isDebugBuild { get { return UnityEngine.Debug.isDebugBuild; } }

#region Assert
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void Assert(bool condition, string message, Object context) { UnityEngine.Debug.Assert(condition, message, context); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void Assert(bool condition) { UnityEngine.Debug.Assert(condition); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void Assert(bool condition, object message, Object context) { UnityEngine.Debug.Assert(condition, message, context); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void Assert(bool condition, string message) { UnityEngine.Debug.Assert(condition, message); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void Assert(bool condition, object message) { UnityEngine.Debug.Assert(condition, message); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void Assert(bool condition, Object context) { UnityEngine.Debug.Assert(condition, context); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void AssertFormat(bool condition, Object context, string format, params object[] args) { UnityEngine.Debug.AssertFormat(condition, context, format, args); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void AssertFormat(bool condition, string format, params object[] args) { UnityEngine.Debug.AssertFormat(condition, format, args); }
#endregion

#region Log
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void Log(object message) { UnityEngine.Debug.Log(message); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void Log(object message, Object context) { UnityEngine.Debug.Log(message, context); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogAssertion(object message, Object context) { UnityEngine.Debug.LogAssertion(message, context); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogAssertion(object message) { UnityEngine.Debug.LogAssertion(message); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogAssertionFormat(Object context, string format, params object[] args) { UnityEngine.Debug.LogAssertionFormat(context, format, args); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogAssertionFormat(string format, params object[] args) { UnityEngine.Debug.LogAssertionFormat(format, args); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogError(object message, Object context) { UnityEngine.Debug.LogError(message, context); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogError(object message) { UnityEngine.Debug.LogError(message); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogErrorFormat(Object context, string format, params object[] args) { UnityEngine.Debug.LogErrorFormat(context, format, args); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogErrorFormat(string format, params object[] args) { UnityEngine.Debug.LogErrorFormat(format, args); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogException(System.Exception exception) { UnityEngine.Debug.LogException(exception); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogException(System.Exception exception, Object context) { UnityEngine.Debug.LogException(exception, context); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogFormat(string format, params object[] args) { UnityEngine.Debug.LogFormat(format, args); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogFormat(Object context, string format, params object[] args) { UnityEngine.Debug.LogFormat(context, format, args); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args) { UnityEngine.Debug.LogFormat(logType, logOptions, context,format, args); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogWarning(object message, Object context) { UnityEngine.Debug.LogWarning(message, context); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogWarning(object message) { UnityEngine.Debug.LogWarning(message); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogWarningFormat(Object context, string format, params object[] args) { UnityEngine.Debug.LogWarningFormat(context, format, args); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void LogWarningFormat(string format, params object[] args) { UnityEngine.Debug.LogWarningFormat(format, args); }
#endregion

#region 描画
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration) { UnityEngine.Debug.DrawLine(start, end, color, duration); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color) { UnityEngine.Debug.DrawLine(start, end, color); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void DrawLine(Vector3 start, Vector3 end) { UnityEngine.Debug.DrawLine(start, end); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void DrawLine(Vector3 start, Vector3 end, [UnityEngine.Internal.DefaultValue("Color.white")] Color color, [UnityEngine.Internal.DefaultValue("0.0f")] float duration, [UnityEngine.Internal.DefaultValue("true")] bool depthTest) { UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration) { UnityEngine.Debug.DrawRay(start, dir, color, duration); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color) { UnityEngine.Debug.DrawRay(start, dir, color); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void DrawRay(Vector3 start, Vector3 dir, [UnityEngine.Internal.DefaultValue("Color.white")] Color color, [UnityEngine.Internal.DefaultValue("0.0f")] float duration, [UnityEngine.Internal.DefaultValue("true")] bool depthTest) { UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void DrawRay(Vector3 start, Vector3 dir) { UnityEngine.Debug.DrawRay(start, dir); }
#endregion

	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void Break() { UnityEngine.Debug.Break(); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void ClearDeveloperConsole() { UnityEngine.Debug.ClearDeveloperConsole(); }
	[System.Diagnostics.Conditional("DEBUG_LOG")]
	public static void DebugBreak() { UnityEngine.Debug.DebugBreak(); }
}
#endif