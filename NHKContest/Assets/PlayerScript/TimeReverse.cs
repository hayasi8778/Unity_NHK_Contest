using System.Collections.Generic;
using UnityEngine;

public class TimeReverse : MonoBehaviour
{
    private List<Vector3> positionHistory = new List<Vector3>();
    private float recordInterval = 0.1f; // �ʒu�L�^�̊Ԋu�i0.1�b�j
    private float rewindDuration = 3f;    // �����߂��ɂ����鎞�ԁi3�b�j
    private float rewindTimer = 0f;       // �����߂��̃^�C�}�[
    private bool isRewinding = false;

    void Start()
    {
        // �ʒu�̋L�^���J�n
        InvokeRepeating(nameof(RecordPosition), 0f, recordInterval);
    }

    void Update()
    {
        // R�L�[�Ŋ����߂��J�n
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

    // �ʒu���L�^����֐�
    private void RecordPosition()
    {
        if (!isRewinding)
        {
            positionHistory.Add(transform.position);

            // 3�b���̃f�[�^�̂ݕێ�
            if (positionHistory.Count > Mathf.FloorToInt(rewindDuration / recordInterval))
            {
                positionHistory.RemoveAt(0);
            }
        }
    }

    // �����߂����s���֐�
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
            Debug.Log("�����߂����������܂����B");
        }
    }
}
