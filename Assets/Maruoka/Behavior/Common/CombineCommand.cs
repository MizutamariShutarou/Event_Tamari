using System;
using UnityEngine;

/// <summary>
/// 合体命令
/// </summary>
[System.Serializable]
public class CombineCommand
{
    private bool _isCanCombine = false;

    [InputName, SerializeField]
    private string _combineButtonName = default;

    /// <summary>
    /// 合体する
    /// </summary>
    public void Combine()
    {
        if (_isCanCombine && Input.GetButtonDown(_combineButtonName))
        {
            Debug.Log("合体命令が下された。"); // テストコード
            // ここに合体の処理を記述する。
        }
    }
    /// <summary>
    /// 合体可能にする
    /// </summary>
    public void OnPossibleCombine()
    {
        _isCanCombine = true;
    }
    /// <summary>
    /// 合体不可能にする
    /// </summary>
    public void OnImpossibleCombine()
    {
        _isCanCombine = false;
    }
}