using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理用
using UnityEngine.UI;             // UI操作用

public class SceneChanger : MonoBehaviour
{
    // シーン名を指定する変数
    [SerializeField] private string sceneToLoad;

    // ボタンのクリック時に呼ばれるメソッド
    public void ChangeScene()
    {
        Debug.Log("ボタン押されたよ");
        SceneManager.LoadScene(sceneToLoad);
    }
}