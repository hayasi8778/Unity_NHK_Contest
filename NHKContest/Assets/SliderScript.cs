using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider slider; // �X���C�_�[�R���|�[�l���g�ւ̎Q��

    void Start()
    {
        // �����l��ݒ�
        slider.value = 0.5f;

        // �X���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂяo�����C�x���g��o�^
        slider.onValueChanged.AddListener(OnSliderValueChanged);

    }

    void Update()
    {
        // �L�[�{�[�h����ŃX���C�_�[�l��ύX�����
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            slider.value += 0.1f; // �l�𑝉�
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            slider.value -= 0.1f; // �l������
        }
    }

    private void OnSliderValueChanged(float value)
    {
        // �X���C�_�[�l�ύX���ɓ���̓�������s
        Debug.Log("�X���C�_�[�̒l���ς��܂���: " + value);
    }
}
