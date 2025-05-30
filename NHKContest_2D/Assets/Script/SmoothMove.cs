using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    [SerializeField]
    public Vector3 goal;
    
    [SerializeField]
    public float speed;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, goal, speed * Time.deltaTime);
    }
}
