using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    [SerializeField]
    // 動かす速度
    public float speed = 1;

    [SerializeField]
    // 動かす間隔
    public float amplitude = 0.1f;

    // 動いた距離
    private float deltaY = 0;
    // 生まれた時間
    private float awakeTime = 0;

    private void Awake()
    {
        awakeTime = Time.time;
    }

    private void Update()
    {
        Vector2 pos = transform.position;
        pos.y -= deltaY;
        deltaY = Mathf.Sin((Time.time - awakeTime) * speed) * transform.localScale.y * amplitude;
        pos.y += deltaY;
        transform.position = pos;
    }


}
