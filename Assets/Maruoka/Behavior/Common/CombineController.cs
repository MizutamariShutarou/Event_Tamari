using System;
using UnityEngine;

/// <summary>
/// 合体制御クラス
/// </summary>
[System.Serializable]
public class CombineController
{
    [InputName, SerializeField]
    protected string _combineButtonName = default;
    [SerializeField]
    protected bool _isReadyCanCombine = false;

    /// <summary>
    /// 合体する
    /// </summary>
    public void Update()
    {
        if (IsRun())
        {
            Debug.Log("合体命令が下された。");
            OperableCharacterManager.Instance.Coalesce();
        }
    }
    protected virtual bool IsRun()
    {
        return false;
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