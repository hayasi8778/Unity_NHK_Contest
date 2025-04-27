using UnityEngine;

public class PauseGameScript : MonoBehaviour
{
    public static float timeScale = 0.0f;  // ƒ|[ƒY’†‚ÌŠÔ”{—¦
    public static bool IsPaused { get; private set; } = false;

    public static void PauseGame() {
        Time.timeScale = timeScale; // ƒQ[ƒ€’â~
        IsPaused = true;
    }

    public static void ResumeGame() {
        Time.timeScale = 1; // ƒQ[ƒ€ÄŠJ
        IsPaused = false;
    }
}