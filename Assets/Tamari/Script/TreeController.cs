using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : GimmickBase
{
    // <summary>
    /// トナカイが木に触れた時
    /// </summary>
    protected override void EnterFunc()
    {
        _enterEvent?.Invoke();
    }

    /// <summary>
    /// トナカイが木から抜けた時
    /// </summary>
    protected override void ExitFunc()
    {
        _exitEvent?.Invoke();
    }


    /// <summary>
    /// トナカイの突進を受けた時
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Deer"))
        {
            Debug.Log("突進された");
            EnterFunc();
        }
    }

    /// <summary>
    /// トナカイが突進後、木から抜けた時
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Deer"))
        {
            Debug.Log("突進完了");
            ExitFunc();
        }
    }
}
