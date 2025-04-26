using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // ポーズ画面のUI

    private void Start()
    {
        pauseMenuUI.SetActive(false); // 初期状態でUIを非表示にする
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGameScript.IsPaused)
            {
                ResumeMenu();
            }
            else
            {
                PauseMenu();
            }
        }
    }

    public void PauseMenu()
    {
        PauseGameScript.PauseGame(); // 時間停止を呼び出し
        pauseMenuUI.SetActive(true); // UIを表示
    }

    public void ResumeMenu()
    {
        PauseGameScript.ResumeGame(); // 時間再開を呼び出し
        pauseMenuUI.SetActive(false); // UIを非表示
    }
}
