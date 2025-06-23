using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutState : IState
{
    [SerializeField]
    private GameObject fadeOut;
    [SerializeField] 
    private GameObject picture;
    [SerializeField]
    private GameObject screen;
    [SerializeField]
    private Vector2 pictureZoomPos;
    [SerializeField]
    private Vector2 pictureZoomScale;
    [SerializeField]
    private Vector2 screenZoomPos;
    [SerializeField]
    private Vector2 screenZoomScale;
    [SerializeField]
    private float pictureZoomSpeed;
    [SerializeField]
    private float screenZoomSpeed;
    [SerializeField]
    private float epsilon;

    [SerializeField]
    private CassetteSelectState cassetteSelectState;
    [SerializeField]
    private StageSelectState stageSelectState;
    private int worldIndex;
    private int stageIndex;
    private string sceneName;

    public override void StateEnter()
    {
        worldIndex = cassetteSelectState.selectIndex;
        stageIndex = stageSelectState.selectIndex;
        sceneName = stageSelectState.allPreview[worldIndex].GetComponent<StageData>().stageData[stageIndex].sceneName;

        fadeOut.SetActive(true);
        screen.GetComponent<FollowBack>().enabled = false;
    }
    public override void StateUpdate()
    { 
        Vector3 picPos = picture.transform.position;
        Vector3 picScale = picture.transform.localScale;
        Vector3 scrPos = screen.transform.position;
        Vector3 scrScale = screen.transform.localScale;
        picPos = new Vector3(Mathf.Lerp(picPos.x, pictureZoomPos.x, Time.deltaTime * pictureZoomSpeed), Mathf.Lerp(picPos.y, pictureZoomPos.y, Time.deltaTime * pictureZoomSpeed), 0);
        picScale = new Vector3(Mathf.Lerp(picScale.x, pictureZoomScale.x, Time.deltaTime * pictureZoomSpeed), Mathf.Lerp(picScale.y, pictureZoomScale.y, Time.deltaTime * pictureZoomSpeed), 0);
        scrPos = new Vector3(Mathf.Lerp(scrPos.x, screenZoomPos.x, Time.deltaTime * screenZoomSpeed), Mathf.Lerp(scrPos.y, screenZoomPos.y, Time.deltaTime * screenZoomSpeed), 0);
        scrScale = new Vector3(Mathf.Lerp(scrScale.x, screenZoomScale.x, Time.deltaTime * screenZoomSpeed), Mathf.Lerp(scrScale.y, screenZoomScale.y, Time.deltaTime * screenZoomSpeed), 0);

        picture.transform.position = picPos;
        picture.transform.localScale = picScale;
        screen.transform.position = scrPos;
        screen.transform.localScale = scrScale;

        if (screenZoomScale.x < scrScale.x + epsilon && screenZoomScale.y < scrScale.y + epsilon)
        {
            Debug.Log("ここでsceneNameを使ってシーン遷移 " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }
    public override void StateExit()
    {

    }
}