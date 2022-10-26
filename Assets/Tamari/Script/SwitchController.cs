using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class SwitchController : MonoBehaviour
{
    [SerializeField, Tooltip("手前のドア")] GameObject _flontDoor;
    [SerializeField, Tooltip("後ろのドア")] GameObject _backDoor;

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
    /// サンタに踏まれたら扉を開く
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("踏まれたので扉開く");

    }

    /// <summary>
    /// サンタが離れたら扉を閉める
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("離れたので扉閉める");
    }
}
