using System;
using UnityEngine;

[System.Serializable]
public class SantaLifeControl : LifeControllerBase
{
    public override void Damage(int damage, Vector2 dir, float power)
    {
        _life -= damage;

        // ノックバックする
        _rb2D.velocity = Vector2.zero;
        _rb2D.AddForce(dir.normalized * power, ForceMode2D.Impulse);
        _mover.StopMove(1000);
    }
}