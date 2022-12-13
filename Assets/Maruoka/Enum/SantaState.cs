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

    /// <summary> 匍匐Idle </summary>
    CREEPING_IDLE,
    /// <summary> 匍匐Move </summary>
    CREEPING_MOVE,

    /// <summary> ワイヤーアクション : 着地待ち </summary>
    WIRE_DO_NOTHING,
    /// <summary> ワイヤーアクション : 崖探し中 </summary>
    WIRE_MOVE,
    /// <summary> ワイヤーアクション : トナカイ到着待ち </summary>
    WIRE_STOP_MOVE,

    /// <summary> 一時的な死 </summary>
    TEMPORARY_DEATH,
}