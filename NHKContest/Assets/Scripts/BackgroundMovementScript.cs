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
    // 直前のカメラ位置
    Vector3 wasCameraPosition;
    // 初期位置
    Vector3 initialPosition;

    void Start()
    {
        // 初期位置を保存
        initialPosition = transform.position;
        wasCameraPosition = cameraObject.transform.position;
    }

    void Update()
    {
        // カメラの移動距離に移動倍率をかけた値を適用する
        initialPosition.x += (cameraObject.transform.position.x - wasCameraPosition.x) * movementMultiplier;
        wasCameraPosition = cameraObject.transform.position;

        // ある程度画面外に出たら反対側に移動させる
        if (initialPosition.x > (transform.lossyScale.x) * 1.5f) {
            initialPosition.x = -(transform.lossyScale.x) * 1.5f;
        }
        if (initialPosition.x < -(transform.lossyScale.x) * 1.5f) {
            initialPosition.x = (transform.lossyScale.x) * 1.5f;
        }

        transform.position = initialPosition;
    }
}
