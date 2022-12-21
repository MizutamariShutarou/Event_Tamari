using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class SwitchController : GimmickBase
{
    [SerializeField] string _santaLayer;

    /// <summary>
    /// スイッチ踏んだ時
    /// </summary>
    protected override void EnterFunc()
    {
        _enterEvent?.Invoke();
    }

    /// <summary>
    /// スイッチから離れた時
    /// </summary>
    protected override void ExitFunc()
    {
        _exitEvent?.Invoke();
    }

    /// <summary>
    /// サンタに踏まれたら扉を開く
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer(_santaLayer))
        {
            Debug.Log("踏まれたので扉開く");
            EnterFunc();
        }
    }

    /// <summary>
    /// サンタが離れたら扉を閉める
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_santaLayer))
        {
            Debug.Log("離れたので扉閉める");
            //ExitFunc();
        }
    }
}
