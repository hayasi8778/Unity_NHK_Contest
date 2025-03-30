using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderTimeCounter : MonoBehaviour
{
    public Slider slider;   // スライダーコンポーネントへの参照
    float timeCounter = 0;     // FPSカウンタ

    void Start()
    {
        // 初期値を設定
        slider.value = 0.0f;
        slider.maxValue = 60.0f;
        slider.minValue = 0.0f;
    }

    void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= 0.1)
        {
            // timeCounterが１以上（前回の更新から１秒以上経過した）のとき、スライダーの値を増やす
            slider.value += 0.1f;
            timeCounter = 0;
        }
    }
}
