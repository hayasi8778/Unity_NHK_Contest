using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    // 遷移したいシーン名（Inspectorで設定可能）
    public string sceneName;


    private void Start()
    {
        Physics2D.gravity = new Vector2(0, -9.8f);
        Time.timeScale = 1f; 
    }
    // ボタンが押されたときに呼ばれる関数
    public void OnButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}