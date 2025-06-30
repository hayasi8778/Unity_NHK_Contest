using UnityEngine;
public abstract class IState : MonoBehaviour
{
    public StateMachine parent;
    public abstract void StateEnter();
    public abstract void StateUpdate();
    public abstract void StateExit();
}