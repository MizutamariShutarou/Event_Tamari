using System;
using UnityEngine;

[System.Serializable]
public class ShootGunBehavior
{
    [InputName, SerializeField]
    private string _fireButtonName = default;
    [SerializeField]
    private bool _isReadyFire = false;
    [SerializeField]
    private LayerMask _targetLayer = default;
    [SerializeField]
    private float _maxDistance = 10f;
    [SerializeField]
    private bool _isDrawGizmo = false;
    [SerializeField]
    private Color _gizmoColor = Color.red;

    private Transform _transform = null;
    private UnionStateController _stateController = null;

    public float MaxDistance => _maxDistance;
    public bool IsDrawGizmo => _isDrawGizmo;
    public Color GizmoColor => _gizmoColor;

    public void Init(Transform transform, UnionStateController stateController)
    {
        _transform = transform;
        _stateController = stateController;
    }

    public void Update()
    {
        if (IsRun())
        {
            Debug.Log("銃を発砲しました");
            // 前方にレイを飛ばす
            var dir = _stateController.FacingDirection == FacingDirection.RIGHT ?
                Vector2.right : Vector2.left;
            RaycastHit2D hit =
                Physics2D.Raycast(_transform.position, dir, _maxDistance, _targetLayer);

            // ターゲットに当たったら処理を実行する
            if (hit.collider != null
                // && hit.collider.TryGetComponent(out EnemyController enemy)
                )
            {
                Debug.Log($"\"{hit.collider.name}\"に攻撃しました");
                // enemy.Damage();
            }
        }
    }
    private bool IsRun()
    {
        bool result = false;

        result =
            (_stateController.CurrentState == UnionState.IDLE ||
            _stateController.CurrentState == UnionState.MOVE);

        _isReadyFire = result;

        return result&& Input.GetButtonDown(_fireButtonName);
    }
}