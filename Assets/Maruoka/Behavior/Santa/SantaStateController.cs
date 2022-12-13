using System;
using UnityEngine;

[System.Serializable]
public class SantaStateController : StateControllerBase<SantaState>
{
    private SantaController _controller = null;
    private GroundCheck _groundChecker = null;

    public void Init(Rigidbody2D rb, GroundCheck groundChecker, SantaController controller)
    {
        _rb2D = rb;
        _controller = controller;
        _groundChecker = groundChecker;
    }

    public override void Update()
    {
        FacingDirectionUpdate();
        StateUpdate();
    }
    private void StateUpdate()
    {
        // Idleで初期化
        _currentState = SantaState.IDLE;
        // 横の速度があれば移動に変更する
        if (Mathf.Abs(_rb2D.velocity.x) > 0.1f)
        {
            _currentState = SantaState.MOVE;
        }
        // ジャンプしていればジャンプにステートを変更する
        if (_controller.Jumper.IsJump)
        {
            _currentState = SantaState.JUMP;
        }
        if (_controller.Mover.IsCreeping)
        {
            // 匍匐状態でx軸の速度がほぼ0であれば 匍匐Idle ステートに変更する
            // 匍匐状態でx軸の速度がほぼ0でなければ 匍匐Move ステートに変更する
            if (Mathf.Abs(_rb2D.velocity.x) < 0.1f)
            {
                _currentState = SantaState.CREEPING_IDLE;
            }
            else
            {
                _currentState = SantaState.CREEPING_MOVE;
            }

        }
        if (!_groundChecker.IsGrounded)
        {
            // 接地しておらずy軸の速度が正の値であればRiseに変更する
            if (_rb2D.velocity.y > 0f)
            {
                _currentState = SantaState.FLY_UP;
            }
            // 接地しておらずy軸の速度が負の値であればFallに変更する
            else
            {
                _currentState = SantaState.FALL_DOWN;
            }
        }
        // ダメージをくらっている状態
        if (_controller.LifeController.IsDamageNow)
        {
            _currentState = SantaState.DAMAGE;
        }
        // 一時的に死んでいる状態（サンタは死んだら一定時間で蘇生する）
        if (_controller.LifeController.IsDeath)
        {
            _currentState = SantaState.TEMPORARY_DEATH;
        }
        // ワイヤーアクション状態で投げられている状態の場合
        if (_controller.SantaWireController.CurrentState == SantaWireState.DO_NOTHING)
        {
            _currentState = SantaState.WIRE_DO_NOTHING;
        }
        // ワイヤーアクション状態で引きずられている状態の場合
        if (_controller.SantaWireController.CurrentState == SantaWireState.MOVE)
        {
            _currentState = SantaState.WIRE_MOVE;
        }
        // ワイヤーアクション状態で引っかかっている状態の場合
        if (_controller.SantaWireController.CurrentState == SantaWireState.STOP_MOVE)
        {
            _currentState = SantaState.WIRE_STOP_MOVE;
        }
    }
}