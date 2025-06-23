using System;
using UnityEngine;

[Serializable]
public class sData
{
    public Sprite preview;
    public string sceneName;
}

public class StageData : MonoBehaviour
{
    [SerializeField]
    public sData[] stageData;
}