using UnityEngine;

public class CassetteSelectState : IState
{
    [SerializeField]
    private GameObject[] allCassette;   // 使うすべてのカセットオブジェクト
    [SerializeField]
    public int viewMaxCount = 5;    // 表示されるカセットの数
    [SerializeField]
    public float interval = 8.0f;   // 各カセットの間隔
    [SerializeField]
    public float initInterval = 10.0f;   // 各カセットの間隔
    [SerializeField]
    private float errorRange = 0.3f;  // 選択したカセットのズレ範囲
    [SerializeField]
    private float inputRange = 2.0f;   // 先行入力範囲
    [SerializeField]
    private float lifeTime = 1.0f;  // 画面外のカセットの消える時間

[SerializeField]
    public GameObject[] viewCassettes;  // 表示されるカセットオブジェクト
    [SerializeField]
    private SmoothMove[] smoothMoves;   // カセットのsmoothMoveパス用
    [SerializeField]
    public int selectIndex = 0;     // 選んでいるカセット
    private bool next;              // 先行入力保存用

    public override void StateEnter()
    {
        // 初期化
        next = false;
        viewCassettes = new GameObject[viewMaxCount];
        smoothMoves = new SmoothMove[viewMaxCount];
        for (int i = 0; i < viewMaxCount; i++)
        {
            // カセットを一定間隔で生成
            Vector3 pos = new Vector3((i - viewMaxCount / 2) * initInterval, 0, 0);
            viewCassettes[i] = Object.Instantiate(allCassette[(i - viewMaxCount / 2 + allCassette.Length + selectIndex) % allCassette.Length], pos, Quaternion.identity);
            
            // 生成したカセットのええ感の動きを起動
            viewCassettes[i].GetComponent<FloatEffect>().enabled = true;
            viewCassettes[i].GetComponent<MoveCassette>().enabled = true;
            viewCassettes[i].GetComponent<PointZoom>().enabled = true;
            viewCassettes[i].GetComponent<SmoothMove>().enabled = true;

            // SmoothMoveにアクセスしやすいように設定
            smoothMoves[i] = viewCassettes[i].GetComponent<SmoothMove>();
            smoothMoves[i].goal = new Vector3((i - viewMaxCount / 2) * interval, 0, 0);
        }
    }

    public override void StateUpdate()
    {
        // 次へに待機しているか
        if (next)
        {
            // 位置がいい感じに収まったら
            if (-errorRange < viewCassettes[viewMaxCount / 2].transform.position.x && viewCassettes[viewMaxCount / 2].transform.position.x < errorRange)
            {
                // カセット入れる状態へ
                parent.ChangeState("DropState");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // 選択カセットIndexを循環させながら減らす
                selectIndex = (selectIndex + allCassette.Length - 1) % allCassette.Length;

                // 各カセットをずらす
                Destroy(viewCassettes[viewMaxCount - 1], lifeTime);
                for (int i = 1; i < viewMaxCount; i++)
                {
                    viewCassettes[viewMaxCount - i] = viewCassettes[viewMaxCount - 1 - i];
                    smoothMoves[viewMaxCount - i] = smoothMoves[viewMaxCount - 1 - i];
                }

                // 新しくカセットを生成
                Vector3 pos = new Vector3((-(viewMaxCount / 2)) * interval, 0, 0);
                viewCassettes[0] = Object.Instantiate(allCassette[(selectIndex + allCassette.Length - viewMaxCount / 2) % allCassette.Length], pos, Quaternion.identity);
                smoothMoves[0] = viewCassettes[0].GetComponent<SmoothMove>();
                smoothMoves[0].goal = new Vector3(-(viewMaxCount / 2) * interval, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // 選択カセットIndexを循環させながら増やす
                selectIndex = (selectIndex + 1) % allCassette.Length;

                // 各カセットをずらす
                Destroy(viewCassettes[0], lifeTime);
                for (int i = 0; i < viewMaxCount - 1; i++)
                {
                    viewCassettes[i] = viewCassettes[i + 1];
                    smoothMoves[i] = smoothMoves[i + 1];
                }

                // 新しくカセットを生成
                Vector3 pos = new Vector3((viewMaxCount / 2) * interval, 0, 0);
                viewCassettes[viewMaxCount - 1] = Object.Instantiate(allCassette[(selectIndex + viewMaxCount / 2) % allCassette.Length], pos, Quaternion.identity);
                smoothMoves[viewMaxCount - 1] = viewCassettes[viewMaxCount - 1].GetComponent<SmoothMove>();
                smoothMoves[viewMaxCount - 1].goal = new Vector3((viewMaxCount / 2) * interval, 0, 0);
            }


            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // 真ん中のカセットがええ感の範囲にあったら
                if (-inputRange < viewCassettes[viewMaxCount / 2].transform.position.x && viewCassettes[viewMaxCount / 2].transform.position.x < inputRange)
                {
                    // 次に待機
                    next = true;
                }
            }
        }
    }

    public override void StateExit()
    {
        // カセットの動きを止める
        for (int i = 0; i < viewMaxCount; i++)
        {
            viewCassettes[i].GetComponent<FloatEffect>().enabled = false;
            viewCassettes[i].GetComponent<MoveCassette>().enabled = false;
            viewCassettes[i].GetComponent<PointZoom>().enabled = false;
            smoothMoves[i].enabled = false;
        }
    }
}