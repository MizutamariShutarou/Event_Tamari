using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class SwitchController : MonoBehaviour
{
    [SerializeField, Tooltip("��O�̃h�A")] GameObject _flontDoor;
    [SerializeField, Tooltip("���̃h�A")] GameObject _backDoor;

    //[SerializeField] 
    //private UnityEvent _myEvent = new UnityEvent();
    void Start()
    {
        
    }

    void Update()
    {

    }

    //public void TestFunc()
    //{
    //    _myEvent.Invoke();
    //}

    /// <summary>
    /// �T���^�ɓ��܂ꂽ������J��
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("���܂ꂽ�̂Ŕ��J��");

    }

    /// <summary>
    /// �T���^�����ꂽ�����߂�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("���ꂽ�̂Ŕ��߂�");
    }
}
