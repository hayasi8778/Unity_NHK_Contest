using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 300f;
    public float Vod = 5f;

    public Vector3 customInitialPosition; // インスペクターで指定可能
    public bool useCustomPosition = false; // true のとき customInitialPosition を使う

    private Vector3 initialPosition;
    private Rigidbody2D rb;
    private bool hasExploded = false;

    public AudioClip shortFuseClip;
    public AudioClip longFuseClip;
    private AudioSource audioSource;

    void Start()
    {
        initialPosition = useCustomPosition ? customInitialPosition : transform.position;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (CompareTag("bomb1"))
        {
            Debug.Log("爆弾タグ: bomb1 → 爆発なし・再生成しない");
            return;
        }
        else if (CompareTag("bomb2"))
        {
            Vod = 3f;
            Debug.Log("爆弾タグ: bomb2 → 3秒後に爆発");
        }
        else if (CompareTag("bomb3"))
        {
            Vod = 10f;
            Debug.Log("爆弾タグ: bomb3 → 10秒後に爆発");
        }

        if (audioSource != null)
        {
            if (Vod <= 3f && shortFuseClip != null)
                audioSource.PlayOneShot(shortFuseClip);
            else if (Vod > 3f && longFuseClip != null)
                audioSource.PlayOneShot(longFuseClip);
        }

        Invoke("Explode", Vod);
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        Debug.Log("💥 爆発処理開始");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D colRb = col.attachedRigidbody;
            if (colRb != null && colRb != rb)
            {
                Vector2 direction = colRb.position - (Vector2)transform.position;
                colRb.AddForce(direction.normalized * explosionForce);
            }

            if (col.CompareTag("Object1") || col.CompareTag("Object2") || col.CompareTag("Object3"))
            {
                Destroy(col.gameObject);
            }
        }

        ResetBomb();
    }

    void ResetBomb()
    {
        Debug.Log("🔁 初期位置にワープ");

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.position = initialPosition;
        rb.rotation = 0f;

        hasExploded = false;
        Invoke("Explode", Vod);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
