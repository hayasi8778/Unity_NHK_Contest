using System;
using UnityEngine;

public abstract class PlayerMoveAmin : MonoBehaviour
{
    /*
    public Sprite[] sprites; // 2つの画像をセット
    private SpriteRenderer spriteRenderer;
    private int Anim = 0;//アニメーションを管理する変数
    */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       // InvokeRepeating("ChangeSprite", 0.5f, 0.5f); // 0.5秒ごとに切り替え

    }
    */

    public abstract void ChangeSprite();

    public abstract void ChangeSprite_Push();

    /*
    public void ChangeSprite()
    {
        //複雑にするとバグりそうやから事前にセットしたスプライトの切り替えだけにする
        Anim = (Anim + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[Anim];
    }
    */

    // Update is called once per frame
}
