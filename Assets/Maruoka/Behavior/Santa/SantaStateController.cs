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
        // Idle�ŏ�����
        _currentState = SantaState.IDLE;
        // ���̑��x������Έړ��ɕύX����
        if (Mathf.Abs(_rb2D.velocity.x) > 0.1f)
        {
            _currentState = SantaState.MOVE;
        }
        // �W�����v���Ă���΃W�����v�ɃX�e�[�g��ύX����
        if (_controller.Jumper.IsJump)
        {
            _currentState = SantaState.JUMP;
        }
        if (_controller.Mover.IsCreeping)
        {
            // ������Ԃ�x���̑��x���ق�0�ł���� ����Idle �X�e�[�g�ɕύX����
            // ������Ԃ�x���̑��x���ق�0�łȂ���� ����Move �X�e�[�g�ɕύX����
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
            // �ڒn���Ă��炸y���̑��x�����̒l�ł����Rise�ɕύX����
            if (_rb2D.velocity.y > 0f)
            {
                _currentState = SantaState.FLY_UP;
            }
            // �ڒn���Ă��炸y���̑��x�����̒l�ł����Fall�ɕύX����
            else
            {
                _currentState = SantaState.FALL_DOWN;
            }
        }
        // �_���[�W��������Ă�����
        if (_controller.LifeController.IsDamageNow)
        {
            _currentState = SantaState.DAMAGE;
        }
        // �ꎞ�I�Ɏ���ł����ԁi�T���^�͎��񂾂��莞�Ԃőh������j
        if (_controller.LifeController.IsDeath)
        {
            _currentState = SantaState.TEMPORARY_DEATH;
        }
        // ���C���[�A�N�V������Ԃœ������Ă����Ԃ̏ꍇ
        if (_controller.SantaWireController.CurrentState == SantaWireState.DO_NOTHING)
        {
            _currentState = SantaState.WIRE_DO_NOTHING;
        }
        // ���C���[�A�N�V������Ԃň��������Ă����Ԃ̏ꍇ
        if (_controller.SantaWireController.CurrentState == SantaWireState.MOVE)
        {
            _currentState = SantaState.WIRE_MOVE;
        }
        // ���C���[�A�N�V������Ԃň����������Ă����Ԃ̏ꍇ
        if (_controller.SantaWireController.CurrentState == SantaWireState.STOP_MOVE)
        {
            _currentState = SantaState.WIRE_STOP_MOVE;
        }
    }
}