using UnityEngine;

public class LightRay : MonoBehaviour
{

    public GameObject platformPrefab; // 足場のプレハブ
    public float rayDistance = 5f; // レイの長さ
    public string MirrorTag = "Mirror"; // 鏡のタグ（反射用）
    public string GoalTag = "Goal"; // 鏡のタグ（反射用）
    public string LightTag = "ground"; //作成した足場のタグ
    public Vector2 customRayDirection = Vector2.left; // 初期レイ方向
    public int maxReflections = 100; // 最大反射回数

    bool LightFlag = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !LightFlag) // スペースキーで光線を生成
        {
            CastRay(transform.position, customRayDirection, maxReflections);
            LightFlag = true;
        }
    }

    public void CastRay(Vector2 origin, Vector2 direction, int remainingReflections)
    {
        if (remainingReflections <= 0) return;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction.normalized, rayDistance);
        RaycastHit2D validHit = default;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && !hit.collider.CompareTag(LightTag) && hit.distance > 0.01f) // ✅ `LightTag` を持つオブジェクトを透過 & 長さ 0 を防ぐ
            {
                validHit = hit;
                break;
            }
        }

        if (validHit.collider != null)
        {
            //Debug.Log($"Ray hit object: {validHit.collider.gameObject.name} at {validHit.point}, Distance: {validHit.distance}");

            // **長さ 0 のオブジェクトを防ぐ**
            if (validHit.distance > 0.01f)
            {
                CreateLightObject(origin, validHit.point);
            }
            else
            {
                Debug.Log("Skipping platform creation: Distance too small");
            }

            if (validHit.collider.CompareTag(MirrorTag)|| validHit.collider.CompareTag(GoalTag))
            {
                validHit.collider.GetComponent<MirrorRay>().ReflectRay(validHit.point, remainingReflections - 1);
            }
        }

        Debug.DrawRay(origin, direction.normalized * rayDistance, Color.yellow, 10f);
    }



    void CreateLightObject(Vector2 startPoint, Vector2 endPoint)
    {
        // **中間地点を計算**
        Vector2 midPoint = (startPoint + endPoint) / 2;

        // **オブジェクトを生成**
        GameObject platform = Instantiate(platformPrefab, midPoint, Quaternion.identity);

        // **オブジェクトの回転をレイの角度に合わせる**
        Vector2 direction = endPoint - startPoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        platform.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // **オブジェクトの横幅をレイの距離に変更**
        Vector3 newScale = platform.transform.localScale;
        newScale.x = Vector2.Distance(startPoint, endPoint);
        platform.transform.localScale = newScale;
    }


}
