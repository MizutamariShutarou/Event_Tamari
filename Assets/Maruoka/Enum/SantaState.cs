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

    /// <summary> 引きずられる（ワイヤーアクション用） </summary>
    BE_DRAGGED,
    /// <summary> 引っかかる（ワイヤーアクション用 </summary>
    GET_CAUGHT,

    /// <summary> 一時的な死 </summary>
    TEMPORARY_DEATH,
}