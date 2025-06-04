using UnityEngine;
using UnityEngine.UI;

public class TimeSlider_Light : TimeSliderObject_Base
{
    public GameObject[] replacementPrefabs;
    public int replacementIndex = 0;
    public Slider slider; //スライダー

    private int Currentnum = 0;//配列の何番目にいるか

    //画質戻すための時間関係
    private float revertTimer = 0f;
    private float revertTimeLimit = 8f; // 5秒で戻す

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        revertTimer += Time.deltaTime;
        if (revertTimer >= revertTimeLimit)
        {
            revertTimer = 0f; // タイマーリセット
            TryRevertObject();
            
        }
    }

    public override GameObject ReplaceObject()//オブジェクト入れ替え(後ろ)
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

        if (newObj == null)
        {
            Debug.LogError("次のライトオブジェクトないよ");
            return null;
        }

        TimeSlider_Light newScript = newObj.GetComponent<TimeSlider_Light>();
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
            Debug.LogWarning("配列設定" + Currentnum);
            counter.SetCurrentObjects(newObj, Currentnum);
        }
        */
        
        Destroy(this.gameObject);

        var counter = slider.GetComponent<SliderTimeCounter>();
        if (counter != null)
        {
            Debug.LogWarning("配列設定" + Currentnum);
            counter.SetCurrentObjects(newObj, Currentnum);
        }

        return newObj;

    }

    public override void SetCurrentnum(int num)
    {
        //配列が設定されたよ
        Debug.LogWarning("配列設定" + num);
        Currentnum = num;
    }

    private void TryRevertObject()
    {
        if (replacementPrefabs == null || replacementPrefabs.Length == 0)
            return;

        if (replacementIndex <= 0)
        {
            Debug.LogWarning("これ以上戻れない！");
            return;
        }

        // ここでいったん減らす（戻す）
        replacementIndex--;

        Vector3 spawnPosition = transform.position;
        GameObject prevPrefab = replacementPrefabs[replacementIndex];
        GameObject newObj = Instantiate(prevPrefab, spawnPosition, transform.rotation);

        var newScript = newObj.GetComponent<TimeSlider_Light>();
        if (newScript != null)
        {
            newScript.slider = this.slider;
            newScript.replacementPrefabs = this.replacementPrefabs;
            newScript.Currentnum = this.Currentnum; //配列番号更新処理を追加

            // 🔥 注意！！戻った後のオブジェクトでは「次に行けるよう」replacementIndexを1つ進めた値にする！
            newScript.replacementIndex = this.replacementIndex;
        }

        // ここでスライダー側に「新しいオブジェクト」を教える！
        var counter = slider.GetComponent<SliderTimeCounter>();
        if (counter != null)
        {
            Debug.LogWarning("配列設定" + Currentnum);
            counter.SetCurrentObjects(newObj, Currentnum);
        }

        Destroy(this.gameObject);
    }

    

}
