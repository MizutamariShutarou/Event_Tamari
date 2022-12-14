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
        // Idle�ŏ�����
        _currentState = DeerState.IDLE;
        // ���̑��x������Έړ��ɕύX����
        if (Mathf.Abs(_rb2D.velocity.x) > 0.1f)
        {
            _currentState = DeerState.MOVE;
        }
        if (!_groundCheck.IsGrounded)
        {
            // �ڒn���Ă��炸y���̑��x�����̒l�ł����Rise�ɕύX����
            if (_rb2D.velocity.y > 0f)
            {
                _currentState = DeerState.FLY_UP;
            }
            // �ڒn���Ă��炸y���̑��x�����̒l�ł����Fall�ɕύX����
            else
            {
                _currentState = DeerState.FALL_DOWN;
            }
        }
        // �_���[�W��H����Ă�����
        if (_controller.LifeController.IsDamage)
        {
            _currentState = DeerState.DAMAGE;
        }
        // ����ł�����
        if (_controller.LifeController.IsDeath)
        {
            _currentState = DeerState.DEATH;
        }
        // �p�\�[�h�ōU�����Ă�����
        if (_controller.HornSwordAttacker.IsAttackNow)
        {
            _currentState = DeerState.HORN_SWORD_ATTACK;
        }
        // �ːi�U�������Ă�����
        if (_controller.RushAttacker.IsRushNow)
        {
            _currentState = DeerState.RUSH_ATTACK;
        }
        // �T���^�̒��n�E�R�T���҂�
        if (_controller.DeerWireController.CurrentState == DeerWireState.DO_NOTHING)
        {
            _currentState = DeerState.WIRE_DO_NOTHING;
        }
        // �T���^�̊�Ɉړ��i�����j
        if (_controller.DeerWireController.CurrentState == DeerWireState.WALK_TO_SANTA)
        {
            _currentState = DeerState.WIRE_MOVE;
        }
        // �T���^�̊�Ɉړ��i���C���[�N���C���j
        if (_controller.DeerWireController.CurrentState == DeerWireState.CLIMB_TO_SANTA)
        {
            _currentState = DeerState.WIRE_CLIMB;
        }
    }
}