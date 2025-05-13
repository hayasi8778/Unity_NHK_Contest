using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody rb;
    public float JumpPower = 10.0f; // �W�����v��
    public float gravity = 20.0f;

    private HashSet<GameObject> validGround = new HashSet<GameObject>(); // �L����"ground"��ǐ�
    private const float NormalThreshold = 0.7f; // �@�������̂������l

    private AudioSource audioSource; //�W�����v����SE�Đ����邽�߂̂��
    public AudioClip jumpSE;


    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //AudioSource���Z�b�g

    }

    void Update()
    {
        // �W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && validGround.Count > 0)
        {
            rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(jumpSE); // �W�����v����SE�Đ�
            }
            

        }


        if (validGround.Count == 0)
        { // �󒆂ɂ���Ƃ�
                rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration); // �����������̗͂�������
            
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") ||
         collision.gameObject.CompareTag("object1") ||
         collision.gameObject.CompareTag("Object2") ||
         collision.gameObject.CompareTag("Object3")
        )
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                // �Փ˖ʂ��قڏ�����̏ꍇ�̂݃J�E���g
                if (Vector3.Dot(contact.normal, Vector3.up) > NormalThreshold)
                {
                    validGround.Add(collision.gameObject);
                    break; // ��x�����𖞂������烋�[�v�𔲂���
                }
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") ||
         collision.gameObject.CompareTag("object1") ||
         collision.gameObject.CompareTag("Object2") ||
         collision.gameObject.CompareTag("Object3"))
        {
            validGround.Remove(collision.gameObject); // �ڐG�������Ƀ��X�g����폜
        }
    }
}
