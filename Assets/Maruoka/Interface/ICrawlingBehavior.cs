using System;
using UnityEngine;

/// <summary>
/// 匍匐行動のインターフェース
/// </summary>
public interface ICrawlingBehavior

{
    public void CreepingEnter();
    public void CreepingExit();
}