using UnityEngine;

public class DestroyCassette : MonoBehaviour
{
    private const float width = 16.01f;
    private void Update()
    {
        if (transform.position.x <= -width || width <= transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
