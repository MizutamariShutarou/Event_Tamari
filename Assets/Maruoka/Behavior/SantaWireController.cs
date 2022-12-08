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
    private LayerMask _targetLayer = default;
    [Tooltip("崖を検知するためのレイの方向"), SerializeField]
    private Vector2 _checkCliffRayDir = default;
    [SerializeField]
    private float _checkCliffRayMaxDistance = 5f;
    [SerializeField]
    private LayerMask _checkCliffTargetLayer = default;


    private Rigidbody2D _rigidbody2D = null;
    private Transform _santaTransform = null;
    private Transform _deerTransform = null;
    private SantaStateController _stateController = null;

    public void Init(Rigidbody2D rigidbody2D,
        Transform santaTransform,
        Transform deerTransform,
        SantaStateController stateController)
    {
        _rigidbody2D = rigidbody2D;
        _santaTransform = santaTransform;
        _deerTransform = deerTransform;
        _stateController = stateController;
    }
    public void Update()
    {

    }

    // 発射
    public void Shot()
    {
        _rigidbody2D.AddForce(_shotDir.normalized * _shotPower, ForceMode2D.Impulse);
    }
    // 停止判定
    private bool CheckStop()
    {
        return _rigidbody2D.velocity.x < 0.1f && _rigidbody2D.velocity.y < 0.1f;
    }
    // にじり寄るモード中の処理
    private void WireMove()
    {
        // トナカイが右にいれば正の値を、左にいれば負の値を取得する。
        var dirX = _deerTransform.position.x > _santaTransform.position.x ?
            1f : -1f;
        // 移動処理
        _rigidbody2D.velocity = new Vector2(dirX, _rigidbody2D.velocity.y);
    }
    // 進行方向チェック処理
    private bool CheckForward()
    {
        var pos = _checkForwardOffset;
        pos.x *= _stateController.FacingDirection == FacingDirection.RIGHT ?
            1f : -1f;
        pos += _checkForwardOffset;
        return Physics2D.OverlapBoxAll(pos, _checkForwardSize, 0f,
            _targetLayer).Length > 0;
    }
    // 単独行動モード遷移処理
    private void TransitionToNomal()
    {

    }

    // 崖検知処理
    private bool CheckCliff()
    {
        return Physics2D.Raycast(_santaTransform.position,
            _checkCliffRayDir,
            _checkCliffRayMaxDistance,
            _checkCliffTargetLayer);
    }

    // サンタ停止モードに遷移する処理
    private void TransitionToStop()
    {

    }

    // トナカイ近づくモードに遷移する処理
    private void StartDeerWireActionMode()
    {

    }

    //トナカイ接触判定
    private bool IsCoalesce()
    {
        bool fake = false;
        Debug.LogError("未実装です");
        return fake;
    }

    // 合体処理
    private void OnCoalesce()
    {
        OperableCharacterManager.Instance.Coalesce();
    }
}

public enum SantaWireState
{
    DO_NOTHING,
    MOVE,
    STOP_MOVE,
}