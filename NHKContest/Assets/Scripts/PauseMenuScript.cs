using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // �|�[�Y��ʂ�UI

    private void Start()
    {
        pauseMenuUI.SetActive(false); // ������Ԃ�UI���\���ɂ���
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
        PauseGameScript.PauseGame(); // ���Ԓ�~���Ăяo��
        pauseMenuUI.SetActive(true); // UI��\��
    }

    public void ResumeMenu()
    {
        PauseGameScript.ResumeGame(); // ���ԍĊJ���Ăяo��
        pauseMenuUI.SetActive(false); // UI���\��
    }
}
