using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class SwitchController : GimmickBase
{
    [SerializeField] string _santaLayer;

    /// <summary>
    /// �X�C�b�`���񂾎�
    /// </summary>
    protected override void EnterFunc()
    {
        _enterEvent?.Invoke();
    }

    /// <summary>
    /// �X�C�b�`���痣�ꂽ��
    /// </summary>
    protected override void ExitFunc()
    {
        _exitEvent?.Invoke();
    }

    /// <summary>
    /// �T���^�ɓ��܂ꂽ������J��
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer(_santaLayer))
        {
            Debug.Log("���܂ꂽ�̂Ŕ��J��");
            EnterFunc();
        }
    }

    /// <summary>
    /// �T���^�����ꂽ�����߂�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_santaLayer))
        {
            Debug.Log("���ꂽ�̂Ŕ��߂�");
            //ExitFunc();
        }
    }
}
