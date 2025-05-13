using UnityEngine;

public class TagBasedCollider : MonoBehaviour
{
    
    private Collider blockCollider;
    void Start()
    {
        blockCollider = GetComponent<Collider>();
        blockCollider.isTrigger = true;  // �ŏ��͂��蔲�����
    }
    void OnTriggerEnter(Collider other)
    {
        // Object3���G�ꂽ��g���K�[�𖳌��ɂ��ĕ����Փ˗L����
        if (other.CompareTag("Object3"))
        {
            blockCollider.isTrigger = false;
            Debug.Log("Object3���ڐG �� Trigger�����A��������ON");
        }
        else
        {
            // Object3�ȊO�ɂ͏Փ˂𖳌������Ă��蔲���ɂ���
            Physics.IgnoreCollision(blockCollider, other);
            Debug.Log($"���̃I�u�W�F�N�g�i{other.name}�j�Ƃ̏Փ˂𖳌���");
        }
    }
}
