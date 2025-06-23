using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 300f;
    public float Vod = 5f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;

        if (CompareTag("bomb1"))
        {
            Debug.Log("爆弾タグ: bomb1 → 爆発なし・再生成しない");
            return;
        }
        else if (CompareTag("bomb2"))
        {
            Debug.Log("爆弾タグ: bomb2 → 3秒後に爆発");
            Vod = 3f;
            Invoke("Explode", Vod);
        }
        else if (CompareTag("bomb3"))
        {
            Debug.Log("爆弾タグ: bomb3 → 10秒後に爆発");
            Vod = 10f;
            Invoke("Explode", Vod);
        }
    }

    void Explode()
    {
        Debug.Log("💥 爆発処理開始");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.attachedRigidbody;
            if (rb != null)
            {
                Vector2 direction = rb.position - (Vector2)transform.position;
                rb.AddForce(direction.normalized * explosionForce);
            }

            if (col.CompareTag("Object1") || col.CompareTag("Object2") || col.CompareTag("Object3"))
            {
                Destroy(col.gameObject);
            }
        }

        // bomb1生成なし
        Destroy(gameObject);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
