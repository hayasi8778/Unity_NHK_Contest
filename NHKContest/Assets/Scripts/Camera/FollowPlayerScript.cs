using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayerScript : MonoBehaviour
{
    // �Q�Ƃ������I�u�W�F�N�g���i�[����ϐ�
    [SerializeField]
    public Transform target;
    public Vector3 offset;

    private bool isFollowing = true; //�J�����̒Ǐ]�t���O

    void LateUpdate()
    {
        if (isFollowing && target != null)
        {
            // ��Ԉړ��ŃJ�����Ɋ��炩���Ƒ��x����������
            Vector3 desiredPosition = target.position + offset;
            //Lerp�g���ă��[�V�����u���[�ɑΉ��ł���悤�ɂ���
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10f);
        }
    }

    public void SetTarget(Transform newTarget)//�I�u�W�F�N�g�؂�ւ��Ă��p�����邽�߂̊֐�
    {
        target = newTarget;
        PauseFollowing(0.5f);//0.5�b�ԃJ�����̒Ǐ]�~�߂�
    }

    public void PauseFollowing(float pauseDuration)//�w��b���ԃJ�����Ǐ]�~�߂�֐�
    {
        StartCoroutine(PauseCoroutine(pauseDuration));
    }

    private System.Collections.IEnumerator PauseCoroutine(float pauseDuration)
    {
        isFollowing = false;
        yield return new WaitForSeconds(pauseDuration);//�w��b���ԑ҂����������鏈��
        isFollowing = true;
    }

}

