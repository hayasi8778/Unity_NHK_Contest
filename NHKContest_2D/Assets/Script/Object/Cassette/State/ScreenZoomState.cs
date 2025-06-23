using UnityEngine;

public class ScreenZoomState : IState
{
    [SerializeField]
    private float delta = 0.01f;  // 小さい数値
    [SerializeField]
    private float centerY = 6;
    [SerializeField]
    private float min = 3;
    [SerializeField]
    private float max = 5;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private GameObject screenPicture;
    [SerializeField]
    public bool zoom;
    private Vector2 center;
    private CassetteSelectState cassetteSelectState;
    private GameObject[] viewCassettes;
    private int viewMaxCount;
    private GameObject mainCassette;

    public float CenterY { get => centerY; set => centerY = value; }

    public override void StateEnter()
    {
        cassetteSelectState = parent.transform.Find("CassetteSelectState").GetComponent<CassetteSelectState>();
        viewCassettes = cassetteSelectState.viewCassettes;
        viewMaxCount = cassetteSelectState.viewMaxCount;
        mainCassette = viewCassettes[viewMaxCount / 2];
    }

    public override void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            zoom = false;
        }

        if (Camera.main.orthographicSize > min && zoom)
        {
            Vector3 pos = Camera.main.transform.position;
            pos.y = Mathf.Lerp(pos.y, CenterY, Time.deltaTime * speed);
            Camera.main.transform.position = pos;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, min, Time.deltaTime * speed);
        }

        if (Camera.main.orthographicSize < min + delta && zoom)
        {
            screenPicture.SetActive(true);
            parent.ChangeState("StageSelectState");
        }

        if (Camera.main.orthographicSize < max && !zoom)
        {
            screenPicture.SetActive(false);
            Vector3 pos = Camera.main.transform.position;
            pos.y = Mathf.Lerp(pos.y, 0, Time.deltaTime * speed);
            Camera.main.transform.position = pos;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, max, Time.deltaTime * speed);

            Vector3 casPos = mainCassette.transform.position;
            casPos.y = Mathf.Lerp(casPos.y, 0, Time.deltaTime * speed);
            mainCassette.transform.position = casPos;
        }

        if (Camera.main.orthographicSize > max - delta && !zoom)
        {
            foreach (GameObject cassette in viewCassettes)
                Object.Destroy(cassette);
            parent.ChangeState("CassetteSelectState");
        }
    }

    public override void StateExit()
    {

    }
}