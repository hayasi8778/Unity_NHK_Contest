using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PressPlate : MonoBehaviour
{
    [SerializeField]
    // �����ꂽ���̑傫��
    private float onSize = 0.25f;
    [SerializeField]
    // �f�t�H�̑傫��
    private float offSize = 0.5f;
    
    // �O�̏��
    private bool oldPress = false;
    // ���̏��
    public bool press = false;

    private void Start()
    {
        oldPress = false;
        press = false;
    }

    private void Update()
    {
        // OFF����ON�ɐ؂�ւ�鎞
        if (press && !oldPress)
        {
            Vector3 pos = transform.position;
            pos.y += (onSize - offSize) / 2;
            transform.position = pos;

            Vector3 scale = transform.localScale;
            scale.y = onSize;
            transform.localScale = scale;
        }
        // ON����OFF�ɐ؂�ւ�鎞
        if (!press && oldPress)
        {
            Vector3 pos = transform.position;
            pos.y += (offSize - onSize) / 2;
            transform.position = pos;

            Vector3 scale = transform.localScale;
            scale.y = offSize;
            transform.localScale = scale;
        }

        oldPress = press;
    }
}
