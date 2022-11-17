using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class MoveBehavior
{
    public Rigidbody2D Rigidbody2D => _rb2D;

    [SerializeField]
    protected float _moveSpeed = 1f;
    [InputName, SerializeField]
    protected string _horizontalButtonName = default;

    protected Rigidbody2D _rb2D;
    protected bool _isMove = true;

    public virtual void Init(Rigidbody2D rb2D)
    {
        _rb2D = rb2D;
    }
    public virtual void Move()
    {
        if (_isMove)
        {
            var h = Input.GetAxisRaw(_horizontalButtonName);
            _rb2D.velocity = new Vector2(h * _moveSpeed, _rb2D.velocity.y);
        }
    }
    public async void StopMove(int stopTime)
    {
        _isMove = false;
        Debug.Log("移動ストップ開始");
        await Task.Run(() => Thread.Sleep(stopTime));
        _isMove = true;
        Debug.Log("移動ストップ終了");
    }
    public void StopMove()
    {
        Debug.Log("移動ストップします");
        _isMove = false;
    }
    public void ResumeMove()
    {
        Debug.Log("移動再開します");
        _isMove = true;
    }
}