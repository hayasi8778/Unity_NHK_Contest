using Unity.Cinemachine;
using UnityEngine;

public class Camera_Flip : MonoBehaviour
{
    public CinemachineCamera virtualCamera; // �J�����̎Q��
    private bool isFlipped = false;

    void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera ���ݒ肳��Ă��܂���I");
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

            // �J��������]������R���[�`�����J�n
            StartCoroutine(RotateCamera(targetRotation, 1.0f));
        }
        */

        isFlipped = !isFlipped;
        float targetRotation = isFlipped ? 180f : 0f;

        Debug.Log($"Flip Start: isFlipped={isFlipped}");

        StartCoroutine(RotateCamera( 1.0f)); // 1.0�b�����ĉ�]


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

        // **���݂̃J�����̉�]�� Quaternion ����擾**
        float startRotation = virtualCamera.transform.rotation.eulerAngles.z;

        Debug.Log($"Start Rotation: {startRotation}, Target Rotation: {targetRotation}");

        // **��]�����S�Ɏ~�܂��Ă��܂��̂�h��**
        if (Mathf.Abs(startRotation - targetRotation) < 0.1f)
        {
            Debug.Log("Rotation already close to target, forcing small offset.");
            startRotation = targetRotation - 0.1f; // �킸���ɃY�����ē����ۏ�
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

        // **���݂̃J�����̉�]���擾**
        float startRotation = virtualCamera.transform.rotation.eulerAngles.z;

        // **�^�[�Q�b�g�̊p�x�𓮓I�Ɍ���**
        float targetRotation = (Mathf.Approximately(startRotation, 180f)) ? 0f : 180f;

        Debug.Log($"Start Rotation: {startRotation}, Target Rotation: {targetRotation}");

        Time.timeScale = 0f; // **���Ԃ��~**
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

        Time.timeScale = 1f; // **���Ԃ����ɖ߂�**
        Debug.Log("Time resumed.");
    }


}
