using UnityEditor.Rendering.Universal;
using UnityEngine;

namespace Cassette
{
    public abstract class IState
    {
        public StateMachine parent;
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }

    public class StateMachine
    {
        public GameObject gameObject;
        private IState currentState;

        public System.Type GetStateType()
        {
            return currentState.GetType();
        }

        public void ChangeState(IState newState)
        {
            currentState?.Exit();  // 現在の状態が存在する場合、終了処理を呼び出す
            currentState = newState;  // 新しい状態を現在の状態に設定
            currentState.parent = this;
            currentState.Enter();  // 新しい状態の初期化処理を実行
        }
        public void Update()
        {
            currentState?.Update();  // 現在の状態のUpdateメソッドを呼び出す
        }
    }
}
