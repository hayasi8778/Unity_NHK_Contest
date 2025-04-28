using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayerScript : MonoBehaviour
{
    // 参照したいオブジェクトを格納する変数
    [SerializeField]
    public Transform target;
    public Vector3 offset;

    void LateUpdate()
    {
        if (target != null)//ターゲットが設定されてるなら
        {
            transform.position = target.position + offset;
        }
    }

    public void SetTarget(Transform newTarget)//オブジェクト切り替えても継承するための関数
    {
        target = newTarget;
    }

}

