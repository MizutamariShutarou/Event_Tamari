using System;
using UnityEngine;

/// <summary>
/// ����L�����̕ύX�𐧌䂷��N���X
/// </summary>
[System.Serializable]
public abstract class ChangeOperatCharacter
{
    [InputName, SerializeField]
    protected string _changeButtonName = default;
    [SerializeField]
    protected bool _isReadyChange = false;
    [Tooltip("���_�̖��O"), SerializeField]
    protected OperableCharacter _buddyName = OperableCharacter.NOT_SET;

    public virtual void Update()
    {
        if (IsRun())
        {
            Debug.Log("����L������ύX���܂�");
            // �����ɑ���L������ύX����R�[�h���L�q����B
            OperableCharacterManager.Instance.SwapSantaAndDeer(_buddyName);
        }
    }

    protected abstract bool IsRun();
}