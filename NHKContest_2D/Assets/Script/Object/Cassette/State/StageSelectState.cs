using TMPro;
using UnityEngine;

public class StageSelectState : IState
{
    [SerializeField]
    public GameObject[] allPreview;
    [SerializeField]
    public int selectIndex;
    [SerializeField]
    private GameObject screenPicture;
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private CassetteSelectState cassetteSelectState;
    private sData[] stageData;
    private SpriteRenderer spriteRenderer;

    public override void StateEnter()
    {
        stageData = allPreview[cassetteSelectState.selectIndex].GetComponent<StageData>().stageData;
        spriteRenderer = screenPicture.GetComponent<SpriteRenderer>();

        selectIndex = 0;
        screenPicture.SetActive(true);
    }
    public override void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Indexを循環させながら減らす
            selectIndex = (selectIndex + stageData.Length - 1) % stageData.Length;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Indexを循環させながら減らす
            selectIndex = (selectIndex +  1) % stageData.Length;
        }
        spriteRenderer.sprite = stageData[selectIndex].preview;

        // ホントは画像を切り替えたいよね
        text.text = (cassetteSelectState.selectIndex + 1).ToString() + " - " + (selectIndex + 1).ToString();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            screenPicture.SetActive(false);
            parent.transform.Find("ScreenZoomState").GetComponent<ScreenZoomState>().zoom = false;
            parent.ChangeState("ScreenZoomState");
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            parent.ChangeState("FadeOutState");
        }
    }
    public override void StateExit()
    {
        
    }
}