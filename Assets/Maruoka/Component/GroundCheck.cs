using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset = default;
    [SerializeField]
    private Vector2 _size = default;
    [SerializeField]
    private LayerMask _groundLayer = default;
    [SerializeField]
    private Color _debugColor = Color.red;

    public bool IsGrounded => _isGrounded;
    private bool _isGrounded = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _debugColor;
        Gizmos.DrawCube(
            transform.position + _offset,
            _size);
    }
    private void Update()
    {
        _isGrounded = IsGround();
    }
    private bool IsGround()
    {
        var colliders = Physics2D.OverlapBoxAll(
            transform.position + _offset,
            _size,
            0f,
            _groundLayer);

        if (colliders.Length > 0) return true;
        else return false;
    }
}