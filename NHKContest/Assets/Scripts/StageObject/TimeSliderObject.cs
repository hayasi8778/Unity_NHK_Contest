using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections;
using UnityEngine.UI;

public class TimeSliderObject : MonoBehaviour
{
    private Vector3[] positionHistory = new Vector3[3000];
    private int currentIndex = 0;

    public Slider slider; //スライダー

    public GameObject[] replacementPrefabs;
    public int replacementIndex = 0;

    private float revertTimer = 0f;
    private float revertTimeLimit = 5f; // 5秒で戻す
    private bool isBeingDestroyed = false;

    void Start()
    {
        for (int i = 0; i < positionHistory.Length; i++)
        {
            positionHistory[i] = transform.position;
        }
    }

    void Update()
    {
        if (isBeingDestroyed) return;

        revertTimer += Time.deltaTime;
        if (revertTimer >= revertTimeLimit)
        {
            TryRevertObject();
            revertTimer = 0f; // タイマーリセット
        }
    }

    public void UpdatePositionHistory(float sliderValue)
    {
        int index = Mathf.RoundToInt(sliderValue * 10f);
        if (index >= 0 && index < positionHistory.Length)
        {
            positionHistory[index] = transform.position;
            currentIndex = index;
        }
    }

    public void RewindToSlider(float sliderValue)
    {
        int index = Mathf.RoundToInt(sliderValue * 10f);
        if (index >= 0 && index < positionHistory.Length)
        {
            transform.position = positionHistory[index];
            currentIndex = index;
        }
    }

    public GameObject ReplaceObject()
    {
        if (replacementPrefabs == null || replacementPrefabs.Length == 0)
            return null;

        if (replacementIndex >= replacementPrefabs.Length - 1)
        {
            Debug.LogWarning("最後のオブジェクトなので入れ替えしません");
            return null;
        }

        replacementIndex++;

        Vector3 spawnPosition = transform.position;
        GameObject nextPrefab = replacementPrefabs[replacementIndex];
        GameObject newObj = Instantiate(nextPrefab, spawnPosition, transform.rotation);

        // ここでreplacement情報を引き継ぐ！！
        var newScript = newObj.GetComponent<TimeSliderObject>();
        if (newScript != null)
        {
            newScript.slider = this.slider;
            newScript.replacementPrefabs = this.replacementPrefabs;
            newScript.replacementIndex = this.replacementIndex;
        }

        Destroy(this.gameObject);

        return newObj;
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

        var newScript = newObj.GetComponent<TimeSliderObject>();
        if (newScript != null)
        {
            newScript.slider = this.slider;
            newScript.replacementPrefabs = this.replacementPrefabs;

            // 🔥 注意！！戻った後のオブジェクトでは「次に行けるよう」replacementIndexを1つ進めた値にする！
            newScript.replacementIndex = this.replacementIndex;
        }

        // ここでスライダー側に「新しいオブジェクト」を教える！
        var counter = slider.GetComponent<SliderTimeCounter>();
        if (counter != null)
        {
            counter.SetCurrentObjects(newObj, 0);
        }

        StartCoroutine(DestroyAfterFrame());
    }

    private IEnumerator DestroyAfterFrame()
    {
        yield return null; // 1フレーム待ってから
        if (this != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Debug.LogWarning("ゲームオブジェクト消せてないかも");
        }
    }

}
