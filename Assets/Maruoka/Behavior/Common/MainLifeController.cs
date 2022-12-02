using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class MainLifeController
{
    [SerializeField]
    private bool _isGodMode = false;
    [SerializeField]
    private int _knockBackTime = 1000;

    private static int _life = CommonConstant.MAX_LIFE;

    public static int MainLife => _life; 
    public bool IsDeath => _isDeath;
    public bool IsDamage => _isDamage;

    private Rigidbody2D _rb2D = default;
    private MoveBehavior _mover = default;
    private bool _isDamage = false;
    private bool _isDeath = false;

    public void Init(MoveBehavior moveController)
    {
        _mover = moveController;
        _rb2D = _mover.Rigidbody2D;
    }

    public void ResetLife()
    {
        _life = CommonConstant.MAX_LIFE;
    }

    public void Damage(int damage, Vector2 dir, float power, int moveStopTime)
    {
        if (!_isGodMode)
        {
            StartKnockBack();
            _life -= damage;
            if (_life < 0)
            {
                Debug.LogWarning("Playerが倒されました");
                _isDeath = true;
            }

            // ノックバックする
            _rb2D.velocity = Vector2.zero;
            _rb2D.AddForce(dir.normalized * power, ForceMode2D.Impulse);
            _mover.StopMove(moveStopTime);
        }
    }

    private async void StartKnockBack()
    {
        _isGodMode = true;
        _isDamage = true;
        await Task.Run(() => Thread.Sleep(_knockBackTime));
        _isGodMode = false;
        _isDamage = false;
    }
}