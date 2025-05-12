using UnityEngine;

public class TagBasedCollider : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    // Object3だけはぶつかる（Triggerを通さない）
    //    if (other.CompareTag("Object3"))
    //    {
    //        // ブロック自身のColliderとObject3のColliderの衝突を再有効化
    //        Collider myCol = GetComponent<Collider>();
    //        Collider[] others = other.GetComponentsInChildren<Collider>();
    //        foreach (var oc in others)
    //        {
    //            Physics.IgnoreCollision(myCol, oc, false);
    //        }
    //    }
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    // Object3以外は常にすり抜ける
    //    if (!other.CompareTag("Object3"))
    //    {
    //        Collider myCol = GetComponent<Collider>();
    //        Collider[] others = other.GetComponentsInChildren<Collider>();
    //        foreach (var oc in others)
    //        {
    //            Physics.IgnoreCollision(myCol, oc, true);
    //        }
    //    }
    //}
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
