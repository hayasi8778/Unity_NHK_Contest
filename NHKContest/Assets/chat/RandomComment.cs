using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomComment : MonoBehaviour
{
    [Header("チャット表示の対象")]
    [Tooltip("コメントを表示する ChatManager を指定してください")]
    public ChatManager chatManager;
    [Header("表示するコメント一覧")]
    [Tooltip("1回ずつ順番に表示されるコメントをここに追加してください")]
   // [TextArea(2, 5)]
    public List<string> commentList = new List<string>();
    [Header("表示間隔（秒）")]
    [Tooltip("次のコメントを表示するまでの最小秒数")]
    public float minInterval = 10f;
    [Tooltip("次のコメントを表示するまでの最大秒数")]
    public float maxInterval = 20f;
    private int currentIndex = 0;
    private bool isRunning = true;
    void Start()
    {
        StartCoroutine(CommentCoroutine());
    }
    IEnumerator CommentCoroutine()
    {
        // 全コメントを表示し終えるまで繰り返す
        while (isRunning && currentIndex < commentList.Count)
        {
            // ランダムな時間だけ待機
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
            // 現在のコメントを表示
            chatManager.AddMessage(commentList[currentIndex]);
            // 次のコメントへ
            currentIndex++;
            // 最後のコメントまで到達したら停止
            if (currentIndex >= commentList.Count)
            {
                isRunning = false;
            }
        }
    }
}
