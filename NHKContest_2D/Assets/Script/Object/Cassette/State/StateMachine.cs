using UnityEngine;
public class StateMachine : MonoBehaviour
{
    [SerializeField]
    private IState currentState;    // 現在の状態

    /// <summary>
    /// 状態を変える
    /// </summary>
    public void ChangeState(string _stateName)
    {
        IState newState;    // 次の状態

        // 入力の検証
        if (newState = transform.Find(_stateName).GetComponent<IState>())
        { 
            currentState?.StateExit();  // 現在の状態が存在する場合、終了処理を呼び出す
            currentState = newState;    // 新しい状態を現在の状態に設定
            currentState.parent = this; // 新しい状態に本体のパスを設定
            currentState.StateEnter();  // 新しい状態の初期化処理を実行
        }
        else
        {
            Debug.Log("存在しない状態を指定してるよ");
        }
    }

    private void Start()
    {
        // 初期値の状態のEnterを実行
        currentState?.StateEnter();
    }
    private void Update()
    {
        // 現在のUpdateを実行
        currentState?.StateUpdate();
    }
}
