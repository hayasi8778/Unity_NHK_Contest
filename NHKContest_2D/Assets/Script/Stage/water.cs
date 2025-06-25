using UnityEngine;

public class WaterScript : MonoBehaviour
{
    // �ڐG�Ώۂ̃I�u�W�F�N�g�����^�O���w��iInspector����ύX�\�j
    public string blockTag = "Block";
    public string playerTag = "Player";

    // ���̃I�u�W�F�N�g��Renderer��Collider�̎Q�Ƃ�ێ�
    private Renderer objRenderer;

    [Header("��������2�ڂ̃u���b�N��")]
    public bool second_Block = false;
    [Header("�d�͔��]���ɕ`�悷�邩")]
    public bool Flip = false;

    [Header("�d�͔��]�M�~�b�N������ꍇ�APlayerObject�̎Q�Ƃ�ݒ�")]
    public GameObject PlayerObject;
    public bool isGravity = true;
    public bool wasGravity = true;

    [Header("�����̈��O�̃u���b�N�̎Q�Ƃ�ݒ�")]
    // �����̈��O�̃u���b�N�̎Q�Ƃ�ێ�
    [SerializeField] private GameObject previousBlock;

    // �u���b�N�Ƃ̐ڐG�t���O
    bool isBlockInWater = false;

    // �f���^���Ԃ̕ۑ�
    private float wasDeltaTime;

    // �����߂���
    public float forceStrength = 500f;

    void Start()
    {
        // �����I�u�W�F�N�g�ɕt���Ă���Renderer��Collider���擾
        objRenderer = GetComponent<Renderer>();

        if (!Flip) {
            // Renderer��L���ɂ���
            if (objRenderer != null) objRenderer.enabled = true;
        } else {
            // Renderer�𖳌��ɂ���
            if (objRenderer != null) objRenderer.enabled = false;
        }
    }

    void Update()
    {
        // �d�͂̔��]�󋵂��擾
        if (PlayerObject != null) isGravity = PlayerObject.GetComponent<PlayerMove>().GetGravityFlag();

        if (!isBlockInWater && !second_Block)   // ���������Ԗڂ̃u���b�N�ȊO
        {
            if (previousBlock != null)
            {
                // �����̈��O�̃u���b�N��Renderer���L���Ȃ�
                if (previousBlock.GetComponent<Renderer>().enabled)
                {
                    // Renderer��L���ɂ���
                    if (objRenderer != null) objRenderer.enabled = true;
                }
                else
                {
                    // Renderer�𖳌��ɂ���
                    if (objRenderer != null) objRenderer.enabled = false;
                }
            }
            
        }
        else if (!isBlockInWater && second_Block)   // ���������Ԗڂ̃u���b�N
        {
            if (isGravity != wasGravity) // �d�͏󋵂��ω�������
            {
                objRenderer.enabled = !objRenderer.enabled; // ���]����
            }
        }

        wasGravity = isGravity; // �O��̏d�͔��]��Ԃ�ۑ�
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �ڐG�����I�u�W�F�N�g���u���b�N�^�O�������Ă��邩�m�F
        if (other.gameObject.CompareTag(blockTag))
        {
            // Renderer�𖳌��ɂ���
            if (objRenderer != null) objRenderer.enabled = false;

            // �u���b�N�Ƃ̐ڐG�t���O�𗧂Ă�
            isBlockInWater = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // �ڐG�����I�u�W�F�N�g���v���C���[�^�O�������Ă��邩�m�F
        if (other.CompareTag(playerTag) && !isBlockInWater)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // �v���C���[�������߂�
                if (objRenderer.enabled)//�����_���[���L���Ȃ�(���I�u�W�F�N�g�Ƃ��ăV�[���ɓo�ꂵ�Ă���Ȃ�)
                {
                    Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                    rb.AddForce(pushDirection * forceStrength);
                }

            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // �ڐG���I�������I�u�W�F�N�g���u���b�N�^�O�������Ă��邩�m�F
        if (other.gameObject.CompareTag(blockTag))
        {
            // Renderer��L���ɂ���
            if (objRenderer != null) objRenderer.enabled = true;

            // �u���b�N�Ƃ̐ڐG�t���O�����낷
            isBlockInWater = false;
        }
    }
}
