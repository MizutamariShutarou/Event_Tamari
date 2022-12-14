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

    /// <summary> ���C���[�A�N�V���� : ���n�҂� </summary>
    WIRE_DO_NOTHING,
    /// <summary> ���C���[�A�N�V���� : �R�T���� </summary>
    WIRE_MOVE,
    /// <summary> ���C���[�A�N�V���� : �g�i�J�C�����҂� </summary>
    WIRE_STOP_MOVE,

    /// <summary> �ꎞ�I�Ȏ� </summary>
    TEMPORARY_DEATH,
}