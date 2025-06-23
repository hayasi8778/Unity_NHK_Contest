using UnityEngine;
using UnityEngine.EventSystems;

public class Sound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("����")]
    public GameObject[] SoundVolume_Icon;
    public GameObject soundBar;
    [HideInInspector]
    public bool isMouseOver = false; // �}�E�X�I�[�o�[��Ԃ��Ǘ�����ϐ�

    public void Update()
    {
        if (isMouseOver || soundBar.GetComponent<SoundBar>().isMouseOver)
        {
            // �}�E�X�I�[�o�[��Ԃ܂��͉��ʃo�[���}�E�X�I�[�o�[��Ԃ̏ꍇ�A���ʃo�[��\��
            soundBar.SetActive(true);
        }
        else
        {
            // �ǂ�����}�E�X�I�[�o�[��ԂłȂ��ꍇ�A���ʃo�[���\��
            soundBar.SetActive(false);
        }

        if (AudioListener.volume == 0)
        {
            // ���ʂ�0�̏ꍇ
            SoundVolume_Icon[0].SetActive(true);
            SoundVolume_Icon[1].SetActive(false);
            SoundVolume_Icon[2].SetActive(false);
        }
        else if (AudioListener.volume <= 0.5)
        {
            // ���ʂ�0���傫���A0.5�����̏ꍇ
            SoundVolume_Icon[0].SetActive(false);
            SoundVolume_Icon[1].SetActive(true);
            SoundVolume_Icon[2].SetActive(false);
        }
        else
        {
            // ���ʂ�0.5�ȏ�̏ꍇ
            SoundVolume_Icon[0].SetActive(false);
            SoundVolume_Icon[1].SetActive(false);
            SoundVolume_Icon[2].SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }
}

