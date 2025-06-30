using UnityEditor;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [System.Serializable]
    public struct AudioData
    {
        [Header("音源")]
        public AudioClip clip;

        [Header("音量")]
        [Range(0f, 1f)]
        public float volume;
        public bool mute;

        [Header("ゲーム開始時に再生するか/ループするか（BGMならON）")]
        public bool playOnAwake;
        public bool loop;
    }

    [Header("オーディオ設定リスト")]
    public AudioData[] audioDataList;
    private AudioSource[] audioSources;

    void Start()
    {
        audioSources = new AudioSource[audioDataList.Length];

        // 各値をAudioSourceに設定
        for (int i = 0; i < audioDataList.Length; i++)
        {
            var audioData = audioDataList[i];
            var audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.clip        = audioData.clip;
            audioSource.volume      = audioData.volume;
            audioSource.mute        = audioData.mute;
            audioSource.playOnAwake = audioData.playOnAwake;
            audioSource.loop        = audioData.loop;

            audioSources[i] = audioSource;
        }
    }

    void Update()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            // Volumeだけリアルタイムに更新（他はStart時のみ）
            audioSources[i].volume = audioDataList[i].volume;
        }
    }

    // このScriptで再生とかできるなら取得する必要なくね感
    //public AudioSource[] GetAudioSourceArray()
    //{
    //    return audioSources;
    //}

    public void Play(int Index)
    {
        if (audioSources[Index] != null) audioSources[Index].Play();
    }

    public void Stop(int Index)
    {
        if (audioSources[Index] != null) audioSources[Index].Stop();
    }

    public void Pause(int Index)
    {
        if (audioSources[Index] != null) audioSources[Index].Pause();
    }

    public void UnPause(int Index)
    {
        if (audioSources[Index] != null) audioSources[Index].UnPause();
    }

    public bool GetIsPlaying(int Index)
    {
        return audioSources[Index] != null && audioSources[Index].isPlaying;
    }
}