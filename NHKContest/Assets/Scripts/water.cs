using UnityEngine;

public class WaterScript : MonoBehaviour
{
    // 接触対象のオブジェクトが持つタグを指定（Inspectorから変更可能）
    public string blockTag = "Block";
    public string playerTag = "Player";

    // このオブジェクトのRendererとColliderの参照を保持
    private Renderer objRenderer;
    private Collider objCollider;

    // 水流の一つ手前のブロックの参照を保持
    [SerializeField] private GameObject previousBlock;

    // ブロックとの接触フラグ
    bool isBlockInWater = false;

    // デルタ時間の保存
    private float wasDeltaTime;

    // 押し戻す力
    public float forceStrength = 500f; 

    void Start() {
        // 同じオブジェクトに付いているRendererとColliderを取得
        objRenderer = GetComponent<Renderer>();
        objCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (!isBlockInWater) {
            // 水流の一つ手前のブロックのRendererが有効なら
            if (previousBlock.GetComponent<Renderer>().enabled) {
                // Rendererを有効にする
                if (objRenderer != null) objRenderer.enabled = true;
            } else {
                // Rendererを無効にする
                if (objRenderer != null) objRenderer.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        // 接触したオブジェクトがブロックタグを持っているか確認
        if (other.gameObject.CompareTag(blockTag)) {
            // Rendererを無効にする
            if (objRenderer != null) objRenderer.enabled = false;

            // ブロックとの接触フラグを立てる
            isBlockInWater = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        // 接触したオブジェクトがプレイヤータグを持っているか確認
        if (other.CompareTag(playerTag) && !isBlockInWater) {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) {
                // プレイヤーを押し戻す
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                rb.AddForce(pushDirection * forceStrength);
            }
        }
    }

    void OnTriggerExit(Collider other) 
    {
        // 接触が終了したオブジェクトがブロックタグを持っているか確認
        if (other.gameObject.CompareTag(blockTag)) {
            // Rendererを有効にする
            if (objRenderer != null) objRenderer.enabled = true;

            // ブロックとの接触フラグを下ろす
            isBlockInWater = false;
        }
    }
}
