using System;
using UnityEngine;

[System.Serializable]
public abstract class LifeControllerBase
{
    [SerializeField]
    protected int _life = 1;

    protected Rigidbody2D _rb2D = default;
    protected MoveBehavior _mover = default;

    public virtual void Init(MoveBehavior moveController)
    {
        _mover = moveController;
        _rb2D = _mover.Rigidbody2D;
    }

    public abstract void Damage(int damage, Vector2 dir, float power);
}