using System;
using UnityEngine;

/// <summary>
/// ����L�����̕ύX�𐧌䂷��N���X
/// </summary>
[System.Serializable]
public class ChangeOperatCharacter
{
    [InputName, SerializeField]
    private string _changeButtonName = default;

    [SerializeField]
    private bool _isReadyChange = false;

    public void OnChangeOperatCharacter()
    {
        if (_isReadyChange &&
            Input.GetButtonDown(_changeButtonName))
        {
            Debug.Log("����L������ύX���܂�");
            // �����ɑ���L������ύX����R�[�h���L�q����B
        }

    }
}