using UnityEngine;

public class Player01_Animation : PlayerMoveAmin
{
    public SpriteRenderer spriteRenderer; // �X�v���C�g�����_���[
    public Sprite[] sprites; // �؂�ւ��p�X�v���C�g�̔z��
    public Sprite[] sprites_Push; // �؂�ւ��p�X�v���C�g�̔z��
    public Sprite[] sprites_Jump; // �؂�ւ��p�X�v���C�g�̔z��(�W�����v)
    private int Anim = 0; // ���݂̃A�j���[�V�����t���[��
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
        // �t���[�����X�V
        Anim = (Anim + 1) % sprites.Length;

        // �X�v���C�g�̕ύX
        spriteRenderer.sprite = sprites[Anim];
    }

    public override void ChangeSprite_Push()
    {
        // �t���[�����X�V
        Anim = (Anim + 1) % sprites_Push.Length;

        // �X�v���C�g�̕ύX
        spriteRenderer.sprite = sprites_Push[Anim];
    }

    public override void ChangeSprite_Jump()
    {

    }

}
