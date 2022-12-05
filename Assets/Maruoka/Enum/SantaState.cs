using System;
using UnityEngine;

[System.Serializable]
public enum SantaState
{
    IDLE,
    MOVE,
    JUMP,
    FLY_UP,
    FALL_DOWN,
    DAMAGE,
    DEATH,

    /// <summary> ����Idle </summary>
    CREEPING_IDLE,
    /// <summary> ����Move </summary>
    CREEPING_MOVE,

    /// <summary> ����������i���C���[�A�N�V�����p�j </summary>
    BE_DRAGGED,
    /// <summary> ����������i���C���[�A�N�V�����p </summary>
    GET_CAUGHT,

    /// <summary> �ꎞ�I�Ȏ� </summary>
    TEMPORARY_DEATH,
}