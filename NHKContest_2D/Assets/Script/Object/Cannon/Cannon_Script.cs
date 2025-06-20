using System.Collections.Generic;
using UnityEngine;


public class Cannon_Script : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int resolution = 50;
    public float launchSpeed = 10f;
    public float launchAngle = 45f;
    public float gravity = 9.81f;
    public Material dashedMaterial;

    private bool hitDetected = false;
    private int hitIndex = -1;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = resolution;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true;

        if (dashedMaterial != null)
        {
            lineRenderer.material = dashedMaterial;
            lineRenderer.textureMode = LineTextureMode.RepeatPerSegment;
        }
        else
        {
            Debug.LogError("dashedMaterial が設定されていません！");
        }
    }

    void Update()
    {
        DrawTrajectory();
        UpdateTextureScale();
    }

    void DrawTrajectory()
    {
        List<Vector3> points = new List<Vector3>();
        float radianAngle = Mathf.Deg2Rad * launchAngle;
        float vX = launchSpeed * Mathf.Cos(radianAngle);
        float vY = launchSpeed * Mathf.Sin(radianAngle);
        Vector3 startPosition = transform.position;

        for (int i = 0; i < resolution; i++)
        {
            if (hitDetected && i >= hitIndex) break; // 衝突後は描画しない

            float t = i * 0.1f;
            float x = vX * t;
            float y = vY * t - 0.5f * gravity * t * t;
            Vector3 point = startPosition + new Vector3(x, y, 0);

            points.Add(point);

            RaycastHit2D hit = CheckCollision(startPosition, point, i);
            if (!hitDetected && hit.collider != null) // hit を明示的に受け取る
            {
                hitDetected = true;
                hitIndex = i;
                Debug.Log($"衝突検出: {hit.collider.name} at {hit.point}, インデックス: {hitIndex}");
                break;
            }
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    RaycastHit2D CheckCollision(Vector3 startPoint, Vector3 endPoint, int index)
    {
        Vector2 direction = (endPoint - startPoint).normalized;
        float distance = Vector3.Distance(startPoint, endPoint);
        int layerMask = LayerMask.GetMask("Default", "CollisionLayer");

        //RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, distance * 1.5f, layerMask);
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, float.MaxValue, layerMask);

        // レイを可視化
        Debug.DrawRay(startPoint, direction * distance, Color.red, float.MaxValue, false);

        // **もし衝突したオブジェクトが "Cannon1" なら無視**
        if (hit.collider != null && hit.collider.name == "Cannon1")
        {
            //Debug.Log("Ignored collision with: " + hit.collider.name);
            return new RaycastHit2D(); // 空の `RaycastHit2D` を返して判定を無視
        }

        if (hit.collider != null)
        {
            Debug.Log("Collision detected with: " + hit.collider.name + " at position: " + hit.point);
        }

        return hit;//hitを返す

    }


    void UpdateTextureScale()
    {
        float totalLength = 0f;

        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            totalLength += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
        }

        float textureRepeats = totalLength / 2f;
        lineRenderer.material.SetTextureScale("_MainTex", new Vector2(textureRepeats, 1f));
    }

}
