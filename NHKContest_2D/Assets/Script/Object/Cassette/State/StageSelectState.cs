using UnityEngine;

public class StageSelectState : IState
{
    const float delta = 0.01f;  // 小さい数値
    const float centerY = 6;
    const float min = 3;
    const float max = 5;

    private bool zoom;
    private Vector2 center;
    private CassetteSelectState cassetteSelectState;
    private GameObject[] viewCassettes;
    private int viewMaxCount;

    public override void StateEnter()
    {
        cassetteSelectState = parent.transform.Find("CassetteSelectState").GetComponent<CassetteSelectState>();
        viewCassettes = cassetteSelectState.viewCassettes;
        viewMaxCount = cassetteSelectState.viewMaxCount;

        zoom = true;
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
            pos.y = Mathf.Lerp(pos.y, centerY, Time.deltaTime * 10);
            Camera.main.transform.position = pos;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3, Time.deltaTime * 10);
        }

        if (Camera.main.orthographicSize < min + delta && zoom)
        {

        }

        if (Camera.main.orthographicSize < max && !zoom)
        {
            Vector3 pos = Camera.main.transform.position;
            pos.y = Mathf.Lerp(pos.y, 0, Time.deltaTime * 10);
            Camera.main.transform.position = pos;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5, Time.deltaTime * 10);

            Vector3 casPos = viewCassettes[viewMaxCount / 2].transform.position;
            casPos.y = Mathf.Lerp(casPos.y, 0, Time.deltaTime * 10);
            viewCassettes[viewMaxCount / 2].transform.position = casPos;
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