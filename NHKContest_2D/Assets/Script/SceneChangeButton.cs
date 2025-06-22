using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    // �J�ڂ������V�[�����iInspector�Őݒ�\�j
    public string sceneName;


    private void Start()
    {
        Time.timeScale = 1f; 
    }
    // �{�^���������ꂽ�Ƃ��ɌĂ΂��֐�
    public void OnButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}