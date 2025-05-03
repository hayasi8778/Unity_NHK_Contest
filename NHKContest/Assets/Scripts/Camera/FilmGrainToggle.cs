using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FilmGrainToggle : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;

    private FilmGrain filmGrain;

    bool intensityFlag = false;

    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (intensityFlag)
            {
                intensityFlag = false;
                if (filmGrain != null)
                {
                    filmGrain.intensity.Override(1f);  // �m�C�Y���ő��
                    Debug.Log("H�L�[�FFilm Grain ���x = 1");
                }

            }
            else
            {
                intensityFlag = true;
                if (filmGrain != null)
                {
                    filmGrain.intensity.Override(0f);  // �m�C�Y������
                    Debug.Log("H�L�[�FFilm Grain ���x = 1");
                }
            }
        }
    }
}
