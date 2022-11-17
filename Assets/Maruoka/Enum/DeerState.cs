using System;
using UnityEngine;

[System.Serializable]
public enum DeerState
{
    IDLE,
    MOVE,
    FLY_UP,
    FALL_DOWN,
    DAMAGE,
    DETH,

    /// <summary> �p�\�[�h�U�� </summary>
    HORN_SWORD_ATTACK,
    /// <summary> �ːi�U�� </summary>
    RUSH_ATTACK,

    /// <summary> �T���^����J��񂹂�i�n��j�i���C���[�A�N�V�������j </summary>
    HAUL_IN_SANTA_GROUND,
    /// <summary> �T���^����J��񂹂�i�󒆁j�i���C���[�A�N�V�������j </summary>
    HAUL_IN_SANTA_AIR,

    /// <summary> ���C���[����������Ԃőҋ@�i�n��j�i���C���[�A�N�V�������j </summary>
    WAIT_HAUL_IN_SANTA_GROUND,
    /// <summary> ���C���[����������Ԃőҋ@�i�󒆁j�i���C���[�A�N�V�������j </summary>
    WAIT_HAUL_IN_SANTA_AIR,
}