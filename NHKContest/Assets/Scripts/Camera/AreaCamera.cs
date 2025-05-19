using UnityEngine;

public class AreaCamera : MonoBehaviour
{
    [SerializeField]
    private float cameraZ = -6;
    
    FollowPlayerScript followPlayerScript;

    private void Start()
    {
        followPlayerScript = GameObject.FindWithTag("MainCamera").GetComponent<FollowPlayerScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            followPlayerScript.offset.z = cameraZ;
        }
    }

}
