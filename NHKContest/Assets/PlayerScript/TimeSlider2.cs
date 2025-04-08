using UnityEngine;
using UnityEngine.UI;

public class TimeSlider2 : MonoBehaviour
{
    public Slider slider;
    private Vector3[] positionHistory = new Vector3[3000];
    private int currentIndex = 0;
    private bool isRewinding = false;

    private bool isManualInput = false;

    void Start()
    {
        for (int i = 0; i < positionHistory.Length; i++)
        {
            positionHistory[i] = transform.position;
        }
    }

    void Update()
    {
        if (!isRewinding)
        {
            int index = Mathf.RoundToInt(slider.value * 10);
            if (index >= 0 && index < positionHistory.Length)
            {
                positionHistory[index] = transform.position;
                currentIndex = index;
            }
        }
    }

    public void OnSliderValueChanged()
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

    public Vector3[] GetPositionHistory()
    {
        return positionHistory;
    }

    public void SetPositionHistory(Vector3[] history)
    {
        if (history.Length == positionHistory.Length)
        {
            for (int i = 0; i < positionHistory.Length; i++)
            {
                positionHistory[i] = history[i];
            }
        }
    }

    public void ObjectChanged(GameObject newObject)
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

    public void OnSliderMovedByUser(float value)
    {
        isManualInput = true;

        int index = Mathf.Clamp((int)(value * 10f), 0, positionHistory.Length - 1);
        if (positionHistory[index] != Vector3.zero)
        {
            transform.position = positionHistory[index];
        }
    }
}
