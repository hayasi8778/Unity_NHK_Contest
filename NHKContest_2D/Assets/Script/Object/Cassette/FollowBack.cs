using UnityEngine;

public class FollowBack : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraPos;
    [SerializeField]
    private float margin = 0;
    [SerializeField]
    private float gradient = 1;

    private void Update()
    {
        Vector3 pos = cameraPos.transform.position;
        pos.x += (2 / (1 + Mathf.Exp(pos.x / gradient)) - 1) * margin;
        pos.y += (2 / (1 + Mathf.Exp(pos.y / gradient)) - 1) * margin;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
