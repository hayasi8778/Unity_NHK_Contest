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

    private bool GravityFlag = true;//重力フラグ(trueで正常falseで反転)

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSourceをセット
    }

    void Update()
    {
        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.Space) && validGround.Count > 0)
        {
            if (GravityFlag)
            {
                rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(jumpSE); // ジャンプ時のSE再生
                }
            }
            else
            {
                //Debug.Log("反転ジャンプ");
                rb.AddForce(Vector2.down * JumpPower, ForceMode2D.Impulse);
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(jumpSE); // ジャンプ時のSE再生
                }
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
                if (GravityFlag)//重力の向きで切り替え
                {
                    //正重力(下方向に地面があるか)
                    // 衝突面がほぼ上向きの場合のみカウント
                    if (Vector2.Dot(contact.normal, Vector2.up) > NormalThreshold)
                    {
                        validGround.Add(collision.gameObject);
                        break; // 一度条件を満たしたらループを抜ける
                    }
                }
                else
                {
                    //反転状態(上方向に地面があるか)
                    if (Vector2.Dot(contact.normal, Vector2.down) > NormalThreshold)
                    {
                        validGround.Add(collision.gameObject);
                        break; // 一度条件を満たしたらループを抜ける
                    }
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

    public void SetGravityFlag()//重力の反転に応じてフラグを切り替える
    {
        Debug.Log("重力反転確認");

        if (GravityFlag)
        {
            GravityFlag = false;
        }
        else
        {
            GravityFlag = true;
        }
    }

    public bool GetGravityFlag()
    {
        return GravityFlag;
    }

    public void SetNewGravityFlag(bool flag)
    {
        Debug.Log("新しいオブジェクトに重力フラグつけるよ");
        GravityFlag = flag;
    }
}
