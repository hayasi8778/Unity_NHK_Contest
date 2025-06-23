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

    public float AnimationTime = 0.5f;//�A�j���[�V�����̑҂�����
    public float JumpToTime = 0.5f;//�W�����v�A�j���[�V�����̋�����

    private bool GravityFlag = true;//�d�̓t���O(true�Ő���false�Ŕ��])

     private bool animFlag = true; //�A�j���[�V�����̃t���O
    private bool P_jumpFlag = false; //�A�j���[�V�����̃t���O

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource���Z�b�g
    }

    [System.Obsolete]
    void Update()
    {
        // �W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && validGround.Count > 0)
        {
            if (GravityFlag)
            {
                rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(jumpSE); // �W�����v����SE�Đ�
                }
            }
            else
            {
                //Debug.Log("���]�W�����v");
                rb.AddForce(Vector2.down * JumpPower, ForceMode2D.Impulse);
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(jumpSE); // �W�����v����SE�Đ�
                }
            }

            //�W�����v�̃A�j���[�V�����t���Oon
            JumpFlagON();
            
        }

        if(!(validGround.Count > 0))//�����ɂ��������Ă��Ȃ��Ȃ�
        {
            if (animFlag && P_jumpFlag)
            {
                Debug.Log("�W�����v�A�j���[�V����");
                PlayJump();//�W�����v�A�j���[�V��������
            }
           
        }

        //animFlag = false;

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
                if (GravityFlag)//�d�͂̌����Ő؂�ւ�
                {
                    //���d��(�������ɒn�ʂ����邩)
                    // �Փ˖ʂ��قڏ�����̏ꍇ�̂݃J�E���g
                    if (Vector2.Dot(contact.normal, Vector2.up) > NormalThreshold)
                    {
                        validGround.Add(collision.gameObject);
                        break; // ��x�����𖞂������烋�[�v�𔲂���
                    }
                }
                else
                {
                    //���]���(������ɒn�ʂ����邩)
                    if (Vector2.Dot(contact.normal, Vector2.down) > NormalThreshold)
                    {
                        validGround.Add(collision.gameObject);
                        break; // ��x�����𖞂������烋�[�v�𔲂���
                    }
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

    public void SetGravityFlag()//�d�͂̔��]�ɉ����ăt���O��؂�ւ���
    {
        Debug.Log("�d�͔��]�m�F");

        if (GravityFlag)
        {
            GravityFlag = false;
        }
        else
        {
            GravityFlag = true;
        }

        //playermuve�̃t���O�������ɐ؂�ւ���
        PlayerMove playermove = this.gameObject.GetComponent<PlayerMove>();

        playermove.SetGravityFrag(GravityFlag);
    }

    public bool GetGravityFlag()
    {
        return GravityFlag;
    }

    public void SetNewGravityFlag(bool flag)
    {
        Debug.Log("�V�����I�u�W�F�N�g�ɏd�̓t���O�����");
        GravityFlag = flag;

        FlipCheck();
    }

    public void FlipPlayerTexture()
    {
        Vector3 scale = transform.localScale;
        scale.y *= -1; // Y�������ɔ��]
        transform.localScale = scale;
    }

    public void FlipCheck()//�㉺�؂�ւ��Ă��邩�𒲂ׂ�
    {
        if (!GravityFlag)
        {
            Vector3 scale = transform.localScale;
            scale.y *= -1; // Y�������ɔ��]
            transform.localScale = scale;
        }
    }

    private void PlayJump()
    {
        animFlag = false;

        //�I�u�W�F�N�g�p������
        PlayerMoveAmin JumpAmin = this.GetComponent<PlayerMoveAmin>();

        JumpAmin.ChangeSprite_Push();

        Invoke("StopAmin", AnimationTime);

    }
    void StopAmin()
    {
        animFlag = true;
    }

    private void JumpFlagON()
    {
        P_jumpFlag = true;

        //Debug.Log("�W�����v�A�j���[�V����");

        Invoke("JumpFlagOFF", JumpToTime);

    }

    private void JumpFlagOFF()
    {
        P_jumpFlag = false;

    }

}
