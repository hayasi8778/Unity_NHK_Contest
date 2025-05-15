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

    // シーン名を指定する変数
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
       　{

            Debug.LogWarning("プレイヤー接触");
            if (!PlateUse)//プレートを使わないならそのまま許可
            {
                SceneManager.LoadScene(sceneToLoad);
            }

            // プレートが押されていれば
            if (pressPlate.press)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
