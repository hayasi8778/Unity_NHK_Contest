using UnityEngine;

[CreateAssetMenu(fileName = "Cassettes", menuName = "Scriptable Objects/Cassettes")]
public class Cassettes : ScriptableObject
{
    [SerializeField]
    public GameObject[] cassettes;
}
