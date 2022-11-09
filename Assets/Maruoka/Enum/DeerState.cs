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

    /// <summary> 角ソード攻撃 </summary>
    HORN_SWORD_ATTACK,
    /// <summary> 突進攻撃 </summary>
    RUSH_ATTACK,

    /// <summary> サンタを手繰り寄せる（地上）（ワイヤーアクション時） </summary>
    HAUL_IN_SANTA_GROUND,
    /// <summary> サンタを手繰り寄せる（空中）（ワイヤーアクション時） </summary>
    HAUL_IN_SANTA_AIR,

    /// <summary> ワイヤーを握った状態で待機（地上）（ワイヤーアクション時） </summary>
    WAIT_HAUL_IN_SANTA_GROUND,
    /// <summary> ワイヤーを握った状態で待機（空中）（ワイヤーアクション時） </summary>
    WAIT_HAUL_IN_SANTA_AIR,
}