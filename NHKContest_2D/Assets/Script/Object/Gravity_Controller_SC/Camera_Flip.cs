using Unity.Cinemachine;
using UnityEngine;

public class Camera_Flip : MonoBehaviour
{
    public CinemachineCamera virtualCamera; // カメラの参照
    private bool isFlipped = false;

    void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera が設定されていません！");
        }
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            FlipCamera();
        }
        */
    }
    public void FlipCamera()
    {
        /*
        if (virtualCamera != null)
        {
            isFlipped = !isFlipped;
            float targetRotation = isFlipped ? 180f : 0f;

            // カメラを回転させるコルーチンを開始
            StartCoroutine(RotateCamera(targetRotation, 1.0f));
        }
        */

        isFlipped = !isFlipped;
        float targetRotation = isFlipped ? 180f : 0f;

        Debug.Log($"Flip Start: isFlipped={isFlipped}");

        StartCoroutine(RotateCamera( 1.0f)); // 1.0秒かけて回転


    }

    private System.Collections.IEnumerator RotateCamera(float targetRotation, float duration)
    {
        //float elapsedTime = 0f;
        //float startRotation = virtualCamera.transform.rotation.eulerAngles.z;

        /*
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newRotation = Mathf.Lerp(startRotation, targetRotation, elapsedTime / duration);
            virtualCamera.transform.rotation = Quaternion.Euler(0, 0, newRotation);
            yield return null;
        }

        virtualCamera.transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        */

        float elapsedTime = 0f;

        // **現在のカメラの回転を Quaternion から取得**
        float startRotation = virtualCamera.transform.rotation.eulerAngles.z;

        Debug.Log($"Start Rotation: {startRotation}, Target Rotation: {targetRotation}");

        // **回転が完全に止まってしまうのを防ぐ**
        if (Mathf.Abs(startRotation - targetRotation) < 0.1f)
        {
            Debug.Log("Rotation already close to target, forcing small offset.");
            startRotation = targetRotation - 0.1f; // わずかにズラして動作を保証
        }

        Time.timeScale = 0f;
        Debug.Log("Time stopped, starting rotation.");

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float newRotation = Mathf.Lerp(startRotation, targetRotation, elapsedTime / duration);
            virtualCamera.transform.rotation = Quaternion.Euler(0, 0, newRotation);

            Debug.Log($"Rotation Progress: {newRotation}");
            yield return null;
        }

        virtualCamera.transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        Debug.Log($"Final Rotation: {virtualCamera.transform.rotation.eulerAngles.z}");

        Time.timeScale = 1f;
        Debug.LogWarning("Time resumed.");

    }
    private System.Collections.IEnumerator RotateCamera(float duration)
    {
        float elapsedTime = 0f;

        // **現在のカメラの回転を取得**
        float startRotation = virtualCamera.transform.rotation.eulerAngles.z;

        // **ターゲットの角度を動的に決定**
        float targetRotation = (Mathf.Approximately(startRotation, 180f)) ? 0f : 180f;

        Debug.Log($"Start Rotation: {startRotation}, Target Rotation: {targetRotation}");

        Time.timeScale = 0f; // **時間を停止**
        Debug.Log("Time stopped, starting rotation.");

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float newRotation = Mathf.Lerp(startRotation, targetRotation, elapsedTime / duration);
            virtualCamera.transform.rotation = Quaternion.Euler(0, 0, newRotation);

            Debug.Log($"Rotation Progress: {newRotation}");
            yield return null;
        }

        virtualCamera.transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        Debug.Log($"Final Rotation: {virtualCamera.transform.rotation.eulerAngles.z}");

        Time.timeScale = 1f; // **時間を元に戻す**
        Debug.Log("Time resumed.");
    }


}
