using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Cassette.State
{
    public class CassetteSelectState : IState
    {
        private Cassettes allCassette;
        public const int viewMaxCount = 5;
        public const float interval = 8;

        public static GameObject[] viewCassettes;
        private SmoothMove[] smoothMoves;
        public static int selectIndex = 0;

        private bool next = false;

        public override void Enter()
        {
            allCassette = (Cassettes)Resources.Load("Cassette/AllCassette");
            viewCassettes = new GameObject[viewMaxCount];
            smoothMoves = new SmoothMove[viewMaxCount];
            for (int i = 0; i < viewMaxCount; i++)
            {
                Vector3 pos = new Vector3((i - viewMaxCount / 2) * interval, 0, 0);
                viewCassettes[i] = Object.Instantiate(allCassette.cassettes[(i - viewMaxCount / 2 + allCassette.cassettes.Length + selectIndex) % allCassette.cassettes.Length], pos, Quaternion.identity);
                viewCassettes[i].GetComponent<FloatEffect>().enabled = true;
                viewCassettes[i].GetComponent<MoveCassette>().enabled = true;
                viewCassettes[i].GetComponent<PointZoom>().enabled = true;
                viewCassettes[i].GetComponent<SmoothMove>().enabled = true;
                smoothMoves[i] = viewCassettes[i].GetComponent<SmoothMove>();
                smoothMoves[i].goal = viewCassettes[i].transform.position;
            }
        }

        public override void Update()
        {
            if (allCassette.cassettes != null)
            {
                if (next)
                {
                    if (-0.3f < viewCassettes[viewMaxCount / 2].transform.position.x && viewCassettes[viewMaxCount / 2].transform.position.x < 0.3f)
                    {
                        parent.ChangeState(new Cassette.State.DropState());
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        selectIndex = (selectIndex + allCassette.cassettes.Length - 1) % allCassette.cassettes.Length;

                        for (int i = 1; i < viewMaxCount; i++)
                        {
                            viewCassettes[viewMaxCount - i] = viewCassettes[viewMaxCount - 1 - i];
                            smoothMoves[viewMaxCount - i] = smoothMoves[viewMaxCount - 1 - i];
                        }
                        Vector3 pos = new Vector3((-(viewMaxCount / 2)) * interval, 0, 0);
                        viewCassettes[0] = Object.Instantiate(allCassette.cassettes[(selectIndex + allCassette.cassettes.Length - viewMaxCount / 2) % allCassette.cassettes.Length], pos, Quaternion.identity);
                        smoothMoves[0] = viewCassettes[0].GetComponent<SmoothMove>();
                        smoothMoves[0].goal = new Vector3(-(viewMaxCount / 2) * interval, 0, 0);
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        selectIndex = (selectIndex + 1) % allCassette.cassettes.Length;

                        for (int i = 0; i < viewMaxCount - 1; i++)
                        {
                            viewCassettes[i] = viewCassettes[i + 1];
                            smoothMoves[i] = smoothMoves[i + 1];
                        }
                        Vector3 pos = new Vector3((viewMaxCount / 2) * interval, 0, 0);
                        viewCassettes[viewMaxCount - 1] = Object.Instantiate(allCassette.cassettes[(selectIndex + viewMaxCount / 2) % allCassette.cassettes.Length], pos, Quaternion.identity);
                        smoothMoves[viewMaxCount - 1] = viewCassettes[viewMaxCount - 1].GetComponent<SmoothMove>();
                        smoothMoves[viewMaxCount - 1].goal = new Vector3((viewMaxCount / 2) * interval, 0, 0);
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (-2.0f < viewCassettes[viewMaxCount / 2].transform.position.x && viewCassettes[viewMaxCount / 2].transform.position.x < 2.0f)
                        {
                            next = true;
                        }
                    }
                }
            }
        }

        public override void Exit()
        {
            for (int i = 0; i < viewMaxCount; i++)
            {
                viewCassettes[i].GetComponent<FloatEffect>().enabled = false;
                viewCassettes[i].GetComponent<MoveCassette>().enabled = false;
                viewCassettes[i].GetComponent<PointZoom>().enabled = false;
                viewCassettes[i].GetComponent<SmoothMove>().enabled = false;
            }
        }
    }
}
