using System;
using UnityEngine;

[System.Serializable]
public class SantaLifeController
{
    [SerializeField]
    protected int _life = 1;

    protected Rigidbody2D _rb2D = default;
    protected MoveBehavior _mover = default;

    public void Init(MoveBehavior moveController)
    {
        _mover = moveController;
        _rb2D = _mover.Rigidbody2D;
    }

    public void Damage(int damage, Vector2 dir, float power)
    {
        _life -= damage;

        // ノックバックする
        _rb2D.velocity = Vector2.zero;
        _rb2D.AddForce(dir.normalized * power, ForceMode2D.Impulse);
        _mover.StopMove(1000);
    }
}