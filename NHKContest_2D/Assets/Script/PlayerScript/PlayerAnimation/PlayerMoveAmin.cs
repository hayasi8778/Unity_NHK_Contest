using System;
using UnityEngine;

public abstract class PlayerMoveAmin : MonoBehaviour
{
    /*
    public Sprite[] sprites; // 2�̉摜���Z�b�g
    private SpriteRenderer spriteRenderer;
    private int Anim = 0;//�A�j���[�V�������Ǘ�����ϐ�
    */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       // InvokeRepeating("ChangeSprite", 0.5f, 0.5f); // 0.5�b���Ƃɐ؂�ւ�

    }
    */

    public abstract void ChangeSprite();

    public abstract void ChangeSprite_Push();

    /*
    public void ChangeSprite()
    {
        //���G�ɂ���ƃo�O�肻���₩�玖�O�ɃZ�b�g�����X�v���C�g�̐؂�ւ������ɂ���
        Anim = (Anim + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[Anim];
    }
    */

    // Update is called once per frame
}
