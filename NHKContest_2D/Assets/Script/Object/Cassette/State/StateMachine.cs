using UnityEngine;
public class StateMachine : MonoBehaviour
{
    [SerializeField]
    private IState currentState;    // ���݂̏��

    /// <summary>
    /// ��Ԃ�ς���
    /// </summary>
    public void ChangeState(string _stateName)
    {
        IState newState;    // ���̏��

        // ���͂̌���
        if (newState = transform.Find(_stateName).GetComponent<IState>())
        { 
            currentState?.StateExit();  // ���݂̏�Ԃ����݂���ꍇ�A�I���������Ăяo��
            currentState = newState;    // �V������Ԃ����݂̏�Ԃɐݒ�
            currentState.parent = this; // �V������Ԃɖ{�̂̃p�X��ݒ�
            currentState.StateEnter();  // �V������Ԃ̏��������������s
        }
        else
        {
            Debug.Log("���݂��Ȃ���Ԃ��w�肵�Ă��");
        }
    }

    private void Start()
    {
        // �����l�̏�Ԃ�Enter�����s
        currentState?.StateEnter();
    }
    private void Update()
    {
        // ���݂�Update�����s
        currentState?.StateUpdate();
    }
}
