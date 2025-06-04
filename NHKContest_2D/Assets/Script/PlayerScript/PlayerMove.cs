using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;
using System.Collections;


public class PlayerMove : MonoBehaviour
{
    public float movespeed = 1.0f;//�ړ��X�s�[�h
    private bool muveFlag = false; //���s���Ă��邩�̃t���O
    
    private AudioSource audioSource; //���炷���߂̃R���|�[�l���g
    public AudioClip walkSE;
    private new SpriteRenderer renderer; //�����_�[���擾����

    public PlayerMoveAmin MoveAmin; //���s���̃A�j���[�V����
    bool animFlag = true;//�A�j���[�V���������̃t���O

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource���擾

        MoveAmin = GetComponent<PlayerMoveAmin>();

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

    // Update is called once per frame
    void Update()
    {

        //�p�x�̓s���ňړ��������E���]����

        
        muveFlag = false; //���s�t���O�͖��t���[���؂�
        

        Vector3 scale = transform.localScale;

       



        //�E�ړ�
        if (Input.GetKey(KeyCode.D))
        {


            transform.Translate(movespeed * Time.deltaTime, 0, 0);

            

            renderer.flipX = false;

            muveFlag = true;

            if (!audioSource.isPlaying) // �����Đ�������Ȃ��Ȃ�
            {
                PlaySound(); //SE�Đ�
            }

        }

        //���ړ�
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-movespeed * Time.deltaTime, 0, 0);


            renderer.flipX = true;

            muveFlag = true;

            if (!audioSource.isPlaying) // �Đ����łȂ��Ȃ�
            {
                PlaySound();

            }

        }

        if (muveFlag && animFlag)
        {

            PlayAnim();
            /*
            if (!videoPlayer.isPlaying)
            {

                videoPlayer.Play(); // �L�[���͂�����΍Đ�

            }
            */
        }
        else
        {
            //Debug.LogError("���[�v��~����");
            /*
            if (videoPlayer.time >= videoPlayer.length)
            {
                videoPlayer.time = 0; // �Đ��ʒu�����Z�b�g

                videoPlayer.Stop(); // ���悪�Ō�܂ōĐ����ꂽ���~
            }
            */
        }

    }

    void PlaySound()
    {
        audioSource.PlayOneShot(walkSE);
        Invoke("StopAudio", 0.5f);
    }


    void StopAudio()
    {
        audioSource.Stop();
    }

    void PlayAnim()
    {
        animFlag = false;
        MoveAmin.ChangeSprite();
        Invoke("StopAmin", 0.5f);
    }

    void StopAmin()
    {
        animFlag=true;
    }

}