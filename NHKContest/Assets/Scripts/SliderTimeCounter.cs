using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderTimeCounter : MonoBehaviour
{
    public Slider slider;   // �X���C�_�[�R���|�[�l���g�ւ̎Q��
    float timeCounter = 0;     // FPS�J�E���^
    float ValueOld = 0;   //�f�o�b�N�p�ɃX���C�_�[�̃��O�ʒu�Ƃ�

    void Start()
    {
        // �����l��ݒ�
        slider.value = 0.0f;
        slider.maxValue = 300.0f;
        slider.minValue = 0.0f;
    }

    void Update()
    {

        if (slider == null)
        {
            Debug.LogError("�V�F�[�_�[���w�肳��ĂȂ���(SliderTimeCounter)");
        }

            timeCounter += Time.deltaTime;

        if (timeCounter >= 0.1)
        {
            ValueOld = slider.value;
            // timeCounter���P�ȏ�i�O��̍X�V����P�b�ȏ�o�߂����j�̂Ƃ��A�X���C�_�[�̒l�𑝂₷
            slider.value += 0.1f;
            timeCounter = 0;

            //Debug.Log($"ValueOld: {ValueOld}, slider.value: {slider.value}");
        }
    }
}