using System;
using UnityEngine;

[System.Serializable]
public class HornSwordAttackBehavior
{
    [InputName, SerializeField]
    private string _fireButtonName = default;
    [SerializeField]
    private bool _isReadyFire = false;

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

    private Transform _transform = default;
    private DeerStateController _stateController = default;

    public void Init(Transform transform, DeerStateController stateController)
    {
        _transform = transform;
        _stateController = stateController;
    }
    public void OnReadyFire()
    {
        _isReadyFire = true;
    }
    public void Fire()
    {
        if (_isReadyFire &&
            Input.GetButtonDown(_fireButtonName))
        {
            Debug.Log("角ソードで攻撃した");
            AttackProcessing();
        }
    }
    /// <summary>
    /// 攻撃処理 : <br/>
    /// アニメーションイベントから呼び出すことを想定して作成したが、<br/>
    /// 現在はテスト段階の為、「実行可能かどうか表す変数がtrueである」ことと、<br/>
    /// 「実行ボタンが押されたとき」に実行する。
    /// </summary>
    public void AttackProcessing()
    {
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
            //if(e.TryGetComponent(out EnemyController enemy))
            //{
            //    enemy.Damage();
            //}
        }
    }
}