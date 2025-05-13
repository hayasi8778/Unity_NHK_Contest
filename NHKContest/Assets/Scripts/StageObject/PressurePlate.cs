using System.Data;
using Unity.VisualScripting;
using UnityEditor.AssetImporters;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private PressPlate pressPlate;

    private string objectName = "TestObject3(Clone)";

    private void Start()
    {
        pressPlate = transform.GetChild(0).GetComponent<PressPlate>();
    }
    private void OnTriggerStay(Collider other)
    {
        pressPlate.press = false;
        if (other.name == objectName)
        {
            pressPlate.press = true;
        }
    }
}
