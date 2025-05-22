using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;
using System.Collections;


public class PlayerMove : MonoBehaviour
{
    public float movespeed = 1.0f;//移動スピード
    public VideoPlayer videoPlayer;
    private bool muveFlag = true; //歩行しているかのフラグ
    public bool isFacingRight = true; //向き
    private bool OldisFacing = true; //1フレーム前の向き
    private AudioSource audioSource; //音鳴らすためのコンポーネント
    public AudioClip walkSE;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>(); // AudioSourceを取得
    }

    // Update is called once per frame
    void Update()
    {

        //角度の都合で移動方向左右反転する

        muveFlag = false; //歩行フラグは毎フレーム切る

        Vector3 scale = transform.localScale;

        OldisFacing = isFacingRight;//1フレーム前の分をとる



        //右移動
        if (Input.GetKey(KeyCode.D))
        {


            transform.Translate(movespeed * Time.deltaTime, 0, 0);

            isFacingRight = true;
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

            isFacingRight = false;

            muveFlag = true;

            if (!audioSource.isPlaying) // 再生中でないなら
            {
                PlaySound();

            }

        }



        if (OldisFacing != isFacingRight)
        {

            // 左右反転（Xスケールを切り替えて対応）
            scale.x = isFacingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        if (videoPlayer == null)
        {
            Debug.Log("動画ないからアップデートここまで");
            return;
        }

        if (muveFlag)
        {
            if (!videoPlayer.isPlaying)
            {

                videoPlayer.Play(); // キー入力があれば再生

            }
        }
        else
        {
            //Debug.LogError("ループ停止処理");

            if (videoPlayer.time >= videoPlayer.length)
            {
                videoPlayer.time = 0; // 再生位置をリセット

                videoPlayer.Stop(); // 動画が最後まで再生されたら停止
            }
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


}