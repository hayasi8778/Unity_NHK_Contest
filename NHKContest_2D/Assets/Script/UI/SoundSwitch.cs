using UnityEngine;

public class Sound : MonoBehaviour
{
    public Camera mainCamera; // ƒƒCƒ“ƒJƒƒ‰‚ğQÆ‚·‚é‚½‚ß‚Ì•Ï”
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
