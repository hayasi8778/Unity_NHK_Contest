using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float movespeed = 1.0f;//�ړ��X�s�[�h

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //�p�x�̓s���ňړ��������E���]����

        //�E�ړ�
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-movespeed * Time.deltaTime, 0, 0);
        }

        //���ړ�
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(movespeed * Time.deltaTime, 0, 0);
        }
    }
}
