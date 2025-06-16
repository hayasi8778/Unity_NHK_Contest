using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Cassette.State
{
    public class StageSelectState : IState
    {
        private Cassettes allCassette;
        public const int viewMaxCount = 5;
        public const float interval = 8;

        public static GameObject[] viewCassettes;
        private SmoothMove[] smoothMoves;
        public static int selectIndex = 0;



        public override void Enter()
        {
            allCassette = (Cassettes)Resources.Load("Cassette/AllCassette");
            viewCassettes = new GameObject[viewMaxCount];
            smoothMoves = new SmoothMove[viewMaxCount];
            for (int i = 0; i < viewMaxCount; i++)
            {
                Vector3 pos = new Vector3((i - viewMaxCount / 2) * interval, 0, 0);
                viewCassettes[i] = Object.Instantiate(allCassette.cassettes[(i - viewMaxCount / 2 + allCassette.cassettes.Length) % allCassette.cassettes.Length], pos, Quaternion.identity);
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
