using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderOnMoveInvalidation : Slider
{
    public override void OnMove(AxisEventData eventData)
    {
        // �f�t�H���g��OnMove�����𖳌���
        Debug.Log("OnMove�C�x���g������������܂���");
        // �K�v�ɉ����ēƎ��̏������L�q�ł��܂�
        // base.OnMove(eventData); // �f�t�H���g�̋������ێ��������ꍇ
    }
}
