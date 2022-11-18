using System;
using UnityEngine;

[System.Serializable]
public class DeerStateController : StateControllerBase<DeerState>
{
    public override void Update()
    {
        FacingDirectionUpdate();

        // ステートを更新する処理を記述する。
    }
}