using System;
using UnityEngine;

[System.Serializable]
public class DeerStateController : StateControllerBase<DeerState>
{
    public override void Update()
    {
        FacingDirectionUpdate();

        // �X�e�[�g���X�V���鏈�����L�q����B
    }
}