using UnityEngine;

public class PointZoom : MonoBehaviour
{
    const float size = 20;
    public Vector3 point;
    
    private Vector3 originScale;

    private void Start()
    {
        originScale = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = originScale / ((transform.localPosition - point).magnitude / size + 1);
    }
}
