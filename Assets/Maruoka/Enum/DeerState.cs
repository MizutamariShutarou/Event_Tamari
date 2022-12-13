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

    /// <summary> 角ソード攻撃 </summary>
    HORN_SWORD_ATTACK,
    /// <summary> 突進攻撃 </summary>
    RUSH_ATTACK,

    /// <summary> サンタを手繰り寄せる（地上）（ワイヤーアクション時） </summary>
    HAUL_IN_SANTA_GROUND,
    /// <summary> サンタを手繰り寄せる（空中）（ワイヤーアクション時） </summary>
    HAUL_IN_SANTA_AIR,

    /// <summary> ワイヤーアクション : サンタの着地・崖探し待ち </summary>
    WIRE_DO_NOTHING,
    /// <summary> ワイヤーアクション : サンタの元まで移動する </summary>
    WIRE_MOVE,
    /// <summary> ワイヤーアクション : サンタの元までワイヤーを昇る </summary>
    WIRE_CLIMB,
}