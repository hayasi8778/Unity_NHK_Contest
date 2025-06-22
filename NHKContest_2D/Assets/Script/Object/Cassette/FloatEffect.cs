using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    [SerializeField]
    // ìÆÇ©Ç∑ë¨ìx
    public float speed = 1;

    [SerializeField]
    // ìÆÇ©Ç∑ä‘äu
    public float amplitude = 0.1f;

    // ìÆÇ¢ÇΩãóó£
    private float deltaY = 0;
    // ê∂Ç‹ÇÍÇΩéûä‘
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
