using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    // 参照したいオブジェクトを格納する変数
    [SerializeField]
    private GameObject playerObject;

    void Update()
    {

        // カメラの位置を取得するための変数
        Vector3 cameraPosition = transform.position;

        // targetObjectが設定されている場合、そのpositionを取得
        if (playerObject != null)
        {
            Vector3 targetPosition = playerObject.transform.position;
            cameraPosition.x = targetPosition.x;
            transform.position = cameraPosition;
        }
    }
}

