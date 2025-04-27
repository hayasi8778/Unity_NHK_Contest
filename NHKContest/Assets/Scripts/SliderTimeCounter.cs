using System;
using Unity.Mathematics;
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




    [SerializeField]
    public float initMaxSeconds = 300.0f;  // 最大秒数の初期値


    //プレイヤーとスライダーが同期しないための変更
    private bool isManualInput = false;  // 手動で操作されたかどうかのフラグ
    private float manualInputTimer = 0f; // 手動操作の無効時間

    private bool Movement = false; //Update内で0.1valueを増やしたときにオブジェクトが戻らない用に無理やり止める

    private float previousSliderValue = 0f;

    private float changeCooldown = 2.0f;       // クールタイム（秒）
    private float changeCooldownTimer = 0f;    // クールタイマーカウント

    //スライダー移動で入れ替わり判定するために配列用意する
    private float[] sliderHistory;
    private int historySize = 5; // 1秒分の履歴（0.1秒ごと）
    private int historyIndex = 0;
    private float autoIncreasePerEntry = 0.1f; // 自動加算分
    private float handMoveThreshold = 10.0f;     // 手動で動かしたとみなす最小差分

    void Start()
    {
        // 初期値を設定
        slider.value = 0.0f;
        slider.maxValue = initMaxSeconds;
        slider.minValue = 0.0f;

        sliderHistory = new float[historySize];
        for (int i = 0; i < historySize; i++) sliderHistory[i] = 0f;
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

        // 手動操作中は時間経過で復帰
        if (isManualInput)
        {
            manualInputTimer += Time.deltaTime;
            if (manualInputTimer >= 1.0f)
            {
                isManualInput = false;
                Debug.Log("手動入力モード解除");
            }
            return;
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

        // 時間加算（0.1秒ごと）
        timeCounter += Time.deltaTime;
        if (timeCounter >= 0.1f)
        {
            // スライダー履歴更新（リングバッファ）
            sliderHistory[historyIndex] = slider.value;
            historyIndex = (historyIndex + 1) % historySize;

            // スライダーを自動で増加
            slider.onValueChanged.RemoveListener(OnSliderMoved);
            Movement = false;
            slider.value += autoIncreasePerEntry;
            Movement = true;
            slider.onValueChanged.AddListener(OnSliderMoved);

            timeCounter = 0f;
        }

        // 履歴が一周したら動きの差分チェック
        if (/* historyIndex == 0 && */changeCooldownTimer <= 0f)//判断基準一旦切る
        {
            float oldest = sliderHistory[(historyIndex + 1) % historySize];
            float newest = sliderHistory[(historyIndex - 1 + historySize) % historySize];

            float rawDiff = Mathf.Abs(newest - oldest);
            float expectedAuto = autoIncreasePerEntry * (historySize - 1);
            float manualMovement = rawDiff - expectedAuto;

            //Debug.Log($"手動移動量: {manualMovement}");

            if (manualMovement > handMoveThreshold && currentObject != null)
            {
                TimeSlider2 script = currentObject.GetComponent<TimeSlider2>();
                if (script != null)
                {
                    GameObject newObj = script.ObjectChanged();
                    if (newObj != null)
                    {
                        currentObject = newObj;
                        changeCooldownTimer = changeCooldown;
                        Debug.LogError("オブジェクトを切り替えました！");
                    }
                }
            }
        }

    }


    // スライダーが操作された際に呼ばれる
    public void OnSliderMoved(float value) //valueが更新されたときの処理
    {
        if (Movement)//RemoveListener(OnSliderMoved)で止めれなかったからごり押しで止める
        {

            //ログかさばるからデバック用
            //Debug.Log("スライダー動いた時の処理する");

            // スライダーの手動操作を検知
            /*
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
                        Debug.LogError("こっちで切り替えてるとよくなさそう");
                    }
                }

                
            }
            */

            Debug.Log("手動スライダー操作を検知");
            isManualInput = true;
            manualInputTimer = 0f;

            if (currentObject != null)
            {
                TimeSlider2 script = currentObject.GetComponent<TimeSlider2>();
                if (script != null)
                {
                    script.OnSliderMovedByUser(value);
                }
            }

        }
    }

    public void SetCurrentPlayer(GameObject player)
    {
        currentObject = player;
    }

}