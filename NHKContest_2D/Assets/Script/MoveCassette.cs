using UnityEngine;

public class MoveCassette : MonoBehaviour
{
    SmoothMove smoothMove;
    TypeCassete typeCassete;

    [SerializeField]
    Cassettes allCassette;

    private void Start()
    {
        smoothMove = GetComponent<SmoothMove>();
        typeCassete = GetComponent<TypeCassete>();
    }

    private void Update()
    {
        Vector3 goal = new Vector3((typeCassete.number - SelectCassette.selectIndex) * 10, 0, 0);
        smoothMove.goal = goal;
    }
}