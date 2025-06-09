using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rb;
    public float JumpPower = 10.0f; // �W�����v��

    private HashSet<GameObject> validGround = new HashSet<GameObject>(); // �L����"ground"��ǐ�
    private const float NormalThreshold = 0.7f; // �@�������̂������l

    private AudioSource audioSource; //�W�����v����SE�Đ����邽�߂̂��
    public AudioClip jumpSE;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource���Z�b�g
    }

    void Update()
    {
        // �W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && validGround.Count > 0)
        {
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(jumpSE); // �W�����v����SE�Đ�
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") ||
            collision.gameObject.CompareTag("Object1") ||
            collision.gameObject.CompareTag("Object2") ||
            collision.gameObject.CompareTag("Object3"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // �Փ˖ʂ��قڏ�����̏ꍇ�̂݃J�E���g
                if (Vector2.Dot(contact.normal, Vector2.up) > NormalThreshold)
                {
                    validGround.Add(collision.gameObject);
                    break; // ��x�����𖞂������烋�[�v�𔲂���
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") ||
            collision.gameObject.CompareTag("Object1") ||
            collision.gameObject.CompareTag("Object2") ||
            collision.gameObject.CompareTag("Object3"))
        {
            validGround.Remove(collision.gameObject); // �ڐG�������Ƀ��X�g����폜
        }
    }
}
