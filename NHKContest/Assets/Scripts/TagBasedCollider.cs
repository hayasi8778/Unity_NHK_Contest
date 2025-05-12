using UnityEngine;

public class TagBasedCollider : MonoBehaviour
{
    
    private Collider blockCollider;
    void Start()
    {
        blockCollider = GetComponent<Collider>();
        blockCollider.isTrigger = true;  // 最初はすり抜け状態
    }
    void OnTriggerEnter(Collider other)
    {
        // Object3が触れたらトリガーを無効にして物理衝突有効化
        if (other.CompareTag("Object3"))
        {
            blockCollider.isTrigger = false;
            Debug.Log("Object3が接触 → Trigger無効、物理判定ON");
        }
        else
        {
            // Object3以外には衝突を無効化してすり抜けにする
            Physics.IgnoreCollision(blockCollider, other);
            Debug.Log($"他のオブジェクト（{other.name}）との衝突を無効化");
        }
    }
}
