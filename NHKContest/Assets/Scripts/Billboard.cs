using UnityEngine;

public class Billboard : MonoBehaviour
{
    //�|�����J�����̕��Ɍ����邽�߂̃v���O����
    void Update()
    {
        // ���C���J�����̕������擾���āA���J�����Ɍ�����
        if (Camera.main != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            transform.LookAt(cameraPos);

            // Y���̌Œ�(��U�؂�)
            //Vector3 euler = transform.eulerAngles;
            //transform.eulerAngles = new Vector3(0, euler.y, 0);
        }
    }
}
