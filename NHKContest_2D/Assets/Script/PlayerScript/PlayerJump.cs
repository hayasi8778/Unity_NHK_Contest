using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rb;
    public float JumpPower = 10.0f; // ジャンプ力

    private HashSet<GameObject> validGround = new HashSet<GameObject>(); // 有効な"ground"を追跡
    private const float NormalThreshold = 0.7f; // 法線方向のしきい値

    private AudioSource audioSource; //ジャンプ時のSE再生するためのやつ
    public AudioClip jumpSE;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSourceをセット
    }

    void Update()
    {
        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.Space) && validGround.Count > 0)
        {
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(jumpSE); // ジャンプ時のSE再生
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") ||
            collision.gameObject.CompareTag("Object1") ||
            collision.gameObject.CompareTag("Object2") ||
            collision.gameObject.CompareTag("Object3"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 衝突面がほぼ上向きの場合のみカウント
                if (Vector2.Dot(contact.normal, Vector2.up) > NormalThreshold)
                {
                    validGround.Add(collision.gameObject);
                    break; // 一度条件を満たしたらループを抜ける
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") ||
            collision.gameObject.CompareTag("Object1") ||
            collision.gameObject.CompareTag("Object2") ||
            collision.gameObject.CompareTag("Object3"))
        {
            validGround.Remove(collision.gameObject); // 接触解除時にリストから削除
        }
    }
}
