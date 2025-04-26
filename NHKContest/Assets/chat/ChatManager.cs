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
        // -------------------------------
        // ① メッセージを加工して、10全角文字ごとに改行を入れる
        // -------------------------------
        message = InsertLineBreaks(message, 10);
        // -------------------------------
        // ② 行数を計算する
        // -------------------------------
        int charsPerLine = 10; // 1行あたりの最大全角文字数
        int lineCount = Mathf.CeilToInt((float)message.Split('\n').Length);
        int lineHeight = 28; // 1行あたりの高さ（ピクセル）
        float shiftHeight = lineCount * lineHeight;
        // -------------------------------
        // ③ 既存のコメントをすべて上にずらす
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
        // ④ 新しいコメントを追加して、一番下（Y=0）に置く
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
        // ⑤ スクロールを一番下に更新
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
                count += 2; // 全角は2カウント
            else
                count += 1; // 半角は1カウント
            if (count >= maxCharsPerLine * 2)
            {
                text = text.Insert(i + 1, "\n");
                count = 0;
                i++; // \nぶんだけ次の位置をずらす
            }
        }
        return text;
    }
    // 全角判定
    private bool IsZenkaku(char c)
    {
        return (c >= 0x3000 && c <= 0xFF60);
    }

}