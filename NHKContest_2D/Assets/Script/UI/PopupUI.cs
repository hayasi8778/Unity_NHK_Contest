using UnityEngine;

/// <summary>
/// PopupUI.cs
///
/// �y�g�����i�C���X�y�N�^�[�ݒ�j�z
/// 1. ���̃X�N���v�g�����GameObject�ɃA�^�b�`���Ă��������B
/// 2. �ȉ��� GameObject ���C���X�y�N�^�[�Őݒ肵�Ă��������F
///    - DarkOverlay : �w�i���Â�����UI�i��\���j
///    - PopupScreen : �\�����g�傳���UI�i��\����Scale = 0�j
///    - StageSelectButton : �g�劮����ɕ\�������{�^���iCanvasGroup�K�{�j
///    - StoryButton : �g�劮����ɕ\�������{�^���iCanvasGroup�K�{�j
/// 3. PopupScreen �� RectTransform �������ɂȂ�悤 Anchors ��ݒ肵�Ă����Ă��������B
/// 4. �{�^���͔�\����ԁiCanvasGroup�� alpha=0�j�ŊJ�n���Ă��������B
/// </summary>
public class PopupUI : MonoBehaviour
{
    [Header("UI�I�u�W�F�N�g")]
    public GameObject DarkOverlay;        // �w�i�Ó]�I�[�o�[���C
    public GameObject PopupScreen;        // �g�債�ĕ\�������X�N���[��

    [Header("�\�������{�^��")]
    public GameObject StageSelectButton;  // �g���ɏo��
    public GameObject StoryButton;        // �g���ɏo��

    [Header("�g��A�j���[�V�����ݒ�")]
    public float scaleSpeed = 1f;         // �g�呬�x

    [Range(0.1f, 2.5f)]
    public float widthScaleFactor = 1.6f;   // �������̊g��{��

    [Range(0.1f, 2.5f)]
    public float heightScaleFactor = 0.8f;  // �c�����̊g��{��

    private Vector3 targetScale = Vector3.one;   // �ŏI�I�Ȋg��T�C�Y
    private bool isShowing = false;              // �g�咆�t���O
    private bool buttonsShown = false;           // �{�^���\���ς݃t���O

    void Start()
    {
        // �������F�Ó]�I�[�o�[���C���\��
        if (DarkOverlay != null)
            DarkOverlay.SetActive(false);

        // �������F�|�b�v�A�b�v���\�����X�P�[��0
        if (PopupScreen != null)
        {
            PopupScreen.SetActive(false);
            PopupScreen.transform.localScale = Vector3.zero;

            // �X�N���[���T�C�Y����X�P�[�����O�l���v�Z
            RectTransform rect = PopupScreen.GetComponent<RectTransform>();
            if (rect != null)
            {
                Vector2 size = rect.sizeDelta;
                float scaleX = (Screen.width * widthScaleFactor) / size.x;
                float scaleY = (Screen.height * heightScaleFactor) / size.y;
                targetScale = new Vector3(scaleX, scaleY, 1f);
            }
        }

        // �{�^���̏�����ԁi�����E�񑀍�j
        InitializeButton(StageSelectButton);
        InitializeButton(StoryButton);
    }

    /// <summary>
    /// �{�^���̓������Ɩ�����
    /// </summary>
    void InitializeButton(GameObject button)
    {
        if (button != null)
        {
            button.SetActive(true); // ��\���ɂ͂��� CanvasGroup�Ő���
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
        // �g�咆�Ȃ�X�P�[�����������s
        if (isShowing && PopupScreen != null)
        {
            // ���[�v�i�ʒu�Œ�j
            RectTransform rect = PopupScreen.GetComponent<RectTransform>();
            if (rect != null)
                rect.anchoredPosition = Vector2.zero;
            else
                PopupScreen.transform.localPosition = Vector3.zero;

            // �g��A�j���[�V����
            PopupScreen.transform.localScale = Vector3.MoveTowards(
                PopupScreen.transform.localScale,
                targetScale,
                scaleSpeed * Time.unscaledDeltaTime
            );

            // �g�傪����������{�^����\��
            if (!buttonsShown && PopupScreen.transform.localScale == targetScale)
            {
                ShowButton(StageSelectButton);
                ShowButton(StoryButton);
                buttonsShown = true;
            }
        }
    }

    /// <summary>
    /// �g��J�n�g���K�[�i�{�^������Ăяo���j
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
    /// �{�^����\��������\��
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
