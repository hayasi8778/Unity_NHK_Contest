using UnityEngine;

public class Player01_Animation : PlayerMoveAmin
{
    public SpriteRenderer spriteRenderer; // �X�v���C�g�����_���[
    public Sprite[] sprites; // �؂�ւ��p�X�v���C�g�̔z��
    private int Anim = 0; // ���݂̃A�j���[�V�����t���[��

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

}
