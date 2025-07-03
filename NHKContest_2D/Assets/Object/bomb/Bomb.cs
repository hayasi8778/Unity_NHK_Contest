using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Bomb : MonoBehaviour
{
    [System.Serializable]
    public class ScenePosition
    {
        public string sceneName;
        public Vector3 position;
    }

    [Header("シーンごとの初期位置設定")]
    public List<ScenePosition> scenePositions;

    [Header("爆発設定")]
    public float explosionRadius = 5f;
    public float explosionForce = 300f;
    public float Vod = 5f;

    [Header("サウンド設定")]
    public AudioClip countdownClip;   // 爆発までのカウントダウン音
    public AudioClip explosionClip;   // 爆発時の音

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private bool hasExploded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        SetInitialPositionByScene();

        // タグごとに遅延設定
        if (CompareTag("bomb1"))
        {
            Debug.Log("爆弾タグ: bomb1 → 爆発しない");
            return;
        }

        Vod = CompareTag("bomb2") ? 3f : CompareTag("bomb3") ? 10f : Vod;

        // カウントダウン音再生
        if (audioSource != null && countdownClip != null)
        {
            audioSource.PlayOneShot(countdownClip);
        }

        Invoke(nameof(Explode), Vod);
    }

    void SetInitialPositionByScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        foreach (var sp in scenePositions)
        {
            if (sp.sceneName == sceneName)
            {
                initialPosition = sp.position;
                Debug.Log($"初期位置設定: シーン「{sceneName}」→ {initialPosition}");
                return;
            }
        }

        initialPosition = transform.position;
        Debug.LogWarning($"初期位置未設定: シーン「{sceneName}」。現在位置を使用");
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        Debug.Log("💥 爆発！");

        if (audioSource != null && explosionClip != null)
        {
            audioSource.PlayOneShot(explosionClip);
        }

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

                //Vector3 teleportTarget = col.transform.position + new Vector3(0f, 8f, 0f);
                //col.transform.position = teleportTarget;
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

        // カウントダウン再再生
        if (audioSource != null && countdownClip != null)
        {
            audioSource.PlayOneShot(countdownClip);
        }

        Invoke(nameof(Explode), Vod);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
