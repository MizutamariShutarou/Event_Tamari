using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SantaChangeOperatCharacter : ChangeOperatCharacter
{
    private SantaStateController _stateController = null;

    public void Init(SantaStateController stateController)
    {
        _stateController = stateController;
    }
    public override void Update()
    {
        if (IsRun())
        {
            Debug.Log("����L������ύX���܂�");
            // �����ɑ���L������ύX����R�[�h���L�q����B
            OperableCharacterManager.Instance.SwapSantaAndDeer(_buddyName);
            // Move��Ԃł���Έړ����~����
            if (_stateController.CurrentState == SantaState.MOVE)
            {
                _stateController.Rb2D.velocity = Vector2.zero;
            }
        }
    }
    protected override bool IsRun()
    {
        bool result = false;

        result =
          (_stateController.CurrentState == SantaState.IDLE ||
          _stateController.CurrentState == SantaState.MOVE);

        _isReadyChange = result;

        return result && Input.GetButtonDown(_changeButtonName);
    }
}
