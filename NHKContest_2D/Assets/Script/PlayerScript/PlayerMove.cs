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

    public PlayerMoveAmin MoveAmin; //歩行時のアニメーション
    bool animFlag = true;//アニメーション中かのフラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSourceを取得

        MoveAmin = GetComponent<PlayerMoveAmin>();

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

    // Update is called once per frame
    void Update()
    {

        //角度の都合で移動方向左右反転する

        
        muveFlag = false; //歩行フラグは毎フレーム切る
        

        Vector3 scale = transform.localScale;

       



        //右移動
        if (Input.GetKey(KeyCode.D))
        {


            transform.Translate(movespeed * Time.deltaTime, 0, 0);

            

            renderer.flipX = false;

            muveFlag = true;

            if (!audioSource.isPlaying) // 音が再生中じゃないなら
            {
                PlaySound(); //SE再生
            }

        }

        //左移動
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-movespeed * Time.deltaTime, 0, 0);


            renderer.flipX = true;

            muveFlag = true;

            if (!audioSource.isPlaying) // 再生中でないなら
            {
                PlaySound();

            }

        }

        if (muveFlag && animFlag)
        {

            PlayAnim();
            /*
            if (!videoPlayer.isPlaying)
            {

                videoPlayer.Play(); // キー入力があれば再生

            }
            */
        }
        else
        {
            //Debug.LogError("ループ停止処理");
            /*
            if (videoPlayer.time >= videoPlayer.length)
            {
                videoPlayer.time = 0; // 再生位置をリセット

                videoPlayer.Stop(); // 動画が最後まで再生されたら停止
            }
            */
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
        MoveAmin.ChangeSprite();
        Invoke("StopAmin", 0.5f);
    }

    void StopAmin()
    {
        animFlag=true;
    }

}