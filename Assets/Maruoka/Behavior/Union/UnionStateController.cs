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
        // Idle�ŏ�����
        _currentState = UnionState.IDLE;
        // ���̑��x������Έړ��ɕύX����
        if (Mathf.Abs(_rb2D.velocity.x) > 0.1f)
        {
            _currentState = UnionState.MOVE;
        }
        if (!_groundChecker.IsGrounded)
        {
            // �ڒn���Ă��炸y���̑��x�����̒l�ł����Fly Up�ɕύX����
            if (_rb2D.velocity.y > 0f)
            {
                _currentState = UnionState.FLY_UP;
            }
            // �ڒn���Ă��炸y���̑��x�����̒l�ł����Fall Down�ɕύX����
            else
            {
                _currentState = UnionState.FALL_DOWN;
            }
        }
        // �_���[�W��������Ă�����
        if (_controller.LifeController.IsDamage)
        {
            _currentState = UnionState.DAMAGE;
        }
        // ����ł�����
        if (_controller.LifeController.IsDeath)
        {
            _currentState = UnionState.DEATH;
        }
        // ���T�T�r�T���^�i�㏸���[�h�j
        if (_controller.FlyingSquirrelActioner.IsRiseNow)
        {
            _currentState = UnionState.FLYING_SQUIRREL_SANTA_FLY_UP;
        }
        // ���T�T�r�T���^�i���󃂁[�h�j
        if (_controller.FlyingSquirrelActioner.IsFlyingSquirrelNow)
        {
            _currentState = UnionState.FLYING_SQUIRREL_SANTA_NOMAL;
        }
        // �T���^�𓊂���i���C���[�A�N�V�����J�n�j
        // �T���^�K�[�h�i�T���^�K�[�h��ԁj
        // �e�𔭖C����i�e���C��ԁj
    }
}