using UnityEngine;

public class CenterZoom : MonoBehaviour
{
    const float size = 20;
    
    private Vector3 originScale;

    private void Start()
    {
        originScale = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = originScale / (transform.localPosition.magnitude / size + 1);
    }
}
