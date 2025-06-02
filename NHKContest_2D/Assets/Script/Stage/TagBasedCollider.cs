using UnityEngine;

public class TagBasedCollider : MonoBehaviour
{
    private Collider2D blockCollider;

    void Start()
    {
        blockCollider = GetComponent<Collider2D>();
        blockCollider.isTrigger = true;  // 最初はすり抜け状態
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Object3"))
        {
            blockCollider.isTrigger = false;
            Debug.Log("Object3が接触 → Trigger無効、物理判定ON");
        }
        else
        {
            // Object3以外とは衝突を無効化
            Physics2D.IgnoreCollision(blockCollider, other);
            Debug.Log($"他のオブジェクト（{other.name}）との衝突を無効化");
        }
    }
}
