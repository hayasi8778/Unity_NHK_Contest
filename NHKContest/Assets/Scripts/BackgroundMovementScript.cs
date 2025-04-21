using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundMovementScript : MonoBehaviour
{
    // カメラの移動距離に対する背景の移動距離の倍率
    [SerializeField]
    private float movementMultiplier;
    // カメラオブジェクトの参照
    [SerializeField]
    private GameObject cameraObject;

    void Update()
    {
        Vector3 backgroundPosition = transform.position;
        // カメラの移動距離に移動倍率をかけた値を適用する
        backgroundPosition.x = cameraObject.transform.position.x * movementMultiplier;
        transform.position = backgroundPosition;
    }
}
