using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody rb;
    public float JumpPoewr = 10.0f;//�W�����v��

    public bool JumpFg = false;//�W�����v�t���O
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�W�����v
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpFg == true)//true�̎�
            {
                rb.AddForce(Vector3.up * JumpPoewr, ForceMode.Impulse);
            }
        }
    }

    //�Փ˂����u��
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")//�^�O��guround�̃I�u�W�F�N�g�Ɠ���������
        {
            JumpFg = true;
        }
    }

    //���ꂽ�u��
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")//�^�O��guround�̃I�u�W�F�N�g�Ɠ������Ă��Ȃ��Ƃ�
        {
            JumpFg = false;
        }
    }
}
