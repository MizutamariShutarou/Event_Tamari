using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] float _jumpPower;
    [SerializeField] float _speed;
    Rigidbody2D _rb = default;
    float _h;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _rb.AddForce(Vector2.right * _h * _speed, ForceMode2D.Force);
        // ŠeŽí“ü—Í‚ðŽó‚¯Žæ‚é
        if (Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

        }
    }
}
