using UnityEngine;
using UnityEngine.EventSystems;

public class Sound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("昇順")]
    public GameObject[] SoundVolume_Icon;
    public GameObject soundBar;
    [HideInInspector]
    public bool isMouseOver = false; // マウスオーバー状態を管理する変数

    public void Update()
    {
        if (isMouseOver || soundBar.GetComponent<SoundBar>().isMouseOver)
        {
            // マウスオーバー状態または音量バーがマウスオーバー状態の場合、音量バーを表示
            soundBar.SetActive(true);
        }
        else
        {
            // どちらもマウスオーバー状態でない場合、音量バーを非表示
            soundBar.SetActive(false);
        }

        if (AudioListener.volume == 0)
        {
            // 音量が0の場合
            SoundVolume_Icon[0].SetActive(true);
            SoundVolume_Icon[1].SetActive(false);
            SoundVolume_Icon[2].SetActive(false);
        }
        else if (AudioListener.volume <= 0.5)
        {
            // 音量が0より大きく、0.5未満の場合
            SoundVolume_Icon[0].SetActive(false);
            SoundVolume_Icon[1].SetActive(true);
            SoundVolume_Icon[2].SetActive(false);
        }
        else
        {
            // 音量が0.5以上の場合
            SoundVolume_Icon[0].SetActive(false);
            SoundVolume_Icon[1].SetActive(false);
            SoundVolume_Icon[2].SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }
}

