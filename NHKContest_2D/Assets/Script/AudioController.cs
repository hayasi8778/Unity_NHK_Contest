using UnityEngine;

public class AudioController : MonoBehaviour
{
    [System.Serializable]
    public class AudioData
    {
        [Header("音源")]
        public AudioClip clip;

        [Header("音量")]
        [Range(0f, 1f)]
        public float volume = 1f;
        public bool mute = false;

        [Header("ゲーム開始時に再生するか/ループするか（BGMならON）")]
        public bool playOnAwake = false;
        public bool loop = false;
    }

    [Header("オーディオ設定リスト")]
    public AudioData[] audioDataList;

    private AudioSource[] audioSources;

    void Start()
    {
        int count = audioDataList.Length;
        audioSources = new AudioSource[count];

        for (int i = 0; i < count; i++)
        {
            AudioData data = audioDataList[i];
            AudioSource source = gameObject.AddComponent<AudioSource>();

            source.clip = data.clip;
            source.volume = data.volume;
            source.mute = data.mute;
            source.playOnAwake = data.playOnAwake;
            source.loop = data.loop;

            if (data.playOnAwake && data.clip != null) source.Play();

            audioSources[i] = source;
        }
    }

    void Update()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] != null) audioSources[i].volume = audioDataList[i].volume;
        }
    }

    // インデックスの妥当性をチェックするヘルパーメソッド
    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < audioSources.Length;
    }

    public void Play(int index)
    {
        if (IsValidIndex(index) && audioSources[index].clip != null) audioSources[index].Play();
    }

    public void Stop(int index)
    {
        if (IsValidIndex(index)) audioSources[index].Stop();
    }

    public void Pause(int index)
    {
        if (IsValidIndex(index)) audioSources[index].Pause();
    }

    public void UnPause(int index)
    {
        if (IsValidIndex(index)) audioSources[index].UnPause();
    }

    public void PlayOneShot(int index)
    {
        if (IsValidIndex(index) && audioDataList[index].clip != null) audioSources[index].PlayOneShot(audioDataList[index].clip);
    }

    public bool GetIsPlaying(int index)
    {
        return IsValidIndex(index) && audioSources[index].isPlaying;
    }
}