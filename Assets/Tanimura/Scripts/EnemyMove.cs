using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] int _addDamage;
    [SerializeField] int _hp;
    [SerializeField, TagName] string _playerName;
    [SerializeField] float _pow;
    [SerializeField] int _stopTime;
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
        //移動の処理
        _scale = transform.localScale;
        _rb.velocity = new Vector2(_speed*_scaleX, _rb.velocity.y); ;
    }
    bool IsGround()
    {
        //rayを飛ばして地面か判定する処理
        Vector3 startposition = transform.position + transform.right * -0.3f * _scale.x;
        Vector3 endposition = startposition - transform.up * 2f;
        Debug.DrawLine(startposition, endposition, Color.red);
        return Physics2D.Linecast(startposition, endposition, Ground);
    }
    void ChangeScaleX()
    {
        //向き反転の処理
        _scale = _scale * -1;
        _scaleX = _scaleX * -1;
        gameObject.transform.localScale = _scale;
        Debug.Log("Change");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //ダメージを与える処理
            Debug.Log("playerHit");
            if (collision.TryGetComponent(out SantaController santacontoroller))
            {
                santacontoroller.Damage(_addDamage, _rb.velocity, _pow);
            }
            if (collision.TryGetComponent(out DeerController deerController))
            {
                deerController.Damage(_addDamage, _rb.velocity, _pow, _stopTime);
            }
            if (collision.TryGetComponent(out UnionController unionController))
            {
                unionController.Damage(_addDamage, _rb.velocity, _pow, _stopTime);
            }
            
        }
    }
    public void Damage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
