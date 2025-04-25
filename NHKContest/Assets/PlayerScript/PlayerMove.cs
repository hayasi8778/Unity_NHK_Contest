using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float movespeed = 1.0f;//移動スピード

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //角度の都合で移動方向左右反転する

        //右移動
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-movespeed * Time.deltaTime, 0, 0);
        }

        //左移動
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(movespeed * Time.deltaTime, 0, 0);
        }
    }
}
