using System;
using System.Xml;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private float rayDistance = 10;

    [SerializeField]
    private int rayCount = 8;

    [SerializeField]
    private float minCameraDistance = 0;

    private void Update()
    {
        Vector3 centerPos = new Vector3(0, 0, 0);
        Vector3 targetPos = GameObject.FindWithTag("Player").transform.position;
        float avgHitDistance = 0;

        for (int i = 0; i < rayCount; i++)
        {
            Vector3 dir = Quaternion.Euler(0, 0, i * 360f / rayCount) * Vector3.up;
            Ray ray = new Ray(targetPos, dir);
            Debug.DrawRay(targetPos, dir * rayDistance);
            RaycastHit hit = new RaycastHit();

            var layerMask = 1 << 6;
            if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
            {
                centerPos += hit.point;
                avgHitDistance += hit.distance;
            }
            else
            {
                centerPos += dir * rayDistance + targetPos;
                avgHitDistance += rayDistance;
            }
        }

        centerPos /= rayCount;
        avgHitDistance /= rayCount;

        Vector3 cameraPos = centerPos;
        cameraPos.z = -avgHitDistance * 2;

        transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * 2f);
    }
}
