using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.05f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.05f, 0.0f, 0.0f);
        }
    }
}
