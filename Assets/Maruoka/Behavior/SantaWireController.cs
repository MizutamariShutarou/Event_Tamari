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
    [Tooltip("�R�����m���邽�߂̃��C�̕���"), SerializeField]
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
    // ����
    public void Shot(Rigidbody2D rigidbody2D, bool dirIsRight)
    {
        _currentState = SantaWireState.DO_NOTHING;
        _shotDir.x *= dirIsRight ? 1f : -1f;
        rigidbody2D.AddForce(_shotDir.normalized * _shotPower, ForceMode2D.Impulse);
    }
    // ��~���̏���
    private void DoNothing()
    {
        if (CheckStop())
        {
            _currentState = SantaWireState.MOVE;
        }
    }
    // ��~����
    private bool CheckStop()
    {
        return Mathf.Abs(_rigidbody2D.velocity.x) < 0.1f && Mathf.Abs(_rigidbody2D.velocity.y) < 0.1f;
    }
    // �ɂ����郂�[�h���̏���
    private void WireMove()
    {
        // �g�i�J�C���E�ɂ���ΐ��̒l���A���ɂ���Ε��̒l���擾����B
        var dirX = _deerTransform.position.x > _santaTransform.position.x ?
            1f : -1f;
        // �ړ�����
        _rigidbody2D.velocity = new Vector2(dirX * _moveSpeed, _rigidbody2D.velocity.y);
        if (IsCoalesce()) // ��������ꒆ�Ƀg�i�J�C�ɐڐG�����Ƃ����̂���B
        {
            Debug.Log("���C���[ : ���̂��܂�");
            StartDeerWireActionMode();
        }
        if (CheckForward()) // �ǂ��������Ƃ��P�ƍs�����[�h�ɑJ�ڂ���
        {
            Debug.Log("���C���[ : �P�ƍs�����[�h�Ɉڍs���܂�");
            TransitionToNomal();
        }
        if (CheckCliff()) // �R���������Ƃ���~���A�g�i�J�C���߂Â����[�h�ɂ���B
        {
            Debug.Log("���C�� : �g�i�J�C�����C���[�߂Â����[�h�ɂ��܂�");
            TransitionToStop();
            StartDeerWireActionMode();
        }
    }
    // �i�s�����`�F�b�N����
    private bool CheckForward()
    {
        var pos = _checkForwardOffset;
        pos.x *= _santaController.StateControler.FacingDirection == FacingDirection.RIGHT ?
            1f : -1f;
        pos += _santaController.transform.position;
        return Physics2D.OverlapBoxAll(pos, _checkForwardSize, 0f,
            _checkForwardTargetLayer).Length > 0;
    }
    // �P�ƍs�����[�h�J�ڏ���
    private void TransitionToNomal()
    {
        _santaController.EndWire();
        _deerController.EndWire();
    }
    /// <summary>
    /// �R���m����
    /// </summary>
    /// <returns> 
    /// �R���������Ƃ��i���C�����ɂ�������Ȃ��Ȃ����Ƃ��jtrue��Ԃ��B
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
    // �T���^��~���[�h�ɑJ�ڂ��鏈��
    private void TransitionToStop()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _currentState = SantaWireState.STOP_MOVE;
    }
    // �g�i�J�C�߂Â����[�h�ɑJ�ڂ��鏈��
    private void StartDeerWireActionMode()
    {
        OperableCharacterManager.Instance.ChangeDeerWireState(DeerWireState.WALK_TO_SANTA);
    }
    //�g�i�J�C�ڐG����
    private bool IsCoalesce()
    {
        // �����Ŕ��肷��B
        return (Mathf.Abs(_santaTransform.position.x - _deerTransform.position.x) < _maxDistanceX &&
            Mathf.Abs(_santaTransform.position.y - _deerTransform.position.y) < _maxDistanceY);
    }
    // ���̏���
    private void OnCoalesce()
    {
        OperableCharacterManager.Instance.Coalesce();
    }
}

public enum SantaWireState
{
    /// <summary>
    /// �������Ȃ�
    /// </summary>
    DO_NOTHING,
    MOVE,
    STOP_MOVE,
}