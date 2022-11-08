using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : GimmickBase
{
    // <summary>
    /// �g�i�J�C���؂ɐG�ꂽ��
    /// </summary>
    protected override void EnterFunc()
    {
        _enterEvent?.Invoke();
    }

    /// <summary>
    /// �g�i�J�C���؂��甲������
    /// </summary>
    protected override void ExitFunc()
    {
        _exitEvent?.Invoke();
    }


    /// <summary>
    /// �g�i�J�C�̓ːi���󂯂���
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Deer"))
        {
            Debug.Log("�ːi���ꂽ");
            EnterFunc();
        }
    }

    /// <summary>
    /// �g�i�J�C���ːi��A�؂��甲������
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Deer"))
        {
            Debug.Log("�ːi����");
            ExitFunc();
        }
    }
}
