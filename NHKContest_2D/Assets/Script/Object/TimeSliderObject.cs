﻿using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections;
using UnityEngine.UI;

public class TimeSliderObject : TimeSliderObject_Base
{
    private Vector3[] positionHistory = new Vector3[3000];
    private int currentIndex = 0;

    public Slider slider; //スライダー
    public GameObject ImageChanger;//画質更新するオブジェクト

    public GameObject[] replacementPrefabs;
    public int replacementIndex = 0;

    private float revertTimer = 0f;
    private float revertTimeLimit = 8f; // 5秒で戻す
    private bool isBeingDestroyed = false;

    private int Currentnum = 0;//配列の何番目にいるか

    

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

        /*時間経過の部分無効化する
        revertTimer += Time.deltaTime;
        if (revertTimer >= revertTimeLimit)
        {
            TryRevertObject();
            revertTimer = 0f; // タイマーリセット
        }
        */
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

    public void RewindToSlider(float sliderValue)//オブジェクトが戻る
    {
        int index = Mathf.RoundToInt(sliderValue * 10f);
        if (index >= 0 && index < positionHistory.Length)
        {
            transform.position = positionHistory[index];
            currentIndex = index;
        }
    }

    public override GameObject ReplaceObject()//オブジェクト入れ替え(後ろ)
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
            newScript.ImageChanger = this.ImageChanger;
            newScript.replacementPrefabs = this.replacementPrefabs;
            newScript.replacementIndex = this.replacementIndex;
            newScript.Currentnum = this.Currentnum; //配列番号更新処理を追加
            newScript.positionHistory = this.positionHistory;
        }

        //スライダーに新しいオブジェクト入れる
        var counter = slider.GetComponent<SliderTimeCounter>();
        if (counter != null)
        {
            Debug.LogWarning("配列設定" + Currentnum);
            counter.SetCurrentObjects(newObj, Currentnum);
        }

        var IC_counter = slider.GetComponent<ImageChanger>();
        if (IC_counter != null)
        {
            Debug.LogWarning("配列設定" + Currentnum);
            IC_counter.SetCurrentObjects(newObj, Currentnum);
        }

        //画質変更するやつに新しいオブジェクト入れる

        var IC_Counter = ImageChanger.GetComponent<ImageChanger>();
        if (IC_Counter != null)
        {
            Debug.LogWarning("配列設定" + Currentnum);
            IC_Counter.SetCurrentObjects(newObj, Currentnum);
        }
        

        Destroy(this.gameObject);

        return newObj;
    }

    public override void TryRevertObject()//オブジェクト入れ替え(前)
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
            newScript.ImageChanger = this.ImageChanger;
            newScript.replacementPrefabs = this.replacementPrefabs;
            newScript.Currentnum = this.Currentnum; //配列番号更新処理を追加
            newScript.positionHistory = this.positionHistory;

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

        var IC_Counter = ImageChanger.GetComponent<ImageChanger>();
        if (IC_Counter != null)
        {
            Debug.LogWarning("配列設定" + Currentnum);
            IC_Counter.SetCurrentObjects(newObj, Currentnum);
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

    public override void SetCurrentnum(int num)
    {
        //配列が設定されたよ
        Debug.LogWarning("配列設定" + num);
        Currentnum = num;
    }

    public override void ChangeImageQuality(int num)//オブジェクト入れ替え(前)
    {
        if (replacementPrefabs == null || replacementPrefabs.Length == 0)
            return;

        if (replacementIndex == num)
        {
            Debug.LogWarning("これ以上戻れない！");
            return;
        }

        // ここでいったん減らす（戻す）
        replacementIndex = num;

        Vector3 spawnPosition = transform.position;
        GameObject prevPrefab = replacementPrefabs[replacementIndex];
        GameObject newObj = Instantiate(prevPrefab, spawnPosition, transform.rotation);

        var newScript = newObj.GetComponent<TimeSliderObject>();
        if (newScript != null)
        {
            newScript.slider = this.slider;
            newScript.ImageChanger = this.ImageChanger;
            newScript.replacementPrefabs = this.replacementPrefabs;
            newScript.Currentnum = this.Currentnum; //配列番号更新処理を追加
            newScript.positionHistory = this.positionHistory;

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

        var IC_Counter = ImageChanger.GetComponent<ImageChanger>();
        if (IC_Counter != null)
        {
            Debug.LogWarning("配列設定" + Currentnum);
            IC_Counter.SetCurrentObjects(newObj, Currentnum);
        }

        StartCoroutine(DestroyAfterFrame());
    }

}
