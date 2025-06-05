using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 300f;
    public LayerMask explosionMask;
    public float Vod = 5f;


    void Start()
    {
        if (CompareTag("bomb1"))
        {
            Debug.Log("爆弾タグ: bomb1 → 爆発なし");
            return;
        }
        else if (CompareTag("bomb2"))
        {
            Debug.Log("爆弾タグ: bomb2 → 3秒後に爆発");
            Invoke("Explode", Vod);
        }
        else if (CompareTag("bomb3"))
        {
            Debug.Log("爆弾タグ: bomb3 → 10秒後に爆発");
            Invoke("Explode", Vod);
        }
    }

    void Explode()
    {
        Debug.Log("💥 爆発処理開始");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            Debug.Log("検出: " + col.name + " / タグ: " + col.tag);

            // 💨 吹き飛ばす処理（Rigidbody2D がある場合）
            Rigidbody2D rb = col.attachedRigidbody;
            if (rb != null)
            {
                Vector2 direction = rb.position - (Vector2)transform.position;
                rb.AddForce(direction.normalized * explosionForce);
                Debug.Log("💨 力を加えた: " + col.name);
            }

            // ✅ 破壊対象かどうかチェックして破壊
            if (col.CompareTag("Object1") || col.CompareTag("Object2") || col.CompareTag("Object3"))
            {
                Debug.Log("✅ 破壊対象に一致: " + col.name);
                Destroy(col.gameObject);
            }
            else
            {
                Debug.Log("❌ タグ一致せず: " + col.tag);
            }
        }

        // 自分（爆弾）も消す
        Destroy(gameObject);
    }


    // 爆風範囲をシーンビューに表示
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
