using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal :MonoBehaviour
{
    private PressPlate pressPlate;

    private void Start()
    {
        pressPlate = transform.parent.GetChild(0).GetComponent<PressPlate>();
    }

    // �V�[�������w�肷��ϐ�
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
       �@{
            // �v���[�g��������Ă����
            if (pressPlate.press)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
