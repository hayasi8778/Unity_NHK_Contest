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
        Debug.LogWarning("オーバーライドのテスト");

        if (replacementPrefabs == null || replacementPrefabs.Length == 0) return null;
        if (replacementIndex >= replacementPrefabs.Length - 1)
        {
            Debug.LogWarning("最後のオブジェクトなので入れ替えしません");
            return null;
        }

        replacementIndex++;

        Vector3 spawnPosition = transform.position;
        GameObject nextPrefab = replacementPrefabs[replacementIndex];
        GameObject newObj = Instantiate(nextPrefab, spawnPosition, transform.rotation);

        TimeSlider_Light newScript = newObj.GetComponent<TimeSlider_Light>();
        if (newScript == null)
        {
            Debug.LogError("TimeSlider_Light コンポーネントが新しいオブジェクトに見つかりません！");
            return null;
        }

        Destroy(this.gameObject);
        return newScript.gameObject;
    }


}
