using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    public float timeScale = 0.0f;  // ポーズ中の時間倍率 

    void Update()
    {
        // ポーズ切り替え（例: Escキーでポーズ）
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume_Game();
            }
            else {
                Pause_Game();
            }
        }
    }

    public void Pause_Game() {
        Time.timeScale = timeScale; // ゲーム停止
        isPaused = true;
    }

    public void Resume_Game() {
        Time.timeScale = 1; // ゲーム再開
        isPaused = false;
    }
}