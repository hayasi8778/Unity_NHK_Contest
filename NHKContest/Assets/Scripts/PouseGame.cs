using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    public float timeScale = 0.0f;  // �|�[�Y���̎��Ԕ{�� 

    void Update()
    {
        // �|�[�Y�؂�ւ��i��: Esc�L�[�Ń|�[�Y�j
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
        Time.timeScale = timeScale; // �Q�[����~
        isPaused = true;
    }

    public void Resume_Game() {
        Time.timeScale = 1; // �Q�[���ĊJ
        isPaused = false;
    }
}