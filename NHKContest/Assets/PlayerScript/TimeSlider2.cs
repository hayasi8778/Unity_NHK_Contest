using UnityEngine;
using UnityEngine.UI;

public class TimeSlider2 : MonoBehaviour
{
    public Slider slider; //スライダー
    private Vector3[] positionHistory = new Vector3[3000]; //秒数(5分入るサイズ)
    private int currentIndex = 0;  //現在のスライダー値
    private bool isRewinding = false;  //スライダー巻き戻し中かを判定するフラグ

    //オブジェクトの切り替えこっちでやるからスライダーから移植
    public GameObject[] replacementPrefabs; // 順番に切り替えるプレハブ群
    private int replacementIndex = 0;

    private bool isManualInput = false; //手動で巻き戻しているかのフラグ

    void Start()
    {
        for (int i = 0; i < positionHistory.Length; i++)
        {
            if (positionHistory[i] == null) //オブジェクト切り替え時に初期座標で埋める可能性があるからnullチェック
            {
                positionHistory[i] = transform.position;//配列の初期化
            }
            
        }
    }

    void Update()
    {
        if (!isRewinding) //巻き戻し中じゃないなら座標を配列に追加
        {
            int index = Mathf.RoundToInt(slider.value * 10); 
            if (index >= 0 && index < positionHistory.Length)
            {
                positionHistory[index] = transform.position;
                currentIndex = index;
            }
        }
    }

    public void OnSliderValueChanged()//プレイヤーの巻き戻し処理
    {
        if (slider == null)
        {
            Debug.LogError("スライダーが指定されてないぞ(TimeSlider)");
            return;
        }

        isRewinding = true;
        int index = Mathf.RoundToInt(slider.value * 10);

        if (index == currentIndex + 1)
        {
            return;
        }

        if (index < positionHistory.Length)
        {
            if (index <= currentIndex)
            {
                transform.position = positionHistory[index];
                for (int i = currentIndex + 1; i < positionHistory.Length; i++)
                {
                    positionHistory[i] = transform.position;
                }
            }
            else
            {
                for (int i = currentIndex + 1; i <= index; i++)
                {
                    positionHistory[i] = transform.position;
                }
            }
            currentIndex = index;
        }
        isRewinding = false;
    }

    public Vector3[] GetPositionHistory() //オブジェクトの引継ぎのためのゲッター
    {
        return positionHistory;
    }

    public void SetPositionHistory(Vector3[] history)//オブジェクト引継ぎのためのセッター
    {
        if (history.Length == positionHistory.Length)
        {
            for (int i = 0; i < positionHistory.Length; i++)
            {
                positionHistory[i] = history[i];
            }
        }
    }

    public GameObject ObjectChanged()
    {
        if (replacementPrefabs == null || replacementPrefabs.Length == 0)
        {
            Debug.LogWarning("replacementPrefabs が設定されていません！");
            return null;
        }

        GameObject nextPrefab = replacementPrefabs[replacementIndex];
        replacementIndex = (replacementIndex + 1) % replacementPrefabs.Length;

        Vector3 spawnPosition = transform.position;
        GameObject newObj = Instantiate(nextPrefab, spawnPosition, Quaternion.identity);

        TimeSlider2 newScript = newObj.GetComponent<TimeSlider2>();
        if (newScript != null)
        {
            newScript.slider = this.slider;
            newScript.SetPositionHistory(this.GetPositionHistory());
            newScript.replacementPrefabs = this.replacementPrefabs;
            newScript.replacementIndex = this.replacementIndex;
        }

        Destroy(this.gameObject);

        return newObj; // ← ここで返す！
    }

    public void ObjectChanged(GameObject newObject) //新しいオブジェクトにスライダーを引継ぎ自身を削除
    {

        
        if (newObject == null) return;

        var newSliderScript = newObject.GetComponent<TimeSlider2>();
        if (newSliderScript != null)
        {
            newSliderScript.slider = this.slider;
            newSliderScript.SetPositionHistory(this.GetPositionHistory());
        }

        Destroy(this.gameObject);
        
    }

    public void OnSliderMovedByUser(float value) //スライダーが引き戻されたときにプレイヤー座標を巻き戻す
    {
        isManualInput = true;

        int index = Mathf.Clamp((int)(value * 10f), 0, positionHistory.Length - 1);
        if (positionHistory[index] != Vector3.zero)
        {
            transform.position = positionHistory[index];
        }
    }
}
