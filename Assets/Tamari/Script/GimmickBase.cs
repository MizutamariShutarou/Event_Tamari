using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GimmickBase : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent _enterEvent = default;
    [SerializeField]
    protected UnityEvent _exitEvent = default;

    /// <summary>
    /// ギミック作動の時に呼ぶイベント
    /// </summary>
    protected virtual void EnterFunc()
    {

    }

    /// <summary>
    /// ギミック作動から離れた時に呼ぶイベント
    /// </summary>
    protected virtual void ExitFunc()
    {

    }

}
