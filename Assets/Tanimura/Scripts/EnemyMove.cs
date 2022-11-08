using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float _speed;
    float _scaleX = -1;
    Rigidbody2D _rb;
    public LayerMask Ground;
    Vector3 _scale;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsGround())
        {
            ChangeScaleX();
        }
    }
    private void FixedUpdate()
    {
        //ˆÚ“®‚Ìˆ—
        _scale = transform.localScale;
        _rb.velocity = new Vector2(_speed*_scaleX, _rb.velocity.y); ;
    }
    bool IsGround()
    {
        //ray‚ğ”ò‚Î‚µ‚Ä’n–Ê‚©”»’è‚·‚éˆ—
        Vector3 startposition = transform.position + transform.right * -0.3f * _scale.x;
        Vector3 endposition = startposition - transform.up * 2f;
        Debug.DrawLine(startposition, endposition, Color.red);
        return Physics2D.Linecast(startposition, endposition, Ground);
    }
    void ChangeScaleX()
    {
        //Œü‚«”½“]‚Ìˆ—
        _scale = _scale * -1;
        _scaleX = _scaleX * -1;
        gameObject.transform.localScale = _scale;
        Debug.Log("Change");
    }
}
