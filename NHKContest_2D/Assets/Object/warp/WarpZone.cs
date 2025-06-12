using System.Collections;
using UnityEngine;

/*
WarpZone2D.cs

【概要】
このスクリプトは、指定されたタグ（Warp1, Warp2, Warp3）を持つワープゾーンに
プレイヤーや特定のオブジェクトが触れた際に、設定された遅延時間の後に
同じタグを持つ別のワープ地点へワープさせる機能を実装しています。

- Warp1は即時ワープ
- Warp2は2秒遅延ワープ
- Warp3は6秒遅延ワープ

ワープ中は対象オブジェクトを透明（非表示）にし、
プレイヤーであれば移動を禁止します。

ワープ先は同じタグの別オブジェクトの位置で、  
プレイヤーはそのままの位置に、  
プレイヤー以外のオブジェクトは少し先にオフセットしてワープします。

SpriteRendererが無いオブジェクトには
Rendererコンポーネントを使って透明化処理を行います。

タグが遅延中に変わった場合は即座にそのタグに応じたワープを実行します。

---

【詳細な処理の流れ】

1. OnTriggerEnter2Dで、接触したオブジェクトがPlayerかObjectタグを持つか判定し、それ以外は無視。

2. ワープゾーンのタグ(Warp1/2/3)から遅延時間を決定。

3. 接触位置から「右から来たか左から来たか」を判定し、
   ワープ先を少しずらすための方向係数を +1 または -1 に設定。

4. コルーチンWarpAfterDelayを開始し、
   - プレイヤーの場合はPlayerMoveスクリプトで移動禁止に設定。
   - 対象オブジェクトのSpriteRendererかRendererを無効にして透明化。

5. 遅延中にタグが変わった場合は、タグが変わった瞬間に即座にワープを実行。

6. 遅延終了時に、同じタグの別のワープ地点を探し、
   プレイヤーはその位置に移動、
   それ以外はワープ先から方向に少しずらして移動。

7. 透明化を解除し、プレイヤーの移動禁止を解除。

---

【注意点】
- ワープ先が見つからない場合はワープしません。
- 対象オブジェクトはPlayerタグかObject1,2タグを必ず設定してください。
*/

public class WarpZone2D : MonoBehaviour
{
    // ワープ先からのオフセット量（左右方向）
    public Vector2 offset = new Vector2(-1.5f, 0f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ワープ対象かどうか判定（プレイヤーかWarpableObjectタグのみ）
        if (!other.CompareTag("Player") && !other.CompareTag("Object1") && !other.CompareTag("Object2"))
            return;

        // 現在のワープゾーンのタグから遅延時間を取得
        float delay = GetWarpDelayByTag(tag);

        // プレイヤー（またはオブジェクト）の位置とワープゾーンの位置の差分を計算
        Vector2 dir = other.transform.position - transform.position;

        // 右から来たなら +1、左から来たなら -1 を方向係数に設定
        int directionFactor = dir.x > 0 ? 1 : -1;

        // Warp2またはWarp3の場合はワープ中に透明化する
        bool makeInvisible = (tag == "Warp2" || tag == "Warp3");

        // ワープ開始時のタグを保持（遅延中にタグが変わったか確認用）
        string initialTag = tag;

        // 遅延後にワープする処理をコルーチンで開始
        StartCoroutine(WarpAfterDelay(other.gameObject, delay, directionFactor, makeInvisible, initialTag));
    }

    // タグに応じたワープ遅延時間を返す関数
    float GetWarpDelayByTag(string tag)
    {
        switch (tag)
        {
            case "Warp1": return 0f;  // 即時ワープ
            case "Warp2": return 2f;  // 2秒遅延
            case "Warp3": return 6f;  // 6秒遅延
            default: return 0f;
        }
    }

    // 同じタグの別のワープポイントを探して返す（自身は除く）
    Transform FindOtherWarpPointWithTag(string tag)
    {
        GameObject[] warps = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject warp in warps)
        {
            if (warp != this.gameObject)
                return warp.transform;
        }
        return null;  // 見つからなければnull
    }

    // 遅延後にワープ処理を行うコルーチン
    IEnumerator WarpAfterDelay(GameObject obj, float delay, int directionFactor, bool makeInvisible, string initialTag)
    {
        // プレイヤーの移動禁止を設定（PlayerMoveがある場合）
        var moveScript = obj.GetComponent<PlayerMove>();
        if (moveScript != null) moveScript.SetCanMove(false);

        // SpriteRendererを取得（子オブジェクトも含む）
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = obj.GetComponentInChildren<SpriteRenderer>();

        // SpriteRendererがない場合はRenderer（MeshRenderer等）を取得
        Renderer otherRenderer = obj.GetComponent<Renderer>();
        if (otherRenderer == null) otherRenderer = obj.GetComponentInChildren<Renderer>();

        // ワープ中に透明化（非表示）する場合
        if (makeInvisible)
        {
            if (sr != null) sr.enabled = false;
            else if (otherRenderer != null) otherRenderer.enabled = false;
        }

        bool warped = false;  // すでにワープしたかどうかのフラグ
        float elapsed = 0f;

        // 遅延時間分ループしながらタグの変化を監視
        while (elapsed < delay)
        {
            // タグが変わったら即座にワープを実行
            if (this.tag != initialTag && !warped)
            {
                Transform newDest = FindOtherWarpPointWithTag(this.tag);
                if (newDest != null)
                    obj.transform.position = GetWarpPosition(obj, newDest.position, directionFactor);

                // 透明化解除
                if (sr != null) sr.enabled = true;
                else if (otherRenderer != null) otherRenderer.enabled = true;

                // 移動可能に戻す
                if (moveScript != null) moveScript.SetCanMove(true);
                yield break;  // コルーチン終了
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 遅延終了後にワープ先を探す
        Transform dest = FindOtherWarpPointWithTag(initialTag);
        if (dest != null)
            obj.transform.position = GetWarpPosition(obj, dest.position, directionFactor);

        // 透明化解除
        if (sr != null) sr.enabled = true;
        else if (otherRenderer != null) otherRenderer.enabled = true;

        // 移動可能に戻す
        if (moveScript != null) moveScript.SetCanMove(true);
    }

    // ワープ先の位置を計算する（プレイヤー以外は少しオフセット）
    Vector2 GetWarpPosition(GameObject obj, Vector2 basePos, int directionFactor)
    {
        Vector2 baseOffset = offset * directionFactor;

        // プレイヤーでなければさらに少し前にオフセットを追加
        if (!obj.CompareTag("Player"))
        {
            baseOffset += new Vector2(-0.5f, 0f) * directionFactor;
        }

        return basePos + baseOffset;
    }
}
