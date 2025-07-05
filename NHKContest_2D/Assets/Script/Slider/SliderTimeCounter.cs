using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderTimeCounter : MonoBehaviour
{
    public Slider slider;   // スライダーコンポーネントへの参照
    public GameObject currentObject; //プレイヤー
    public GameObject CameraController; //カメラ制御用の空オブジェクト
    private int currentPrefabIndex = 0;  // プレハブ配列の現在のインデックス

    public GameObject[] currentObjects;

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

    // スライダー操作時に一時的に表示するオブジェクト
    // 作成者：畦内
    [Header("スライダー操作時に表示するオブジェクト")]
    public GameObject[] targetObjects;

    // 時間停止時に表示（透明解除）するオブジェクト
    // 作成者：畦内
    [Header("スライダーが最大まで進んだ時に表示するオブジェクト")]
    public GameObject[] unhideOnStopObjects;

    private bool isTimeStopped = false; // 時間停止状態フラグ

    void Start()
    {
        // 初期値を設定
        slider.value = 0.0f;
        slider.maxValue = initMaxSeconds;
        slider.minValue = 0.0f;

        sliderHistory = new float[historySize];
        for (int i = 0; i < historySize; i++) sliderHistory[i] = 0f;

        //オブジェクトたちに配列番号を与える
        for (int i = 0; i < currentObjects.Length; i++)
        {
            GameObject obj = currentObjects[i];
            if (obj == null) continue;

            //親オブジェクトから継承した物を呼び出す
            TimeSliderObject_Base timeObj = obj.GetComponent<TimeSliderObject_Base>();
            if (timeObj != null)
            {
                timeObj.SetCurrentnum(i);
            }
        }

        // 作成者：畦内 - 対象オブジェクトを非表示で初期化
        HideTargetObjects();
        HideUnhideOnStopObjects();
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
            manualInputTimer += Time.unscaledDeltaTime; // ★UnscaledTimeで回す（Time.timeScale=0でもカウント）
            if (manualInputTimer >= 1.0f)
            {
                isManualInput = false;
                Debug.Log("手動入力モード解除");

                // 作成者：畦内 - スライダー操作終了後、再び非表示にする
                HideTargetObjects();
            }
            return;
        }

        // ★最大値から戻った場合の復帰処理
        if (isTimeStopped && slider.value < slider.maxValue)
        {
            Time.timeScale = 1f;
            slider.interactable = true;
            isTimeStopped = false;
            HideUnhideOnStopObjects();
        }

        // 時間停止時に処理スキップ（自動加算含む）
        if (isTimeStopped) return;

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

        if (slider.value > 0)//オブジェクトに座標のログを取らせる
        {
            if (currentObjects != null)
            {
                for (int i = 0; i < currentObjects.Length; i++)
                {
                    GameObject obj = currentObjects[i];
                    if (obj == null) continue;

                    var timeObj = obj.GetComponent<TimeSliderObject>();
                    if (timeObj != null)
                    {
                        timeObj.UpdatePositionHistory(slider.value);
                    }
                }
            }
        }

        if (!isTimeStopped && slider.value >= slider.maxValue)
        {
            HideTargetObjects(); // スライダー中に表示していたやつを消す
            StartCoroutine(StopTimeAfterOneFrame()); // ★コルーチンで時間停止を遅延実行
            return;
        }
    }

    // 作成者：畦内 - 時間停止処理を1フレーム遅らせて確実に止める
    private System.Collections.IEnumerator StopTimeAfterOneFrame()
    {
        yield return null; // 1フレーム待機
        Time.timeScale = 0f; // ★確実に停止させる
                             //slider.interactable = false;
        isTimeStopped = true;
        ShowUnhideOnStopObjects();
    }
    // スライダーが操作された際に呼ばれる
    public void OnSliderMoved(float value) //valueが更新されたときの処理
    {
        if (Movement)//RemoveListener(OnSliderMoved)で止めれなかったからごり押しで止める
        {
            //ログかさばるからデバック用
            //Debug.Log("スライダー動いた時の処理する");

            //Debug.Log("手動スライダー操作を検知");
            isManualInput = true;
            manualInputTimer = 0f;

            if (currentObject != null)
            {
                TimeSlider2 script = currentObject.GetComponent<TimeSlider2>();
                if (script != null)
                {
                    //プレイヤーの巻き戻し処理を一旦無効化する
                    //script.OnSliderMovedByUser(value);
                }
            }

            if (CameraController != null) //スライダーの操作に同期してカメラにエフェクトかける
            {
                FilmGrainToggle script = CameraController.GetComponent<FilmGrainToggle>();
                if (script != null)
                {
                    script.SliderMovedControl();//カメラにエフェクトをかける命令
                }
            }

            // ★ステージ内オブジェクトたちの巻き戻し
            if (currentObjects != null)
            {
                foreach (var obj in currentObjects)
                {
                    if (obj == null) continue;

                    var timeObj = obj.GetComponent<TimeSliderObject>();
                    if (timeObj != null)
                    {
                        timeObj.RewindToSlider(value);
                    }
                }
            }

            // 作成者：畦内 - スライダー操作中は対象オブジェクトを一時的に表示
            ShowTargetObjects();
        }
    }

    public void SetCurrentPlayer(GameObject player)
    {
        currentObject = player;
    }

    public void SetCurrentObjects(GameObject objects, int it)
    {
        Debug.LogWarning("ステージオブジェクト継承");
        currentObjects[it] = objects;
    }

    // 作成者：畦内 - スライダー操作中に対象オブジェクトを表示
    public void ShowTargetObjects()
    {
        if (targetObjects == null) return;

        foreach (var obj in targetObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    // 作成者：畦内 - スライダー操作終了時に対象オブジェクトを非表示に戻す
    public void HideTargetObjects()
    {
        if (targetObjects == null) return;

        foreach (var obj in targetObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    // 作成者：畦内 - 時間停止時に表示（透明解除）するオブジェクト
    public void ShowUnhideOnStopObjects()
    {
        if (unhideOnStopObjects == null) return;

        foreach (var obj in unhideOnStopObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    // 作成者：畦内 - 時間再開時に非表示に戻す
    public void HideUnhideOnStopObjects()
    {
        if (unhideOnStopObjects == null) return;

        foreach (var obj in unhideOnStopObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
