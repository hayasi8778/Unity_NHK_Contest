using Unity.Cinemachine;
using UnityEditor.Rendering;
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

    //時間で画質戻すための処理
    private float revertTimer = 0f;
    private float revertTimeLimit = 5f; // 5秒で戻す
    private bool isBeingDestroyed = false; //削除フラグ
    private GameObject currentPlayer; //操作対象のプレイヤー


    public CinemachineCamera virtualCamera;


    void Start()
    {

        //謎回転するのを修正する
        //transform.rotation = Quaternion.identity;

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

        if (isBeingDestroyed) return; //消されるならアップデート動かさない

        if (!isRewinding) //巻き戻し中じゃないなら座標を配列に追加
        {
            int index = Mathf.RoundToInt(slider.value * 10); 
            if (index >= 0 && index < positionHistory.Length)
            {
                positionHistory[index] = transform.position;
                currentIndex = index;
            }
        }

        //タイマーで5秒経ったら前に戻す処理
        revertTimer += Time.deltaTime;
        if (revertTimer >= revertTimeLimit)
        {
            TryRevertObject();
            revertTimer = 0f; // タイマーリセット
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

        // 最後まで来たら入れ替えしない

        // 🔍 今の自分が replacementPrefabs の最後のプレハブと同じなら入れ替えしない
        if (replacementPrefabs[replacementPrefabs.Length - 1].name == this.gameObject.name.Replace("(Clone)", "").Trim())
        {
            Debug.LogWarning("最後のオブジェクトなので入れ替えしません");
            return null;
        }
        
        replacementIndex = (replacementIndex + 1) % replacementPrefabs.Length;

        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 0.5f; // 0.5f上にずらす
        Quaternion spawnRotation = Quaternion.Euler(90f, 90f, -90f);

        GameObject nextPrefab = replacementPrefabs[replacementIndex];

        GameObject newObj = Instantiate(nextPrefab, spawnPosition, spawnRotation);

        // 新しいオブジェクトに情報を渡す
        TimeSlider2 newScript = newObj.GetComponent<TimeSlider2>();
        PlayerJump newjump = newObj.GetComponent<PlayerJump>();
        newObj.SetActive(true); // 念のためアクティブ化

        if (newScript != null)
        {
            newScript.slider = this.slider;
            newScript.SetPositionHistory(this.GetPositionHistory());
            newScript.replacementPrefabs = this.replacementPrefabs;
            newScript.replacementIndex = this.replacementIndex;
            newScript.virtualCamera = this.virtualCamera;
        }

        if (newjump != null) 
        {
            PlayerJump tihsjump = this.GetComponent<PlayerJump>();
            newjump.SetNewGravityFlag(tihsjump.GetGravityFlag());
        }

        // カメラの追従対象も更新する
        Camera mainCamera = Camera.main;
        //バーチャルカメラ使うからこっちに変更する
        //Debug.Log("バーチャルカメラ変更");
        virtualCamera.Follow = newObj.transform;
        virtualCamera.LookAt = newObj.transform;


        //そのままだと入れ替え時に角度バグるから矯正する
        newObj.transform.rotation = Quaternion.Euler(newObj.transform.rotation.x, newObj.transform.rotation.y, 0f);
        //Debug.LogError("透明なの直したい");

        //最後に自分を消す！
        Destroy(this.gameObject);

        return newObj;
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

    private void TryRevertObject()//オブジェクトが画質よくなる
    {
        Debug.LogWarning($"[TryRevert] Current replacementIndex: {replacementIndex}");

        if (replacementIndex > 0)
        {
            GameObject prevPrefab = replacementPrefabs[replacementIndex - 1];

            if (prevPrefab == null)
            {
                Debug.LogError("戻ろうとしたPrefabがnullです！");
                return;
            }

            Vector3 spawnPosition = transform.position;
            spawnPosition.y += 0.5f;
            Quaternion spawnRotation = Quaternion.Euler(90f, 90f, -90f);

            GameObject newObj = Instantiate(prevPrefab, spawnPosition, spawnRotation);
            newObj.SetActive(true); // 念のためアクティブ化

            Debug.Log($"[TryRevert] Instantiated: {newObj.name}");

            TimeSlider2 newScript = newObj.GetComponent<TimeSlider2>();
            PlayerJump newjump = newObj.GetComponent<PlayerJump>();
            if (newScript != null)
            {
                newScript.slider = this.slider;
                newScript.SetPositionHistory(this.GetPositionHistory());
                newScript.replacementPrefabs = this.replacementPrefabs;
                newScript.replacementIndex = this.replacementIndex - 1;
                newScript.virtualCamera = this.virtualCamera;
            }

            if (newjump != null)
            {
                PlayerJump tihsjump = this.GetComponent<PlayerJump>();
                newjump.SetNewGravityFlag(tihsjump.GetGravityFlag());
            }

            // 🔥ここでスライダー側に「新しいプレイヤー」を教える！
            var counter = slider.GetComponent<SliderTimeCounter>();
            if (counter != null)
            {
               counter.SetCurrentPlayer(newObj);
            }

            //カメラ切り替え
            virtualCamera.Follow = newObj.transform;
            virtualCamera.LookAt = newObj.transform;

            /*
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                FollowPlayerScript followScript = mainCamera.GetComponent<FollowPlayerScript>();
                if (followScript != null)
                {
                    followScript.SetTarget(newObj.transform);
                }
            }
            */

            //そのままだと入れ替え時に角度バグるから矯正する
            newObj.transform.rotation = Quaternion.Euler(newObj.transform.rotation.x, newObj.transform.rotation.y, 0f);
            //Debug.LogError("透明なの直したい");

            Debug.Log("画質向上");

            //Destroy(this.gameObject); // 念のためnullチェックしてDestroy

            StartCoroutine(DestroyAfterFrame());
        }
        else
        {
            //Debug.LogError(replacementIndex);
            Debug.LogWarning("これ以上戻れない！");
        }
    }

    private System.Collections.IEnumerator DestroyAfterFrame()
    {
        yield return null; // 1フレーム待ってから

        

        if (this != null)
        {
            Destroy(this.gameObject); // 念のためnullチェックしてDestroy
        }
    }

    public void SetCurrentPlayer(GameObject player)
    {
        currentPlayer = player;
    }

    //ギリ使うかもやから旧ObjectChangedおいておく
    /*
   public void ObjectChanged(GameObject newObject) //新しいオブジェクトにスライダーを引継ぎ自身を削除
   {
       if (newObject == null) return;

       newObject.SetActive(true); // 念のためアクティブ化

       GameObject nextPrefab = replacementPrefabs[replacementIndex];
       replacementIndex = (replacementIndex + 1) % replacementPrefabs.Length;

       Vector3 spawnPosition = transform.position;
       GameObject newObj = Instantiate(nextPrefab, spawnPosition, Quaternion.identity);

       // 🔽 ここで角度を引き継ぐ
       newObj.transform.rotation = this.transform.rotation;

       var newSliderScript = newObject.GetComponent<TimeSlider2>();

       if (newSliderScript != null)
       {

           newSliderScript.slider = this.slider;
           newSliderScript.SetPositionHistory(this.GetPositionHistory());
           newSliderScript.replacementPrefabs = this.replacementPrefabs;
           newSliderScript.replacementIndex = this.replacementIndex;

       }

       Destroy(this.gameObject);

   }
   */
}



