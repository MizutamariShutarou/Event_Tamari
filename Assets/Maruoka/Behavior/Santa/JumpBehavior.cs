using System;
using UnityEngine;

[System.Serializable]
public class JumpBehavior
{
    [SerializeField]
    private float _jumpPower = 1f;
    [InputName, SerializeField]
    private string _jumpButtonName = default;

    private Rigidbody2D _rb2D = default;
    private GroundCheck _groundChecker = default;

    public void Init(Rigidbody2D rb2D,GroundCheck groundCheck)
    {
        _rb2D = rb2D;
        _groundChecker = groundCheck;

    }
    public void Jump()
    {
        if (Input.GetButtonDown(_jumpButtonName) && _groundChecker.IsGround())
        {
            _rb2D.velocity = new Vector2(0f, _jumpPower);
        }
    }
}