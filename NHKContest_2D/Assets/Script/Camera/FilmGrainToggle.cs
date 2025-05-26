using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FilmGrainToggle : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;

    private FilmGrain filmGrain;
    private ColorAdjustments colorAdjustments;

    bool intensityFlag = false;
    bool SliderMoveFlag = false; //スライダーが動かされたかのフラグ

    void Start()
    {

        globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
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
        if (SliderMoveFlag)
        {
            if (filmGrain != null)
            {
                filmGrain.intensity.Override(1f);  // ノイズを最大に
                colorAdjustments.postExposure.Override(0.0f);//画面ちょっと暗めに
                colorAdjustments.saturation.Override(20.0f);//画面の色彩強めで
            }
        }

        if (Input.GetKeyDown(KeyCode.H))//デバック用にボタンでの切り替えは残しておく
        {
            if (intensityFlag)
            {
                intensityFlag = false;
                if (filmGrain != null)
                {
                    filmGrain.intensity.Override(1f);  // ノイズを最大に
                    colorAdjustments.postExposure.Override(0.0f);//画面ちょっと暗めに
                    colorAdjustments.saturation.Override(20.0f);//画面の色彩強めで
                    Debug.Log("Hキー：Film Grain 強度 = 1");
                }

            }
            else
            {
                intensityFlag = true;
                if (filmGrain != null)
                {
                    filmGrain.intensity.Override(0f);  // ノイズを消す
                    colorAdjustments.postExposure.Override(0.5f);//画面の明るさを初期に
                    colorAdjustments.saturation.Override(0.0f);//画面の色彩戻す
                    Debug.Log("Hキー：Film Grain 強度 = 1");
                }
            }
        }
    }

    public void SliderMovedControl()//スライダー動かしたときの処理
    {
        //Debug.LogWarning("スライダー同期開始(カメラエフェクト)");
        if (!SliderMoveFlag)//既に同じ関数の実行中なら動かないようにする
        {
            PauseFollowing(0.2f);

        }
    }

    public void PauseFollowing(float pauseDuration)//指定秒数間カメラ追従止める関数
    {
        StartCoroutine(PauseCoroutine(pauseDuration));
    }

    public void EfectReset()//カメラについているエフェクトを初期の状態に戻す
    {
        if (filmGrain != null)
        {
            filmGrain.intensity.Override(0f);  // ノイズを消す
            colorAdjustments.postExposure.Override(0.5f);//画面の明るさを初期に
            colorAdjustments.saturation.Override(0.0f);//画面の色彩戻す
        }
    }

    private System.Collections.IEnumerator PauseCoroutine(float pauseDuration)
    {
        SliderMoveFlag = true;
        //colorAdjustments.saturation.Override(50.0f);//画面の色彩でテストする
        Debug.LogWarning("カメラエフェクト開始(カメラエフェクト)");
        yield return new WaitForSeconds(pauseDuration);//指定秒数間待ちが発生する処理
        SliderMoveFlag = false;
        EfectReset();
        Debug.LogWarning("カメラエフェクト終了(カメラエフェクト)");
    }
}
