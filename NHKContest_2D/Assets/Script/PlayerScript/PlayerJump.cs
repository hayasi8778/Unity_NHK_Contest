using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rb;//2Dにするので変更
    public float JumpPower = 10.0f; // ジャンプ力
    public float gravity = 20.0f;

    private HashSet<GameObject> validGround = new HashSet<GameObject>(); // 有効な"ground"を追跡
    private const float NormalThreshold = 0.7f; // 法線方向のしきい値

    private bool JumpFlag = false;

    private AudioSource audioSource; //ジャンプ時のSE再生するためのやつ
    public AudioClip jumpSE;


    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //AudioSourceをセット

    }

    void Update()
    {
        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.Space) && validGround.Count > 0)
        //if (Input.GetKeyDown(KeyCode.Space) && JumpFlag)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpPower); // AddForce の代わりに velocity を直接変更
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(jumpSE); // ジャンプ時のSE再生
            }
            

        }


        if (validGround.Count == 0)
        { // 空中にいるとき
            //下向きの力(重力)の加算
            rb.linearVelocity += Vector2.down * gravity * Time.deltaTime; // Rigidbody2DではAddForceよりvelocityの調整が適切
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") ||
         collision.gameObject.CompareTag("Object1") ||
         collision.gameObject.CompareTag("Object2") ||
         collision.gameObject.CompareTag("Object3")
        )
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 衝突面がほぼ上向きの場合のみカウント
                if (Vector2.Dot(contact.normal, Vector2.up) > NormalThreshold)
                {
                    if (validGround.Count < 1)
                    {
                        validGround.Add(collision.gameObject);
                        break; // 一度条件を満たしたらループを抜ける
                    }
                    else
                    {
                        break; // 一度条件を満たしたらループを抜ける
                    }
                }
            }

        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") ||
         collision.gameObject.CompareTag("Object1") ||
         collision.gameObject.CompareTag("Object2") ||
         collision.gameObject.CompareTag("Object3"))
        {
            if(validGround.Count > 0)
            {
                validGround.Remove(collision.gameObject); // 接触解除時にリストから削除
            }
            
        }
    }
}
