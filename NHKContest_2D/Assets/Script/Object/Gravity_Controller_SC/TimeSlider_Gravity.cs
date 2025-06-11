using UnityEngine;
using UnityEngine.UI;

public class TimeSlider_Gravity : TimeSliderObject_Base
{

    public GameObject[] replacementPrefabs;
    public int replacementIndex = 0;
    public Slider slider; //�X���C�_�[

    private int Currentnum = 0;//�z��̉��Ԗڂɂ��邩

    //�d�͂̔��]�t���O
    private bool isGravityFlipped = false;

    //�d�͔��]��ɐG��{�^�����ǂ���
    public bool ButtonRotatFlag = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // �X�y�[�X�L�[�ŏd�͔��]
        {
            isGravityFlipped = !isGravityFlipped;
            Physics2D.gravity = new Vector2(0, isGravityFlipped ? -9.8f : 9.8f);
        }

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // �v���C���[�Ƃ̐ڐG�𔻒�
        if (collision.gameObject.CompareTag("Player"))
        {
            // �v���C���[�̃R���C�_�[���擾(���s���Ƀv���C���[�ς�邩��)
            Collider2D playerCollider = collision.collider;

            // ���̃I�u�W�F�N�g�̃R���C�_�[�i�{�^���j���擾
            Collider2D buttonCollider = GetComponent<Collider2D>();

            if (playerCollider != null && buttonCollider != null)//NULL�`�F�b�N
            {
                // **�i���������`�F�b�N (�v���C���[���㉺�ǂ������痈�Ă��邩)**
                bool isComingFromAbove = false;

                if (!ButtonRotatFlag) 
                {
                    isComingFromAbove = collision.relativeVelocity.y < 0;
                }
                else
                {
                    isComingFromAbove = collision.relativeVelocity.y > 0.3;
                }

                 PlayerJump playerJump = collision.gameObject.GetComponent<PlayerJump>();

               

                if (/*playerBottom <= buttonTop &&*/ isComingFromAbove) // �v���C���[�����񂾏ꍇ�̂ݔ��]
                {
                    //�J�����̏㉺���]������
                    Camera_Flip camera_fl = this.gameObject.GetComponent<Camera_Flip>();

                    camera_fl.FlipCamera();

                    isGravityFlipped = !playerJump.GetGravityFlag();

                    Physics2D.gravity = new Vector2(0, isGravityFlipped ? -9.8f : 9.8f);

                    //Debug.Log($"isGravityFlipped: {isGravityFlipped}");
                    Debug.Log(Physics2D.gravity);

                    playerJump.SetGravityFlag();//�d�͔��]�������Ƃ��v���C���[�ɓ`����
                    playerJump.FlipPlayerTexture();
                }

            }
        }
    }

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <returns></returns>
    //�X���C�_�[�֌W�̏���
    public override GameObject ReplaceObject()//�I�u�W�F�N�g����ւ�(���)
    {
        Debug.LogWarning("�I�[�o�[���C�h�̃e�X�g");

        if (replacementPrefabs == null || replacementPrefabs.Length == 0) return null;
        if (replacementIndex >= replacementPrefabs.Length - 1)
        {
            Debug.LogWarning("�Ō�̃I�u�W�F�N�g�Ȃ̂œ���ւ����܂���");
            return null;
        }

        replacementIndex++;

        Vector3 spawnPosition = transform.position;
        GameObject nextPrefab = replacementPrefabs[replacementIndex];
        GameObject newObj = Instantiate(nextPrefab, spawnPosition, transform.rotation);

        if (newObj == null)
        {
            Debug.LogError("���̃��C�g�I�u�W�F�N�g�Ȃ���");
            return null;
        }

        TimeSlider_Gravity newScript = newObj.GetComponent<TimeSlider_Gravity>();
        if (newScript != null)
        {
            newScript.slider = this.slider;
            newScript.replacementPrefabs = this.replacementPrefabs;
            newScript.replacementIndex = this.replacementIndex;
            newScript.Currentnum = this.Currentnum;
        }
        /*
        var counter = slider.GetComponent<SliderTimeCounter>();
        if (counter != null)
        {
            Debug.LogWarning("�z��ݒ�" + Currentnum);
            counter.SetCurrentObjects(newObj, Currentnum);
        }
        */



        Destroy(this.gameObject);

        var counter = slider.GetComponent<SliderTimeCounter>();
        if (counter != null)
        {
            Debug.LogWarning("�z��ݒ�" + Currentnum);
            counter.SetCurrentObjects(newObj, Currentnum);
        }

        return newObj;

    }

    public override void SetCurrentnum(int num)
    {
        //�z�񂪐ݒ肳�ꂽ��
        Debug.LogWarning("�z��ݒ�" + num);
        Currentnum = num;
    }
}
