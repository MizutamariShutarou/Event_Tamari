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
    private bool _isReadyFire = false;
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

    public bool IsRiseNow => _isRiseNow;
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

    private void UpdateRise()
    {
        // アクション可能かつ、地上で入力が発生したときの処理。
        if (_isReadyFire &&
            Input.GetButtonDown(_fireButtonName) &&
            _groundChecker.IsGrounded)
        {
            // 上昇開始
            StartRise();
        }
        if (_isRiseNow)
        {
            // 上昇処理
            _rigidbody2D.velocity = Vector2.up * _risePower;
        }
    }
    private void UpdateFlyingSquirrel()
    {
        // 条件が多くなってしまった。
        // 関数化する？なんかいいアイデア求む。
        if (_isReadyFire && !_isRiseNow && !_isFlyingSquirrelNow &&
            Input.GetButtonDown(_fireButtonName) &&
            !_groundChecker.IsGrounded)
        {
            StartFlyingSquirrel();
        }
        else if (_isFlyingSquirrelNow && (_groundChecker.IsGrounded ||
            Input.GetButtonUp(_fireButtonName)))
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

    private async void StartRise()
    {
        _isRiseNow = true;
        await Task.Run(() => Thread.Sleep(_riseTime));
        _isRiseNow = false;
        if (Input.GetButton(_fireButtonName) &&
            !_groundChecker.IsGrounded)
        {
            StartFlyingSquirrel();
        }
    }
    private void StartFlyingSquirrel()
    {
        _isFlyingSquirrelNow = true;
        // 重力を0にする。
        _rigidbody2D.gravityScale = 0f;
        // 指定の方向に進む
        var xDir = _stateController.FacingDirection == FacingDirection.RIGHT ? 1f : -1f;
        _rigidbody2D.velocity = new Vector2(_forwardPropulsion * xDir, _flyingSquirrelsGravity);
        // 移動処理を停止する。
        _mover.StopMove();
    }

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
    private bool ForwardCheck()
    {
        var xOffset = _forwardCheckOffset.x * (_stateController.FacingDirection == FacingDirection.RIGHT ? 1f : -1f);
        var pos = _transform.position + new Vector3(xOffset, _forwardCheckOffset.y, 0f);
        var colliders = Physics2D.OverlapBoxAll(pos, _forwardCheckSize, 0f, _forwardCheckTargetLayer);
        return colliders.Length > 0;
    }
}