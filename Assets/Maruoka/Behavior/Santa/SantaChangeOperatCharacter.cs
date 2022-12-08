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
            Debug.Log("操作キャラを変更します");
            // ここに操作キャラを変更するコードを記述する。
            OperableCharacterManager.Instance.SwapSantaAndDeer(_buddyName);
            // Move状態であれば移動を停止する
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
