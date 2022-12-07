using System;
using UnityEngine;

/// <summary>
/// 分離命令
/// </summary>
[System.Serializable]
public class SeparationInstruction
{
    [InputName, SerializeField]
    private string _separationButtonName = default;
    [SerializeField]
    private bool _isReadySeparation = false;

    private UnionStateController _stateController = null;

    public void OnReadySeparation()
    {
        _isReadySeparation = true;
    }
    public void OnDisableSeparation()
    {
        _isReadySeparation = false;
    }
    public void Init(GameObject union, UnionStateController stateController)
    {
        OperableCharacterManager.Instance.SetUnion(union);
        _stateController = stateController;
    }
    public void Update()
    {
        if (IsReady())
        {
            OperableCharacterManager.Instance.Separate();
            Debug.Log("分離します");
        }
    }
    private bool IsReady()
    {
        bool result = false;

        result =
            Input.GetButtonDown(_separationButtonName) &&
            (_stateController.CurrentState == UnionState.IDLE ||
            _stateController.CurrentState == UnionState.MOVE);

        return result;
    }
}