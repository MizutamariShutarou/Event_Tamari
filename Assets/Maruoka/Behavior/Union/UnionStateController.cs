using System;
using UnityEngine;

[System.Serializable]
public class UnionStateController : StateControllerBase<UnionState>
{
    private UnionController _controller = null;
    private GroundCheck _groundChecker = null;

    public void Init(Rigidbody2D rb, GroundCheck groundCheck, UnionController controller)
    {
        _rb2D = rb;
        _controller = controller;
        _groundChecker = groundCheck;
    }

    public override void Update()
    {
        FacingDirectionUpdate();
        StateUpdate();
    }

    private void StateUpdate()
    {
        // Idleで初期化
        _currentState = UnionState.IDLE;
        // 横の速度があれば移動に変更する
        if (Mathf.Abs(_rb2D.velocity.x) > 0.1f)
        {
            _currentState = UnionState.MOVE;
        }
        if (!_groundChecker.IsGrounded)
        {
            // 接地しておらずy軸の速度が正の値であればFly Upに変更する
            if (_rb2D.velocity.y > 0f)
            {
                _currentState = UnionState.FLY_UP;
            }
            // 接地しておらずy軸の速度が負の値であればFall Downに変更する
            else
            {
                _currentState = UnionState.FALL_DOWN;
            }
        }
        // ダメージをくらっている状態
        if (_controller.LifeController.IsDamage)
        {
            _currentState = UnionState.DAMAGE;
        }
        // 死んでいる状態
        if (_controller.LifeController.IsDeath)
        {
            _currentState = UnionState.DEATH;
        }
        // ムササビサンタ（上昇モード）
        if (_controller.FlyingSquirrelActioner.IsRiseNow)
        {
            _currentState = UnionState.FLYING_SQUIRREL_SANTA_FLY_UP;
        }
        // ムササビサンタ（滑空モード）
        if (_controller.FlyingSquirrelActioner.IsFlyingSquirrelNow)
        {
            _currentState = UnionState.FLYING_SQUIRREL_SANTA_NOMAL;
        }
        // サンタを投げる（ワイヤーアクション開始）
        // サンタガード（サンタガード状態）
        // 銃を発砲する（銃発砲状態）
    }
}