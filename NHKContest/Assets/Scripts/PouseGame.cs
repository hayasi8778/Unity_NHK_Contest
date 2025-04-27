using UnityEngine;

public class PauseGameScript : MonoBehaviour
{
    public static float timeScale = 0.0f;  // �|�[�Y���̎��Ԕ{��
    public static bool IsPaused { get; private set; } = false;

    public static void PauseGame() {
        Time.timeScale = timeScale; // �Q�[����~
        IsPaused = true;
    }

    public static void ResumeGame() {
        Time.timeScale = 1; // �Q�[���ĊJ
        IsPaused = false;
    }
}