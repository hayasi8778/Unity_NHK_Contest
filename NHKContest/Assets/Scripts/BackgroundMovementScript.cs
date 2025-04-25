using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundMovementScript : MonoBehaviour
{
    // �J�����̈ړ������ɑ΂���w�i�̈ړ������̔{��
    [SerializeField]
    private float movementMultiplier;
    // �J�����I�u�W�F�N�g�̎Q��
    [SerializeField]
    private GameObject cameraObject;
    // ���O�̃J�����ʒu
    Vector3 wasCameraPosition;
    // �����ʒu
    Vector3 initialPosition;

    void Start()
    {
        // �����ʒu��ۑ�
        initialPosition = transform.position;
        wasCameraPosition = cameraObject.transform.position;
    }

    void Update()
    {
        // �J�����̈ړ������Ɉړ��{�����������l��K�p����
        initialPosition.x += (cameraObject.transform.position.x - wasCameraPosition.x) * movementMultiplier;
        wasCameraPosition = cameraObject.transform.position;

        // ������x��ʊO�ɏo���甽�Α��Ɉړ�������
        if (initialPosition.x > (transform.lossyScale.x) * 1.5f) {
            initialPosition.x = -(transform.lossyScale.x) * 1.5f;
        }
        if (initialPosition.x < -(transform.lossyScale.x) * 1.5f) {
            initialPosition.x = (transform.lossyScale.x) * 1.5f;
        }

        transform.position = initialPosition;
    }
}
