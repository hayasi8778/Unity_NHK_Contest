using UnityEngine;
using System.Collections;

public class One_Way_PratForm : MonoBehaviour
{
    public Collider2D platformCollider; // 足場のコライダー
    public float passThroughTime = 1.5f; // すり抜けられる時間
    private float lastPressTime = 0f;
    private int pressCount = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //下から上にすり抜ける処理
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) // 上キー or ジャンプキーが押された
        {
            // プレイヤーが足場の下にいるか確認
            if (IsPlayerBelowPlatform())
            {
                PassThroughPlatform();
            }
        }


        //キーを２回押してすり抜ける処理
        if (Input.GetKeyDown(KeyCode.DownArrow)) // 下キーが押されたか確認
        {
            float currentTime = Time.time;

            // 素早く2回押したか確認
            if (currentTime - lastPressTime < 0.3f)
            {
                pressCount++;
            }
            else
            {
                pressCount = 1; // リセット
            }

            lastPressTime = currentTime;

            if (pressCount >= 2)
            {
                PassThroughPlatform();
            }
        }

    }

    bool IsPlayerBelowPlatform()
    {
        // プレイヤーの位置を取得
        GameObject player = GameObject.FindWithTag("Player"); // プレイヤーのタグを設定しておく
        if (player == null) return false;

        return player.transform.position.y < platformCollider.bounds.min.y; // プレイヤーのY座標が足場の下か
    }

    void PassThroughPlatform()
    {
        platformCollider.enabled = false; // コライダーを無効化（すり抜け可能）
        Invoke("RestorePlatform", passThroughTime); // 指定時間後にコライダーを戻す
    }

    void RestorePlatform()
    {
        platformCollider.enabled = true; // コライダーを復活させる
    }


}
