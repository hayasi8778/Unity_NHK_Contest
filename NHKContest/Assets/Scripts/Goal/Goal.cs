using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal :MonoBehaviour
{

    // �V�[�������w�肷��ϐ�
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        
    }
}
