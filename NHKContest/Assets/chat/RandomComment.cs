using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomComment : MonoBehaviour
{
    [Header("�`���b�g�\���̑Ώ�")]
    [Tooltip("�R�����g��\������ ChatManager ���w�肵�Ă�������")]
    public ChatManager chatManager;
    [Header("�\������R�����g�ꗗ")]
    [Tooltip("1�񂸂��Ԃɕ\�������R�����g�������ɒǉ����Ă�������")]
   // [TextArea(2, 5)]
    public List<string> commentList = new List<string>();
    [Header("�\���Ԋu�i�b�j")]
    [Tooltip("���̃R�����g��\������܂ł̍ŏ��b��")]
    public float minInterval = 10f;
    [Tooltip("���̃R�����g��\������܂ł̍ő�b��")]
    public float maxInterval = 20f;
    private int currentIndex = 0;
    private bool isRunning = true;
    void Start()
    {
        StartCoroutine(CommentCoroutine());
    }
    IEnumerator CommentCoroutine()
    {
        // �S�R�����g��\�����I����܂ŌJ��Ԃ�
        while (isRunning && currentIndex < commentList.Count)
        {
            // �����_���Ȏ��Ԃ����ҋ@
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
            // ���݂̃R�����g��\��
            chatManager.AddMessage(commentList[currentIndex]);
            // ���̃R�����g��
            currentIndex++;
            // �Ō�̃R�����g�܂œ��B�������~
            if (currentIndex >= commentList.Count)
            {
                isRunning = false;
            }
        }
    }
}
