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

    /// <summary> �T���^�𓊂��� </summary>
    THROW_SANTA,
    /// <summary> ���T�T�r�T���^�i�㏸���[�h�j </summary>
    FLYING_SQUIRREL_SANTA_FLY_UP,
    /// <summary> ���T�T�r�T���^�i���󃂁[�h�j </summary>
    FLYING_SQUIRREL_SANTA_NOMAL,
    /// <summary> �T���^�K�[�h </summary>
    SANTA_GUARD,
    /// <summary> �e�𔭖C���� </summary>
    REINDEER_FIRE
}