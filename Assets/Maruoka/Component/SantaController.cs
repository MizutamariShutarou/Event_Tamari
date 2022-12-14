using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SantaController : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]
    private SantaMoveBehavior _mover = default;
    [SerializeField]
    private JumpBehavior _jumper = default;
    [SerializeField]
    private SantaLifeController _lifeController = default;
    [SerializeField]
    private SantaChangeOperatCharacter _operatCharacterChanger = default;
    [SerializeField]
    private SantaCombineController _combiner = default;
    [SerializeField]
    private SantaStateController _stateControler = default;
    [SerializeField]
    private SantaAnimationController _animationController = default;
    [SerializeField]
    private SantaWireController _santaWireController = default;

    public SantaMoveBehavior Mover => _mover;
    public JumpBehavior Jumper => _jumper;
    public SantaLifeController LifeController => _lifeController;
    public SantaChangeOperatCharacter OperatCharacterChanger => _operatCharacterChanger;
    public SantaCombineController Combiner => _combiner;
    public SantaStateController StateControler => _stateControler;
    public SantaAnimationController AnimationController => _animationController;
    public SantaWireController SantaWireController => _santaWireController;
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
    #endregion

    #region Private Methods
    private void Init()
    {
        var rb2D = GetComponent<Rigidbody2D>();
        var gc = GetComponent<GroundCheck>();
        _mover.Init(rb2D, _stateControler);
        _jumper.Init(rb2D, gc, _stateControler);
        _operatCharacterChanger.Init(_stateControler);
        _stateControler.Init(rb2D, gc, this);
        _lifeController.Init(_mover, _stateControler);
        _animationController.Init(_stateControler);
        _combiner.Init(_stateControler);
        _santaWireController.Init(rb2D, transform, OperableCharacterManager.Instance.Deer.transform,
            this, OperableCharacterManager.Instance.Deer.GetComponent<DeerController>());
    }
    private void Process()
    {
        if (_isWire)
        {
            _santaWireController.Update();
        }
        else
        {
            _mover.Update();
            _jumper.Update();
            _operatCharacterChanger.Update();
            _combiner.Update();
        }
        _stateControler.Update();
        _animationController.Update();
    }
    private void OnDrawGizmosSelected()
    {
        OnDrawGizmo_WireActionFrontCheckBox();
        OnDrawGizmo_WireActionCliffCheckBox();
    }
    #endregion
    [SerializeField]
    private bool _isWire = false;
    public bool IsWire => _isWire;

    public void StartWire()
    {
        _isWire = true;
    }
    public void EndWire()
    {
        _isWire = false;
    }

    #region Test
#if UNITY_EDITOR
    // テストコード群
    public void TestDamage()
    {
        _lifeController.Damage(1, new Vector2(1, 1), 10f);
    }
    /// <summary>
    /// ワイヤーアクション中 前方チェック（壁とか）
    /// </summary>
    private void OnDrawGizmo_WireActionFrontCheckBox()
    {
        if (_santaWireController.IsDrawGizmoFrontCheck)
        {
            Gizmos.color = Color.white;
            var pos = _santaWireController.CheckForwardOffset;
            pos.x *= _stateControler.FacingDirection == FacingDirection.RIGHT ?
                1f : -1f;
            pos += transform.position;
            Gizmos.DrawCube(pos, _santaWireController.CheckForwardSize);
        }
    }
    /// <summary>
    /// ワイヤーアクション中 崖チェック
    /// </summary>
    private void OnDrawGizmo_WireActionCliffCheckBox()
    {
        if (_santaWireController.IsDrawGizmoCheckCliff)
        {
            Gizmos.color = Color.green;
            var dir = _santaWireController.CheckCliffRayDir;
            dir.x *= _stateControler.FacingDirection == FacingDirection.RIGHT ?
                1f : -1f;
            Gizmos.DrawRay(transform.position, dir);
        }
    }
#endif
    #endregion
}