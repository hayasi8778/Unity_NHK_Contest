using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rb;//2D�ɂ���̂ŕύX
    public float JumpPower = 10.0f; // �W�����v��
    public float gravity = 20.0f;

    private HashSet<GameObject> validGround = new HashSet<GameObject>(); // �L����"ground"��ǐ�
    private const float NormalThreshold = 0.7f; // �@�������̂������l

    private bool JumpFlag = false;

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
        //if (Input.GetKeyDown(KeyCode.Space) && JumpFlag)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpPower); // AddForce �̑���� velocity �𒼐ڕύX
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(jumpSE); // �W�����v����SE�Đ�
            }
            

        }


        if (validGround.Count == 0)
        { // �󒆂ɂ���Ƃ�
            //�������̗�(�d��)�̉��Z
            rb.linearVelocity += Vector2.down * gravity * Time.deltaTime; // Rigidbody2D�ł�AddForce���velocity�̒������K��
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") ||
         collision.gameObject.CompareTag("Object1") ||
         collision.gameObject.CompareTag("Object2") ||
         collision.gameObject.CompareTag("Object3")
        )
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // �Փ˖ʂ��قڏ�����̏ꍇ�̂݃J�E���g
                if (Vector2.Dot(contact.normal, Vector2.up) > NormalThreshold)
                {
                    if (validGround.Count < 1)
                    {
                        validGround.Add(collision.gameObject);
                        break; // ��x�����𖞂������烋�[�v�𔲂���
                    }
                    else
                    {
                        break; // ��x�����𖞂������烋�[�v�𔲂���
                    }
                }
            }

        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") ||
         collision.gameObject.CompareTag("Object1") ||
         collision.gameObject.CompareTag("Object2") ||
         collision.gameObject.CompareTag("Object3"))
        {
            if(validGround.Count > 0)
            {
                validGround.Remove(collision.gameObject); // �ڐG�������Ƀ��X�g����폜
            }
            
        }
    }
}
