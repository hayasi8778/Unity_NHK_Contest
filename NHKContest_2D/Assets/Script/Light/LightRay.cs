using UnityEngine;

public class LightRay : MonoBehaviour
{

    public GameObject platformPrefab; // 足場のプレハブ
    public float rayDistance = 5f; // レイの長さ
    public string targetTag = "PlatformTarget"; // 足場を生成する対象のタグ
    public Vector2 customRayDirection = Vector2.left; // レイの方向(初期は左側)
    bool LightColFlag = false; //ライトが他のオブジェクトに当たって足場を生成したかのフラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !LightColFlag) // スペースキーで足場を生成
        {
            // **オブジェクトの左端の位置を計算**
            float leftEdgeX = transform.position.x - (GetComponent<SpriteRenderer>().bounds.size.x / 2f);
            Vector2 rayOrigin = new Vector2(leftEdgeX, transform.position.y); // 左端の座標を作成

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, customRayDirection.normalized, rayDistance);

            if (hit.collider != null) // 何かにヒットしたか確認
            {
                Debug.Log($"Ray hit object: {hit.collider.gameObject.name} at {hit.point}, Distance: {hit.distance}");

                // タグ判定
                if (hit.collider.CompareTag(targetTag))
                {
                    // 中間地点を計算
                    Vector2 midPoint = (rayOrigin + hit.point) / 2;

                    // 衝突地点ではなく中間地点にオブジェクトを生成
                    GameObject platform = Instantiate(platformPrefab, midPoint, Quaternion.identity);

                    // 横幅（X軸のスケール）をレイの距離に変更
                    Vector3 newScale = platform.transform.localScale;
                    newScale.x = hit.distance;  // 横幅をレイの距離に
                    platform.transform.localScale = newScale;

                    //ライトの生成済みフラグを立てる
                    LightColFlag = true;
                }
            }
            else
            {
                Debug.Log("Ray did NOT hit anything.");
            }



            // デバッグ用にレイを表示
            Debug.DrawRay(transform.position, customRayDirection.normalized * rayDistance, Color.yellow, 10f);
        }


    }
}
