using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]
    private DeerMoveController _mover = default;
    [SerializeField]
    private HornSwordAttackBehavior _hornSwordAttacker = default;
    [SerializeField]
    private RushAttackBehavior _rushAttacker = default;
    [SerializeField]
    private DeerMainLifeController _lifeController = default;
    [SerializeField]
    private DeerChangeOperatCharacter _operatCharacterChanger = default;
    [SerializeField]
    private DeerCombineController _combiner = default;
    [SerializeField]
    private DeerStateController _stateController = default;
    [SerializeField]
    private DeerAnimationController _animationController = default;
    [SerializeField]
    private DeerWireController _deerWireController = default;

    public DeerMoveController Mover => _mover;
    public HornSwordAttackBehavior HornSwordAttacker => _hornSwordAttacker;
    public RushAttackBehavior RushAttacker => _rushAttacker;
    public DeerMainLifeController LifeController => _lifeController;
    public DeerChangeOperatCharacter OperatCharacterChanger => _operatCharacterChanger;
    public DeerCombineController Combiner => _combiner;
    public DeerStateController StateController => _stateController;
    public DeerAnimationController AnimationController => _animationController;
    public DeerWireController DeerWireController => _deerWireController;
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
        OnDrawGizmo_WireActionFrontCheckBox();
    }
#endif
    #endregion

    #region Private Methods
    private void Init()
    {
        var rb2D = GetComponent<Rigidbody2D>();
        _mover.Init(rb2D);
        _stateController.Init(rb2D, GetComponent<GroundCheck>(), this);
        _operatCharacterChanger.Init(_stateController);
        _hornSwordAttacker.Init(transform, _stateController);
        _rushAttacker.Init(transform, _stateController, rb2D);
        _lifeController.Init(_mover, _stateController);
        _animationController.Init(_stateController);
        _combiner.Init(_stateController);
        _deerWireController.Init(rb2D,
            OperableCharacterManager.Instance.Santa.transform,transform,this,
            OperableCharacterManager.Instance.Santa.GetComponent<SantaController>());
    }
    private void Process()
    {
        if (_isWire)
        {
            _deerWireController.Update();
        }
        else
        {
            _mover.Update();
            _hornSwordAttacker.Update();
            _rushAttacker.Update();
            _operatCharacterChanger.Update();
            _combiner.Update();
            _stateController.Update();
            _animationController.Update();
        }
    }
    private void OnReadyFire()
    {

    }
    #endregion

    private bool _isWire = false;

    public void StartWire()
    {
        _isWire = true;
    }
    public void EndWire()
    {
        _isWire = false;
    }

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
    private void OnDrawGizmo_WireActionFrontCheckBox()
    {
        if (_deerWireController.IsDrawGizmoCheckForward)
        {
            Gizmos.color = Color.green;

            var pos = _deerWireController.CheckForwardOffset;
            pos.x *= _stateController.FacingDirection == FacingDirection.RIGHT ?
                1f : -1f;
            pos += transform.position;
            Gizmos.DrawCube(pos, _deerWireController.CheckForwardSize);
        }
    }
    public void OnRush()
    {
        _rushAttacker.StartRush();
    }
#endif
    #endregion

    #region Test
    // テストコード群
    public void TestDamage()
    {
        _lifeController.Damage(1, new Vector2(1, 1), 10f, 1000);
    }
    #endregion
}