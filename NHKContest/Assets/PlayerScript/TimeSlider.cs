using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    public Slider slider; // スライダー参照
    private Vector3[] positionHistory = new Vector3[3000]; // 0.1秒ごとに300秒分の履歴を記録
    private int currentIndex = 0; // 現在の履歴インデックス
    private bool isRewinding = false; // 巻き戻しフラグ

    void Start()
    {
        for (int i = 0; i < positionHistory.Length; i++)
        {
            positionHistory[i] = transform.position; // 初期座標で埋める
        }
    }

    void Update()
    {
        if (!isRewinding)
        {
            // スライダーの現在の位置を配列に記録
            int index = Mathf.RoundToInt(slider.value * 10);
            if (index >= 0 && index < positionHistory.Length)
            {
                positionHistory[index] = transform.position;
                currentIndex = index;
            }
        }
    }

    public void OnSliderValueChanged()
    {

        if (slider == null)
        {
            Debug.LogError("スライダーが指定されてないぞ(TimeSlider)");
            return;
        }

        isRewinding = true;
        int index = Mathf.RoundToInt(slider.value * 10);

        if (index == currentIndex + 1)
        {
            //Debug.LogError("スライダーの位置取れたよ+1で取れるよ");
            return;
        }

        if (index < positionHistory.Length)
        {
            if (index <= currentIndex)
            {
                Debug.LogError("巻き戻ったぞ");
                // 過去に記録された座標に巻き戻す
                transform.position = positionHistory[index];

                // 現在位置より先の履歴を消去する
                for (int i = currentIndex + 1; i < positionHistory.Length; i++)
                {
                    positionHistory[i] = transform.position; // 現在座標で埋める
                }
            }
            else
            {
                // 未記録の領域なら、現在の座標で埋める
                for (int i = currentIndex + 1; i <= index; i++)
                {
                    positionHistory[i] = transform.position;
                }

                //Debug.LogError("先に進みすぎ");

                //Debug.Log($"Index: {index}, CurrentIndex: {currentIndex}");

            }
            currentIndex = index;
        }
        isRewinding = false;
    }
}
