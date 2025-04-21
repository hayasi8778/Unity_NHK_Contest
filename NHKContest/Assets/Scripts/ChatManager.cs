using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject chatContent;           // Content
    public GameObject chatMessagePrefab;     // コメント1つ分のTextプレハブ
    public ScrollRect scrollRect;            // Scroll View
    public void AddMessage(string message)
    {
        // 1. 既存のチャットをすべて上に20ずつずらす
        foreach (Transform child in chatContent.transform)
        {
            RectTransform rt = child.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition += new Vector2(0, 20f);
            }
        }
        // 2. 新しいメッセージを追加
        GameObject newMessage = Instantiate(chatMessagePrefab, chatContent.transform);
        var tmp = newMessage.GetComponent<TextMeshProUGUI>();
        if (tmp != null) tmp.text = message;
        // 3. クローンは一番下（Y=0）に置く
        RectTransform newRT = newMessage.GetComponent<RectTransform>();
        if (newRT != null)
        {
            newRT.anchoredPosition = new Vector2(newRT.anchoredPosition.x, 0);
        }
        // 4. レイアウト更新
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}