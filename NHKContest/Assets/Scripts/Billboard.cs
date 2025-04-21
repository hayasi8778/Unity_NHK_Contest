using UnityEngine;

public class Billboard : MonoBehaviour
{
    //板ポリをカメラの方に向けるためのプログラム
    void Update()
    {
        // メインカメラの方向を取得して、板をカメラに向ける
        if (Camera.main != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            transform.LookAt(cameraPos);

            // Y軸の固定(一旦切る)
            //Vector3 euler = transform.eulerAngles;
            //transform.eulerAngles = new Vector3(0, euler.y, 0);
        }
    }
}
