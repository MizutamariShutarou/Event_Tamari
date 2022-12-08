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
    [Tooltip("�R�����m���邽�߂̃��C�̕���"), SerializeField]
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

    // ����
    public void Shot()
    {
        _rigidbody2D.AddForce(_shotDir.normalized * _shotPower, ForceMode2D.Impulse);
    }
    // ��~����
    private bool CheckStop()
    {
        return _rigidbody2D.velocity.x < 0.1f && _rigidbody2D.velocity.y < 0.1f;
    }
    // �ɂ����郂�[�h���̏���
    private void WireMove()
    {
        // �g�i�J�C���E�ɂ���ΐ��̒l���A���ɂ���Ε��̒l���擾����B
        var dirX = _deerTransform.position.x > _santaTransform.position.x ?
            1f : -1f;
        // �ړ�����
        _rigidbody2D.velocity = new Vector2(dirX, _rigidbody2D.velocity.y);
    }
    // �i�s�����`�F�b�N����
    private bool CheckForward()
    {
        var pos = _checkForwardOffset;
        pos.x *= _stateController.FacingDirection == FacingDirection.RIGHT ?
            1f : -1f;
        pos += _checkForwardOffset;
        return Physics2D.OverlapBoxAll(pos, _checkForwardSize, 0f,
            _targetLayer).Length > 0;
    }
    // �P�ƍs�����[�h�J�ڏ���
    private void TransitionToNomal()
    {

    }

    // �R���m����
    private bool CheckCliff()
    {
        return Physics2D.Raycast(_santaTransform.position,
            _checkCliffRayDir,
            _checkCliffRayMaxDistance,
            _checkCliffTargetLayer);
    }

    // �T���^��~���[�h�ɑJ�ڂ��鏈��
    private void TransitionToStop()
    {

    }

    // �g�i�J�C�߂Â����[�h�ɑJ�ڂ��鏈��
    private void StartDeerWireActionMode()
    {

    }

    //�g�i�J�C�ڐG����
    private bool IsCoalesce()
    {
        bool fake = false;
        Debug.LogError("�������ł�");
        return fake;
    }

    // ���̏���
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