using System;
using UnityEngine;

[System.Serializable]
public abstract class StateControllerBase<T> where T : Enum
{
    /// <summary>
    /// 現在のステート
    /// </summary>
    public T NowState => _nowState;
    /// <summary>
    /// 向いている方向を表す値
    /// </summary>
    public int FacingDirection => _facingDirection;

    protected T _nowState = default;
    protected int _facingDirection;

    /// <summary>
    /// ステート更新処理
    /// </summary>
    public abstract void Update();
}