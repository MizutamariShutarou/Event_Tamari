using System;
using UnityEngine;

/// <summary>
/// ����L������ύX����B<br/>
/// �i�P�ƍs�����̂ݎ��s�\�j
/// </summary>
[System.Serializable]
public class ChangeOperatCharacter
{
    [InputName, SerializeField]
    private string _changeButtonName = default;

    private void OnChangeOperatCharacter()
    {
        if (Input.GetButtonDown(_changeButtonName))
        {
            Debug.Log("����L������ύX���܂�");
            // �����ɑ���L������ύX����R�[�h���L�q����B
        }

    }
}