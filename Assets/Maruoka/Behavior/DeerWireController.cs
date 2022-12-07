using System;
using UnityEngine;

[System.Serializable]
public class DeerWireController
{
    [SerializeField]
    private float _moveSpeed = 2f;
    [SerializeField]
    private float _climbSpeed = 2f;


    private Rigidbody2D _rigidbody2D = null;
    private DeerWireState _currentState = DeerWireState.DO_NOTHING;
    private Transform _santaTransform = null;
    private Transform _deerTransform = null;

    public void Init(Rigidbody2D rigidbody2D,
        Transform santaTransform,
        Transform deerTransform)
    {
        _rigidbody2D = rigidbody2D;
        _santaTransform = santaTransform;
        _deerTransform = deerTransform;
    }
    public void Update()
    {
        switch (_currentState)
        {
            case DeerWireState.DO_NOTHING:
                DoNothing();
                break;
            case DeerWireState.WALK_TO_SANTA:
                WalkToSanta();
                break;
            case DeerWireState.CLIMB_TO_SANTA:
                ClimbToSanta();
                break;
        }
    }
    public void ChangeState(DeerWireState newState)
    {
        _currentState = newState;
    }

    // 何もしないモード
    private void DoNothing()
    {

    }
    // サンタに向かって歩くモード
    private void WalkToSanta()
    {
        var xDir = _santaTransform.position.x > _deerTransform.position.x ? 1f : -1f;
        _rigidbody2D.velocity = new Vector2(xDir * _moveSpeed, _rigidbody2D.velocity.y);

        if (Mathf.Abs(_santaTransform.position.x - _deerTransform.position.x) < 0.1f)
        {
            ChangeState(DeerWireState.CLIMB_TO_SANTA);
        }
    }
    // サンタに向かって昇るモード
    private void ClimbToSanta()
    {
        _rigidbody2D.velocity = new Vector2(0f, _climbSpeed);

        //if(サンタと接触したとき)
        //{
        //    Coalesce();
        //}

    }
    // サンタの位置で合体する
    private void Coalesce()
    {

    }
}
public enum DeerWireState
{
    DO_NOTHING,
    WALK_TO_SANTA,
    CLIMB_TO_SANTA,
}