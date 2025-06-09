using UnityEngine;

public class MirrorRay : MonoBehaviour
{
    public Vector2 MirrorRayDirection = Vector2.right; // 鏡が反射する固定方向

    public void ReflectRay(Vector2 hitPoint, int remainingReflections)
    {
        if (remainingReflections <= 0) return;

        LightRay lightRay = Object.FindFirstObjectByType<LightRay>(); //`LightRay` のインスタンスを取得
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
