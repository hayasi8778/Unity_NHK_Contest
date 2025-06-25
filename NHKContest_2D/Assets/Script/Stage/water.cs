using UnityEngine;

public class WaterScript : MonoBehaviour
{
    // 接触対象のオブジェクトが持つタグを指定（Inspectorから変更可能）
    public string blockTag = "Block";
    public string playerTag = "Player";

    // このオブジェクトのRendererとColliderの参照を保持
    private Renderer objRenderer;

    [Header("水流から2つ目のブロックか")]
    public bool second_Block = false;
    [Header("重力反転時に描画するか")]
    public bool Flip = false;

    [Header("重力反転ギミックがある場合、PlayerObjectの参照を設定")]
    public GameObject PlayerObject;
    public bool isGravity = true;
    public bool wasGravity = true;

    [Header("水流の一つ手前のブロックの参照を設定")]
    // 水流の一つ手前のブロックの参照を保持
    [SerializeField] private GameObject previousBlock;

    // ブロックとの接触フラグ
    bool isBlockInWater = false;

    // デルタ時間の保存
    private float wasDeltaTime;

    // 押し戻す力
    public float forceStrength = 500f;

    void Start()
    {
        // 同じオブジェクトに付いているRendererとColliderを取得
        objRenderer = GetComponent<Renderer>();

        if (!Flip) {
            // Rendererを有効にする
            if (objRenderer != null) objRenderer.enabled = true;
        } else {
            // Rendererを無効にする
            if (objRenderer != null) objRenderer.enabled = false;
        }
    }

    void Update()
    {
        // 重力の反転状況を取得
        if (PlayerObject != null) isGravity = PlayerObject.GetComponent<PlayerMove>().GetGravityFlag();

        if (!isBlockInWater && !second_Block)   // 水源から二番目のブロック以外
        {
            if (previousBlock != null)
            {
                // 水流の一つ手前のブロックのRendererが有効なら
                if (previousBlock.GetComponent<Renderer>().enabled)
                {
                    // Rendererを有効にする
                    if (objRenderer != null) objRenderer.enabled = true;
                }
                else
                {
                    // Rendererを無効にする
                    if (objRenderer != null) objRenderer.enabled = false;
                }
            }
            
        }
        else if (!isBlockInWater && second_Block)   // 水源から二番目のブロック
        {
            if (isGravity != wasGravity) // 重力状況が変化した際
            {
                objRenderer.enabled = !objRenderer.enabled; // 反転する
            }
        }

        wasGravity = isGravity; // 前回の重力反転状態を保存
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 接触したオブジェクトがブロックタグを持っているか確認
        if (other.gameObject.CompareTag(blockTag))
        {
            // Rendererを無効にする
            if (objRenderer != null) objRenderer.enabled = false;

            // ブロックとの接触フラグを立てる
            isBlockInWater = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // 接触したオブジェクトがプレイヤータグを持っているか確認
        if (other.CompareTag(playerTag) && !isBlockInWater)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // プレイヤーを押し戻す
                if (objRenderer.enabled)//レンダラーが有効なら(水オブジェクトとしてシーンに登場しているなら)
                {
                    Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                    rb.AddForce(pushDirection * forceStrength);
                }

            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 接触が終了したオブジェクトがブロックタグを持っているか確認
        if (other.gameObject.CompareTag(blockTag))
        {
            // Rendererを有効にする
            if (objRenderer != null) objRenderer.enabled = true;

            // ブロックとの接触フラグを下ろす
            isBlockInWater = false;
        }
    }
}
