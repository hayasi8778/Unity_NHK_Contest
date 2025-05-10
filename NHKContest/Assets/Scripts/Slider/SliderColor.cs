using UnityEngine;
using UnityEngine.UI;

public class SliderColor : MonoBehaviour
{
    /*
    public Slider slider;
    public Image redProgress; // �Ԃ��i�s������ Image

    public float fixedHeight = 20f; // �������Œ肷��l

    
    void Update()
    {
        if (slider != null && redProgress != null)
        {
            float percentage = slider.value / slider.maxValue;

            // ���[���Œ�
            redProgress.rectTransform.anchorMin = new Vector2(0, 0);
            redProgress.rectTransform.anchorMax = new Vector2(percentage, 1);

            // �������Œ�
            redProgress.rectTransform.sizeDelta = new Vector2(redProgress.rectTransform.sizeDelta.x, fixedHeight);
        }
    }
    */
    /*
    public Slider slider;
    public Image redProgress; // �Đ��o�[�̐Ԃ�����

    private float initialWidth; // ������
    private RectTransform sliderRect; // �X���C�_�[�� RectTransform

    void Start()
    {
        if (slider != null && redProgress != null)
        {
            sliderRect = slider.GetComponent<RectTransform>();

            // �����̃X���C�_�[�̕����擾
            initialWidth = sliderRect.sizeDelta.x;

            //initialWidth = redProgress.flexibleWidth;

            // �Ԃ�Image�̏�����Ԃ�ݒ�i����0�ɂ���j
            redProgress.rectTransform.sizeDelta = new Vector2(0, redProgress.rectTransform.sizeDelta.y);
        }
    }

    void Update()
    {
        if (slider != null && redProgress != null)
        {
            float percentage = slider.value / slider.maxValue; // �i�s����

            // �X���C�_�[�̐i�s�x�ɉ����ĐԂ�Image�̕���ύX
            redProgress.rectTransform.sizeDelta = new Vector2(initialWidth * percentage, redProgress.rectTransform.sizeDelta.y);
        }
    }
    */
    public Slider slider;
    public Image redProgress; // �Đ��o�[�̐Ԃ�����

    private float initialWidth; // ������
    private RectTransform sliderRect; // �X���C�_�[�� RectTransform
    private RectTransform redProgressRect; // �Ԃ�Image�� RectTransform

    public float startOffset = 10f; // **�J�n�ʒu�̒����p�ϐ�**
    public float widthMultiplier = 3f; // **Image�̕��̑����{���i���R�ɕύX�\�j**

    void Start()
    {
        if (slider != null && redProgress != null)
        {
            sliderRect = slider.GetComponent<RectTransform>();
            redProgressRect = redProgress.GetComponent<RectTransform>();

            // �����̃X���C�_�[�̕����擾
            initialWidth = sliderRect.sizeDelta.x;

            // **�Ԃ�Image�̃A���J�[�������ɌŒ�**
            redProgressRect.anchorMin = new Vector2(0f, 0.5f);
            redProgressRect.anchorMax = new Vector2(0f, 0.5f);
            redProgressRect.pivot = new Vector2(0f, 0.5f); // �s�{�b�g�������ɐݒ�

            // �Ԃ�Image�̏�����Ԃ�ݒ�i����0�ɂ��� + �J�n�ʒu�����炷�j
            redProgressRect.sizeDelta = new Vector2(0, redProgressRect.sizeDelta.y);
            redProgressRect.anchoredPosition = new Vector2(startOffset, redProgressRect.anchoredPosition.y);
        }
    }

    void Update()
    {
        if (slider != null && redProgress != null)
        {
            float percentage = slider.value / slider.maxValue; // �i�s����

            // **�T�C�Y�ύX�̔{����K�p**
            redProgressRect.sizeDelta = new Vector2(initialWidth * percentage * widthMultiplier, redProgressRect.sizeDelta.y);
            redProgressRect.anchoredPosition = new Vector2(startOffset, redProgressRect.anchoredPosition.y);
        }
    }






}
