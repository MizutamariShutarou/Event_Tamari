using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        var v = Input.GetAxisRaw("Vertical");
        var h = Input.GetAxisRaw("Horizontal");

        Vector2 dir = new Vector2(h, v);

        _rb.velocity = dir.normalized * _speed + new Vector2(0, _rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
}
