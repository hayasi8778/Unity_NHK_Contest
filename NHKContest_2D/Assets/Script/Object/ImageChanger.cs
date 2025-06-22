using UnityEngine;

public class ImageChanger : MonoBehaviour
{
    public GameObject currentPlayer; //�v���C���[
    public GameObject[] currentObjects;//�V�[�����̃I�u�W�F�N�g

    private int ImageQuality = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ImageQuality = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha3)) 
        {
            Debug.Log(ImageQuality);
            if (ImageQuality >= 2)//
            {
                ImageQuality = 2;
                Debug.Log("�掿�ύX�Ȃ�");
                return;
            }
            ImageQuality = 2;
            //�܂��v���C���[�̓���ւ�
            if (currentPlayer != null)
            {
                TimeSlider2 script = currentPlayer.GetComponent<TimeSlider2>();
                if (script != null)
                {
                    script.ChangeImage(ImageQuality);
                    
                }
            }

            //�I�u�W�F�N�g�̓���ւ�
            for (int i = 0; i < currentObjects.Length; i++)
            {
                GameObject obj = currentObjects[i];
                if (obj == null) continue;
                //Debug.LogError("�I�u�W�F�N�gNULL����Ȃ��ł�");

                //�e�I�u�W�F�N�g���擾(�q�I�u�W�F�N�g���A�^�b�`���ĂĂ��擾�ł���)
                TimeSliderObject_Base timeObj = obj.GetComponent<TimeSliderObject_Base>();

                if (timeObj != null)
                {
                    //Debug.LogError("�I�u�W�F�N�g�؂�ւ��������܂�");
                    timeObj.ChangeImageQuality(ImageQuality); // �q�N���X�̃I�[�o�[���C�h���ꂽ���\�b�h���K�p�����
                    
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (ImageQuality == 1)//
            {
                Debug.Log("�掿�ύX�Ȃ�");
                return;
            }
            ImageQuality = 1;

            //�܂��v���C���[�̓���ւ�
            if (currentPlayer != null)
            {
                TimeSlider2 script = currentPlayer.GetComponent<TimeSlider2>();
                if (script != null)
                {
                    script.ChangeImage(ImageQuality);

                }
            }

            //�I�u�W�F�N�g�̓���ւ�
            for (int i = 0; i < currentObjects.Length; i++)
            {
                GameObject obj = currentObjects[i];
                if (obj == null) continue;
                //Debug.LogError("�I�u�W�F�N�gNULL����Ȃ��ł�");

                //�e�I�u�W�F�N�g���擾(�q�I�u�W�F�N�g���A�^�b�`���ĂĂ��擾�ł���)
                TimeSliderObject_Base timeObj = obj.GetComponent<TimeSliderObject_Base>();

                if (timeObj != null)
                {
                    //Debug.LogError("�I�u�W�F�N�g�؂�ւ��������܂�");
                    timeObj.ChangeImageQuality(ImageQuality); // �q�N���X�̃I�[�o�[���C�h���ꂽ���\�b�h���K�p�����

                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (ImageQuality <= 0)//
            {
                ImageQuality = 0;
                Debug.Log("�掿�ύX�Ȃ�");
                return;
            }
            ImageQuality = 0;

            //�܂��v���C���[�̓���ւ�
            if (currentPlayer != null)
            {
                TimeSlider2 script = currentPlayer.GetComponent<TimeSlider2>();
                if (script != null)
                {
                    script.ChangeImage(ImageQuality);
                }
            }

            //�I�u�W�F�N�g�̓���ւ�
            for (int i = 0; i < currentObjects.Length; i++)
            {
                GameObject obj = currentObjects[i];
                if (obj == null) continue;
                //Debug.LogError("�I�u�W�F�N�gNULL����Ȃ��ł�");

                //�e�I�u�W�F�N�g���擾(�q�I�u�W�F�N�g���A�^�b�`���ĂĂ��擾�ł���)
                TimeSliderObject_Base timeObj = obj.GetComponent<TimeSliderObject_Base>();

                if (timeObj != null)
                {
                    //Debug.LogError("�I�u�W�F�N�g�؂�ւ��������܂�");
                    timeObj.ChangeImageQuality(ImageQuality); // �q�N���X�̃I�[�o�[���C�h���ꂽ���\�b�h���K�p�����
                
                }
            }
        }
    }

    public void SetCurrentObjects(GameObject objects, int it)
    {
        Debug.LogWarning("�X�e�[�W�I�u�W�F�N�g�p��_IC");
        currentObjects[it] = objects;
    }

    public void SetCurrentPlayer(GameObject player)
    {
        currentPlayer = player;
    }

}
