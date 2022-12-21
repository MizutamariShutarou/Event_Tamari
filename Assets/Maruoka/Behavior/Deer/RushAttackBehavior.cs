using DG.Tweening;
using System;
using UnityEngine;

/// <summary>
/// 突進攻撃の振る舞いクラス
/// </summary>
[System.Serializable]
public class RushAttackBehavior
{
    #region Inspector Variable
    [InputName, SerializeField]
    private string _fireButtonName = default;
    [Tooltip(
        "突進可能かどうかを表す値 : " +
        "インスペクタウィンドウで確認する用の値")]
    [SerializeField]
    private bool _isReadyFire = false;

    [Header("当たり判定関連")]
    [SerializeField]
    private Vector3 _hitboxAreaOffset = default;
    [SerializeField]
    private Vector3 _hitboxSize = default;
    [SerializeField]
    private LayerMask _targetLayer = default;
    [SerializeField]
    private bool _isDrawGizmo = false;
    [SerializeField]
    private Color _gizmoColor = Color.red;

    [Header("突進速度関係")]
    [SerializeField]
    private float _maxRushSpeed = 5f;
    [SerializeField]
    private float _startRushSpeed = 0f;
    [SerializeField]
    private float _rushTime = 2f;
    [SerializeField]
    private Ease _easeType = Ease.Linear;
    [SerializeField]
    private float _rushSpeed;
    #endregion

    #region Property
    public Vector3 HitboxAreaOffset => _hitboxAreaOffset;
    public Vector3 HitboxSize => _hitboxSize;
    public bool IsDrawGizmo => _isDrawGizmo;
    public Color GizmoColor => _gizmoColor;
    public bool IsRushNow => _isRushNow;
    #endregion

    #region Member Variable
    private Transform _transform = default;
    private DeerStateController _stateController = default;
    private Rigidbody2D _rigidbody2D = default;
    private bool _isRushNow = false;
    private Tween _rushSpeedController = default;
    #endregion

    #region Public Methods
    public void Init(Transform transform,
        DeerStateController stateController,
        Rigidbody2D rb2D)
    {
        _transform = transform;
        _stateController = stateController;
        _rigidbody2D = rb2D;
    }
    public void Update()
    {
        if (IsRun())
        {
            StartRush();
        }
        Rush();
    }
    #endregion

    #region Private Methods
    private bool IsRun()
    {
        bool result = false;

        result =
            (_stateController.CurrentState == DeerState.IDLE ||
            _stateController.CurrentState == DeerState.MOVE);

        _isReadyFire = result;

        return result && Input.GetButtonDown(_fireButtonName);
    }
    [SerializeField]
    private int _damageValue = 1;
    /// <summary>
    /// ヒット処理
    /// </summary>
    /// <returns> 
    /// 指定したOverlapBoxに何か一つでもコライダーが入って来たフレームでtrueを返す。 
    /// そうでなければfalseを返す。
    /// </returns>
    private bool HitProcess()
    {
        var pos = _hitboxAreaOffset;
        if (_stateController.FacingDirection == FacingDirection.LEFT)
        {
            pos *= -1f;
        }
        pos += _transform.position;

        var collisions = Physics2D.OverlapBoxAll(pos, _hitboxSize, 0.0f, _targetLayer);

        foreach (var e in collisions)
        {
            Debug.Log($"\"{e.name}\"がOverlapBoxに進入した");
            if(e.TryGetComponent(out EnemyMove enemyMove))
            {
                enemyMove.Damage(_damageValue);
            }
            else if(e.TryGetComponent (out MimicScript mimicScript))
            {
                mimicScript.Damage(_damageValue);
            }

            // ギミックに対しては、ギミックの方から処理を行うので記述する必要無し。
        }
        return collisions.Length > 0;
    }
    /// <summary>
    /// 突進開始処理
    /// </summary>
    public void StartRush()
    {
        // 突進中に突進命令をもう一度することはできない
        if (!_isRushNow)
        {
            _isRushNow = true;
            _stateController.CurrentState = DeerState.RUSH_ATTACK;
            Debug.Log("突進します！");
            // 向いている方向に突進する
            var dir = _stateController.FacingDirection == FacingDirection.RIGHT ? 1f : -1f;

            // 値の変化命令を行う
            _rushSpeedController = DOTween.To(
                () => _rushSpeed,               // 変更する値のゲッター
                value => _rushSpeed = value,    // 変更する値のセッター
                _maxRushSpeed * dir,            // 最終的な値
                _rushTime).                     // 掛ける時間
                SetEase(_easeType).
                OnComplete(() =>
                {
                    Debug.Log("突進終了！");
                    _isRushNow = false;
                    _rushSpeed = _startRushSpeed;
                });
        }
    }
    /// <summary>
    /// 突進中の処理
    /// </summary>
    private void Rush()
    {
        if (_isRushNow)
        {
            _rigidbody2D.velocity = new Vector2(_rushSpeed, _rigidbody2D.velocity.y);
            // 何かに接触したら突進状態を異常解除
            // DoTweenをキルし、OnComplete()を実行する。
            if (HitProcess())
            {
                RushFinish();
            }
        }
    }
    public void RushFinish()
    {
        Debug.LogWarning("DOTweenをキルします");
        _rushSpeedController?.Kill(true);
    }
    #endregion
}