using UnityEngine;

public class TagBasedCollider : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    // Object3�����͂Ԃ���iTrigger��ʂ��Ȃ��j
    //    if (other.CompareTag("Object3"))
    //    {
    //        // �u���b�N���g��Collider��Object3��Collider�̏Փ˂��ėL����
    //        Collider myCol = GetComponent<Collider>();
    //        Collider[] others = other.GetComponentsInChildren<Collider>();
    //        foreach (var oc in others)
    //        {
    //            Physics.IgnoreCollision(myCol, oc, false);
    //        }
    //    }
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    // Object3�ȊO�͏�ɂ��蔲����
    //    if (!other.CompareTag("Object3"))
    //    {
    //        Collider myCol = GetComponent<Collider>();
    //        Collider[] others = other.GetComponentsInChildren<Collider>();
    //        foreach (var oc in others)
    //        {
    //            Physics.IgnoreCollision(myCol, oc, true);
    //        }
    //    }
    //}
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
