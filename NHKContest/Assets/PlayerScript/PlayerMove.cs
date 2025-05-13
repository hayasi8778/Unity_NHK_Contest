using UnityEngine;
using UnityEngine.Video;


public class PlayerMove : MonoBehaviour
{
    public float movespeed = 1.0f;//�ړ��X�s�[�h
    public VideoPlayer videoPlayer;
    private bool muveFlag = true; //���s���Ă��邩�̃t���O
    public bool isFacingRight = true; //����
    private bool OldisFacing = true; //1�t���[���O�̌���


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

        //�p�x�̓s���ňړ��������E���]����

        muveFlag = false; //���s�t���O�͖��t���[���؂�

        Vector3 scale = transform.localScale;

        OldisFacing = isFacingRight;//1�t���[���O�̕����Ƃ�

        //�E�ړ�
        if (Input.GetKey(KeyCode.D))
        {
            

            transform.Translate(-movespeed * Time.deltaTime, 0, 0);

            isFacingRight = true;

            muveFlag = true;
        }

        //���ړ�
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(movespeed * Time.deltaTime, 0, 0);

            isFacingRight = false;

            muveFlag = true;
        }

        

        if (OldisFacing != isFacingRight)
        {

            // ���E���]�iX�X�P�[����؂�ւ��đΉ��j
            scale.x = isFacingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        if(videoPlayer == null)
        {
            Debug.Log("����Ȃ�����A�b�v�f�[�g�����܂�");
            return;
        }

        if (muveFlag)
        {
            if (!videoPlayer.isPlaying)
            {

                videoPlayer.Play(); // �L�[���͂�����΍Đ�

            }
        }
        else
        {
            //Debug.LogError("���[�v��~����");

            if (videoPlayer.time >= videoPlayer.length)
            {
                videoPlayer.time = 0; // �Đ��ʒu�����Z�b�g

                videoPlayer.Stop(); // ���悪�Ō�܂ōĐ����ꂽ���~
            }
        }

        
        
        


    }
}
