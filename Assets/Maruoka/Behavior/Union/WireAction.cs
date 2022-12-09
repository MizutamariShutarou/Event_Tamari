using System;
using UnityEngine;

[System.Serializable]
public class WireAction
{
    [InputName, SerializeField]
    private string _fireButtonName = default;
    [SerializeField]
    private bool _isReadyFire = false;
    UnionStateController _stateController = null;

    public void Init(UnionStateController stateController)
    {
        _stateController = stateController;
    }

    public void Update()
    {
        if (IsRun())
        {
            Debug.Log("ワイヤーアクションを実行しました。");
            OperableCharacterManager.Instance.SeparateOnWireAction(_stateController.FacingDirection == FacingDirection.RIGHT);
        }
    }
    private bool IsRun()
    {
        bool result = false;

        result =
            _stateController.CurrentState == UnionState.IDLE ||
            _stateController.CurrentState == UnionState.MOVE;

        _isReadyFire = result;

        return result && Input.GetButtonDown(_fireButtonName);
    }
}