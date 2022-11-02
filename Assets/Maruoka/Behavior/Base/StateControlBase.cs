using System;
using UnityEngine;

[System.Serializable]
public abstract class StateControlBase
{
    /// <summary>
    /// ステート更新処理
    /// </summary>
    public abstract void StateUpdate();
}