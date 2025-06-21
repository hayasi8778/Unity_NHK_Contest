using UnityEngine;

public class Player01_Animation : PlayerMoveAmin
{
    public SpriteRenderer spriteRenderer; // スプライトレンダラー
    public Sprite[] sprites; // 切り替え用スプライトの配列
    public Sprite[] sprites_Push; // 切り替え用スプライトの配列
    public Sprite[] sprites_Jump; // 切り替え用スプライトの配列(ジャンプ)
    private int Anim = 0; // 現在のアニメーションフレーム
    bool jumpAnim = false;

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

    public override void ChangeSprite_Push()
    {
        // フレームを更新
        Anim = (Anim + 1) % sprites_Push.Length;

        // スプライトの変更
        spriteRenderer.sprite = sprites_Push[Anim];
    }

    public override void ChangeSprite_Jump()
    {

    }

}
