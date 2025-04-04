using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody rb;
    public float JumpPower = 10.0f; // ジャンプ力


    private HashSet<GameObject> validGround = new HashSet<GameObject>(); // 有効な"ground"を追跡
    private const float NormalThreshold = 0.7f; // 法線方向のしきい値

    void Start()
    {

    }

    void Update()
    {
        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.Space) && validGround.Count > 0)
        {
            rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                // 衝突面がほぼ上向きの場合のみカウント
                if (Vector3.Dot(contact.normal, Vector3.up) > NormalThreshold)
                {
                    validGround.Add(collision.gameObject);
                    break; // 一度条件を満たしたらループを抜ける
                }
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            validGround.Remove(collision.gameObject); // 接触解除時にリストから削除
        }
    }
}
