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
    DETH,

    /// <summary> サンタを投げる </summary>
    THROW_SANTA,
    /// <summary> ムササビサンタ </summary>
    FLYING_SQUIRREL_SANTA,
    /// <summary> サンタガード </summary>
    SANTA_GUARD,
    /// <summary> 銃を発砲する </summary>
    REINDEER_FIRE
}