using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderTimeCounter : MonoBehaviour
{
    public Slider slider;   // �X���C�_�[�R���|�[�l���g�ւ̎Q��
    public GameObject currentObject;
    public GameObject[] objectPrefabs;

    float timeCounter = 0;     // FPS�J�E���^
    float ValueOld = 0;   //�f�o�b�N�p�ɃX���C�_�[�̃��O�ʒu�Ƃ�

    //�v���C���[�ƃX���C�_�[���������Ȃ����߂̕ύX
    private bool isManualInput = false;  // �蓮�ő��삳�ꂽ���ǂ����̃t���O
    private float manualInputTimer = 0f; // �蓮����̖�������

    private float previousSliderValue = 0f;

    void Start()
    {
        // �����l��ݒ�
        slider.value = 0.0f;
        slider.maxValue = 300.0f;
        slider.minValue = 0.0f;
    }

    void Update()
    {

        if (slider == null)//�f�o�b�N�p�ɃX���C�_�[�o�^����Ă邩�m�F����
        {
            Debug.LogError("�V�F�[�_�[���w�肳��ĂȂ���(SliderTimeCounter)");
        }

        float diff = Mathf.Abs(slider.value - ValueOld);

        // �������蓮����i�������ȏ�j�����o
        if (diff > 5.0f && currentObject != null)
        {
            Debug.LogWarning("�������X���C�_�[��������m");

            Vector3 spawnPosition = currentObject.transform.position;
            int randomIndex = UnityEngine.Random.Range(0, objectPrefabs.Length);

            GameObject newObj = Instantiate(objectPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            TimeSlider2 newSliderScript = newObj.GetComponent<TimeSlider2>();
            TimeSlider2 oldSliderScript = currentObject.GetComponent<TimeSlider2>();

            if (newSliderScript != null && oldSliderScript != null)
            {
                newSliderScript.slider = slider;
                newSliderScript.SetPositionHistory(oldSliderScript.GetPositionHistory());

                oldSliderScript.ObjectChanged(newObj); // �������폜���Đ؂�ւ�
                currentObject = newObj; // �V�����I�u�W�F�N�g��o�^
            }
        }

        // �蓮���삪�s��ꂽ�ꍇ�A��莞��(1�b)�͎��Ԍo�߂ɂ��X���C�_�[�̈ړ����~
        if (isManualInput)
        {
            manualInputTimer += Time.deltaTime;
            if (manualInputTimer >= 1.0f)
            {
                isManualInput = false;
                //manualInputTimer = 0f;
                Debug.Log("�X���C�_�[��~");
            }
            return;
        }

        timeCounter += Time.deltaTime;

        if (timeCounter >= 0.1)
        {
            // �X���C�_�[�̃C�x���g���ꎞ�I�ɉ������Ēl��ύX
            slider.onValueChanged.RemoveListener(OnSliderMoved);//�C�x���g��~(�Ȃ񂩕��ʂɓ������ۂ�)
            //slider.onValueChanged.RemoveListener(OnSliderValueChanged);//�C�x���g��~(�Ȃ񂩕��ʂɓ������ۂ�)
            slider.value += 0.1f;
            slider.onValueChanged.AddListener(OnSliderMoved);//�C�x���g�X�V

            timeCounter = 0;
            Debug.Log("�X���C�_�[�X�V");
            //Debug.Log($"ValueOld: {ValueOld}, slider.value: {slider.value}");
        }

    }


    // �X���C�_�[�����삳�ꂽ�ۂɌĂ΂��
    public void OnSliderMoved(float value)
    {
        // �X���C�_�[�̎蓮��������m
        isManualInput = true;

        if (currentObject != null)
        {
            TimeSlider2 script = currentObject.GetComponent<TimeSlider2>();
            if (script != null)
            {
                script.OnSliderMovedByUser(value); // �� �����I
            }
        }

        // �ȉ��A�X���C�_�[�������������������ǂ����̔���
        float diff = Mathf.Abs(value - previousSliderValue);
        previousSliderValue = value;

        if (diff > 5.0f && currentObject != null && objectPrefabs.Length > 0)
        {
            Vector3 spawnPosition = currentObject.transform.position;
            int randomIndex = UnityEngine.Random.Range(0, objectPrefabs.Length);

            GameObject newObj = Instantiate(objectPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            TimeSlider2 newSliderScript = newObj.GetComponent<TimeSlider2>();
            TimeSlider2 oldSliderScript = currentObject.GetComponent<TimeSlider2>();

            if (newSliderScript != null && oldSliderScript != null)
            {
                newSliderScript.slider = slider;
                newSliderScript.SetPositionHistory(oldSliderScript.GetPositionHistory());

                oldSliderScript.ObjectChanged(newObj);
                currentObject = newObj;
            }
        }
    }

}