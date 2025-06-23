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

    public float AnimationTime = 0.5f;//アニメーションの待ち時間
    public float JumpToTime = 0.5f;//ジャンプアニメーションの許可時間

    private bool GravityFlag = true;//重力フラグ(trueで正常falseで反転)

     private bool animFlag = true; //アニメーションのフラグ
    private bool P_jumpFlag = false; //アニメーションのフラグ

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSourceをセット
    }

    [System.Obsolete]
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

            //ジャンプのアニメーションフラグon
            JumpFlagON();
            
        }

        if(!(validGround.Count > 0))//何処にもあたっていないなら
        {
            if (animFlag && P_jumpFlag)
            {
                Debug.Log("ジャンプアニメーション");
                PlayJump();//ジャンプアニメーション流す
            }
           
        }

        //animFlag = false;

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

        //playermuveのフラグも同時に切り替える
        PlayerMove playermove = this.gameObject.GetComponent<PlayerMove>();

        playermove.SetGravityFrag(GravityFlag);
    }

    public bool GetGravityFlag()
    {
        return GravityFlag;
    }

    public void SetNewGravityFlag(bool flag)
    {
        Debug.Log("新しいオブジェクトに重力フラグつけるよ");
        GravityFlag = flag;

        FlipCheck();
    }

    public void FlipPlayerTexture()
    {
        Vector3 scale = transform.localScale;
        scale.y *= -1; // Y軸方向に反転
        transform.localScale = scale;
    }

    public void FlipCheck()//上下切り替えているかを調べる
    {
        if (!GravityFlag)
        {
            Vector3 scale = transform.localScale;
            scale.y *= -1; // Y軸方向に反転
            transform.localScale = scale;
        }
    }

    private void PlayJump()
    {
        animFlag = false;

        //オブジェクト継承して
        PlayerMoveAmin JumpAmin = this.GetComponent<PlayerMoveAmin>();

        JumpAmin.ChangeSprite_Push();

        Invoke("StopAmin", AnimationTime);

    }
    void StopAmin()
    {
        animFlag = true;
    }

    private void JumpFlagON()
    {
        P_jumpFlag = true;

        //Debug.Log("ジャンプアニメーション");

        Invoke("JumpFlagOFF", JumpToTime);

    }

    private void JumpFlagOFF()
    {
        P_jumpFlag = false;

    }

}
