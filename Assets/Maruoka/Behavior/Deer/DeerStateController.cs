using System;
using UnityEngine;

[System.Serializable]
public class DeerStateController : StateControllerBase<DeerState>
{
    private DeerController _controller = null;
    private GroundCheck _groundCheck = null;
    public void Init(Rigidbody2D rb, GroundCheck groundCheck, DeerController controller)
    {
        _rb2D = rb;
        _controller = controller;
        _groundCheck = groundCheck;
    }
    public override void Update()
    {
        FacingDirectionUpdate();
        UpdateState();
    }
    private void UpdateState()
    {
        // Idleで初期化
        _currentState = DeerState.IDLE;
        // 横の速度があれば移動に変更する
        if (Mathf.Abs(_rb2D.velocity.x) > 0.1f)
        {
            _currentState = DeerState.MOVE;
        }
        if (!_groundCheck.IsGrounded)
        {
            // 接地しておらずy軸の速度が正の値であればRiseに変更する
            if (_rb2D.velocity.y > 0f)
            {
                _currentState = DeerState.FLY_UP;
            }
            // 接地しておらずy軸の速度が負の値であればFallに変更する
            else
            {
                _currentState = DeerState.FALL_DOWN;
            }
        }
        // ダメージを食らっている状態
        if (_controller.LifeController.IsDamage)
        {
            _currentState = DeerState.DAMAGE;
        }
        // 死んでいる状態
        if (_controller.LifeController.IsDeath)
        {
            _currentState = DeerState.DEATH;
        }
        // 角ソードで攻撃している状態
        if (_controller.HornSwordAttacker.IsAttackNow)
        {
            _currentState = DeerState.HORN_SWORD_ATTACK;
        }
        // 突進攻撃をしている状態
        if (_controller.RushAttacker.IsRushNow)
        {
            _currentState = DeerState.RUSH_ATTACK;
        }
        // サンタを手繰り寄せる（地上）（ワイヤーアクション時）
        // サンタを手繰り寄せる（空中）（ワイヤーアクション時）
        // ワイヤーを握った状態で待機（地上）（ワイヤーアクション時）
        // ワイヤーを握った状態で待機（空中）（ワイヤーアクション時）
    }
}