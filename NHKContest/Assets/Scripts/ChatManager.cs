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
        // 1. �����̃`���b�g�����ׂď��20�����炷
        foreach (Transform child in chatContent.transform)
        {
            RectTransform rt = child.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition += new Vector2(0, 20f);
            }
        }
        // 2. �V�������b�Z�[�W��ǉ�
        GameObject newMessage = Instantiate(chatMessagePrefab, chatContent.transform);
        var tmp = newMessage.GetComponent<TextMeshProUGUI>();
        if (tmp != null) tmp.text = message;
        // 3. �N���[���͈�ԉ��iY=0�j�ɒu��
        RectTransform newRT = newMessage.GetComponent<RectTransform>();
        if (newRT != null)
        {
            newRT.anchoredPosition = new Vector2(newRT.anchoredPosition.x, 0);
        }
        // 4. ���C�A�E�g�X�V
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}