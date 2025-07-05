using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    // �J�ڂ������V�[�����iInspector�Őݒ�\�j
    public string sceneName;


    private void Start()
    {
        Physics2D.gravity = new Vector2(0, -9.8f);
        Time.timeScale = 1f; 
    }
    // �{�^���������ꂽ�Ƃ��ɌĂ΂��֐�
    public void OnButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}