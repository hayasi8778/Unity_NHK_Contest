using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理に使用

/// <summary>
/// Goal.cs
/// 
/// 【使い方（インスペクター設定）】
/// 1. このスクリプトを Goal オブジェクトにアタッチしてください。
/// 2. 以下の GameObject をインスペクターで設定してください：
///    - DarkOverlay : ゴール後に画面を暗くするUIオブジェクト（非表示にしておく）
///    - ClearScreen : クリア画面（Canvas内の画像など、非表示＆小さくしておく）
///    - Geme_end : エンドボタン（Canvas内に配置）
///    - RestartButton : リスタートボタン（Canvas内に配置）
///    - SelectButton : セレクト画面に戻るボタン（Canvas内に配置）
/// 3. プレイヤーのオブジェクトにタグ "Player" をつけてください。
/// 4. Goal オブジェクトには 2D Collider をアタッチし、"Is Trigger" を ON にしてください。
/// </summary>
public class Goal : MonoBehaviour
{
    [Header("UIオブジェクト設定")]
    public GameObject DarkOverlay;       // 背景を暗くするUI
    public GameObject ClearScreen;       // 拡大して表示されるクリアスクリーン
    public GameObject Geme_end;          // 「エンド」ボタン
    public GameObject RestartButton;     // 「もう一度プレイ」ボタン
    public GameObject SelectButton;      // 「セレクトへ戻る」ボタン

    [Header("拡大アニメーション設定")]
    public float scaleSpeed = 1f;        // クリア画面の拡大スピード

    [Header("拡大サイズ調整（画面に対する割合）")]
    [Range(0.1f, 2.5f)]
    public float widthScaleFactor = 1.6f;   // 横の拡大倍率（画面幅に対する比率）

    [Range(0.1f, 2.5f)]
    public float heightScaleFactor = 0.8f;  // 縦の拡大倍率（画面高さに対する比率）

    private Vector3 targetScale = Vector3.one;   // 目標スケール（アニメーション完了後のサイズ）
    private bool isGoalReached = false;          // ゴールに到達したかどうかのフラグ
    private bool buttonShown = false;            // ボタンが表示されたかどうかのフラグ

    void Start()
    {
        // 暗転オーバーレイを非表示にしておく
        if (DarkOverlay != null)
            DarkOverlay.SetActive(false);

        // クリアスクリーンを非表示＆初期スケール0にしておく
        if (ClearScreen != null)
        {
            ClearScreen.SetActive(false);
            ClearScreen.transform.localScale = Vector3.zero;

            // RectTransform から目標スケールを計算
            RectTransform rectTransform = ClearScreen.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                float screenWidth = Screen.width;
                float screenHeight = Screen.height;
                Vector2 size = rectTransform.sizeDelta;

                // 指定された倍率に応じてスケールを決定
                float scaleX = (screenWidth * widthScaleFactor) / size.x;
                float scaleY = (screenHeight * heightScaleFactor) / size.y;

                targetScale = new Vector3(scaleX, scaleY, 1f);
            }
            else
            {
                // RectTransformがない場合のデフォルト値
                targetScale = new Vector3(widthScaleFactor, heightScaleFactor, 1f);
            }
        }

        // 各ボタンの透明度と操作設定を初期化
        InitializeButton(Geme_end);
        InitializeButton(RestartButton);
        InitializeButton(SelectButton);
    }

    /// <summary>
    /// ボタンを透明かつ非操作に初期化
    /// </summary>
    void InitializeButton(GameObject button)
    {
        if (button != null)
        {
            button.SetActive(true); // 表示はしておく（CanvasGroupで透明にする）
            var cg = button.GetComponent<CanvasGroup>();
            if (cg == null)
                cg = button.AddComponent<CanvasGroup>();
            cg.alpha = 0f; // 完全に透明
            cg.interactable = false; // クリック不可
            cg.blocksRaycasts = false; // レイを通す（無視）
        }
    }

    void Update()
    {
        if (isGoalReached && ClearScreen != null)
        {
            // クリアスクリーンを表示
            ClearScreen.SetActive(true);

            // スクリーンを画面中央に固定
            RectTransform rect = ClearScreen.GetComponent<RectTransform>();
            if (rect != null)
                rect.anchoredPosition = Vector2.zero;
            else
                ClearScreen.transform.localPosition = Vector3.zero;

            // 徐々に拡大アニメーション
            ClearScreen.transform.localScale = Vector3.MoveTowards(
                ClearScreen.transform.localScale,
                targetScale,
                scaleSpeed * Time.unscaledDeltaTime
            );

            // 拡大完了時にボタンを表示
            if (!buttonShown && ClearScreen.transform.localScale == targetScale)
            {
                ShowButton(Geme_end);
                ShowButton(RestartButton);
                ShowButton(SelectButton);
                buttonShown = true;
            }
        }
    }

    /// <summary>
    /// ボタンを表示＆操作可能にする
    /// </summary>
    void ShowButton(GameObject button)
    {
        if (button != null)
        {
            var cg = button.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 1f;           // 表示
                cg.interactable = true;  // 操作可能
                cg.blocksRaycasts = true;// クリックなどを受け付ける
            }
        }
    }

    /// <summary>
    /// プレイヤーがゴールに触れたらクリア処理を開始
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGoalReached && collision.CompareTag("Player"))
        {
            isGoalReached = true;

            // 画面を暗くするオーバーレイを表示
            if (DarkOverlay != null)
                DarkOverlay.SetActive(true);

            // ゲーム時間を止める
            Time.timeScale = 0f;
        }
    }
    /// <summary>
    /// エンドボタンが押された時の処理
    /// </summary>
    public void OnEndButtonClicked()
    {
        // 時間を戻す
        Time.timeScale = 1f;

        // ここにエンド処理を追加（例：エンディングへ遷移など）
        Debug.Log("エンドボタンが押されました。");
    }

    /// <summary>
    /// リスタートボタンが押された時の処理
    /// </summary>
    public void OnRestartButtonClicked()
    {
        // 時間を戻して現在のシーンを再読み込み
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// セレクトボタンが押された時の処理
    /// </summary>
    public void OnSelectButtonClicked()
    {
        // 時間を戻してセレクト画面に遷移
        Time.timeScale = 1f;


    }


}
