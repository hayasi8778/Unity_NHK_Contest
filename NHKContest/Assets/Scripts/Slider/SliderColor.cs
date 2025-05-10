using UnityEngine;
using UnityEngine.UI;

public class SliderColor : MonoBehaviour
{
    /*
    public Slider slider;
    public Image redProgress; // 赤い進行部分の Image

    public float fixedHeight = 20f; // 高さを固定する値

    
    void Update()
    {
        if (slider != null && redProgress != null)
        {
            float percentage = slider.value / slider.maxValue;

            // 左端を固定
            redProgress.rectTransform.anchorMin = new Vector2(0, 0);
            redProgress.rectTransform.anchorMax = new Vector2(percentage, 1);

            // 高さを固定
            redProgress.rectTransform.sizeDelta = new Vector2(redProgress.rectTransform.sizeDelta.x, fixedHeight);
        }
    }
    */
    /*
    public Slider slider;
    public Image redProgress; // 再生バーの赤い部分

    private float initialWidth; // 初期幅
    private RectTransform sliderRect; // スライダーの RectTransform

    void Start()
    {
        if (slider != null && redProgress != null)
        {
            sliderRect = slider.GetComponent<RectTransform>();

            // 初期のスライダーの幅を取得
            initialWidth = sliderRect.sizeDelta.x;

            //initialWidth = redProgress.flexibleWidth;

            // 赤いImageの初期状態を設定（幅を0にする）
            redProgress.rectTransform.sizeDelta = new Vector2(0, redProgress.rectTransform.sizeDelta.y);
        }
    }

    void Update()
    {
        if (slider != null && redProgress != null)
        {
            float percentage = slider.value / slider.maxValue; // 進行割合

            // スライダーの進行度に応じて赤いImageの幅を変更
            redProgress.rectTransform.sizeDelta = new Vector2(initialWidth * percentage, redProgress.rectTransform.sizeDelta.y);
        }
    }
    */
    public Slider slider;
    public Image redProgress; // 再生バーの赤い部分

    private float initialWidth; // 初期幅
    private RectTransform sliderRect; // スライダーの RectTransform
    private RectTransform redProgressRect; // 赤いImageの RectTransform

    public float startOffset = 10f; // **開始位置の調整用変数**
    public float widthMultiplier = 3f; // **Imageの幅の増加倍率（自由に変更可能）**

    void Start()
    {
        if (slider != null && redProgress != null)
        {
            sliderRect = slider.GetComponent<RectTransform>();
            redProgressRect = redProgress.GetComponent<RectTransform>();

            // 初期のスライダーの幅を取得
            initialWidth = sliderRect.sizeDelta.x;

            // **赤いImageのアンカーを左側に固定**
            redProgressRect.anchorMin = new Vector2(0f, 0.5f);
            redProgressRect.anchorMax = new Vector2(0f, 0.5f);
            redProgressRect.pivot = new Vector2(0f, 0.5f); // ピボットを左側に設定

            // 赤いImageの初期状態を設定（幅を0にする + 開始位置をずらす）
            redProgressRect.sizeDelta = new Vector2(0, redProgressRect.sizeDelta.y);
            redProgressRect.anchoredPosition = new Vector2(startOffset, redProgressRect.anchoredPosition.y);
        }
    }

    void Update()
    {
        if (slider != null && redProgress != null)
        {
            float percentage = slider.value / slider.maxValue; // 進行割合

            // **サイズ変更の倍率を適用**
            redProgressRect.sizeDelta = new Vector2(initialWidth * percentage * widthMultiplier, redProgressRect.sizeDelta.y);
            redProgressRect.anchoredPosition = new Vector2(startOffset, redProgressRect.anchoredPosition.y);
        }
    }






}
