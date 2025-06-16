using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;
using System.Collections;


public class PlayerMove : MonoBehaviour
{
    public float movespeed = 1.0f;//�ړ��X�s�[�h
    private bool moveFlag = false; //���s���Ă��邩�̃t���O
    
    private AudioSource audioSource; //���炷���߂̃R���|�[�l���g
    public AudioClip walkSE;
    private SpriteRenderer renderer; //�����_�[���擾����

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
            Debug.Log("Renderer is " + (renderer.enabled ? "enabled" : "disabled"));
        }
        else
        {
            Debug.Log("�����_�[�Ȃ���");
        }

        // �T�E���h�̐ݒ�@���J
        audioSource.clip = walkSE;  // ����ݒ�
        audioSource.loop = true;    // ���[�v����悤�ɐݒ�
    }

    // Update is called once per frame
    void Update()
    {

        //�p�x�̓s���ňړ��������E���]����
        moveFlag = false; //���s�t���O�͖��t���[���؂�
        Vector3 scale = transform.localScale;

        //�E�ړ�
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(movespeed * Time.deltaTime, 0, 0);

            // ���Q�[�������Ԃ��~�߂Ă��U��ނ����Ⴄ��ŏ����ǉ����܂����@���J
            if (Time.timeScale > 0) renderer.flipX = false;

            moveFlag = true;
        }

        //���ړ�
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-movespeed * Time.deltaTime, 0, 0);

            // ���Q�[�������Ԃ��~�߂Ă��U��ނ����Ⴄ��ŏ����ǉ����܂����@���J
            if (Time.timeScale > 0) renderer.flipX = true;

            moveFlag = true;
        }

        if (moveFlag && animFlag)
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

        // ���͏󋵂ƃQ�[�������Ԃ̊m�F�����ĉ���炷���~�߂�@���J
        if (moveFlag && Time.timeScale > 0) PlaySound();
        else StopAudio();
    }

    void PlaySound()
    {
        // �Đ��󋵂̊m�F���֐����ōs���@���J
        if (!audioSource.isPlaying) audioSource.Play();
    }

    void StopAudio()
    {
        // �Đ��󋵂̊m�F���֐����ōs���@���J
        if (audioSource.isPlaying) audioSource.Stop();
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