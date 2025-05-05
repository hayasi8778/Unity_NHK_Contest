using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayerScript : MonoBehaviour
{
    // 参照したいオブジェクトを格納する変数
    [SerializeField]
    public Transform target;
    public Vector3 offset;

    private bool isFollowing = true; //カメラの追従フラグ

    void LateUpdate()
    {
        if (isFollowing && target != null)
        {
            // 補間移動でカメラに滑らかさと速度を持たせる
            Vector3 desiredPosition = target.position + offset;
            //Lerp使ってモーションブラーに対応できるようにする
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10f);
        }
    }

    public void SetTarget(Transform newTarget)//オブジェクト切り替えても継承するための関数
    {
        target = newTarget;
        PauseFollowing(0.5f);//0.5秒間カメラの追従止める
    }

    public void PauseFollowing(float pauseDuration)//指定秒数間カメラ追従止める関数
    {
        StartCoroutine(PauseCoroutine(pauseDuration));
    }

    private System.Collections.IEnumerator PauseCoroutine(float pauseDuration)
    {
        isFollowing = false;
        yield return new WaitForSeconds(pauseDuration);//指定秒数間待ちが発生する処理
        isFollowing = true;
    }

}

