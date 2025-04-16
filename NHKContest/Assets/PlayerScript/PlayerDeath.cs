using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public string blockTag = "ground"; // 判定したいタグを指定
    private Collider[] currentBlockedColliders;

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, Vector3.one * 0.4f, Quaternion.identity);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag(blockTag)) // タグを比較
            {
                Die();
                break; // 1つでもヒットしたら処理を中断
            }
        }
    }

    void Die()
    {
        Debug.Log("しんだ！");
    }
}
