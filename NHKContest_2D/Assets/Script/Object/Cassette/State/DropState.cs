using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEditor.PlayerSettings;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Cassette.State
{
    public class DropState : IState
    {
        private Vector2 velocity;
        private Vector2 acceleration;

        public override void Enter()
        {
            SmoothMove leftSmoothMove = CassetteSelectState.viewCassettes[CassetteSelectState.viewMaxCount / 2 - 1].GetComponent<SmoothMove>();
            leftSmoothMove.enabled = true;
            leftSmoothMove.goal.x = -(CassetteSelectState.interval * (CassetteSelectState.viewMaxCount / 2));
            SmoothMove rightSmoothMove = CassetteSelectState.viewCassettes[CassetteSelectState.viewMaxCount / 2 + 1].GetComponent<SmoothMove>();
            rightSmoothMove.enabled = true;
            rightSmoothMove.goal.x = CassetteSelectState.interval * (CassetteSelectState.viewMaxCount / 2);
            PointZoom pointZoom = CassetteSelectState.viewCassettes[CassetteSelectState.viewMaxCount / 2].GetComponent<PointZoom>();
            pointZoom.enabled = true;
            pointZoom.point = new Vector3(0, -5, 0);


            velocity.y = 2;
            acceleration.y = -10;
        }
        public override void Update()
        {
            Vector2 position = CassetteSelectState.viewCassettes[CassetteSelectState.viewMaxCount / 2].transform.position;
            if (position.y > -5)
            {
                velocity += acceleration * Time.deltaTime;
                position += velocity * Time.deltaTime;
                position.x = 0;
            }
            else
            {
                position.y = -5;
                parent.ChangeState(new Cassette.State.ZoomTVState());
            }
            CassetteSelectState.viewCassettes[CassetteSelectState.viewMaxCount / 2].transform.position = position;

            Camera.main.transform.position = new Vector3(2 / (1 + Mathf.Exp(-position.x * 2)) - 1, 2 / (1 + Mathf.Exp(-position.y * 2)) - 1, -10);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                foreach (GameObject cassette in CassetteSelectState.viewCassettes)
                    Object.Destroy(cassette);
                parent.ChangeState(new Cassette.State.CassetteSelectState());
            }

        }
        public override void Exit()
        {
            CassetteSelectState.viewCassettes[CassetteSelectState.viewMaxCount / 2 - 1].GetComponent<SmoothMove>().enabled = false;
            CassetteSelectState.viewCassettes[CassetteSelectState.viewMaxCount / 2 + 1].GetComponent<SmoothMove>().enabled = false;
            CassetteSelectState.viewCassettes[CassetteSelectState.viewMaxCount / 2].GetComponent<PointZoom>().enabled = false;
        }
    }
}
