using System;
using UnityEngine;

[System.Serializable]
public class MainLifeController
{
    private static int _life = CommonConstant.MAX_LIFE;

    public static int MainLife => _life; 
    public bool IsDeath => _isDeath;

    private Rigidbody2D _rb2D = default;
    private MoveBehavior _mover = default;
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

    public void Damage(int damage, Vector2 dir, float power)
    {
        _life -= damage;
        if (_life < 0)
        {
            Debug.LogWarning("Player���|����܂���");
            _isDeath = true;
        }

        // �m�b�N�o�b�N����
        _rb2D.velocity = Vector2.zero;
        _rb2D.AddForce(dir.normalized * power, ForceMode2D.Impulse);
        _mover.StopMove(1000);
    }
}