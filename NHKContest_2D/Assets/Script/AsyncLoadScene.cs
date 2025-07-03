using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoadScene : MonoBehaviour
{ 
    private static Dictionary<string, AsyncOperation> asyncOperationMap = new Dictionary<string, AsyncOperation>();

    public static void LoadScene(string sceneName)
    {
        asyncOperationMap[sceneName] = SceneManager.LoadSceneAsync(sceneName);
        asyncOperationMap[sceneName].allowSceneActivation = false;
    }

    public static float isLoadedScene(string sceneName)
    {
        return asyncOperationMap.ContainsKey(sceneName) ? asyncOperationMap[sceneName].progress : 0;
    }

    public static void LoadedSceneChange(string sceneName)
    {
        if (asyncOperationMap.ContainsKey(sceneName))
        {
            asyncOperationMap[sceneName].allowSceneActivation = true;
        }
        else
        {
            Debug.Log("ì«Ç›çûÇ‹ÇÍÇƒÇ»Ç¢ÇÊ");
        }
    }
}
