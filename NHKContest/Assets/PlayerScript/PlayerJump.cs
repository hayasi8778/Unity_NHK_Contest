using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody rb;
    public float JumpPoewr = 10.0f;//ジャンプ力

    public bool JumpFg = false;//ジャンプフラグ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpFg == true)//trueの時
            {
                rb.AddForce(Vector3.up * JumpPoewr, ForceMode.Impulse);
            }
        }
    }

    //衝突した瞬間
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")//タグがguroundのオブジェクトと当たった時
        {
            JumpFg = true;
        }
    }

    //離れた瞬間
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")//タグがguroundのオブジェクトと当たっていないとき
        {
            JumpFg = false;
        }
    }
}
