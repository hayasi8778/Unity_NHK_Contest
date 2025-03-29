using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider slider; // スライダーコンポーネントへの参照

    void Start()
    {
        // 初期値を設定
        slider.value = 0.5f;

        // スライダーの値が変更されたときに呼び出されるイベントを登録
        slider.onValueChanged.AddListener(OnSliderValueChanged);

    }

    void Update()
    {
        // キーボード操作でスライダー値を変更する例
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            slider.value += 0.1f; // 値を増加
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            slider.value -= 0.1f; // 値を減少
        }
    }

    private void OnSliderValueChanged(float value)
    {
        // スライダー値変更時に特定の動作を実行
        Debug.Log("スライダーの値が変わりました: " + value);
    }
}
