using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移の確認用
/// </summary>
public class SceneChangeCheck : MonoBehaviour
{
    // 変更するシーンの名前
    [SerializeField] private string sceneName;
    public void Update()
    {
        // スペースを押すとシーンが変わる
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
