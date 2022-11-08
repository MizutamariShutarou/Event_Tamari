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
    /// �M�~�b�N�쓮�̎��ɌĂԃC�x���g
    /// </summary>
    protected virtual void EnterFunc()
    {

    }

    /// <summary>
    /// �M�~�b�N�쓮���痣�ꂽ���ɌĂԃC�x���g
    /// </summary>
    protected virtual void ExitFunc()
    {

    }

}
