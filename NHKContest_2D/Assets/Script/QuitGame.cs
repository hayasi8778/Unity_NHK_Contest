using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // このメソッドをボタンに紐づけます
    public void Quit()
    {
        // エディタとビルドで動作を分ける
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
