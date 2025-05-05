using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FilmGrainToggle : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;

    private FilmGrain filmGrain;
    private ColorAdjustments colorAdjustments;

    bool intensityFlag = false;
    bool SliderMoveFlag = false; //�X���C�_�[���������ꂽ���̃t���O

    void Start()
    {

        globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        if (globalVolume != null && globalVolume.profile.TryGet<FilmGrain>(out filmGrain))
        {
            // ������������
        }
        else
        {
            Debug.LogWarning("Film Grain��Volume�ɐݒ肳��Ă��܂���I");
        }
    }

    void Update()
    {
        if (SliderMoveFlag)
        {
            if (filmGrain != null)
            {
                filmGrain.intensity.Override(1f);  // �m�C�Y���ő��
                colorAdjustments.postExposure.Override(0.0f);//��ʂ�����ƈÂ߂�
                colorAdjustments.saturation.Override(20.0f);//��ʂ̐F�ʋ��߂�
            }
        }

        if (Input.GetKeyDown(KeyCode.H))//�f�o�b�N�p�Ƀ{�^���ł̐؂�ւ��͎c���Ă���
        {
            if (intensityFlag)
            {
                intensityFlag = false;
                if (filmGrain != null)
                {
                    filmGrain.intensity.Override(1f);  // �m�C�Y���ő��
                    colorAdjustments.postExposure.Override(0.0f);//��ʂ�����ƈÂ߂�
                    colorAdjustments.saturation.Override(20.0f);//��ʂ̐F�ʋ��߂�
                    Debug.Log("H�L�[�FFilm Grain ���x = 1");
                }

            }
            else
            {
                intensityFlag = true;
                if (filmGrain != null)
                {
                    filmGrain.intensity.Override(0f);  // �m�C�Y������
                    colorAdjustments.postExposure.Override(0.5f);//��ʂ̖��邳��������
                    colorAdjustments.saturation.Override(0.0f);//��ʂ̐F�ʖ߂�
                    Debug.Log("H�L�[�FFilm Grain ���x = 1");
                }
            }
        }
    }

    public void SliderMovedControl()//�X���C�_�[���������Ƃ��̏���
    {
        //Debug.LogWarning("�X���C�_�[�����J�n(�J�����G�t�F�N�g)");
        if (!SliderMoveFlag)//���ɓ����֐��̎��s���Ȃ瓮���Ȃ��悤�ɂ���
        {
            PauseFollowing(0.2f);

        }
    }

    public void PauseFollowing(float pauseDuration)//�w��b���ԃJ�����Ǐ]�~�߂�֐�
    {
        StartCoroutine(PauseCoroutine(pauseDuration));
    }

    public void EfectReset()//�J�����ɂ��Ă���G�t�F�N�g�������̏�Ԃɖ߂�
    {
        if (filmGrain != null)
        {
            filmGrain.intensity.Override(0f);  // �m�C�Y������
            colorAdjustments.postExposure.Override(0.5f);//��ʂ̖��邳��������
            colorAdjustments.saturation.Override(0.0f);//��ʂ̐F�ʖ߂�
        }
    }

    private System.Collections.IEnumerator PauseCoroutine(float pauseDuration)
    {
        SliderMoveFlag = true;
        //colorAdjustments.saturation.Override(50.0f);//��ʂ̐F�ʂŃe�X�g����
        Debug.LogWarning("�J�����G�t�F�N�g�J�n(�J�����G�t�F�N�g)");
        yield return new WaitForSeconds(pauseDuration);//�w��b���ԑ҂����������鏈��
        SliderMoveFlag = false;
        EfectReset();
        Debug.LogWarning("�J�����G�t�F�N�g�I��(�J�����G�t�F�N�g)");
    }
}
