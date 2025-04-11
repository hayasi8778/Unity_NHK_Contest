using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderTimeCounter : MonoBehaviour
{
    public Slider slider;   // スライダーコンポーネントへの参照
    public GameObject currentObject;
    private int currentPrefabIndex = 0;  // プレハブ配列の現在のインデックス

    float timeCounter = 0;     // FPSカウンタ
    float ValueOld = 0;   //デバック用にスライダーのログ位置とる

    

    //プレイヤーとスライダーが同期しないための変更
    private bool isManualInput = false;  // 手動で操作されたかどうかのフラグ
    private float manualInputTimer = 0f; // 手動操作の無効時間

    private bool Movement = false; //Update内で0.1valueを増やしたときにオブジェクトが戻らない用に無理やり止める

    private float previousSliderValue = 0f;

    private float changeCooldown = 2.0f;       // クールタイム（秒）
    private float changeCooldownTimer = 0f;    // クールタイマーカウント

    void Start()
    {
        // 初期値を設定
        slider.value = 0.0f;
        slider.maxValue = 300.0f;
        slider.minValue = 0.0f;
    }

    void Update()
    {

        if (slider == null)//デバック用にスライダー登録されてるか確認する
        {
            Debug.LogError("シェーダーが指定されてないぞ(SliderTimeCounter)");
        }

        // クールタイム中ならタイマー更新してスキップ
        if (changeCooldownTimer > 0f)
        {
            changeCooldownTimer -= Time.deltaTime;
        }

        float diff = Mathf.Abs(slider.value - ValueOld);

        

        // 手動操作が行われた場合、一定時間(1秒)は時間経過によるスライダーの移動を停止
        if (isManualInput)
        {
            manualInputTimer += Time.deltaTime;
            if (manualInputTimer >= 1.0f)
            {
                isManualInput = false;
                //manualInputTimer = 0f;
                Debug.Log("スライダー停止");
            }
            return;
        }

        timeCounter += Time.deltaTime;

        if (timeCounter >= 0.1)
        {
            // スライダーのイベントを一時的に解除して値を変更
            slider.onValueChanged.RemoveListener(OnSliderMoved);//イベント停止(なんか普通に動くっぽい)
            Movement = false; //ここであらかじめvalue動いた時の処理を切る
            slider.value += 0.1f;
            Movement = true; //valueが動くとOnSliderMovedが起動するように
            slider.onValueChanged.AddListener(OnSliderMoved);//イベント更新

            timeCounter = 0;
            Debug.Log("スライダー更新");
            //Debug.Log($"ValueOld: {ValueOld}, slider.value: {slider.value}");
        }

    }


    // スライダーが操作された際に呼ばれる
    public void OnSliderMoved(float value) //valueが更新されたときの処理
    {
        if (Movement)//RemoveListener(OnSliderMoved)で止めれなかったからごり押しで止める
        {

            Debug.Log("スライダー動いた時の処理する");

            // スライダーの手動操作を検知
            isManualInput = true;

            if (currentObject != null)
            {
                TimeSlider2 script = currentObject.GetComponent<TimeSlider2>();
                if (script != null)
                {
                    script.OnSliderMovedByUser(value); // オブジェクトの巻き戻し処理
                }
            }

            // 以下、スライダーを激しく動かしたかどうかの判定
            float diff = Mathf.Abs(value - previousSliderValue);
            previousSliderValue = value;

            if (diff > 5.0f && currentObject != null && changeCooldownTimer <= 0f)
            {
                TimeSlider2 script = currentObject.GetComponent<TimeSlider2>();
                if (script != null)
                {
                    GameObject newObj = script.ObjectChanged();
                    if (newObj != null)
                    {
                        currentObject = newObj;
                        changeCooldownTimer = changeCooldown;
                    }
                }

                
            }
        }
    }

}