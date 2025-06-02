using UnityEngine;

public class LightRay : MonoBehaviour
{

    public GameObject platformPrefab; // ����̃v���n�u
    public float rayDistance = 5f; // ���C�̒���
    public string mirrorTag = "Mirror"; // ���̃^�O�i���˗p�j
    public string ignoreTag = "LightObject"; // �����I�u�W�F�N�g�̃^�O�i���C�L���X�g�̑ΏۊO�j
    public Vector2 customRayDirection = Vector2.left; // ���C�̕���(�����͍���)
    public int maxReflections = 3; // �ő唽�ˉ�

    bool LightColFlag = false; //���̐�������
    //bool LightColFlag = true; //���̐�������

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !LightColFlag) // �X�y�[�X�L�[�Ō����𐶐�
        {
            CastRay(transform.position, customRayDirection, maxReflections);
        }

    }

    void CastRay(Vector2 origin, Vector2 direction, int remainingReflections)
    {
        if (remainingReflections <= 0) return; // �ő唽�ˉ񐔂𒴂�����I��

        RaycastHit2D hit = Physics2D.Raycast(origin, direction.normalized, rayDistance);

        if (hit.collider != null) // �����Ƀq�b�g�������m�F
        {
            // ��������^�O�Ȃ�X�L�b�v
            if (hit.collider.CompareTag(ignoreTag))
            {
                CastRay(hit.point, direction, remainingReflections);
                return;
            }

            Debug.Log($"Ray hit object: {hit.collider.gameObject.name} at {hit.point}, Distance: {hit.distance}");

            // �~���[�ɓ��������ꍇ
            if (hit.collider.CompareTag(mirrorTag))
            {
                // �~���[�ɓ����������_�Ō����I�u�W�F�N�g�𐶐�
                CreateLightObject(origin, hit.point);

                // **���˕������v�Z**
                Vector2 newDirection = Vector2.Reflect(direction, hit.normal);

                // **�V�������C���o���i�ċA�I�ɔ��˂𑱂���j**
                CastRay(hit.point, newDirection, remainingReflections - 1);
            }
            else
            {
                // ����𐶐��i���ȊO�ɓ��������炻���Ō������I�u�W�F�N�g���j
                CreateLightObject(origin, hit.point);
            }
        }

        // �f�o�b�O�p�Ƀ��C��\��
        Debug.DrawRay(origin, direction.normalized * rayDistance, Color.yellow, 10f);

        //2��ڂ̐�����؂�
        LightColFlag = true;
    }

    void CreateLightObject(Vector2 startPoint, Vector2 endPoint)
    {
        // ���Ԓn�_���v�Z
        Vector2 midPoint = (startPoint + endPoint) / 2;

       

        // �Փ˒n�_�ł͂Ȃ����Ԓn�_�ɃI�u�W�F�N�g�𐶐�
        GameObject platform = Instantiate(platformPrefab, midPoint, Quaternion.identity);
        platform.tag = ignoreTag; // ���������I�u�W�F�N�g�ɖ�������^�O��ݒ�

        // ���C�̕������擾
        Vector2 direction = endPoint - startPoint;

        // �p�x���v�Z
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

        // �����iX���̃X�P�[���j�����C�̋����ɕύX
        Vector3 newScale = platform.transform.localScale;
        newScale.x = Vector2.Distance(startPoint, endPoint);  // ���������C�̋�����
        platform.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Z����]�̂ݓK�p
        platform.transform.localScale = newScale;
    }

}
