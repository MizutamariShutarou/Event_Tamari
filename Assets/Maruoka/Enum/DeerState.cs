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
    DEATH,

    /// <summary> �p�\�[�h�U�� </summary>
    HORN_SWORD_ATTACK,
    /// <summary> �ːi�U�� </summary>
    RUSH_ATTACK,

    /// <summary> �T���^����J��񂹂�i�n��j�i���C���[�A�N�V�������j </summary>
    HAUL_IN_SANTA_GROUND,
    /// <summary> �T���^����J��񂹂�i�󒆁j�i���C���[�A�N�V�������j </summary>
    HAUL_IN_SANTA_AIR,

    /// <summary> ���C���[�A�N�V���� : �T���^�̒��n�E�R�T���҂� </summary>
    WIRE_DO_NOTHING,
    /// <summary> ���C���[�A�N�V���� : �T���^�̌��܂ňړ����� </summary>
    WIRE_MOVE,
    /// <summary> ���C���[�A�N�V���� : �T���^�̌��܂Ń��C���[������ </summary>
    WIRE_CLIMB,
}