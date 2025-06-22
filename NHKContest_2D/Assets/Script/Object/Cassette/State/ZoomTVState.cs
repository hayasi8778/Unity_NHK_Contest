using UnityEngine;

namespace Cassette.State
{
    public class ZoomTVState : IState
    {
        private bool zoom;
       
        public override void Enter()
        {
            zoom = true;
        }

        public override void Update()
        {
            if (Camera.main.orthographicSize > 3 && zoom)
            {
                Vector3 pos = Camera.main.transform.position;
                pos.y = Mathf.Lerp(pos.y, 6, Time.deltaTime * 10);
                Camera.main.transform.position = pos;
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3, Time.deltaTime * 10);


            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                zoom = false;
            }

            if (Camera.main.orthographicSize < 5 && !zoom)
            {
                Vector3 pos = Camera.main.transform.position;
                pos.y = Mathf.Lerp(pos.y, 0, Time.deltaTime * 10);
                Camera.main.transform.position = pos;
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5, Time.deltaTime * 10);

                Vector3 casPos = CassetteSelectState.viewCassettes[CassetteSelectState.viewMaxCount / 2].transform.position;
                casPos.y = Mathf.Lerp(casPos.y, 0, Time.deltaTime * 10);
                CassetteSelectState.viewCassettes[CassetteSelectState.viewMaxCount / 2].transform.position = casPos;
            }
            
            if (Camera.main.orthographicSize > 4.99f && !zoom)
            {
                foreach (GameObject cassette in CassetteSelectState.viewCassettes)
                    Object.Destroy(cassette);
                parent.ChangeState(new Cassette.State.CassetteSelectState());
            }
        }
        public override void Exit()
        {

        }
    }
}
