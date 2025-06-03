using UnityEngine;

public class TimeSlider_Light : TimeSliderObject_Base
{
    public GameObject[] replacementPrefabs;
    public int replacementIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override GameObject ReplaceObject()
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

        TimeSlider_Light newScript = newObj.GetComponent<TimeSlider_Light>();
        if (newScript == null)
        {
            Debug.LogError("TimeSlider_Light �R���|�[�l���g���V�����I�u�W�F�N�g�Ɍ�����܂���I");
            return null;
        }

        Destroy(this.gameObject);
        return newScript.gameObject;
    }


}
