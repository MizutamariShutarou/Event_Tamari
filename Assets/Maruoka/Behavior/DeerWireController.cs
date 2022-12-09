using System;
using UnityEngine;

[System.Serializable]
public class DeerWireController
{
    [SerializeField]
    private float _moveSpeed = 2f;
    [SerializeField]
    private float _climbSpeed = 2f;
    [SerializeField]
    private float _coalesceDistanceX = 1f;
    [SerializeField]
    private float _coalesceDistanceY = 1f;
    [SerializeField]
    private bool _isDrawGizmoCheckForward = false;
    [SerializeField]
    private Vector3 _checkForwardOffset = default;
    [SerializeField]
    private Vector3 _checkForwardSize = default;
    [SerializeField]
    private LayerMask _targetLayer = default;

    private Rigidbody2D _rigidbody2D = null;
    private DeerWireState _currentState = DeerWireState.DO_NOTHING;
    private Transform _santaTransform = null;
    private Transform _deerTransform = null;
    private DeerController _deerController = null;
    private SantaController _santaController = null;

    public bool IsDrawGizmoCheckForward => _isDrawGizmoCheckForward;
    public Vector3 CheckForwardOffset => _checkForwardOffset;
    public Vector3 CheckForwardSize => _checkForwardSize;

    public void Init(Rigidbody2D rigidbody2D,
        Transform santaTransform,
        Transform deerTransform,
        DeerController deerController,
        SantaController santaController)
    {
        _rigidbody2D = rigidbody2D;
        _santaTransform = santaTransform;
        _deerTransform = deerTransform;
        _deerController = deerController;
        _santaController = santaController;
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
    public void Enter()
    {
        ChangeState(DeerWireState.DO_NOTHING);
        _deerController.StartWire();
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

        // サンタとx座標の距離が十分に距離が近くなった時、上昇する。
        if (Mathf.Abs(_santaTransform.position.x - _deerTransform.position.x) < 0.1f)
        {
            Debug.Log("ワイヤ : トナカイをワイヤー昇るモードにします");
            ChangeState(DeerWireState.CLIMB_TO_SANTA);
        }
        // 前方に壁等の何かがあるとき、単独行動モードに移行する。
        if (CheckForward())
        {
            Debug.Log("ワイヤ : 単独行動モードにします");
            _deerController.EndWire();
            _santaController.EndWire();
        }
        // サンタと十分に近ければ合体する。
        if (Mathf.Abs(_santaTransform.position.x - _deerTransform.position.x) < _coalesceDistanceX &&
           Mathf.Abs(_santaTransform.position.y - _deerTransform.position.y) < _coalesceDistanceY)
        {
            Debug.Log("ワイヤ : 合体します");
            Coalesce();
        }
    }
    // 進行方向チェック処理
    private bool CheckForward()
    {
        var pos = _checkForwardOffset;
        pos.x *= _deerController.StateController.FacingDirection == FacingDirection.RIGHT ?
            1f : -1f;
        pos += _deerTransform.position;
        return Physics2D.OverlapBoxAll(pos, _checkForwardSize, 0f,
            _targetLayer).Length > 0;
    }
    // サンタに向かって昇るモード
    private void ClimbToSanta()
    {
        // 上昇する
        _rigidbody2D.velocity = new Vector2(0f, _climbSpeed);
        // Debug.Log($"{_santaTransform.position.y - _deerTransform.position.y}"); // 高さの差確認用
        // サンタと十分に近ければ合体する。
        if (Mathf.Abs(_santaTransform.position.x - _deerTransform.position.x) < _coalesceDistanceX &&
            Mathf.Abs(_santaTransform.position.y - _deerTransform.position.y) < _coalesceDistanceY)
        {
            Coalesce();
        }

    }
    // サンタの位置で合体する
    private void Coalesce()
    {
        OperableCharacterManager.Instance.CoalesceOnSantaPos();
    }
}
public enum DeerWireState
{
    DO_NOTHING,
    WALK_TO_SANTA,
    CLIMB_TO_SANTA,
}