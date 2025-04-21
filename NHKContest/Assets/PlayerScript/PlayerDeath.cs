using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public string blockTag = "ground"; // ���肵�����^�O���w��
    private Collider[] currentBlockedColliders;

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, Vector3.one * 0.4f, Quaternion.identity);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag(blockTag)) // �^�O���r
            {
                Die();
                break; // 1�ł��q�b�g�����珈���𒆒f
            }
        }
    }

    void Die()
    {
        Debug.Log("���񂾁I");
    }
}
