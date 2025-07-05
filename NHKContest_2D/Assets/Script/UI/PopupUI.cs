using UnityEngine;

/// <summary>
/// PopupUI.cs
///
/// 【使い方（インスペクター設定）】
/// 1. このスクリプトを空のGameObjectにアタッチしてください。
/// 2. 以下の GameObject をインスペクターで設定してください：
///    - DarkOverlay : 背景を暗くするUI（非表示）
///    - PopupScreen : 表示＆拡大されるUI（非表示＆Scale = 0）
///    - StageSelectButton : 拡大完了後に表示されるボタン（CanvasGroup必須）
///    - StoryButton : 拡大完了後に表示されるボタン（CanvasGroup必須）
/// 3. PopupScreen の RectTransform が中央になるよう Anchors を設定しておいてください。
/// 4. ボタンは非表示状態（CanvasGroupの alpha=0）で開始してください。
/// </summary>
public class PopupUI : MonoBehaviour
{
    [Header("UIオブジェクト")]
    public GameObject DarkOverlay;        // 背景暗転オーバーレイ
    public GameObject PopupScreen;        // 拡大して表示されるスクリーン

    [Header("表示されるボタン")]
    public GameObject StageSelectButton;  // 拡大後に出現
    public GameObject StoryButton;        // 拡大後に出現

    [Header("拡大アニメーション設定")]
    public float scaleSpeed = 1f;         // 拡大速度

    [Range(0.1f, 2.5f)]
    public float widthScaleFactor = 1.6f;   // 横方向の拡大倍率

    [Range(0.1f, 2.5f)]
    public float heightScaleFactor = 0.8f;  // 縦方向の拡大倍率

    private Vector3 targetScale = Vector3.one;   // 最終的な拡大サイズ
    private bool isShowing = false;              // 拡大中フラグ
    private bool buttonsShown = false;           // ボタン表示済みフラグ

    void Start()
    {
        // 初期化：暗転オーバーレイを非表示
        if (DarkOverlay != null)
            DarkOverlay.SetActive(false);

        // 初期化：ポップアップを非表示＆スケール0
        if (PopupScreen != null)
        {
            PopupScreen.SetActive(false);
            PopupScreen.transform.localScale = Vector3.zero;

            // スクリーンサイズからスケーリング値を計算
            RectTransform rect = PopupScreen.GetComponent<RectTransform>();
            if (rect != null)
            {
                Vector2 size = rect.sizeDelta;
                float scaleX = (Screen.width * widthScaleFactor) / size.x;
                float scaleY = (Screen.height * heightScaleFactor) / size.y;
                targetScale = new Vector3(scaleX, scaleY, 1f);
            }
        }

        // ボタンの初期状態（透明・非操作）
        InitializeButton(StageSelectButton);
        InitializeButton(StoryButton);
    }

    /// <summary>
    /// ボタンの透明化と無効化
    /// </summary>
    void InitializeButton(GameObject button)
    {
        if (button != null)
        {
            button.SetActive(true); // 非表示にはせず CanvasGroupで制御
            CanvasGroup cg = button.GetComponent<CanvasGroup>();
            if (cg == null)
                cg = button.AddComponent<CanvasGroup>();

            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }

    void Update()
    {
        // 拡大中ならスケール処理を実行
        if (isShowing && PopupScreen != null)
        {
            // ワープ（位置固定）
            RectTransform rect = PopupScreen.GetComponent<RectTransform>();
            if (rect != null)
                rect.anchoredPosition = Vector2.zero;
            else
                PopupScreen.transform.localPosition = Vector3.zero;

            // 拡大アニメーション
            PopupScreen.transform.localScale = Vector3.MoveTowards(
                PopupScreen.transform.localScale,
                targetScale,
                scaleSpeed * Time.unscaledDeltaTime
            );

            // 拡大が完了したらボタンを表示
            if (!buttonsShown && PopupScreen.transform.localScale == targetScale)
            {
                ShowButton(StageSelectButton);
                ShowButton(StoryButton);
                buttonsShown = true;
            }
        }
    }

    /// <summary>
    /// 拡大開始トリガー（ボタンから呼び出す）
    /// </summary>
    public void ShowPopup()
    {
        if (DarkOverlay != null)
            DarkOverlay.SetActive(true);

        if (PopupScreen != null)
        {
            PopupScreen.SetActive(true);
            isShowing = true;
        }
    }

    /// <summary>
    /// ボタンを表示＆操作可能に
    /// </summary>
    void ShowButton(GameObject button)
    {
        if (button != null)
        {
            CanvasGroup cg = button.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 1f;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
        }
    }
}
