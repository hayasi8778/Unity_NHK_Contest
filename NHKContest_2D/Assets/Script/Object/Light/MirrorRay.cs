using UnityEngine;

public class MirrorRay : MonoBehaviour
{
    public Vector2 MirrorRayDirection = Vector2.right; // �������˂���Œ����

    public void ReflectRay(Vector2 hitPoint, int remainingReflections)
    {
        if (remainingReflections <= 0) return;

        LightRay lightRay = Object.FindFirstObjectByType<LightRay>(); //`LightRay` �̃C���X�^���X���擾
        if (lightRay != null)
        {
            lightRay.CastRay(hitPoint, MirrorRayDirection, remainingReflections);
        }
        else
        {
            Debug.LogError("LightRay instance not found!");
        }
    }



}
