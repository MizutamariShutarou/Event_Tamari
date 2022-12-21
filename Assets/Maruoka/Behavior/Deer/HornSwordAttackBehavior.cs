using System;
using UnityEngine;

[System.Serializable]
public class HornSwordAttackBehavior
{
    [InputName, SerializeField]
    private string _fireButtonName = default;
    [SerializeField]
    private bool _isReady = false;

    [SerializeField]
    private Vector3 _attackPosOffset = default;
    [SerializeField]
    private Vector3 _attackAreaSize = default;
    [SerializeField]
    private LayerMask _targetLayer = default;
    [SerializeField]
    private bool _isDrawGizmo = false;
    [SerializeField]
    private Color _attackGizmoColor = Color.red;

    public Vector3 AttackPosOffset => _attackPosOffset;
    public Vector3 AttackAreaSize => _attackAreaSize;
    public LayerMask TargetLayer => _targetLayer;
    public bool IsDrawGizmo => _isDrawGizmo;
    public Color GizmoColor => _attackGizmoColor;
    public bool IsAttackNow => _isAttackNow;

    private Transform _transform = default;
    private DeerStateController _stateController = default;
    private bool _isAttackNow = false;

    public void Init(Transform transform, DeerStateController stateController)
    {
        _transform = transform;
        _stateController = stateController;
    }
    public void Update()
    {
        if (IsRun())
        {
            Debug.Log("角ソードで攻撃した");
            AttackProcessing();
        }
    }
    private bool IsRun()
    {
        bool result = false;

        result =
            (_stateController.CurrentState == DeerState.IDLE ||
            _stateController.CurrentState == DeerState.MOVE);
        _isReady = result;

        return result && Input.GetButtonDown(_fireButtonName);
    }
    [SerializeField]
    private int _damageValue = 1;
    /// <summary>
    /// 攻撃処理 : <br/>
    /// アニメーションイベントから呼び出すことを想定して作成したが、<br/>
    /// 現在はテスト段階の為、「実行可能かどうか表す変数がtrueである」ことと、<br/>
    /// 「実行ボタンが押されたとき」に実行する。
    /// </summary>
    public void AttackProcessing()
    {
        _stateController.CurrentState = DeerState.HORN_SWORD_ATTACK;

        var pos = AttackPosOffset;
        if (_stateController.FacingDirection == FacingDirection.LEFT)
        {
            pos *= -1f;
        }
        pos += _transform.position;

        var collisions = Physics2D.OverlapBoxAll(pos, _attackAreaSize, 0.0f, _targetLayer);

        foreach (var e in collisions)
        {
            Debug.Log($"\"{e.name}\"に攻撃した");
            if (e.TryGetComponent(out EnemyMove enemyMove))
            {
                enemyMove.Damage(_damageValue);
                Debug.Log($"\"{e.name}\"に攻撃した");
            }
            else if (e.TryGetComponent(out MimicScript mimicScript))
            {
                mimicScript.Damage(_damageValue);
                Debug.Log($"\"{e.name}\"に攻撃した");
            }
        }
    }
}