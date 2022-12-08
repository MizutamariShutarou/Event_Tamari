using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ムササビアクションクラス
/// </summary>
[System.Serializable]
public class FlyingSquirrelAction
{
    [InputName, SerializeField]
    private string _fireButtonName = default;
    [SerializeField]
    private bool _isReadyFlyUp = false;
    [SerializeField]
    private bool _isReadyNomal = false;
    [Tooltip("上昇時間"), SerializeField]
    private int _riseTime = 1000;
    [SerializeField]
    private float _risePower = 4f;
    [Tooltip("前方への推進力"), SerializeField]
    private float _forwardPropulsion = 4f;
    [Tooltip("ムササビ中の重力"), SerializeField]
    private float _flyingSquirrelsGravity = -1f;
    [SerializeField]
    private Vector3 _forwardCheckOffset = default;
    [SerializeField]
    private Vector3 _forwardCheckSize = default;
    [SerializeField]
    private LayerMask _forwardCheckTargetLayer = default;
    [SerializeField]
    private bool _isDrawGizmo = false;
    [SerializeField]
    private Color _gizmoColor = Color.red;

    /// <summary>
    /// ムササビ状態（上昇モード）中かどうかを表す値
    /// </summary>
    public bool IsRiseNow => _isRiseNow;
    /// <summary>
    /// ムササビ状態（滑空モード）中かどうかを表す値
    /// </summary>
    public bool IsFlyingSquirrelNow => _isFlyingSquirrelNow;
    public Vector3 ForwardCheckOffset => _forwardCheckOffset;
    public Vector3 ForwardCheckSize => _forwardCheckSize;
    public bool IsDrawGizmo => _isDrawGizmo;
    public Color GizmoColor => _gizmoColor;

    private bool _isRiseNow = false;
    private bool _isFlyingSquirrelNow = false;
    private Rigidbody2D _rigidbody2D = null;
    private UnionStateController _stateController = null;
    private GroundCheck _groundChecker = null;
    private Transform _transform = null;
    private MoveBehavior _mover = null;

    private float _saveTemporarilyGravityValue = 0f;

    public void Init(Rigidbody2D rigidbody2D, UnionStateController stateController,
        GroundCheck groundChecker, Transform transform, MoveBehavior mover)
    {
        _rigidbody2D = rigidbody2D;
        _stateController = stateController;
        _groundChecker = groundChecker;
        _saveTemporarilyGravityValue = _rigidbody2D.gravityScale;
        _transform = transform;
        _mover = mover;
    }

    public void Update()
    {
        UpdateRise();
        UpdateFlyingSquirrel();
    }
    /// <summary>
    /// 上昇処理の制御処理
    /// </summary>
    private void UpdateRise()
    {
        // 上昇開始
        if (IsRiseStart())
        {
            StartRise();
        }
        // 上昇処理
        if (_isRiseNow)
        {
            _rigidbody2D.velocity = Vector2.up * _risePower;
        }
    }
    /// <summary>
    /// ムササビ行動の制御処理
    /// </summary>
    private void UpdateFlyingSquirrel()
    {
        // ムササビ処理開始
        if (IsFlyingSquirrelStart())
        {
            StartFlyingSquirrel();
        }
        // ムササビ処理終了
        else if (IsFlyingSquirrelEnd())
        {
            EndFlyingSquirrel();
        }
        // ムササビ中の処理
        if (_isFlyingSquirrelNow)
        {
            // 進行方向に何かあれば落下する。
            if (ForwardCheck())
            {
                EndFlyingSquirrel();
            }
        }
    }
    /// <summary>
    /// ムササビ状態 上昇処理
    /// </summary>
    private async void StartRise()
    {
        _isRiseNow = true;
        _stateController.CurrentState = UnionState.FLYING_SQUIRREL_SANTA_FLY_UP;
        await Task.Run(() => Thread.Sleep(_riseTime));
        _isRiseNow = false;
        if (Input.GetButton(_fireButtonName) &&
            !_groundChecker.IsGrounded)
        {
            StartFlyingSquirrel();
        }
    }
    /// <summary>
    /// ムササビ状態 滑空開始処理
    /// </summary>
    private void StartFlyingSquirrel()
    {
        _isFlyingSquirrelNow = true;
        _stateController.CurrentState = UnionState.FLYING_SQUIRREL_SANTA_NOMAL;
        // 重力を0にする。
        _rigidbody2D.gravityScale = 0f;
        // 指定の方向に進む
        var xDir = _stateController.FacingDirection == FacingDirection.RIGHT ? 1f : -1f;
        _rigidbody2D.velocity = new Vector2(_forwardPropulsion * xDir, _flyingSquirrelsGravity);
        // 移動処理を停止する。
        _mover.StopMove();
    }
    /// <summary>
    /// ムササビ行動終了処理
    /// </summary>
    private void EndFlyingSquirrel()
    {
        _isFlyingSquirrelNow = false;
        // 重力を元に戻す。
        _rigidbody2D.gravityScale = _saveTemporarilyGravityValue;
        // 速度を0にする
        _rigidbody2D.velocity = Vector2.zero;
        // 移動処理を再開する。
        _mover.ResumeMove();
    }
    private bool IsRiseStart()
    {
        bool result = false;

        result =
            !_isRiseNow &&
            !_isFlyingSquirrelNow &&
            _groundChecker.IsGrounded &&
            (_stateController.CurrentState == UnionState.IDLE ||
            _stateController.CurrentState == UnionState.MOVE);

        _isReadyFlyUp = result;

        return result && Input.GetButtonDown(_fireButtonName);
    }
    private bool IsFlyingSquirrelStart()
    {
        bool result = false;

        result =
            !_isRiseNow &&
            !_isFlyingSquirrelNow &&
            !_groundChecker.IsGrounded &&
            (_stateController.CurrentState == UnionState.FLY_UP ||
            _stateController.CurrentState == UnionState.FALL_DOWN);
        _isReadyNomal = result; // 実行可能かどうかをインスペクタウィンドウに表示する。

        return result && Input.GetButtonDown(_fireButtonName);
    }
    private bool IsFlyingSquirrelEnd()
    {
        bool result = false;

        result =
            _isFlyingSquirrelNow &&
            (_groundChecker.IsGrounded ||
            Input.GetButtonUp(_fireButtonName));

        return result;
    }
    private bool ForwardCheck()
    {
        var xOffset = _forwardCheckOffset.x * (_stateController.FacingDirection == FacingDirection.RIGHT ? 1f : -1f);
        var pos = _transform.position + new Vector3(xOffset, _forwardCheckOffset.y, 0f);
        var colliders = Physics2D.OverlapBoxAll(pos, _forwardCheckSize, 0f, _forwardCheckTargetLayer);
        return colliders.Length > 0;
    }
}