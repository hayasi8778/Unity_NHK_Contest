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

    }

    private bool canMove = true; // 移動許可フラグ
    public void SetCanMove(bool flag)
    {
        canMove = flag;
    }

    // Update is called once per frame
    void Update()
    {

        //角度の都合で移動方向左右反転する

        
        muveFlag = false; //歩行フラグは毎フレーム切る
        
        Vector3 scale = transform.localScale;


        //右移動
        if (Input.GetKey(KeyCode.D))
        {
            //左右切り替え
            if (GravityFlag) //画面回るから操作方向切り替える
            {
                transform.Translate(movespeed * Time.deltaTime, 0, 0);

                renderer.flipX = false;
            }
            else
            {
                transform.Translate(-movespeed * Time.deltaTime, 0, 0);

                renderer.flipX = true;
            }


                

            muveFlag = true;

            if (!audioSource.isPlaying) // 音が再生中じゃないなら
            {
                PlaySound(); //SE再生
            }

        }

        //左移動
        if (Input.GetKey(KeyCode.A))
        {
            if (GravityFlag)
            {
                //左右切り替え
                transform.Translate(-movespeed * Time.deltaTime, 0, 0);

                renderer.flipX = true;
            }
            else
            {
                transform.Translate(movespeed * Time.deltaTime, 0, 0);

                renderer.flipX = false;
            }



                

            muveFlag = true;

            if (!audioSource.isPlaying) // 再生中でないなら
            {
                PlaySound();

            }

        }

        if (muveFlag && animFlag)
        {

            PlayAnim();
        }

    }

    void PlaySound()
    {
        audioSource.PlayOneShot(walkSE);
        Invoke("StopAudio", 0.5f);
    }


    void StopAudio()
    {
        audioSource.Stop();
    }

    void PlayAnim()
    {
        animFlag = false;
        //オブジェクト継承して
        PlayerMoveAmin MoveAmin = this.GetComponent<PlayerMoveAmin>();

        MoveAmin.ChangeSprite();
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

}