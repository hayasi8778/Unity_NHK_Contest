using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;
using System.Collections;


public class PlayerMove : MonoBehaviour
{
    public float movespeed = 1.0f;//移動スピード
    private bool moveFlag = false; //歩行しているかのフラグ
    
    private AudioSource audioSource; //音鳴らすためのコンポーネント
    public AudioClip walkSE;
    private SpriteRenderer renderer; //レンダーを取得する

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
            Debug.Log("Renderer is " + (renderer.enabled ? "enabled" : "disabled"));
        }
        else
        {
            Debug.Log("レンダーないぞ");
        }

        // サウンドの設定　中谷
        audioSource.clip = walkSE;  // 音を設定
        audioSource.loop = true;    // ループするように設定
    }

    // Update is called once per frame
    void Update()
    {

        //角度の都合で移動方向左右反転する
        moveFlag = false; //歩行フラグは毎フレーム切る
        Vector3 scale = transform.localScale;

        //右移動
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(movespeed * Time.deltaTime, 0, 0);

            // ↓ゲーム内時間を止めても振りむいちゃうんで条件追加しました　中谷
            if (Time.timeScale > 0) renderer.flipX = false;

            moveFlag = true;
        }

        //左移動
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-movespeed * Time.deltaTime, 0, 0);

            // ↓ゲーム内時間を止めても振りむいちゃうんで条件追加しました　中谷
            if (Time.timeScale > 0) renderer.flipX = true;

            moveFlag = true;
        }

        if (moveFlag && animFlag)
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

        // 入力状況とゲーム内時間の確認をして音を鳴らすか止める　中谷
        if (moveFlag && Time.timeScale > 0) PlaySound();
        else StopAudio();
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
        MoveAmin.ChangeSprite();
        Invoke("StopAmin", 0.5f);
    }

    void StopAmin()
    {
        animFlag=true;
    }

}