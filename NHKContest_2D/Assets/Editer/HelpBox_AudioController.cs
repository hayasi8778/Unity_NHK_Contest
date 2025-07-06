using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioController))]
[CanEditMultipleObjects]
public class AudioControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("AudioController は複数の音源（SE/BGM）をまとめて管理します。\n\n" +
                                //"GetAudioSourceArray() : インスペクターで設定したAudioSorce配列の取得関数です。\n\n" +
                                "AudioSorceの再生状況に関するメソッド\n" +
                                "Play(int Index)     : 音源を戦闘から再生\n"+
                                "Stop(int Index)     : 再生を完全に停止\n" +
                                "Pause(int Index)    : 再生を一時停止\n" +
                                "UnPause(int Index)  : 一時停止を解除\n" +
                                "PlayOneShot(int Index)  : SE等の複数回重なる音の再生\n" +
                                "GetIsPlaying(int Index)  : 現在の再生状況", MessageType.None);

        base.OnInspectorGUI();
    }
}