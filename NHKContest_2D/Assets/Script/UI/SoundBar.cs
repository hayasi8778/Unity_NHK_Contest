using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Scrollbar scrollbar;
    [HideInInspector]
    public bool isMouseOver = false; // マウスオーバー状態を管理する変数

    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        scrollbar.value = 1; // 初期値を1に設定（音量100%）

        gameObject.SetActive(false); // 初期状態では音量バーを非表示にする
    }

    void Update()
    {
        AudioListener.volume = scrollbar.value; // スクロールバーの値を音量に反映
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
