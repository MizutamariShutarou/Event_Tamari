using System;
using UnityEngine;

[System.Serializable]
public abstract class LifeControllerBase
{
    [SerializeField]
    protected int _life = 1;

    protected Rigidbody2D _rb2D = default;
    protected MoveBehavior _mover = default;

    public virtual void Init(Rigidbody2D rb2D, MoveBehavior move)
    {
        _rb2D = rb2D;
        _mover = move;
    }

    public abstract void Damage(int damage, Vector2 dir, float power);
}