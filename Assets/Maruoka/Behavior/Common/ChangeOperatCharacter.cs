using System;
using UnityEngine;

/// <summary>
/// ����L�����̕ύX�𐧌䂷��N���X
/// </summary>
[System.Serializable]
public class ChangeOperatCharacter
{
    [InputName, SerializeField]
    protected string _changeButtonName = default;
    [SerializeField]
    protected bool _isReadyChange = false;
    [Tooltip("���_�̖��O"), SerializeField]
    private OperableCharacter _buddyName = OperableCharacter.NOT_SET;

    public void Update()
    {
        if (IsRun())
        {
            Debug.Log("����L������ύX���܂�");
            // �����ɑ���L������ύX����R�[�h���L�q����B
            OperableCharacterManager.Instance.SwapSantaAndDeer(_buddyName);
        }
    }

    protected virtual bool IsRun()
    {
        // _isReadyChange &&
        //     Input.GetButtonDown(_changeButtonName)
        return false;
    }
}