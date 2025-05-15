using UnityEngine;
using System.Collections.Generic; //HashSetを使うため


public class RockMoveSe : MonoBehaviour
{

    public AudioSource audioSource;
    public Rigidbody rb; //物理オブジェクト

    private float speedThreshold = 0.1f; //速度のしきい値
    private HashSet<GameObject> touchingGroundObjects = new HashSet<GameObject>(); //接触しているGroundオブジェクトを管理




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //NULLチェック
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSourceがないよ");
        }
        if (rb == null)
        {
            Debug.LogWarning("Rigidbodyがないよ");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //速度一定以上かつ地面に接触している
        if (rb.linearVelocity.magnitude > speedThreshold && touchingGroundObjects.Count > 0)
        {

            if (!audioSource.isPlaying) //既に再生中じゃないなら再生
            {
                audioSource.Play();
            }
        }
        else // ��~�����特���~�߂�
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            touchingGroundObjects.Add(collision.gameObject); //Groundタグのオブジェクトを追加する
            //Debug.LogError("地面に接触しました！");//Debug.LogError("地面に接触しました！");

        }
        
    }

    


    void OnCollisionExit(Collision collision)
    {
        if (touchingGroundObjects.Contains(collision.gameObject))
        {
            touchingGroundObjects.Remove(collision.gameObject); //接触解除されたら削除
            //Debug.LogError("地面から離れた！");
        }
    }

}
