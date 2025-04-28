using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayerScript : MonoBehaviour
{
    // �Q�Ƃ������I�u�W�F�N�g���i�[����ϐ�
    [SerializeField]
    public Transform target;
    public Vector3 offset;

    void LateUpdate()
    {
        if (target != null)//�^�[�Q�b�g���ݒ肳��Ă�Ȃ�
        {
            transform.position = target.position + offset;
        }
    }

    public void SetTarget(Transform newTarget)//�I�u�W�F�N�g�؂�ւ��Ă��p�����邽�߂̊֐�
    {
        target = newTarget;
    }

}

