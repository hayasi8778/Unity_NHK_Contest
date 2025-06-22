using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject DarkOverRay; // �Â��w�i
    public GameObject menuUI;
    Animator manuAnimator; // �����j���[�̃A�j���[�^�[
    private bool isPaused = false;

    void Start()
    {
        manuAnimator = menuUI.GetComponent<Animator>();
        DarkOverRay.SetActive(false); // ������Ԃł͈Â��w�i���\���ɂ���i�Ӗ��˂��I�Ȃ�ŁH�H�H�j
    }

    public void GamePause()
    {
        Debug.Log("�I�u�W�F�N�g���N���b�N����܂����I");

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;    // isPaused��true�Ȃ玞�Ԃ��~�߂�Afalse�Ȃ玞�Ԃ�i�߂�
        DarkOverRay.SetActive(isPaused);

        if(isPaused)
        {
            // �Q�[�����ꎞ��~���ꂽ�Ƃ��̏���
            Debug.Log("�Q�[�����ꎞ��~����܂����B");
            manuAnimator.SetTrigger("Pause_on");
        }
        else
        {
            // �Q�[�����ĊJ���ꂽ�Ƃ��̏���
            Debug.Log("�Q�[�����ĊJ����܂����B");
            manuAnimator.SetTrigger("Pause_off");
        }
    }
}
