using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderOnMoveInvalidation : Slider
{
    public override void OnMove(AxisEventData eventData)
    {
        // デフォルトのOnMove処理を無効化
        Debug.Log("OnMoveイベントが無効化されました");
        // 必要に応じて独自の処理を記述できます
        // base.OnMove(eventData); // デフォルトの挙動を維持したい場合
    }
}
