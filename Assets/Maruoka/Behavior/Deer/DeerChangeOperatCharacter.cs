using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeerChangeOperatCharacter : ChangeOperatCharacter
{
    private DeerStateController _stateController = null;

    public void Init(DeerStateController stateController)
    {
        _stateController = stateController;
    }
    public override void Update()
    {
        if (IsRun())
        {
            Debug.Log("操作キャラを変更します");
            // ここに操作キャラを変更するコードを記述する。
            OperableCharacterManager.Instance.SwapSantaAndDeer(_buddyName);
            // Move状態であれば移動を停止する
            if (_stateController.CurrentState == DeerState.MOVE)
            {
                _stateController.Rb2D.velocity = Vector2.zero;
            }
        }
    }
    protected override bool IsRun()
    {
        bool result = false;

        result =
          (_stateController.CurrentState == DeerState.IDLE ||
          _stateController.CurrentState == DeerState.MOVE);

        _isReadyChange = result;

        return result && Input.GetButtonDown(_changeButtonName);
    }
}
