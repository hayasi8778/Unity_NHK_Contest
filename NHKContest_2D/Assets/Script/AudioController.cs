using UnityEditor;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [System.Serializable]
    public struct AudioData
    {
        [Header("����")]
        public AudioClip clip;

        [Header("����")]
        [Range(0f, 1f)]
        public float volume;
        public bool mute;

        [Header("�Q�[���J�n���ɍĐ����邩/���[�v���邩�iBGM�Ȃ�ON�j")]
        public bool playOnAwake;
        public bool loop;
    }

    [Header("�I�[�f�B�I�ݒ胊�X�g")]
    public AudioData[] audioDataList;
    private AudioSource[] audioSources;

    void Start()
    {
        audioSources = new AudioSource[audioDataList.Length];

        // �e�l��AudioSource�ɐݒ�
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
            // Volume�������A���^�C���ɍX�V�i����Start���̂݁j
            audioSources[i].volume = audioDataList[i].volume;
        }
    }

    // ����Script�ōĐ��Ƃ��ł���Ȃ�擾����K�v�Ȃ��ˊ�
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