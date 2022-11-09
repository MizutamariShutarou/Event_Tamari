using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]
    private MoveBehavior _mover = default;
    [SerializeField]
    private HornSwordAttackBehavior _hornSwordAttacker = default;
    [SerializeField]
    private RushAttackBehavior _rushAttacker = default;
    [SerializeField]
    private MainLifeController _lifeControler = default;
    [SerializeField]
    private ChangeOperatCharacter _operatCharacterChanger = default;
    [SerializeField]
    private CombineController _combiner = default;
    [SerializeField]
    private DeerStateController _stateController = default;
    [SerializeField]
    private DeerAnimationController _animationController = default;
    #endregion

    #region Unity Methods
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        Process();
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        OnDrawGizmo_HornSwordAttackHitBox();
        OnDrawGizmo_RushAttackHitBox();
    }
#endif
    #endregion

    #region Private Methods
    private void Init()
    {
        var rb2D = GetComponent<Rigidbody2D>();
        _mover.Init(rb2D);
        _stateController.Init(rb2D);
        _hornSwordAttacker.Init(transform, _stateController);
        _rushAttacker.Init(transform, _stateController, rb2D);
        _lifeControler.Init(_mover);
        _animationController.Init(_stateController);
    }
    private void Process()
    {
        _mover.Move();
        _hornSwordAttacker.Fire();
        _rushAttacker.Update();
        _operatCharacterChanger.OnChangeOperatCharacter();
        _combiner.Combine();
        _stateController.Update();
        _animationController.Update();

    }

    private void OnReadyFire()
    {

    }
    #endregion

    #region Animation Event
    // アニメーションイベントから呼び出すことを想定して作成されたメソッド群
    public void OnHornSwordAttack()
    {
        _hornSwordAttacker.AttackProcessing();
    }
    #endregion

    #region Test
#if UNITY_EDITOR
    // テストコード群
    private void OnDrawGizmo_HornSwordAttackHitBox()
    {
        if (_hornSwordAttacker.IsDrawGizmo)
        {
            Gizmos.color = _hornSwordAttacker.GizmoColor;

            var pos = _hornSwordAttacker.AttackPosOffset;
            if (_stateController.FacingDirection == FacingDirection.LEFT)
            {
                pos *= -1f;
            }
            pos += transform.position;

            Gizmos.DrawCube(pos, _hornSwordAttacker.AttackAreaSize);
        }
    }
    private void OnDrawGizmo_RushAttackHitBox()
    {
        if (_rushAttacker.IsDrawGizmo)
        {
            Gizmos.color = _rushAttacker.GizmoColor;

            var pos = _rushAttacker.HitboxAreaOffset;
            if (_stateController.FacingDirection == FacingDirection.LEFT)
            {
                pos *= -1f;
            }
            pos += transform.position;

            Gizmos.DrawCube(pos, _rushAttacker.HitboxSize);
        }
    }
    public void OnRush()
    {
        _rushAttacker.StartRush();
    }
#endif
    #endregion
}