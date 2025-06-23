using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Scrollbar scrollbar;
    [HideInInspector]
    public bool isMouseOver = false; // �}�E�X�I�[�o�[��Ԃ��Ǘ�����ϐ�

    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        scrollbar.value = 1; // �����l��1�ɐݒ�i����100%�j

        gameObject.SetActive(false); // ������Ԃł͉��ʃo�[���\���ɂ���
    }

    void Update()
    {
        AudioListener.volume = scrollbar.value; // �X�N���[���o�[�̒l�����ʂɔ��f
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
