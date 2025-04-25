using UnityEngine;

public class Billboard : MonoBehaviour
{
    //�|�����J�����̕��Ɍ����邽�߂̃v���O����
    public Vector3 rotationOffset; // �ǉ��������p�x�i��FY����30�x�Ȃ� (0, 30, 0)�j

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera == null) return;

        // �J�����̕�����������]���v�Z�i���ʂ������������̂ň����Z�j
        Quaternion lookRotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);

        // �I�t�Z�b�g��]��������
        Quaternion offsetRotation = Quaternion.Euler(rotationOffset);

        // �������Đݒ�
        transform.rotation = lookRotation * offsetRotation;
    }
}
