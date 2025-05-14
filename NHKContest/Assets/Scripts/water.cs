using UnityEngine;

public class WaterScript : MonoBehaviour
{
    // �ڐG�Ώۂ̃I�u�W�F�N�g�����^�O���w��iInspector����ύX�\�j
    public string blockTag = "Block";
    public string playerTag = "Player";

    // ���̃I�u�W�F�N�g��Renderer��Collider�̎Q�Ƃ�ێ�
    private Renderer objRenderer;
    private Collider objCollider;

    // �����̈��O�̃u���b�N�̎Q�Ƃ�ێ�
    [SerializeField] private GameObject previousBlock;

    // �u���b�N�Ƃ̐ڐG�t���O
    bool isBlockInWater = false;

    // �f���^���Ԃ̕ۑ�
    private float wasDeltaTime;

    // �����߂���
    public float forceStrength = 500f; 

    void Start() {
        // �����I�u�W�F�N�g�ɕt���Ă���Renderer��Collider���擾
        objRenderer = GetComponent<Renderer>();
        objCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (!isBlockInWater) {
            // �����̈��O�̃u���b�N��Renderer���L���Ȃ�
            if (previousBlock.GetComponent<Renderer>().enabled) {
                // Renderer��L���ɂ���
                if (objRenderer != null) objRenderer.enabled = true;
            } else {
                // Renderer�𖳌��ɂ���
                if (objRenderer != null) objRenderer.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        // �ڐG�����I�u�W�F�N�g���u���b�N�^�O�������Ă��邩�m�F
        if (other.gameObject.CompareTag(blockTag)) {
            // Renderer�𖳌��ɂ���
            if (objRenderer != null) objRenderer.enabled = false;

            // �u���b�N�Ƃ̐ڐG�t���O�𗧂Ă�
            isBlockInWater = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        // �ڐG�����I�u�W�F�N�g���v���C���[�^�O�������Ă��邩�m�F
        if (other.CompareTag(playerTag) && !isBlockInWater) {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) {
                // �v���C���[�������߂�
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                rb.AddForce(pushDirection * forceStrength);
            }
        }
    }

    void OnTriggerExit(Collider other) 
    {
        // �ڐG���I�������I�u�W�F�N�g���u���b�N�^�O�������Ă��邩�m�F
        if (other.gameObject.CompareTag(blockTag)) {
            // Renderer��L���ɂ���
            if (objRenderer != null) objRenderer.enabled = true;

            // �u���b�N�Ƃ̐ڐG�t���O�����낷
            isBlockInWater = false;
        }
    }
}
