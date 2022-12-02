using System;
using UnityEngine;

/// <summary>
/// 合体制御クラス
/// </summary>
[System.Serializable]
public class CombineController
{
    [InputName, SerializeField]
    private string _combineButtonName = default;
    [SerializeField]
    private bool _isReadyCanCombine = false;

    /// <summary>
    /// 合体する
    /// </summary>
    public void Update()
    {
        if (_isReadyCanCombine &&
            Input.GetButtonDown(_combineButtonName))
        {
            Debug.Log("合体命令が下された。");
            OperableCharacterManager.Instance.Coalesce();
        }
    }
    /// <summary>
    /// 合体可能にする
    /// </summary>
    public void OnPossibleCombine()
    {
        _isReadyCanCombine = true;
    }
    /// <summary>
    /// 合体不可能にする
    /// </summary>
    public void OnImpossibleCombine()
    {
        _isReadyCanCombine = false;
    }
}