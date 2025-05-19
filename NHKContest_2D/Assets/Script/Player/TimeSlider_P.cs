using UnityEngine;
using UnityEngine.UI;

public class TimeSlider_P : MonoBehaviour
{
    public Slider slider; //�X���C�_�[
    private Vector3[] positionHistory = new Vector3[3000]; //�b��(5������T�C�Y)
    private int currentIndex = 0;  //���݂̃X���C�_�[�l
    private bool isRewinding = false;  //�X���C�_�[�����߂������𔻒肷��t���O

    //�I�u�W�F�N�g�̐؂�ւ��������ł�邩��X���C�_�[����ڐA
    public GameObject[] replacementPrefabs; // ���Ԃɐ؂�ւ���v���n�u�Q
    private int replacementIndex = 0;

    private bool isManualInput = false; //�蓮�Ŋ����߂��Ă��邩�̃t���O

    //���Ԃŉ掿�߂����߂̏���
    private float revertTimer = 0f;
    private float revertTimeLimit = 5f; // 5�b�Ŗ߂�
    private bool isBeingDestroyed = false; //�폜�t���O
    private GameObject currentPlayer; //����Ώۂ̃v���C���[

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
    void Update()
    {
        if (isBeingDestroyed) return; //�������Ȃ�A�b�v�f�[�g�������Ȃ�

        if (!isRewinding) //�����߂�������Ȃ��Ȃ���W��z��ɒǉ�
        {
            int index = Mathf.RoundToInt(slider.value * 10);
            if (index >= 0 && index < positionHistory.Length)
            {
                positionHistory[index] = transform.position;
                currentIndex = index;
            }
        }

        //�^�C�}�[��5�b�o������O�ɖ߂�����
        revertTimer += Time.deltaTime;
        if (revertTimer >= revertTimeLimit)
        {
            TryRevertObject();
            revertTimer = 0f; // �^�C�}�[���Z�b�g
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

        // �Ō�܂ŗ��������ւ����Ȃ�

        //���̎����� replacementPrefabs �̍Ō�̃v���n�u�Ɠ����Ȃ����ւ����Ȃ�
        if (replacementPrefabs[replacementPrefabs.Length - 1].name == this.gameObject.name.Replace("(Clone)", "").Trim())
        {
            Debug.LogWarning("�Ō�̃I�u�W�F�N�g�Ȃ̂œ���ւ����܂���");
            return null;
        }

        replacementIndex = (replacementIndex + 1) % replacementPrefabs.Length;

        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 0.5f; // 0.5f��ɂ��炷
        Quaternion spawnRotation = Quaternion.Euler(90f, 90f, -90f);

        GameObject nextPrefab = replacementPrefabs[replacementIndex];

        GameObject newObj = Instantiate(nextPrefab, spawnPosition, spawnRotation);

        // �V�����I�u�W�F�N�g�ɏ���n��
        TimeSlider_P newScript = newObj.GetComponent<TimeSlider_P>();
        if (newScript != null)
        {
            newScript.slider = this.slider;
            newScript.SetPositionHistory(this.GetPositionHistory());
            newScript.replacementPrefabs = this.replacementPrefabs;
            newScript.replacementIndex = this.replacementIndex;
        }

        // �J�����̒Ǐ]�Ώۂ��X�V����
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            
            FollowPlayerScript followScript = mainCamera.GetComponent<FollowPlayerScript>();
            if (followScript != null)
            {
                followScript.SetTarget(newObj.transform);
            }
            
        }

        //�Ō�Ɏ����������I
        Destroy(this.gameObject);

        return newObj;
    }
    public void ObjectChanged(GameObject newObject) //�V�����I�u�W�F�N�g�ɃX���C�_�[�����p�����g���폜
    {
        if (newObject == null) return;

        GameObject nextPrefab = replacementPrefabs[replacementIndex];
        replacementIndex = (replacementIndex + 1) % replacementPrefabs.Length;

        Vector3 spawnPosition = transform.position;
        GameObject newObj = Instantiate(nextPrefab, spawnPosition, Quaternion.identity);

        //�����Ŋp�x�������p��
        newObj.transform.rotation = this.transform.rotation;

        var newSliderScript = newObject.GetComponent<TimeSlider_P>();
        if (newSliderScript != null)
        {
            newSliderScript.slider = this.slider;
            newSliderScript.SetPositionHistory(this.GetPositionHistory());
            newSliderScript.replacementPrefabs = this.replacementPrefabs;
            newSliderScript.replacementIndex = this.replacementIndex;
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
    private void TryRevertObject()
    {
        Debug.LogWarning($"[TryRevert] Current replacementIndex: {replacementIndex}");

        if (replacementIndex > 0)
        {
            GameObject prevPrefab = replacementPrefabs[replacementIndex - 1];

            if (prevPrefab == null)
            {
                Debug.LogError("�߂낤�Ƃ���Prefab��null�ł��I");
                return;
            }

            Vector3 spawnPosition = transform.position;
            spawnPosition.y += 0.5f;
            Quaternion spawnRotation = Quaternion.Euler(90f, 90f, -90f);

            GameObject newObj = Instantiate(prevPrefab, spawnPosition, spawnRotation);
            Debug.Log($"[TryRevert] Instantiated: {newObj.name}");

            TimeSlider_P newScript = newObj.GetComponent<TimeSlider_P>();
            if (newScript != null)
            {
                newScript.slider = this.slider;
                newScript.SetPositionHistory(this.GetPositionHistory());
                newScript.replacementPrefabs = this.replacementPrefabs;
                newScript.replacementIndex = this.replacementIndex - 1;
            }

            //�����ŃX���C�_�[���Ɂu�V�����v���C���[�v��������I
            /*
            var counter = slider.GetComponent<SliderTimeCounter>();
            if (counter != null)
            {
                counter.SetCurrentPlayer(newObj);
            }*/

            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                
                FollowPlayerScript followScript = mainCamera.GetComponent<FollowPlayerScript>();
                if (followScript != null)
                {
                    followScript.SetTarget(newObj.transform);
                }
            }

            Debug.LogError("�掿����");

            StartCoroutine(DestroyAfterFrame());
        }
        else
        {
            Debug.LogError(replacementIndex);
            Debug.LogError("����ȏ�߂�Ȃ��I");
        }
    }
    private System.Collections.IEnumerator DestroyAfterFrame()
    {
        yield return null; // 1�t���[���҂��Ă���
        if (this != null)
        {
            Destroy(this.gameObject); // �O�̂���null�`�F�b�N����Destroy
        }
    }
    public void SetCurrentPlayer(GameObject player)
    {
        currentPlayer = player;
    }
}
