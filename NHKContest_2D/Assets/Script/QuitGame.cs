using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // ���̃��\�b�h���{�^���ɕR�Â��܂�
    public void Quit()
    {
        // �G�f�B�^�ƃr���h�œ���𕪂���
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
