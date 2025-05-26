using UnityEngine;


public class SelectCassette : MonoBehaviour
{
    [SerializeField]
    public Cassettes allCassette;
    private const int viewMaxCount = 5;
    private const float interval = 8;

    private GameObject[] viewCassettes;
    private SmoothMove[] smoothMoves;
    public static int selectIndex = 0;


    private void Start()
    {
        viewCassettes = new GameObject[viewMaxCount];
        smoothMoves = new SmoothMove[viewMaxCount];
        for (int i = 0; i < viewMaxCount; i++)
        {
            Vector3 pos = new Vector3 ((viewMaxCount / 2 - i) * interval, 0, 0);
            viewCassettes[i] = Instantiate(allCassette.cassettes[(i + allCassette.cassettes.Length) % +allCassette.cassettes.Length], pos, Quaternion.identity);
            smoothMoves[i] = viewCassettes[i].GetComponent<SmoothMove>();
            smoothMoves[i].goal = viewCassettes[i].transform.position;
        }
    }

    private void Update()
    {
        if (allCassette.cassettes != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectIndex = (selectIndex + allCassette.cassettes.Length - 1) % allCassette.cassettes.Length;
                Vector3 vec = new Vector3(interval, 0, 0);
                for (int i = 0; i < viewMaxCount; i++)
                {
                    smoothMoves[i].goal += vec; 
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectIndex = (selectIndex + 1) % allCassette.cassettes.Length;
                Vector3 vec = new Vector3(-interval, 0, 0);
                for (int i = 0; i < viewMaxCount; i++)
                {
                    smoothMoves[i].goal += vec;
                }
            }
        }
    }
}
