using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject DarkOverRay; // 暗い背景
    public GameObject menuUI;
    Animator manuAnimator; // ↑メニューのアニメーター
    private bool isPaused = false;

    void Start()
    {
        manuAnimator = menuUI.GetComponent<Animator>();
        DarkOverRay.SetActive(false); // 初期状態では暗い背景を非表示にする（意味ねぇ！なんで？？？）
    }

    public void GamePause()
    {
        Debug.Log("オブジェクトがクリックされました！");

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;    // isPausedがtrueなら時間を止める、falseなら時間を進める
        DarkOverRay.SetActive(isPaused);

        if(isPaused)
        {
            // ゲームが一時停止されたときの処理
            Debug.Log("ゲームが一時停止されました。");
            manuAnimator.SetTrigger("Pause_on");
        }
        else
        {
            // ゲームが再開されたときの処理
            Debug.Log("ゲームが再開されました。");
            manuAnimator.SetTrigger("Pause_off");
        }
    }
}
