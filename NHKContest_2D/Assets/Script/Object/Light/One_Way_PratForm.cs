using UnityEngine;
using System.Collections;

public class One_Way_PratForm : MonoBehaviour
{
    public Collider2D platformCollider; // ����̃R���C�_�[
    public float passThroughTime = 1.5f; // ���蔲�����鎞��
    private float lastPressTime = 0f;
    private int pressCount = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�������ɂ��蔲���鏈��
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) // ��L�[ or �W�����v�L�[�������ꂽ
        {
            // �v���C���[������̉��ɂ��邩�m�F
            if (IsPlayerBelowPlatform())
            {
                PassThroughPlatform();
            }
        }


        //�L�[���Q�񉟂��Ă��蔲���鏈��
        if (Input.GetKeyDown(KeyCode.DownArrow)) // ���L�[�������ꂽ���m�F
        {
            float currentTime = Time.time;

            // �f����2�񉟂������m�F
            if (currentTime - lastPressTime < 0.3f)
            {
                pressCount++;
            }
            else
            {
                pressCount = 1; // ���Z�b�g
            }

            lastPressTime = currentTime;

            if (pressCount >= 2)
            {
                PassThroughPlatform();
            }
        }

    }

    bool IsPlayerBelowPlatform()
    {
        // �v���C���[�̈ʒu���擾
        GameObject player = GameObject.FindWithTag("Player"); // �v���C���[�̃^�O��ݒ肵�Ă���
        if (player == null) return false;

        return player.transform.position.y < platformCollider.bounds.min.y; // �v���C���[��Y���W������̉���
    }

    void PassThroughPlatform()
    {
        platformCollider.enabled = false; // �R���C�_�[�𖳌����i���蔲���\�j
        Invoke("RestorePlatform", passThroughTime); // �w�莞�Ԍ�ɃR���C�_�[��߂�
    }

    void RestorePlatform()
    {
        platformCollider.enabled = true; // �R���C�_�[�𕜊�������
    }


}
