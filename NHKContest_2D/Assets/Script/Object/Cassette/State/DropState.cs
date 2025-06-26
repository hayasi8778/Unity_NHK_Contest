using UnityEngine;

public class DropState : IState
{
    private float velocity;  // 落下用の速度
    [SerializeField]
    private float initVelocity = 2.0f;  // 落下用の速度
    [SerializeField]
    private float acceleration = -10.0f;   // 落下用の加速度
    [SerializeField]
    private Vector2 mouthPos;    // 拡大の中心設定
    [SerializeField]
    private float initChangeWaitTime;

    private float changeWaitTime;

    // 取得用
    [SerializeField]
    private CassetteSelectState cassetteSelectState;
    private GameObject[] viewCassettes;
    private int viewMaxCount;
    private float interval;
    private GameObject mainCassette;

    public override void StateEnter()
    {
        // 取得
        viewCassettes = cassetteSelectState.viewCassettes;
        viewMaxCount = cassetteSelectState.viewMaxCount;
        interval = cassetteSelectState.interval;
        mainCassette = viewCassettes[viewMaxCount / 2];

        // 真ん中から一つ左右のカセットを画面外に飛ばす
        SmoothMove leftSmoothMove = viewCassettes[viewMaxCount / 2 - 1].GetComponent<SmoothMove>();
        leftSmoothMove.enabled = true;
        leftSmoothMove.goal.x = -(interval * (viewMaxCount / 2));
        SmoothMove rightSmoothMove = viewCassettes[viewMaxCount / 2 + 1].GetComponent<SmoothMove>();
        rightSmoothMove.enabled = true;
        rightSmoothMove.goal.x = interval * (viewMaxCount / 2);

        // カセットのサイズ調整
        PointZoom pointZoom = mainCassette.GetComponent<PointZoom>();
        pointZoom.enabled = true;
        pointZoom.point = new Vector3(mouthPos.x, mouthPos.y, 0);

        velocity = initVelocity;
        changeWaitTime = initChangeWaitTime;
    }
    public override void StateUpdate()
    {
        // カセットを落とす
        Vector2 position = mainCassette.transform.position;
        if (position.y > mouthPos.y)
        {
            velocity += acceleration * Time.deltaTime;
            position.y += velocity * Time.deltaTime;
            position.x = 0;
        }
        // 落ちたらステージ選択へ
        else
        {
            position.y = mouthPos.y;
            changeWaitTime -= Time.deltaTime;
        }

        // 位置の更新
        mainCassette.transform.position = position;

        // カメラの位置をええ感にする
        Camera.main.transform.position = new Vector3(2 / (1 + Mathf.Exp(-position.x * 2)) - 1, 2 / (1 + Mathf.Exp(-position.y * 2)) - 1, -10);

        if (changeWaitTime < 0)
        {
            parent.transform.Find("ScreenZoomState").GetComponent<ScreenZoomState>().zoom = true;
            parent.ChangeState("ScreenZoomState");
        }


        // 前の処理
        //
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    // 画面外のゴミを消す
        //    foreach (GameObject cassette in viewCassettes)
        //        Object.Destroy(cassette);

        //    // カセット選択へ
        //    parent.ChangeState("CassetteSelectState");
        //}

    }
    public override void StateExit()
    {
        // カセットの動きを止める
        viewCassettes[viewMaxCount / 2 - 1].GetComponent<SmoothMove>().enabled = false;
        viewCassettes[viewMaxCount / 2 + 1].GetComponent<SmoothMove>().enabled = false;
        mainCassette.GetComponent<PointZoom>().enabled = false;
    }
}