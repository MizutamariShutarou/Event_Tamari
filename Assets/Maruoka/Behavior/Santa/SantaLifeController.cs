using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class SantaLifeController
{
    [SerializeField]
    private int _maxLife = 5;
    [SerializeField]
    private int _life = default;
    [SerializeField]
    private bool _isGodMode = false;
    [SerializeField]
    private int _knockBackTime = 1000;
    [SerializeField]
    private int _deathTime = 1000;

    private Rigidbody2D _rb2D = default;
    private MoveBehavior _mover = default;
    private SantaStateController _stateController = null;

    private bool _isDamageNow = false;
    private bool _isDeath = false;

    public bool IsDamageNow => _isDamageNow;
    public bool IsDeath => _isDeath;

    public void Init(MoveBehavior moveController, SantaStateController stateController)
    {
        _mover = moveController;
        _rb2D = _mover.Rigidbody2D;
        _life = _maxLife;
        _stateController = stateController;
    }

    public void Damage(int damage, Vector2 dir, float power)
    {
        if (!_isGodMode)
        {
            _life -= damage;
            if (_life < 1)
            {
                StartDeath();
            }
            else
            {
                StartKnockBack();
            }

            // ノックバックする
            _rb2D.velocity = Vector2.zero;
            _rb2D.AddForce(dir.normalized * power, ForceMode2D.Impulse);
        }
    }

    private void ResetLife()
    {
        _life = _maxLife;
    }
    private async void StartKnockBack()
    {
        _isGodMode = true;
        _isDamageNow = true;
        _stateController.CurrentState = SantaState.DAMAGE;
        _mover.StopMove();

        await Task.Run(() => Thread.Sleep(_knockBackTime));

        _mover.ResumeMove();
        _isDamageNow = false;
        _isGodMode = false;
    }
    private async void StartDeath()
    {
        Debug.Log("倒されました");
        _stateController.CurrentState = SantaState.DEATH;
        _isDeath = true;
        _isGodMode = true;
        _mover.StopMove();

        await Task.Run(() => Thread.Sleep(_deathTime));

        Debug.Log("復活しました");
        ResetLife();
        _isDeath = false;
        _isGodMode = false;
        _mover.ResumeMove();
    }
}