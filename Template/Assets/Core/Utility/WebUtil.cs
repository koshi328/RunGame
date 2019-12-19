using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class WebUtil
{
	public static IEnumerator Request(Action<UnityWebRequest, bool> result, Func<UnityWebRequest> createFunc, float timeout, int retryCount)
	{
		using(var www = createFunc())
		{
			var lastProgressTime = 0.0f;
			var nonProgressTime = 0.0f;
			bool isTimeout = false;

			var request = www.SendWebRequest();
			do
			{
				lastProgressTime = www.downloadProgress;
				yield return null;

				if(lastProgressTime != www.downloadProgress)
				{
					nonProgressTime = 0.0f;
				}
				else
				{
					nonProgressTime += Time.unscaledDeltaTime;
				}
				isTimeout = nonProgressTime > timeout;
			} while(!request.isDone && !request.webRequest.isNetworkError && !isTimeout);

			// 失敗
			bool fail = (request.webRequest.isNetworkError || isTimeout);
			if(fail && retryCount > 0)
			{
				www.Abort();
				www.Dispose();
				yield return Request(result, createFunc, timeout, --retryCount);
			}
			else
			{
				// 結果(詳細, 成否)
				result?.Invoke(www, !fail);
			}
		}
	}
}
