using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal :MonoBehaviour
{
    private PressPlate pressPlate;

    private void Start()
    {
        pressPlate = transform.parent.GetChild(0).GetComponent<PressPlate>();
    }

    // シーン名を指定する変数
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
       　{
            // プレートが押されていれば
            if (pressPlate.press)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
