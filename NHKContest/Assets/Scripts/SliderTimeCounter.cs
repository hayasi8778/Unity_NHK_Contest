using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderTimeCounter : MonoBehaviour
{
    public Slider slider;   // �X���C�_�[�R���|�[�l���g�ւ̎Q��
    float timeCounter = 0;     // FPS�J�E���^

    void Start()
    {
        // �����l��ݒ�
        slider.value = 0.0f;
        slider.maxValue = 60.0f;
        slider.minValue = 0.0f;
    }

    void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= 0.1)
        {
            // timeCounter���P�ȏ�i�O��̍X�V����P�b�ȏ�o�߂����j�̂Ƃ��A�X���C�_�[�̒l�𑝂₷
            slider.value += 0.1f;
            timeCounter = 0;
        }
    }
}
