using UnityEngine;
using UnityEngine.UI;

public class TimeSlider_Gravity : TimeSliderObject_Base
{

    public GameObject[] replacementPrefabs;

    public int replacementIndex = 0;
    public Slider slider; //スライダー
    public GameObject Imagechanger;

    // スプライト切り替え用
    public Sprite normalSprite;
    public Sprite flippedSprite;
    private SpriteRenderer spriteRenderer;

    private int Currentnum = 0;//配列の何番目にいるか

    //重力の反転フラグ
    private bool isGravityFlipped = false;

    private bool Old_isGravityFlipped = false;

    //重力反転後に触るボタンかどうか
    public bool ButtonRotatFlag = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite(); // 初期状態に応じたスプライトを表示
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // スペースキーで重力反転
        {
            isGravityFlipped = !isGravityFlipped;
            Physics2D.gravity = new Vector2(0, isGravityFlipped ? -9.8f : 9.8f);
        }

        if (Old_isGravityFlipped != isGravityFlipped) 
        {
            UpdateSprite();
        }
        Old_isGravityFlipped = isGravityFlipped;

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーとの接触を判定
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーのコライダーを取得(実行中にプレイヤー変わるから)
            Collider2D playerCollider = collision.collider;

            // このオブジェクトのコライダー（ボタン）を取得
            Collider2D buttonCollider = GetComponent<Collider2D>();

            if (playerCollider != null && buttonCollider != null)//NULLチェック
            {
                // **進入方向をチェック (プレイヤーが上下どっちから来ているか)**
                bool isComingFromAbove = false;

                if (!ButtonRotatFlag) 
                {
                    isComingFromAbove = collision.relativeVelocity.y < 0;
                }
                else
                {
                    isComingFromAbove = collision.relativeVelocity.y > 0.3;
                }

                 PlayerJump playerJump = collision.gameObject.GetComponent<PlayerJump>();

               

                if (/*playerBottom <= buttonTop &&*/ isComingFromAbove) // プレイヤーが踏んだ場合のみ反転
                {
                    //カメラの上下反転させる
                    Camera_Flip camera_fl = this.gameObject.GetComponent<Camera_Flip>();

                    camera_fl.FlipCamera();

                    isGravityFlipped = !playerJump.GetGravityFlag();

                    Physics2D.gravity = new Vector2(0, isGravityFlipped ? -9.8f : 9.8f);

                    //UpdateSprite();

                    //Debug.Log($"isGravityFlipped: {isGravityFlipped}");
                    Debug.Log(Physics2D.gravity);

                    playerJump.SetGravityFlag();//重力反転したことをプレイヤーに伝える
                    playerJump.FlipPlayerTexture();

                   
                }

            }
        }
    }

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <returns></returns>
    //スライダー関係の処理
    public override GameObject ReplaceObject()//オブジェクト入れ替え(後ろ)
    {
        Debug.LogWarning("オーバーライドのテスト");

        if (replacementPrefabs == null || replacementPrefabs.Length == 0) return null;
        if (replacementIndex >= replacementPrefabs.Length - 1)
        {
            Debug.LogWarning("最後のオブジェクトなので入れ替えしません");
            return null;
        }

        replacementIndex++;

        Vector3 spawnPosition = transform.position;
        GameObject nextPrefab = replacementPrefabs[replacementIndex];
        GameObject newObj = Instantiate(nextPrefab, spawnPosition, transform.rotation);

        if (newObj == null)
        {
            Debug.LogError("次のライトオブジェクトないよ");
            return null;
        }

        TimeSlider_Gravity newScript = newObj.GetComponent<TimeSlider_Gravity>();
        if (newScript != null)
        {
            newScript.slider = this.slider;
            newScript.replacementPrefabs = this.replacementPrefabs;
            newScript.replacementIndex = this.replacementIndex;
            newScript.Currentnum = this.Currentnum;
        }
        /*
        var counter = slider.GetComponent<SliderTimeCounter>();
        if (counter != null)
        {
            Debug.LogWarning("配列設定" + Currentnum);
            counter.SetCurrentObjects(newObj, Currentnum);
        }
        */



        Destroy(this.gameObject);

        var counter = slider.GetComponent<SliderTimeCounter>();
        if (counter != null)
        {
            Debug.LogWarning("配列設定" + Currentnum);
            counter.SetCurrentObjects(newObj, Currentnum);
        }

        return newObj;

    }

    public override void TryRevertObject()//オブジェクト入れ替え(前)
    {
        //一旦画質の影響を受けないから消す
    }

    public override void ChangeImageQuality(int num)//オブジェクト入れ替え(前)
    {
        //一旦画質の影響を受けないから消す
    }

    public override void SetCurrentnum(int num)
    {
        //配列が設定されたよ
        Debug.LogWarning("配列設定" + num);
        Currentnum = num;
    }

    private void UpdateSprite()//スプライト切り替え
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = isGravityFlipped ? flippedSprite : normalSprite;
        }
    }

}
