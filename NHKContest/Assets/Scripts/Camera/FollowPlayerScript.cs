using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    // �Q�Ƃ������I�u�W�F�N�g���i�[����ϐ�
    [SerializeField]
    private GameObject playerObject;

    void Update()
    {

        // �J�����̈ʒu���擾���邽�߂̕ϐ�
        Vector3 cameraPosition = transform.position;

        // targetObject���ݒ肳��Ă���ꍇ�A����position���擾
        if (playerObject != null)
        {
            Vector3 targetPosition = playerObject.transform.position;
            cameraPosition.x = targetPosition.x;
            transform.position = cameraPosition;
        }
    }
}

