using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;
using System.Collections;


public class PlayerMove : MonoBehaviour
{
    public float movespeed = 1.0f;//移動スピード
    private bool muveFlag = false; //歩行しているかのフラグ
    
    private AudioSource audioSource; //音鳴らすためのコンポーネント
    public AudioClip walkSE;
    private new SpriteRenderer renderer; //レンダーを取得する

    //public PlayerMoveAmin MoveAmin; //歩行時のアニメーション
    bool animFlag = true;//アニメーション中かのフラグ

    public float AnimationTime = 0.5f;

    private bool GravityFlag = true;//重力フラグ(trueで正常falseで反転)

    //壁の接触フラグ
    bool pushFlag = false;
    [SerializeField] private float wallCheckOffset = 0.5f; // プレイヤーの中心から左右に離す距離
    [SerializeField] private float wallCheckDistance = 0.1f;
    [SerializeField] private LayerMask wallLayer; // 判定したいレイヤーを指定


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSourceを取得

        //MoveAmin = GetComponent<PlayerMoveAmin>();

        //プレイヤーが描画されないからレンダーが有効か確認する
        renderer = GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            //Debug.Log("Renderer is " + (renderer.enabled ? "enabled" : "disabled"));
        }
        else
        {
            Debug.Log("レンダーないぞ");
        }


        // サウンドの設定　中谷
        audioSource.clip = walkSE;  // 音を設定
        audioSource.loop = true;    // ループするように設定
    }

    private bool canMove = true; // 移動許可フラグ
    public void SetCanMove(bool flag)
    {
        canMove = flag;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!canMove) return;

        pushFlag = false;

        // 壁との接触チェック
        CheckPush(); // ← 追加

        //if (pushFlag) Debug.Log("push判定on");

        //角度の都合で移動方向左右反転する


        muveFlag = false; //歩行フラグは毎フレーム切る
        
        Vector3 scale = transform.localScale;


        //右移動
        if (Input.GetKey(KeyCode.D))
        {



            //左右切り替え
            if (GravityFlag) //画面回るから操作方向切り替える  林
            {
                // ↓ゲーム内時間を止めても振りむいちゃうんで条件追加しました　中谷
                if (Time.timeScale > 0)
                {
                    transform.Translate(movespeed * Time.deltaTime, 0, 0);

                    renderer.flipX = false;
                }
            }
            else
            {
                // ↓ゲーム内時間を止めても振りむいちゃうんで条件追加しました　中谷
                if (Time.timeScale > 0)
                {
                    
                    transform.Translate(-movespeed * Time.deltaTime, 0, 0);

                    renderer.flipX = true;
                }
            }


                

            muveFlag = true;
            /*
            if (!audioSource.isPlaying) // 音が再生中じゃないなら
            {
                PlaySound(); //SE再生
            }*/

        }

        //左移動
        if (Input.GetKey(KeyCode.A))
        {
            if (GravityFlag)
            {
                // ↓ゲーム内時間を止めても振りむいちゃうんで条件追加しました　中谷
                if (Time.timeScale > 0)
                {
                    
                    //左右切り替え
                    transform.Translate(-movespeed * Time.deltaTime, 0, 0);

                    renderer.flipX = true;
                }
            }
            else
            {
                // ↓ゲーム内時間を止めても振りむいちゃうんで条件追加しました　中谷
                if (Time.timeScale > 0)
                {
                   
                    transform.Translate(movespeed * Time.deltaTime, 0, 0);

                    renderer.flipX = false;
                }
            }



                

            muveFlag = true;
            /*
            if (!audioSource.isPlaying) // 再生中でないなら
            {
                PlaySound();

            }
            */

        }

        if (muveFlag && animFlag)
        {

            PlayAnim();
        }

        // 入力状況とゲーム内時間の確認をして音を鳴らすか止める　中谷
        if (muveFlag && Time.timeScale > 0) PlaySound();
        else StopAudio();

    }

    void CheckPush()
    {
        Vector2 position = transform.position;

        // Rayを出す起点を左右にずらす
        Vector2 rightOrigin = position + Vector2.right * wallCheckOffset;
        Vector2 leftOrigin = position + Vector2.left * wallCheckOffset;

        // 各方向にRayを飛ばす
        RaycastHit2D hitRight = Physics2D.Raycast(rightOrigin, Vector2.right, wallCheckDistance, wallLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(leftOrigin, Vector2.left, wallCheckDistance, wallLayer);

        pushFlag = (hitRight.collider != null || hitLeft.collider != null);

        // デバッグ表示（シーンビューでRayを見えるように）
        Debug.DrawRay(rightOrigin, Vector2.right * wallCheckDistance, Color.red);
        Debug.DrawRay(leftOrigin, Vector2.left * wallCheckDistance, Color.red);

    }

    void PlaySound()
    {
        // 再生状況の確認を関数内で行う　中谷
        if (!audioSource.isPlaying) audioSource.Play();
    }


    void StopAudio()
    {
        // 再生状況の確認を関数内で行う　中谷
        if (audioSource.isPlaying) audioSource.Stop();
    }

    void PlayAnim()
    {
        animFlag = false;
        //オブジェクト継承して
        PlayerMoveAmin MoveAmin = this.GetComponent<PlayerMoveAmin>();

        if (pushFlag)
        {
            MoveAmin.ChangeSprite_Push();
        }
        else
        {
            MoveAmin.ChangeSprite();
            
        }
        Invoke("StopAmin", AnimationTime);
    }

    void StopAmin()
    {
        animFlag=true;
    }

    public void SetGravityFrag(bool flag)
    {
        GravityFlag = flag;
    }

    public bool GetGravityFlag() 
    {
        return GravityFlag;
    }

}