using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderTimeCounter : MonoBehaviour
{
    public Slider slider;   // スライダーコンポーネントへの参照
    float timeCounter = 0;     // FPSカウンタ
    float ValueOld = 0;   //デバック用にスライダーのログ位置とる

    void Start()
    {
        // 初期値を設定
        slider.value = 0.0f;
        slider.maxValue = 300.0f;
        slider.minValue = 0.0f;
    }

    void Update()
    {

        if (slider == null)
        {
            Debug.LogError("シェーダーが指定されてないぞ(SliderTimeCounter)");
        }

            timeCounter += Time.deltaTime;

        if (timeCounter >= 0.1)
        {
            ValueOld = slider.value;
            // timeCounterが１以上（前回の更新から１秒以上経過した）のとき、スライダーの値を増やす
            slider.value += 0.1f;
            timeCounter = 0;

            //Debug.Log($"ValueOld: {ValueOld}, slider.value: {slider.value}");
        }
    }
}