using UnityEngine;

public class ImageChanger : MonoBehaviour
{
    public GameObject currentPlayer; //プレイヤー
    public GameObject[] currentObjects;//シーン内のオブジェクト

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
                Debug.Log("画質変更なし");
                return;
            }
            ImageQuality = 2;
            //まずプレイヤーの入れ替え
            if (currentPlayer != null)
            {
                TimeSlider2 script = currentPlayer.GetComponent<TimeSlider2>();
                if (script != null)
                {
                    script.ChangeImage(ImageQuality);
                    
                }
            }

            //オブジェクトの入れ替え
            for (int i = 0; i < currentObjects.Length; i++)
            {
                GameObject obj = currentObjects[i];
                if (obj == null) continue;
                //Debug.LogError("オブジェクトNULLじゃないです");

                //親オブジェクトを取得(子オブジェクトをアタッチしてても取得できる)
                TimeSliderObject_Base timeObj = obj.GetComponent<TimeSliderObject_Base>();

                if (timeObj != null)
                {
                    //Debug.LogError("オブジェクト切り替え処理します");
                    timeObj.ChangeImageQuality(ImageQuality); // 子クラスのオーバーライドされたメソッドが適用される
                    
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (ImageQuality == 1)//
            {
                Debug.Log("画質変更なし");
                return;
            }
            ImageQuality = 1;

            //まずプレイヤーの入れ替え
            if (currentPlayer != null)
            {
                TimeSlider2 script = currentPlayer.GetComponent<TimeSlider2>();
                if (script != null)
                {
                    script.ChangeImage(ImageQuality);

                }
            }

            //オブジェクトの入れ替え
            for (int i = 0; i < currentObjects.Length; i++)
            {
                GameObject obj = currentObjects[i];
                if (obj == null) continue;
                //Debug.LogError("オブジェクトNULLじゃないです");

                //親オブジェクトを取得(子オブジェクトをアタッチしてても取得できる)
                TimeSliderObject_Base timeObj = obj.GetComponent<TimeSliderObject_Base>();

                if (timeObj != null)
                {
                    //Debug.LogError("オブジェクト切り替え処理します");
                    timeObj.ChangeImageQuality(ImageQuality); // 子クラスのオーバーライドされたメソッドが適用される

                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (ImageQuality <= 0)//
            {
                ImageQuality = 0;
                Debug.Log("画質変更なし");
                return;
            }
            ImageQuality = 0;

            //まずプレイヤーの入れ替え
            if (currentPlayer != null)
            {
                TimeSlider2 script = currentPlayer.GetComponent<TimeSlider2>();
                if (script != null)
                {
                    script.ChangeImage(ImageQuality);
                }
            }

            //オブジェクトの入れ替え
            for (int i = 0; i < currentObjects.Length; i++)
            {
                GameObject obj = currentObjects[i];
                if (obj == null) continue;
                //Debug.LogError("オブジェクトNULLじゃないです");

                //親オブジェクトを取得(子オブジェクトをアタッチしてても取得できる)
                TimeSliderObject_Base timeObj = obj.GetComponent<TimeSliderObject_Base>();

                if (timeObj != null)
                {
                    //Debug.LogError("オブジェクト切り替え処理します");
                    timeObj.ChangeImageQuality(ImageQuality); // 子クラスのオーバーライドされたメソッドが適用される
                
                }
            }
        }
    }

    public void SetCurrentObjects(GameObject objects, int it)
    {
        Debug.LogWarning("ステージオブジェクト継承_IC");
        currentObjects[it] = objects;
    }

    public void SetCurrentPlayer(GameObject player)
    {
        currentPlayer = player;
    }

}
