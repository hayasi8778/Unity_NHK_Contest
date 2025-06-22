using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    [SerializeField]
    // ���������x
    public float speed = 1;

    [SerializeField]
    // �������Ԋu
    public float amplitude = 0.1f;

    // ����������
    private float deltaY = 0;
    // ���܂ꂽ����
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
