using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal :MonoBehaviour
{
    private PressPlate pressPlate;
    public bool PlateUse;
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

            Debug.LogWarning("�v���C���[�ڐG");
            if (!PlateUse)//�v���[�g���g��Ȃ��Ȃ炻�̂܂܋���
            {
                SceneManager.LoadScene(sceneToLoad);
            }

            // �v���[�g��������Ă����
            if (pressPlate.press)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
