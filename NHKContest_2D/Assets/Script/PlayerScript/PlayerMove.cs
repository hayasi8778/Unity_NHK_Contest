using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float movespeed = 1.0f;//�ړ��X�s�[�h
    private bool muveFlag = false; //���s���Ă��邩�̃t���O
    
    private new SpriteRenderer renderer; //�����_�[���擾����

    //public PlayerMoveAmin MoveAmin; //���s���̃A�j���[�V����
    bool animFlag = true;//�A�j���[�V���������̃t���O

    public float AnimationTime = 0.5f;

    private bool GravityFlag = true;//�d�̓t���O(true�Ő���false�Ŕ��])

    //�ǂ̐ڐG�t���O
    bool pushFlag = false;
    [SerializeField] private float wallCheckOffset = 0.5f; // �v���C���[�̒��S���獶�E�ɗ�������
    [SerializeField] private float wallCheckDistance = 0.1f;
    [SerializeField] private LayerMask wallLayer; // ���肵�������C���[���w��

    private AudioController audioController; // Audio�̑���R���|�[�l���g�@���J
    enum AudioType { walkSE,jump02 } // ���̎�ށ@���J


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MoveAmin = GetComponent<PlayerMoveAmin>();
        audioController = GetComponent<AudioController>(); // AudioController���擾�@���J
        audioController.Pause((int)AudioType.walkSE); // �v���C���[�̈ړ������ꎞ��~�@�i�������ꎞ��~�ƍĊJ���g���Ĉ�a�����Ȃ����Ă݂悤���j�@���J

        //�v���C���[���`�悳��Ȃ����烌���_�[���L�����m�F����
        renderer = GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            //Debug.Log("Renderer is " + (renderer.enabled ? "enabled" : "disabled"));
        }
        else
        {
            Debug.Log("�����_�[�Ȃ���");
        }
    }

    private bool canMove = true; // �ړ����t���O
    public void SetCanMove(bool flag)
    {
        canMove = flag;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!canMove) return;

        pushFlag = false;

        // �ǂƂ̐ڐG�`�F�b�N
        CheckPush(); // �� �ǉ�

        //if (pushFlag) Debug.Log("push����on");

        //�p�x�̓s���ňړ��������E���]����


        muveFlag = false; //���s�t���O�͖��t���[���؂�
        
        Vector3 scale = transform.localScale;


        //�E�ړ�
        if (Input.GetKey(KeyCode.D))
        {
            //���E�؂�ւ�
            if (GravityFlag) //��ʉ�邩�瑀������؂�ւ���  ��
            {
                // ���Q�[�������Ԃ��~�߂Ă��U��ނ����Ⴄ��ŏ����ǉ����܂����@���J
                if (Time.timeScale > 0)
                {
                    transform.Translate(movespeed * Time.deltaTime, 0, 0);

                    renderer.flipX = false;
                }
            }
            else
            {
                // ���Q�[�������Ԃ��~�߂Ă��U��ނ����Ⴄ��ŏ����ǉ����܂����@���J
                if (Time.timeScale > 0)
                {
                    
                    transform.Translate(-movespeed * Time.deltaTime, 0, 0);

                    renderer.flipX = true;
                }
            }

            muveFlag = true;
            /*
            if (!audioSource.isPlaying) // �����Đ�������Ȃ��Ȃ�
            {
                PlaySound(); //SE�Đ�
            }*/
        }

        //���ړ�
        if (Input.GetKey(KeyCode.A))
        {
            if (GravityFlag)
            {
                // ���Q�[�������Ԃ��~�߂Ă��U��ނ����Ⴄ��ŏ����ǉ����܂����@���J
                if (Time.timeScale > 0)
                {
                    
                    //���E�؂�ւ�
                    transform.Translate(-movespeed * Time.deltaTime, 0, 0);

                    renderer.flipX = true;
                }
            }
            else
            {
                // ���Q�[�������Ԃ��~�߂Ă��U��ނ����Ⴄ��ŏ����ǉ����܂����@���J
                if (Time.timeScale > 0)
                {
                   
                    transform.Translate(movespeed * Time.deltaTime, 0, 0);

                    renderer.flipX = false;
                }
            }

            muveFlag = true;
            /*
            if (!audioSource.isPlaying) // �Đ����łȂ��Ȃ�
            {
                PlaySound();
            }
            */
        }

        if (muveFlag && animFlag)
        {

            PlayAnim();
        }

        if (muveFlag && Time.timeScale > 0) audioController.UnPause((int)AudioType.walkSE); // ���s�����ĊJ�@���J
        else audioController.Pause((int)AudioType.walkSE); // ���s�����ꎞ��~�@���J
    }

    void CheckPush()
    {
        Vector2 position = transform.position;

        // Ray���o���N�_�����E�ɂ��炷
        Vector2 rightOrigin = position + Vector2.right * wallCheckOffset;
        Vector2 leftOrigin = position + Vector2.left * wallCheckOffset;

        // �e������Ray���΂�
        RaycastHit2D hitRight = Physics2D.Raycast(rightOrigin, Vector2.right, wallCheckDistance, wallLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(leftOrigin, Vector2.left, wallCheckDistance, wallLayer);

        pushFlag = (hitRight.collider != null || hitLeft.collider != null);

        // �f�o�b�O�\���i�V�[���r���[��Ray��������悤�Ɂj
        Debug.DrawRay(rightOrigin, Vector2.right * wallCheckDistance, Color.red);
        Debug.DrawRay(leftOrigin, Vector2.left * wallCheckDistance, Color.red);

    }

    void PlayAnim()
    {
        animFlag = false;
        //�I�u�W�F�N�g�p������
        PlayerMoveAmin MoveAmin = this.GetComponent<PlayerMoveAmin>();

        if (pushFlag)
        {
            MoveAmin.ChangeSprite_Push();
        }
        else
        {
            MoveAmin.ChangeSprite();
        }
        Invoke("StopAmin", AnimationTime);
    }

    void StopAmin()
    {
        animFlag=true;
    }

    public void SetGravityFrag(bool flag)
    {
        GravityFlag = flag;
    }

    public bool GetGravityFlag() 
    {
        return GravityFlag;
    }

}