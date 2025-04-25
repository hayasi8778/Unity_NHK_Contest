using UnityEngine;

public class Billboard : MonoBehaviour
{
    //板ポリをカメラの方に向けるためのプログラム
    public Vector3 rotationOffset; // 追加したい角度（例：Y軸に30度なら (0, 30, 0)）

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera == null) return;

        // カメラの方向を向く回転を計算（正面を向かせたいので引き算）
        Quaternion lookRotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);

        // オフセット回転を加える
        Quaternion offsetRotation = Quaternion.Euler(rotationOffset);

        // 合成して設定
        transform.rotation = lookRotation * offsetRotation;
    }
}
