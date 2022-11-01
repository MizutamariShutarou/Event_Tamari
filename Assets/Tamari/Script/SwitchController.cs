using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class SwitchController : MonoBehaviour
{
    [SerializeField] 
    private UnityEvent _enterEvent = default;
    [SerializeField]
    private UnityEvent _exitEvent = default;
    
    /// <summary>
    /// �X�C�b�`���񂾎�
    /// </summary>
    public void EnterFunc()
    {
        _enterEvent?.Invoke();
    }

    /// <summary>
    /// �X�C�b�`���痣�ꂽ��
    /// </summary>
    public void ExitFunc()
    {
        _exitEvent?.Invoke();
    }

    /// <summary>
    /// �T���^�ɓ��܂ꂽ������J��
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("���܂ꂽ�̂Ŕ��J��");
        EnterFunc();
    }

    /// <summary>
    /// �T���^�����ꂽ�����߂�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("���ꂽ�̂Ŕ��߂�");
        ExitFunc();
    }
}
