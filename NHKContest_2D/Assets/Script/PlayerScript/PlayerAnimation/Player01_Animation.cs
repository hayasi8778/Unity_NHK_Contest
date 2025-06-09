using UnityEngine;

public class Player01_Animation : PlayerMoveAmin
{
    public SpriteRenderer spriteRenderer; // スプライトレンダラー
    public Sprite[] sprites; // 切り替え用スプライトの配列
    private int Anim = 0; // 現在のアニメーションフレーム

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public override void ChangeSprite()
    {
        // フレームを更新
        Anim = (Anim + 1) % sprites.Length;

        // スプライトの変更
        spriteRenderer.sprite = sprites[Anim];
    }

}
