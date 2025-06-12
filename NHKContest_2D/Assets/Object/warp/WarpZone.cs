using System.Collections;
using UnityEngine;

/*
WarpZone2D.cs

�y�T�v�z
���̃X�N���v�g�́A�w�肳�ꂽ�^�O�iWarp1, Warp2, Warp3�j�������[�v�]�[����
�v���C���[�����̃I�u�W�F�N�g���G�ꂽ�ۂɁA�ݒ肳�ꂽ�x�����Ԃ̌��
�����^�O�����ʂ̃��[�v�n�_�փ��[�v������@�\���������Ă��܂��B

- Warp1�͑������[�v
- Warp2��2�b�x�����[�v
- Warp3��6�b�x�����[�v

���[�v���͑ΏۃI�u�W�F�N�g�𓧖��i��\���j�ɂ��A
�v���C���[�ł���Έړ����֎~���܂��B

���[�v��͓����^�O�̕ʃI�u�W�F�N�g�̈ʒu�ŁA  
�v���C���[�͂��̂܂܂̈ʒu�ɁA  
�v���C���[�ȊO�̃I�u�W�F�N�g�͏�����ɃI�t�Z�b�g���ă��[�v���܂��B

SpriteRenderer�������I�u�W�F�N�g�ɂ�
Renderer�R���|�[�l���g���g���ē������������s���܂��B

�^�O���x�����ɕς�����ꍇ�͑����ɂ��̃^�O�ɉ��������[�v�����s���܂��B

---

�y�ڍׂȏ����̗���z

1. OnTriggerEnter2D�ŁA�ڐG�����I�u�W�F�N�g��Player��Object�^�O���������肵�A����ȊO�͖����B

2. ���[�v�]�[���̃^�O(Warp1/2/3)����x�����Ԃ�����B

3. �ڐG�ʒu����u�E���痈���������痈�����v�𔻒肵�A
   ���[�v����������炷���߂̕����W���� +1 �܂��� -1 �ɐݒ�B

4. �R���[�`��WarpAfterDelay���J�n���A
   - �v���C���[�̏ꍇ��PlayerMove�X�N���v�g�ňړ��֎~�ɐݒ�B
   - �ΏۃI�u�W�F�N�g��SpriteRenderer��Renderer�𖳌��ɂ��ē������B

5. �x�����Ƀ^�O���ς�����ꍇ�́A�^�O���ς�����u�Ԃɑ����Ƀ��[�v�����s�B

6. �x���I�����ɁA�����^�O�̕ʂ̃��[�v�n�_��T���A
   �v���C���[�͂��̈ʒu�Ɉړ��A
   ����ȊO�̓��[�v�悩������ɏ������炵�Ĉړ��B

7. ���������������A�v���C���[�̈ړ��֎~�������B

---

�y���ӓ_�z
- ���[�v�悪������Ȃ��ꍇ�̓��[�v���܂���B
- �ΏۃI�u�W�F�N�g��Player�^�O��Object1,2�^�O��K���ݒ肵�Ă��������B
*/

public class WarpZone2D : MonoBehaviour
{
    // ���[�v�悩��̃I�t�Z�b�g�ʁi���E�����j
    public Vector2 offset = new Vector2(-1.5f, 0f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���[�v�Ώۂ��ǂ�������i�v���C���[��WarpableObject�^�O�̂݁j
        if (!other.CompareTag("Player") && !other.CompareTag("Object1") && !other.CompareTag("Object2"))
            return;

        // ���݂̃��[�v�]�[���̃^�O����x�����Ԃ��擾
        float delay = GetWarpDelayByTag(tag);

        // �v���C���[�i�܂��̓I�u�W�F�N�g�j�̈ʒu�ƃ��[�v�]�[���̈ʒu�̍������v�Z
        Vector2 dir = other.transform.position - transform.position;

        // �E���痈���Ȃ� +1�A�����痈���Ȃ� -1 ������W���ɐݒ�
        int directionFactor = dir.x > 0 ? 1 : -1;

        // Warp2�܂���Warp3�̏ꍇ�̓��[�v���ɓ���������
        bool makeInvisible = (tag == "Warp2" || tag == "Warp3");

        // ���[�v�J�n���̃^�O��ێ��i�x�����Ƀ^�O���ς�������m�F�p�j
        string initialTag = tag;

        // �x����Ƀ��[�v���鏈�����R���[�`���ŊJ�n
        StartCoroutine(WarpAfterDelay(other.gameObject, delay, directionFactor, makeInvisible, initialTag));
    }

    // �^�O�ɉ��������[�v�x�����Ԃ�Ԃ��֐�
    float GetWarpDelayByTag(string tag)
    {
        switch (tag)
        {
            case "Warp1": return 0f;  // �������[�v
            case "Warp2": return 2f;  // 2�b�x��
            case "Warp3": return 6f;  // 6�b�x��
            default: return 0f;
        }
    }

    // �����^�O�̕ʂ̃��[�v�|�C���g��T���ĕԂ��i���g�͏����j
    Transform FindOtherWarpPointWithTag(string tag)
    {
        GameObject[] warps = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject warp in warps)
        {
            if (warp != this.gameObject)
                return warp.transform;
        }
        return null;  // ������Ȃ����null
    }

    // �x����Ƀ��[�v�������s���R���[�`��
    IEnumerator WarpAfterDelay(GameObject obj, float delay, int directionFactor, bool makeInvisible, string initialTag)
    {
        // �v���C���[�̈ړ��֎~��ݒ�iPlayerMove������ꍇ�j
        var moveScript = obj.GetComponent<PlayerMove>();
        if (moveScript != null) moveScript.SetCanMove(false);

        // SpriteRenderer���擾�i�q�I�u�W�F�N�g���܂ށj
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = obj.GetComponentInChildren<SpriteRenderer>();

        // SpriteRenderer���Ȃ��ꍇ��Renderer�iMeshRenderer���j���擾
        Renderer otherRenderer = obj.GetComponent<Renderer>();
        if (otherRenderer == null) otherRenderer = obj.GetComponentInChildren<Renderer>();

        // ���[�v���ɓ������i��\���j����ꍇ
        if (makeInvisible)
        {
            if (sr != null) sr.enabled = false;
            else if (otherRenderer != null) otherRenderer.enabled = false;
        }

        bool warped = false;  // ���łɃ��[�v�������ǂ����̃t���O
        float elapsed = 0f;

        // �x�����ԕ����[�v���Ȃ���^�O�̕ω����Ď�
        while (elapsed < delay)
        {
            // �^�O���ς�����瑦���Ƀ��[�v�����s
            if (this.tag != initialTag && !warped)
            {
                Transform newDest = FindOtherWarpPointWithTag(this.tag);
                if (newDest != null)
                    obj.transform.position = GetWarpPosition(obj, newDest.position, directionFactor);

                // ����������
                if (sr != null) sr.enabled = true;
                else if (otherRenderer != null) otherRenderer.enabled = true;

                // �ړ��\�ɖ߂�
                if (moveScript != null) moveScript.SetCanMove(true);
                yield break;  // �R���[�`���I��
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // �x���I����Ƀ��[�v���T��
        Transform dest = FindOtherWarpPointWithTag(initialTag);
        if (dest != null)
            obj.transform.position = GetWarpPosition(obj, dest.position, directionFactor);

        // ����������
        if (sr != null) sr.enabled = true;
        else if (otherRenderer != null) otherRenderer.enabled = true;

        // �ړ��\�ɖ߂�
        if (moveScript != null) moveScript.SetCanMove(true);
    }

    // ���[�v��̈ʒu���v�Z����i�v���C���[�ȊO�͏����I�t�Z�b�g�j
    Vector2 GetWarpPosition(GameObject obj, Vector2 basePos, int directionFactor)
    {
        Vector2 baseOffset = offset * directionFactor;

        // �v���C���[�łȂ���΂���ɏ����O�ɃI�t�Z�b�g��ǉ�
        if (!obj.CompareTag("Player"))
        {
            baseOffset += new Vector2(-0.5f, 0f) * directionFactor;
        }

        return basePos + baseOffset;
    }
}
