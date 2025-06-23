using UnityEngine;

public class StageSelectState : IState
{
    public override void StateEnter()
    {

    }
    public override void StateUpdate()
    { 
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
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