using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal :MonoBehaviour
{

    // ƒV[ƒ“–¼‚ğw’è‚·‚é•Ï”
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        
    }
}
