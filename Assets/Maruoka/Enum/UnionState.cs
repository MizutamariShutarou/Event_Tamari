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

    /// <summary> �T���^�𓊂��� </summary>
    THROW_SANTA,
    /// <summary> ���T�T�r�T���^ </summary>
    FLYING_SQUIRREL_SANTA,
    /// <summary> �T���^�K�[�h </summary>
    SANTA_GUARD,
    /// <summary> �e�𔭖C���� </summary>
    REINDEER_FIRE
}