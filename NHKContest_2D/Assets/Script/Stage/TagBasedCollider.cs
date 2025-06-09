using UnityEngine;

public class TagBasedCollider : MonoBehaviour
{
    private Collider2D blockCollider;

    void Start()
    {
        blockCollider = GetComponent<Collider2D>();
        blockCollider.isTrigger = true;  // �ŏ��͂��蔲�����
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Object3"))
        {
            blockCollider.isTrigger = false;
            Debug.Log("Object3���ڐG �� Trigger�����A��������ON");
        }
        else
        {
            // Object3�ȊO�Ƃ͏Փ˂𖳌���
            Physics2D.IgnoreCollision(blockCollider, other);
            Debug.Log($"���̃I�u�W�F�N�g�i{other.name}�j�Ƃ̏Փ˂𖳌���");
        }
    }
}
