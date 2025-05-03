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
            // 初期化が成功
        }
        else
        {
            Debug.LogWarning("Film GrainがVolumeに設定されていません！");
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
                    filmGrain.intensity.Override(1f);  // ノイズを最大に
                    Debug.Log("Hキー：Film Grain 強度 = 1");
                }

            }
            else
            {
                intensityFlag = true;
                if (filmGrain != null)
                {
                    filmGrain.intensity.Override(0f);  // ノイズを消す
                    Debug.Log("Hキー：Film Grain 強度 = 1");
                }
            }
        }
    }
}
