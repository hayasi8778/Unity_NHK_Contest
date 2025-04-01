using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    public Slider slider; // �X���C�_�[�Q��
    private Vector3[] positionHistory = new Vector3[3000]; // 0.1�b���Ƃ�300�b���̗������L�^
    private int currentIndex = 0; // ���݂̗����C���f�b�N�X
    private bool isRewinding = false; // �����߂��t���O

    void Start()
    {
        for (int i = 0; i < positionHistory.Length; i++)
        {
            positionHistory[i] = transform.position; // �������W�Ŗ��߂�
        }
    }

    void Update()
    {
        if (!isRewinding)
        {
            // �X���C�_�[�̌��݂̈ʒu��z��ɋL�^
            int index = Mathf.RoundToInt(slider.value * 10);
            if (index >= 0 && index < positionHistory.Length)
            {
                positionHistory[index] = transform.position;
                currentIndex = index;
            }
        }
    }

    public void OnSliderValueChanged()
    {

        if (slider == null)
        {
            Debug.LogError("�X���C�_�[���w�肳��ĂȂ���(TimeSlider)");
            return;
        }

        isRewinding = true;
        int index = Mathf.RoundToInt(slider.value * 10);

        if (index == currentIndex + 1)
        {
            //Debug.LogError("�X���C�_�[�̈ʒu��ꂽ��+1�Ŏ����");
            return;
        }

        if (index < positionHistory.Length)
        {
            if (index <= currentIndex)
            {
                Debug.LogError("�����߂�����");
                // �ߋ��ɋL�^���ꂽ���W�Ɋ����߂�
                transform.position = positionHistory[index];

                // ���݈ʒu����̗�������������
                for (int i = currentIndex + 1; i < positionHistory.Length; i++)
                {
                    positionHistory[i] = transform.position; // ���ݍ��W�Ŗ��߂�
                }
            }
            else
            {
                // ���L�^�̗̈�Ȃ�A���݂̍��W�Ŗ��߂�
                for (int i = currentIndex + 1; i <= index; i++)
                {
                    positionHistory[i] = transform.position;
                }

                //Debug.LogError("��ɐi�݂���");

                //Debug.Log($"Index: {index}, CurrentIndex: {currentIndex}");

            }
            currentIndex = index;
        }
        isRewinding = false;
    }
}
