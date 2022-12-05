using System;
using UnityEngine;

[System.Serializable]
public enum UnionState
{
    IDLE,
    MOVE,
    FLY_UP,
    FALL_DOWN,
    DAMAGE,
    DEATH,

    /// <summary> サンタを投げる </summary>
    THROW_SANTA,
    /// <summary> ムササビサンタ（上昇モード） </summary>
    FLYING_SQUIRREL_SANTA_FLY_UP,
    /// <summary> ムササビサンタ（滑空モード） </summary>
    FLYING_SQUIRREL_SANTA_NOMAL,
    /// <summary> サンタガード </summary>
    SANTA_GUARD,
    /// <summary> 銃を発砲する </summary>
    REINDEER_FIRE
}