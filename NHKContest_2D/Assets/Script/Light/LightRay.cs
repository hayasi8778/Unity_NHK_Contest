using UnityEngine;

public class LightRay : MonoBehaviour
{

    public GameObject platformPrefab; // ����̃v���n�u
    public float rayDistance = 5f; // ���C�̒���
    public string targetTag = "PlatformTarget"; // ����𐶐�����Ώۂ̃^�O
    public Vector2 customRayDirection = Vector2.left; // ���C�̕���(�����͍���)
    bool LightColFlag = false; //���C�g�����̃I�u�W�F�N�g�ɓ������đ���𐶐��������̃t���O

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !LightColFlag) // �X�y�[�X�L�[�ő���𐶐�
        {
            // **�I�u�W�F�N�g�̍��[�̈ʒu���v�Z**
            float leftEdgeX = transform.position.x - (GetComponent<SpriteRenderer>().bounds.size.x / 2f);
            Vector2 rayOrigin = new Vector2(leftEdgeX, transform.position.y); // ���[�̍��W���쐬

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, customRayDirection.normalized, rayDistance);

            if (hit.collider != null) // �����Ƀq�b�g�������m�F
            {
                Debug.Log($"Ray hit object: {hit.collider.gameObject.name} at {hit.point}, Distance: {hit.distance}");

                // �^�O����
                if (hit.collider.CompareTag(targetTag))
                {
                    // ���Ԓn�_���v�Z
                    Vector2 midPoint = (rayOrigin + hit.point) / 2;

                    // �Փ˒n�_�ł͂Ȃ����Ԓn�_�ɃI�u�W�F�N�g�𐶐�
                    GameObject platform = Instantiate(platformPrefab, midPoint, Quaternion.identity);

                    // �����iX���̃X�P�[���j�����C�̋����ɕύX
                    Vector3 newScale = platform.transform.localScale;
                    newScale.x = hit.distance;  // ���������C�̋�����
                    platform.transform.localScale = newScale;

                    //���C�g�̐����ς݃t���O�𗧂Ă�
                    LightColFlag = true;
                }
            }
            else
            {
                Debug.Log("Ray did NOT hit anything.");
            }



            // �f�o�b�O�p�Ƀ��C��\��
            Debug.DrawRay(transform.position, customRayDirection.normalized * rayDistance, Color.yellow, 10f);
        }


    }
}
