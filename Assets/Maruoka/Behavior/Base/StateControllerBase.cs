using System;
using UnityEngine;

[System.Serializable]
public abstract class StateControllerBase<T> where T : Enum
{
    /// <summary>
    /// 現在のステート
    /// </summary>
    public T CurrentState { get => _currentState; set => _currentState = value; }
    /// <summary>
    /// 向いている方向を表す値
    /// </summary>
    public FacingDirection FacingDirection => _facingDirection;
    public Rigidbody2D Rb2D => _rb2D;

    [SerializeField]
    protected T _currentState = default;
    protected FacingDirection _facingDirection = default;
    protected Rigidbody2D _rb2D = default;

    /// <summary>
    /// ステート更新処理
    /// </summary>
    public abstract void Update();
    /// <summary>
    /// 向いている向きを更新する
    /// </summary>
    protected void FacingDirectionUpdate()
    {
        // if (!Mathf.Approximately(_rb2D.velocity.x, 0f))
        if (Mathf.Abs(_rb2D.velocity.x) > 0.1f)
        {
            if (_rb2D.velocity.x > 0f)
            {
                _facingDirection = FacingDirection.RIGHT;
            }
            else if (_rb2D.velocity.x < 0f)
            {
                _facingDirection = FacingDirection.LEFT;
            }
        }
    }
}

public enum FacingDirection
{
    RIGHT,
    LEFT,
}