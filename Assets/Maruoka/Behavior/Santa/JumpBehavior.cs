using System;
using UnityEngine;

[System.Serializable]
public class JumpBehavior
{
    [SerializeField]
    private float _jumpPower = 1f;
    [InputName, SerializeField]
    private string _jumpButtonName = default;

    private Rigidbody2D _rb2D = null;
    private GroundCheck _groundChecker = null;
    private SantaStateController _stateController = null;

    private bool _isJump = false;

    public bool IsJump => _isJump;

    public void Init(Rigidbody2D rb2D, GroundCheck groundCheck, SantaStateController stateController)
    {
        _rb2D = rb2D;
        _groundChecker = groundCheck;
        _stateController = stateController;
    }
    public void Update()
    {
        if (Input.GetButtonDown(_jumpButtonName) && _groundChecker.IsGrounded)
        {
            _rb2D.velocity = new Vector2(0f, _jumpPower);
            _isJump = true;
            _stateController.CurrentState = SantaState.JUMP;
        }
        else
        {
            _isJump = false;
        }
    }
    private bool IsRun()
    {
        bool result = false;

        result =
            Input.GetButtonDown(_jumpButtonName) &&
            _groundChecker.IsGrounded &&
            (_stateController.CurrentState == SantaState.IDLE ||
            _stateController.CurrentState == SantaState.MOVE);

        return result;
    }
}