using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderTimeCounter : MonoBehaviour
{
    public Slider slider;   // スライダーコンポーネントへの参照
    public GameObject currentObject;
    public GameObject[] objectPrefabs;

    float timeCounter = 0;     // FPSカウンタ
    float ValueOld = 0;   //デバック用にスライダーのログ位置とる

    //プレイヤーとスライダーが同期しないための変更
    private bool isManualInput = false;  // 手動で操作されたかどうかのフラグ
    private float manualInputTimer = 0f; // 手動操作の無効時間

    private float previousSliderValue = 0f;

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

        float diff = Mathf.Abs(slider.value - ValueOld);

        // 激しい手動操作（差が一定以上）を検出
        if (diff > 5.0f && currentObject != null)
        {
            Debug.LogWarning("激しいスライダー操作を検知");

            Vector3 spawnPosition = currentObject.transform.position;
            int randomIndex = UnityEngine.Random.Range(0, objectPrefabs.Length);

            GameObject newObj = Instantiate(objectPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            TimeSlider2 newSliderScript = newObj.GetComponent<TimeSlider2>();
            TimeSlider2 oldSliderScript = currentObject.GetComponent<TimeSlider2>();

            if (newSliderScript != null && oldSliderScript != null)
            {
                newSliderScript.slider = slider;
                newSliderScript.SetPositionHistory(oldSliderScript.GetPositionHistory());

                oldSliderScript.ObjectChanged(newObj); // 自分を削除して切り替え
                currentObject = newObj; // 新しいオブジェクトを登録
            }
        }

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
            //slider.onValueChanged.RemoveListener(OnSliderValueChanged);//イベント停止(なんか普通に動くっぽい)
            slider.value += 0.1f;
            slider.onValueChanged.AddListener(OnSliderMoved);//イベント更新

            timeCounter = 0;
            Debug.Log("スライダー更新");
            //Debug.Log($"ValueOld: {ValueOld}, slider.value: {slider.value}");
        }

    }


    // スライダーが操作された際に呼ばれる
    public void OnSliderMoved(float value)
    {
        // スライダーの手動操作を検知
        isManualInput = true;

        if (currentObject != null)
        {
            TimeSlider2 script = currentObject.GetComponent<TimeSlider2>();
            if (script != null)
            {
                script.OnSliderMovedByUser(value); // ← ここ！
            }
        }

        // 以下、スライダーを激しく動かしたかどうかの判定
        float diff = Mathf.Abs(value - previousSliderValue);
        previousSliderValue = value;

        if (diff > 5.0f && currentObject != null && objectPrefabs.Length > 0)
        {
            Vector3 spawnPosition = currentObject.transform.position;
            int randomIndex = UnityEngine.Random.Range(0, objectPrefabs.Length);

            GameObject newObj = Instantiate(objectPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            TimeSlider2 newSliderScript = newObj.GetComponent<TimeSlider2>();
            TimeSlider2 oldSliderScript = currentObject.GetComponent<TimeSlider2>();

            if (newSliderScript != null && oldSliderScript != null)
            {
                newSliderScript.slider = slider;
                newSliderScript.SetPositionHistory(oldSliderScript.GetPositionHistory());

                oldSliderScript.ObjectChanged(newObj);
                currentObject = newObj;
            }
        }
    }

}