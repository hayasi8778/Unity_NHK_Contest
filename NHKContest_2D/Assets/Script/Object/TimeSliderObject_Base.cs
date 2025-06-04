using UnityEngine;

public abstract class TimeSliderObject_Base : MonoBehaviour
{
    

    // **抽象メソッド: 派生クラスでオーバーライド必須**
    public abstract GameObject ReplaceObject();

    public abstract void SetCurrentnum(int num);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
