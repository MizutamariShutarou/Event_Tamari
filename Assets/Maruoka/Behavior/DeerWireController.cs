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
    // �������Ȃ����[�h
    private void DoNothing()
    {

    }
    // �T���^�Ɍ������ĕ������[�h
    private void WalkToSanta()
    {
        var xDir = _santaTransform.position.x > _deerTransform.position.x ? 1f : -1f;
        _rigidbody2D.velocity = new Vector2(xDir * _moveSpeed, _rigidbody2D.velocity.y);

        // �T���^��x���W�̋������\���ɋ������߂��Ȃ������A�㏸����B
        if (Mathf.Abs(_santaTransform.position.x - _deerTransform.position.x) < 0.1f)
        {
            Debug.Log("���C�� : �g�i�J�C�����C���[���郂�[�h�ɂ��܂�");
            ChangeState(DeerWireState.CLIMB_TO_SANTA);
        }
        // �O���ɕǓ��̉���������Ƃ��A�P�ƍs�����[�h�Ɉڍs����B
        if (CheckForward())
        {
            Debug.Log("���C�� : �P�ƍs�����[�h�ɂ��܂�");
            _deerController.EndWire();
            _santaController.EndWire();
        }
        // �T���^�Ə\���ɋ߂���΍��̂���B
        if (Mathf.Abs(_santaTransform.position.x - _deerTransform.position.x) < _coalesceDistanceX &&
           Mathf.Abs(_santaTransform.position.y - _deerTransform.position.y) < _coalesceDistanceY)
        {
            Debug.Log("���C�� : ���̂��܂�");
            Coalesce();
        }
    }
    // �i�s�����`�F�b�N����
    private bool CheckForward()
    {
        var pos = _checkForwardOffset;
        pos.x *= _deerController.StateController.FacingDirection == FacingDirection.RIGHT ?
            1f : -1f;
        pos += _deerTransform.position;
        return Physics2D.OverlapBoxAll(pos, _checkForwardSize, 0f,
            _targetLayer).Length > 0;
    }
    // �T���^�Ɍ������ď��郂�[�h
    private void ClimbToSanta()
    {
        // �㏸����
        _rigidbody2D.velocity = new Vector2(0f, _climbSpeed);
        // Debug.Log($"{_santaTransform.position.y - _deerTransform.position.y}"); // �����̍��m�F�p
        // �T���^�Ə\���ɋ߂���΍��̂���B
        if (Mathf.Abs(_santaTransform.position.x - _deerTransform.position.x) < _coalesceDistanceX &&
            Mathf.Abs(_santaTransform.position.y - _deerTransform.position.y) < _coalesceDistanceY)
        {
            Coalesce();
        }

    }
    // �T���^�̈ʒu�ō��̂���
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