using TMPro;
using UnityEngine;

public class ChatFlg : MonoBehaviour
{
    public ChatManager chatManager;
    public string message = "コメントです";
    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            chatManager.AddMessage(message);
            triggered = true;
        }
    }
}
