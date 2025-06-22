using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 300f;
    public LayerMask explosionMask;
    public float Vod = 5f;

    public AudioClip beepClip;            // ピ音
    private AudioSource audioSource;      // AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (CompareTag("bomb1"))
        {
            Debug.Log("爆弾タグ: bomb1 → 爆発なし");
            return;
        }
        else if (CompareTag("bomb2"))
        {
            
            Debug.Log("爆弾タグ: bomb2 → 3秒後に爆発");
            StartCoroutine(CountdownAndExplode(Vod));
        }
        else if (CompareTag("bomb3"))
        {
           
            Debug.Log("爆弾タグ: bomb3 → 10秒後に爆発");
            StartCoroutine(CountdownAndExplode(Vod));
        }
    }

    IEnumerator CountdownAndExplode(float delay)
    {
        float timeLeft = delay;

        while (timeLeft > 0f)
        {
            Debug.Log($"カウントダウン: {timeLeft}秒");
            if (beepClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(beepClip);
            }

            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        Explode();
    }

    void Explode()
    {
        Debug.Log("💥 爆発処理開始");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            Debug.Log("検出: " + col.name + " / タグ: " + col.tag);

            Rigidbody2D rb = col.attachedRigidbody;
            if (rb != null)
            {
                Vector2 direction = rb.position - (Vector2)transform.position;
                rb.AddForce(direction.normalized * explosionForce);
                Debug.Log("💨 力を加えた: " + col.name);
            }

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

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
