using UnityEngine;

public class LightRay : MonoBehaviour
{

    public GameObject platformPrefab; // 足場のプレハブ
    public float rayDistance = 5f; // レイの長さ
    public string mirrorTag = "Mirror"; // 鏡のタグ（反射用）
    public string ignoreTag = "LightObject"; // 光線オブジェクトのタグ（レイキャストの対象外）
    public Vector2 customRayDirection = Vector2.left; // レイの方向(初期は左側)
    public int maxReflections = 3; // 最大反射回数

    bool LightColFlag = false; //光の生成判定
    //bool LightColFlag = true; //光の生成判定

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !LightColFlag) // スペースキーで光線を生成
        {
            CastRay(transform.position, customRayDirection, maxReflections);
        }

    }

    void CastRay(Vector2 origin, Vector2 direction, int remainingReflections)
    {
        if (remainingReflections <= 0) return; // 最大反射回数を超えたら終了

        RaycastHit2D hit = Physics2D.Raycast(origin, direction.normalized, rayDistance);

        if (hit.collider != null) // 何かにヒットしたか確認
        {
            // 無視するタグならスキップ
            if (hit.collider.CompareTag(ignoreTag))
            {
                CastRay(hit.point, direction, remainingReflections);
                return;
            }

            Debug.Log($"Ray hit object: {hit.collider.gameObject.name} at {hit.point}, Distance: {hit.distance}");

            // ミラーに当たった場合
            if (hit.collider.CompareTag(mirrorTag))
            {
                // ミラーに当たった時点で光線オブジェクトを生成
                CreateLightObject(origin, hit.point);

                // **反射方向を計算**
                Vector2 newDirection = Vector2.Reflect(direction, hit.normal);

                // **新しいレイを出す（再帰的に反射を続ける）**
                CastRay(hit.point, newDirection, remainingReflections - 1);
            }
            else
            {
                // 足場を生成（鏡以外に当たったらそこで光線をオブジェクト化）
                CreateLightObject(origin, hit.point);
            }
        }

        // デバッグ用にレイを表示
        Debug.DrawRay(origin, direction.normalized * rayDistance, Color.yellow, 10f);

        //2回目の生成を切る
        LightColFlag = true;
    }

    void CreateLightObject(Vector2 startPoint, Vector2 endPoint)
    {
        // 中間地点を計算
        Vector2 midPoint = (startPoint + endPoint) / 2;

       

        // 衝突地点ではなく中間地点にオブジェクトを生成
        GameObject platform = Instantiate(platformPrefab, midPoint, Quaternion.identity);
        platform.tag = ignoreTag; // 生成したオブジェクトに無視するタグを設定

        // レイの方向を取得
        Vector2 direction = endPoint - startPoint;

        // 角度を計算
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

        // 横幅（X軸のスケール）をレイの距離に変更
        Vector3 newScale = platform.transform.localScale;
        newScale.x = Vector2.Distance(startPoint, endPoint);  // 横幅をレイの距離に
        platform.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Z軸回転のみ適用
        platform.transform.localScale = newScale;
    }

}
