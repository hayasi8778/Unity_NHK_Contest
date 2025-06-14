using UnityEngine;


public class SelectCassette : MonoBehaviour
{
    public static Cassette.StateMachine stateMachine = new Cassette.StateMachine();

    private void Start()
    {
        stateMachine.gameObject = gameObject;
        stateMachine.ChangeState(new Cassette.State.ChooseState());
    }

    private void Update()
    {
        stateMachine.Update();
    }
}