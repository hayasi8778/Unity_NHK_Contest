using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���J�ڂ̊m�F�p
/// </summary>
public class SceneChangeCheck : MonoBehaviour
{
    // �ύX����V�[���̖��O
    [SerializeField] private string sceneName;
    public void Update()
    {
        // �X�y�[�X�������ƃV�[�����ς��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
