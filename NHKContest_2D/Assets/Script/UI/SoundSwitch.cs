using UnityEngine;

public class Sound : MonoBehaviour
{
    public Camera mainCamera; // ���C���J�������Q�Ƃ��邽�߂̕ϐ�
    public GameObject soundSwitchButton;

    public void Start()
    {
        
    }

    public void SoundSwitch()
    {
        mainCamera.GetComponent<AudioListener>().enabled = !mainCamera.GetComponent<AudioListener>().enabled;

        soundSwitchButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
