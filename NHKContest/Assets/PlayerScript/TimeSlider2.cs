using UnityEngine;
using UnityEngine.UI;

public class TimeSlider2 : MonoBehaviour
{
    public Slider slider; //�X���C�_�[
    private Vector3[] positionHistory = new Vector3[3000]; //�b��(5������T�C�Y)
    private int currentIndex = 0;  //���݂̃X���C�_�[�l
    private bool isRewinding = false;  //�X���C�_�[�����߂������𔻒肷��t���O

    //�I�u�W�F�N�g�̐؂�ւ��������ł�邩��X���C�_�[����ڐA
    public GameObject[] replacementPrefabs; // ���Ԃɐ؂�ւ���v���n�u�Q
    private int replacementIndex = 0;

    private bool isManualInput = false; //�蓮�Ŋ����߂��Ă��邩�̃t���O

    void Start()
    {
        for (int i = 0; i < positionHistory.Length; i++)
        {
            if (positionHistory[i] == null) //�I�u�W�F�N�g�؂�ւ����ɏ������W�Ŗ��߂�\�������邩��null�`�F�b�N
            {
                positionHistory[i] = transform.position;//�z��̏�����
            }
            
        }
    }

    void Update()
    {
        if (!isRewinding) //�����߂�������Ȃ��Ȃ���W��z��ɒǉ�
        {
            int index = Mathf.RoundToInt(slider.value * 10); 
            if (index >= 0 && index < positionHistory.Length)
            {
                positionHistory[index] = transform.position;
                currentIndex = index;
            }
        }
    }

    public void OnSliderValueChanged()//�v���C���[�̊����߂�����
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
            return;
        }

        if (index < positionHistory.Length)
        {
            if (index <= currentIndex)
            {
                transform.position = positionHistory[index];
                for (int i = currentIndex + 1; i < positionHistory.Length; i++)
                {
                    positionHistory[i] = transform.position;
                }
            }
            else
            {
                for (int i = currentIndex + 1; i <= index; i++)
                {
                    positionHistory[i] = transform.position;
                }
            }
            currentIndex = index;
        }
        isRewinding = false;
    }

    public Vector3[] GetPositionHistory() //�I�u�W�F�N�g�̈��p���̂��߂̃Q�b�^�[
    {
        return positionHistory;
    }

    public void SetPositionHistory(Vector3[] history)//�I�u�W�F�N�g���p���̂��߂̃Z�b�^�[
    {
        if (history.Length == positionHistory.Length)
        {
            for (int i = 0; i < positionHistory.Length; i++)
            {
                positionHistory[i] = history[i];
            }
        }
    }

    public GameObject ObjectChanged()
    {
        if (replacementPrefabs == null || replacementPrefabs.Length == 0)
        {
            Debug.LogWarning("replacementPrefabs ���ݒ肳��Ă��܂���I");
            return null;
        }

        GameObject nextPrefab = replacementPrefabs[replacementIndex];
        replacementIndex = (replacementIndex + 1) % replacementPrefabs.Length;

        Vector3 spawnPosition = transform.position;
        GameObject newObj = Instantiate(nextPrefab, spawnPosition, Quaternion.identity);

        TimeSlider2 newScript = newObj.GetComponent<TimeSlider2>();
        if (newScript != null)
        {
            newScript.slider = this.slider;
            newScript.SetPositionHistory(this.GetPositionHistory());
            newScript.replacementPrefabs = this.replacementPrefabs;
            newScript.replacementIndex = this.replacementIndex;
        }

        Destroy(this.gameObject);

        return newObj; // �� �����ŕԂ��I
    }

    public void ObjectChanged(GameObject newObject) //�V�����I�u�W�F�N�g�ɃX���C�_�[�����p�����g���폜
    {

        
        if (newObject == null) return;

        var newSliderScript = newObject.GetComponent<TimeSlider2>();
        if (newSliderScript != null)
        {
            newSliderScript.slider = this.slider;
            newSliderScript.SetPositionHistory(this.GetPositionHistory());
        }

        Destroy(this.gameObject);
        
    }

    public void OnSliderMovedByUser(float value) //�X���C�_�[�������߂��ꂽ�Ƃ��Ƀv���C���[���W�������߂�
    {
        isManualInput = true;

        int index = Mathf.Clamp((int)(value * 10f), 0, positionHistory.Length - 1);
        if (positionHistory[index] != Vector3.zero)
        {
            transform.position = positionHistory[index];
        }
    }
}
