using System.Collections.Generic;
using UnityEngine;

public class TimeReverse : MonoBehaviour
{
    private List<Vector3> positionHistory = new List<Vector3>();
    private float recordInterval = 0.1f; // 位置記録の間隔（0.1秒）
    private float rewindDuration = 3f;    // 巻き戻しにかける時間（3秒）
    private float rewindTimer = 0f;       // 巻き戻しのタイマー
    private bool isRewinding = false;

    void Start()
    {
        // 位置の記録を開始
        InvokeRepeating(nameof(RecordPosition), 0f, recordInterval);
    }

    void Update()
    {
        // Rキーで巻き戻し開始
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isRewinding)
            {
                isRewinding = true;
                rewindTimer = rewindDuration;
            }
        }

        if (isRewinding)
        {
            RewindPosition();
        }
    }

    // 位置を記録する関数
    private void RecordPosition()
    {
        if (!isRewinding)
        {
            positionHistory.Add(transform.position);

            // 3秒分のデータのみ保持
            if (positionHistory.Count > Mathf.FloorToInt(rewindDuration / recordInterval))
            {
                positionHistory.RemoveAt(0);
            }
        }
    }

    // 巻き戻しを行う関数
    private void RewindPosition()
    {
        if (positionHistory.Count > 0 && rewindTimer > 0)
        {
            int targetIndex = Mathf.Clamp(
                Mathf.FloorToInt((rewindTimer / rewindDuration) * positionHistory.Count),
                0,
                positionHistory.Count - 1
            );

            transform.position = positionHistory[targetIndex];
            rewindTimer -= Time.deltaTime;
        }
        else
        {
            isRewinding = false;
            Debug.Log("巻き戻しが完了しました。");
        }
    }
}
