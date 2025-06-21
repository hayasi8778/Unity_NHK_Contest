using UnityEngine;

/// <summary>
/// Goal.cs
/// 
/// 使い方（インスペクター設定）
/// 1. このスクリプトをGoalオブジェクトにアタッチしてください。
/// 2. 以下のGameObjectをインスペクターでセットしてください：
///    - DarkOverlay : 画面を暗くするオブジェクト（最初は非表示にしてください）
///    - ClearScreen : クリア画面オブジェクト（最初はCanvas内で非表示にしておく）
///    - Geme_end : クリア後に表示するエンドボタン（Canvas内）
///    - RestartButton : クリア後に表示するリスタートボタン（Canvas内）
///    - SelectButton : クリア後に表示するセレクトボタン（Canvas内）
/// 3. プレイヤーオブジェクトは必ずタグ "Player" を設定してください。
/// 4. ゴールオブジェクトはCollider2Dに「IsTrigger」をONにしてください。
/// </summary>

public class Goal : MonoBehaviour
{
    [Header("UIオブジェクト設定")]
    public GameObject DarkOverlay;       // 画面を暗くするオーバーレイ（最初は非表示）
    public GameObject ClearScreen;       // クリア画面オブジェクト（最初は小さく非表示）
    public GameObject Geme_end;           // エンド（クリア）ボタン
    public GameObject RestartButton;     // もう一度ボタン
    public GameObject SelectButton;      // セレクト画面に戻るボタン

    [Header("拡大スピード")]
    public float scaleSpeed = 1f;        // クリア画面が拡大する速度

    private Vector3 targetScale = Vector3.one;   // 目標スケール（画面の80％ぐらい）
    private bool isGoalReached = false;           // ゴールに到達したか判定
    private bool buttonShown = false;              // ボタンを表示したか判定

    void Start()
    {
        // 最初はオーバーレイ非表示
        if (DarkOverlay != null)
            DarkOverlay.SetActive(false);

        // ClearScreenは非表示＆スケール0に設定
        if (ClearScreen != null)
        {
            ClearScreen.SetActive(false);
            ClearScreen.transform.localScale = Vector3.zero;

            // RectTransformから画面サイズに合わせた目標スケールを計算
            RectTransform rectTransform = ClearScreen.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                float screenWidth = Screen.width;
                float screenHeight = Screen.height;
                Vector2 size = rectTransform.sizeDelta;

                // 横を画面幅の80％ × 2倍（横を大きめに拡大）
                float scaleX = (screenWidth * 0.8f) / size.x * 2.0f;
                float scaleY = (screenHeight * 0.8f) / size.y;

                targetScale = new Vector3(scaleX, scaleY, 1f);
            }
            else
            {
                targetScale = new Vector3(2f, 0.8f, 1f);
            }
        }

        // ボタンは透明＆非操作に初期化
        InitializeButton(Geme_end);
        InitializeButton(RestartButton);
        InitializeButton(SelectButton);
    }

    /// <summary>
    /// ボタンを透明かつクリック不可に設定
    /// </summary>
    void InitializeButton(GameObject button)
    {
        if (button != null)
        {
            button.SetActive(true);
            var cg = button.GetComponent<CanvasGroup>();
            if (cg == null)
                cg = button.AddComponent<CanvasGroup>();
            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }

    void Update()
    {
        if (isGoalReached && ClearScreen != null)
        {
            ClearScreen.SetActive(true);

            // ClearScreenを画面中央に固定
            RectTransform rect = ClearScreen.GetComponent<RectTransform>();
            if (rect != null)
                rect.anchoredPosition = Vector2.zero;
            else
                ClearScreen.transform.localPosition = Vector3.zero;

            // スケールを徐々に目標値まで拡大
            ClearScreen.transform.localScale = Vector3.MoveTowards(
                ClearScreen.transform.localScale,
                targetScale,
                scaleSpeed * Time.unscaledDeltaTime
            );

            // 拡大完了したらボタンを表示可能にする
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
    /// ボタンを見えるようにし操作可能に設定
    /// </summary>
    void ShowButton(GameObject button)
    {
        if (button != null)
        {
            var cg = button.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 1f;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
        }
    }

    /// <summary>
    /// プレイヤーがGoalに触れた時の処理
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGoalReached && collision.CompareTag("Player"))
        {
            isGoalReached = true;

            // 画面を暗くするオーバーレイを表示
            if (DarkOverlay != null)
                DarkOverlay.SetActive(true);

            // ゲーム時間を停止
            Time.timeScale = 0f;
        }
    }

   
}
