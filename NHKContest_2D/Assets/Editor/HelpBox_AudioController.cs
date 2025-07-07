using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioController))]
[CanEditMultipleObjects]
public class AudioControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("AudioController �͕����̉����iSE/BGM�j���܂Ƃ߂ĊǗ����܂��B\n\n" +
                                //"GetAudioSourceArray() : �C���X�y�N�^�[�Őݒ肵��AudioSorce�z��̎擾�֐��ł��B\n\n" +
                                "AudioSorce�̍Đ��󋵂Ɋւ��郁�\�b�h\n" +
                                "Play(int Index)     : ������퓬����Đ�\n"+
                                "Stop(int Index)     : �Đ������S�ɒ�~\n" +
                                "Pause(int Index)    : �Đ����ꎞ��~\n" +
                                "UnPause(int Index)  : �ꎞ��~������\n" +
                                "PlayOneShot(int Index)  : SE���̕�����d�Ȃ鉹�̍Đ�\n" +
                                "GetIsPlaying(int Index)  : ���݂̍Đ���", MessageType.None);

        base.OnInspectorGUI();
    }
}