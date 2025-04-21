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

    void Update()
    {
        Vector3 backgroundPosition = transform.position;
        // �J�����̈ړ������Ɉړ��{�����������l��K�p����
        backgroundPosition.x = cameraObject.transform.position.x * movementMultiplier;
        transform.position = backgroundPosition;
    }
}
