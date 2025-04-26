using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject chatContent;           // Content
    public GameObject chatMessagePrefab;     // �R�����g1����Text�v���n�u
    public ScrollRect scrollRect;            // Scroll View
    public void AddMessage(string message)
    {
        // -------------------------------
        // �@ ���b�Z�[�W�����H���āA10�S�p�������Ƃɉ��s������
        // -------------------------------
        message = InsertLineBreaks(message, 10);
        // -------------------------------
        // �A �s�����v�Z����
        // -------------------------------
        int charsPerLine = 10; // 1�s������̍ő�S�p������
        int lineCount = Mathf.CeilToInt((float)message.Split('\n').Length);
        int lineHeight = 28; // 1�s������̍����i�s�N�Z���j
        float shiftHeight = lineCount * lineHeight;
        // -------------------------------
        // �B �����̃R�����g�����ׂď�ɂ��炷
        // -------------------------------
        foreach (Transform child in chatContent.transform)
        {
            RectTransform rt = child.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition += new Vector2(0, shiftHeight);
            }
        }
        // -------------------------------
        // �C �V�����R�����g��ǉ����āA��ԉ��iY=0�j�ɒu��
        // -------------------------------
        GameObject newMessage = Instantiate(chatMessagePrefab, chatContent.transform);
        var tmp = newMessage.GetComponent<TextMeshProUGUI>();
        if (tmp != null) tmp.text = message;
        RectTransform newRT = newMessage.GetComponent<RectTransform>();
        if (newRT != null)
        {
            newRT.anchoredPosition = new Vector2(newRT.anchoredPosition.x, 0);
        }
        // -------------------------------
        // �D �X�N���[������ԉ��ɍX�V
        // -------------------------------
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    private string InsertLineBreaks(string text, int maxCharsPerLine)
    {
        int count = 0;
        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            if (IsZenkaku(c))
                count += 2; // �S�p��2�J�E���g
            else
                count += 1; // ���p��1�J�E���g
            if (count >= maxCharsPerLine * 2)
            {
                text = text.Insert(i + 1, "\n");
                count = 0;
                i++; // \n�Ԃ񂾂����̈ʒu�����炷
            }
        }
        return text;
    }
    // �S�p����
    private bool IsZenkaku(char c)
    {
        return (c >= 0x3000 && c <= 0xFF60);
    }

}