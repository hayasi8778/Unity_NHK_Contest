using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PressPlate : MonoBehaviour
{
    [SerializeField]
    // 押された時の大きさ
    private float onSize = 0.25f;
    [SerializeField]
    // デフォの大きさ
    private float offSize = 0.5f;
    
    // 前の状態
    private bool oldPress = false;
    // 今の状態
    public bool press = false;

    private void Start()
    {
        oldPress = false;
        press = false;
    }

    private void Update()
    {
        // OFFからONに切り替わる時
        if (press && !oldPress)
        {
            Vector3 pos = transform.position;
            pos.y += (onSize - offSize) / 2;
            transform.position = pos;

            Vector3 scale = transform.localScale;
            scale.y = onSize;
            transform.localScale = scale;
        }
        // ONからOFFに切り替わる時
        if (!press && oldPress)
        {
            Vector3 pos = transform.position;
            pos.y += (offSize - onSize) / 2;
            transform.position = pos;

            Vector3 scale = transform.localScale;
            scale.y = offSize;
            transform.localScale = scale;
        }

        oldPress = press;
    }
}
