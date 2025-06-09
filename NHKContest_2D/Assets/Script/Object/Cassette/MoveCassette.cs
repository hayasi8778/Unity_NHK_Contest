using UnityEditor;
using UnityEngine;

public class MoveCassette : MonoBehaviour
{
    private const float interval = 8;
    private SmoothMove smoothMove;

    private void Start()
    {
        smoothMove = GetComponent<SmoothMove>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 vec = new Vector3(interval, 0, 0);
            smoothMove.goal += vec;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 vec = new Vector3(-interval, 0, 0);
            smoothMove.goal += vec;
        }
    }
}
