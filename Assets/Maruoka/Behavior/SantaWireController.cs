using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SantaWireController
{
    [SerializeField]
    private Vector2 _shotDir = default;
    [SerializeField]
    private float _shotPower = default;
    [SerializeField]
    private float _moveSpeed = 2f;
    [SerializeField]
    private Vector3 _checkForwardOffset = default;
    [SerializeField]
    private Vector3 _checkForwardSize = default;
    [SerializeField]
    private LayerMask _checkForwardTargetLayer = default;
    [SerializeField]
    private bool _isDrawGizmoForwerdCheck = false;
    [Tooltip("崖を検知するためのレイの方向"), SerializeField]
    private Vector2 _checkCliffRayDir = default;
    [SerializeField]
    private float _checkCliffRayMaxDistance = 5f;
    [SerializeField]
    private LayerMask _checkCliffTargetLayer = default;
    [SerializeField]
    private float _maxDistanceX = 1f;
    [SerializeField]
    private float _maxDistanceY = 1f;
    [SerializeField]
    private bool _isDrawGizmoCheckCliff = false;
    [SerializeField]
    private SantaWireState _currentState = SantaWireState.DO_NOTHING;

    private Rigidbody2D _rigidbody2D = null;
    private Transform _santaTransform = null;
    private Transform _deerTransform = null;
    private SantaController _santaController = null;
    private DeerController _deerController = null;

    public Vector3 CheckForwardOffset => _checkForwardOffset;
    public Vector3 CheckForwardSize => _checkForwardSize;
    public bool IsDrawGizmoFrontCheck => _isDrawGizmoForwerdCheck;
    public Vector2 CheckCliffRayDir => _checkCliffRayDir;
    public float CheckCliffRayMaxDistance => _checkCliffRayMaxDistance;
    public bool IsDrawGizmoCheckCliff => _isDrawGizmoCheckCliff;

    public void Init(Rigidbody2D rigidbody2D,
        Transform santaTransform,
        Transform deerTransform,
        SantaController santaControlle,
        DeerController deerController)
    {
        _rigidbody2D = rigidbody2D;
        _santaTransform = santaTransform;
        _deerTransform = deerTransform;
        _santaController = santaControlle;
        _deerController = deerController;
    }
    public void Update()
    {
        switch (_currentState)
        {
            case SantaWireState.DO_NOTHING:
                DoNothing();
                break;
            case SantaWireState.MOVE:
                WireMove();
                break;
            case SantaWireState.STOP_MOVE:
                break;
        }
    }
    // 発射
    public void Shot(Rigidbody2D rigidbody2D, bool dirIsRight)
    {
        _currentState = SantaWireState.DO_NOTHING;
        _shotDir.x *= dirIsRight ? 1f : -1f;
        rigidbody2D.AddForce(_shotDir.normalized * _shotPower, ForceMode2D.Impulse);
    }
    // 停止中の処理
    private void DoNothing()
    {
        if (CheckStop())
        {
            _currentState = SantaWireState.MOVE;
        }
    }
    // 停止判定
    private bool CheckStop()
    {
        return Mathf.Abs(_rigidbody2D.velocity.x) < 0.1f && Mathf.Abs(_rigidbody2D.velocity.y) < 0.1f;
    }
    // にじり寄るモード中の処理
    private void WireMove()
    {
        // トナカイが右にいれば正の値を、左にいれば負の値を取得する。
        var dirX = _deerTransform.position.x > _santaTransform.position.x ?
            1f : -1f;
        // 移動処理
        _rigidbody2D.velocity = new Vector2(dirX * _moveSpeed, _rigidbody2D.velocity.y);
        if (IsCoalesce()) // 引きずられ中にトナカイに接触したとき合体する。
        {
            Debug.Log("ワイヤー : 合体します");
            StartDeerWireActionMode();
        }
        if (CheckForward()) // 壁を見つけたとき単独行動モードに遷移する
        {
            Debug.Log("ワイヤー : 単独行動モードに移行します");
            TransitionToNomal();
        }
        if (CheckCliff()) // 崖を見つけたとき停止し、トナカイを近づくモードにする。
        {
            Debug.Log("ワイヤ : トナカイをワイヤー近づくモードにします");
            TransitionToStop();
            StartDeerWireActionMode();
        }
    }
    // 進行方向チェック処理
    private bool CheckForward()
    {
        var pos = _checkForwardOffset;
        pos.x *= _santaController.StateControler.FacingDirection == FacingDirection.RIGHT ?
            1f : -1f;
        pos += _santaController.transform.position;
        return Physics2D.OverlapBoxAll(pos, _checkForwardSize, 0f,
            _checkForwardTargetLayer).Length > 0;
    }
    // 単独行動モード遷移処理
    private void TransitionToNomal()
    {
        _santaController.EndWire();
        _deerController.EndWire();
    }
    /// <summary>
    /// 崖検知処理
    /// </summary>
    /// <returns> 
    /// 崖を見つけたとき（レイが何にも当たらなくなったとき）trueを返す。
    /// </returns>
    private bool CheckCliff()
    {
        var dir = _checkCliffRayDir;
        dir.x *= _santaController.StateControler.FacingDirection == FacingDirection.RIGHT ?
            1f : -1f;
        return !Physics2D.Raycast(_santaTransform.position,
            dir,
            _checkCliffRayMaxDistance,
            _checkCliffTargetLayer);
    }
    // サンタ停止モードに遷移する処理
    private void TransitionToStop()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _currentState = SantaWireState.STOP_MOVE;
    }
    // トナカイ近づくモードに遷移する処理
    private void StartDeerWireActionMode()
    {
        OperableCharacterManager.Instance.ChangeDeerWireState(DeerWireState.WALK_TO_SANTA);
    }
    //トナカイ接触判定
    private bool IsCoalesce()
    {
        // 距離で判定する。
        return (Mathf.Abs(_santaTransform.position.x - _deerTransform.position.x) < _maxDistanceX &&
            Mathf.Abs(_santaTransform.position.y - _deerTransform.position.y) < _maxDistanceY);
    }
    // 合体処理
    private void OnCoalesce()
    {
        OperableCharacterManager.Instance.Coalesce();
    }
}

public enum SantaWireState
{
    /// <summary>
    /// 何もしない
    /// </summary>
    DO_NOTHING,
    MOVE,
    STOP_MOVE,
}