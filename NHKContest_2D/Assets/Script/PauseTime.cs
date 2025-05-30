using UnityEngine;

public class PauseTime : MonoBehaviour
{
    bool pauseFlg;

    private void Start()
    {
        pauseFlg = true;
        GetComponent<Renderer>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseFlg)
            {
                // �|�[�Y����
                Time.timeScale = 1.0f;
                GetComponent<Renderer>().enabled = true;
                pauseFlg = false;
            }
            else
            {
                // �|�[�Y
                Time.timeScale = 0.0f;
                GetComponent<Renderer>().enabled = false;
                pauseFlg = true;
            }
        }
    }
}
