using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] int _addDamage;
    [SerializeField] int _hp;
    [SerializeField, TagName] string _playerName;
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
        //à⁄ìÆÇÃèàóù
        _scale = transform.localScale;
        _rb.velocity = new Vector2(_speed*_scaleX, _rb.velocity.y); ;
    }
    bool IsGround()
    {
        //rayÇîÚÇŒÇµÇƒínñ Ç©îªíËÇ∑ÇÈèàóù
        Vector3 startposition = transform.position + transform.right * -0.3f * _scale.x;
        Vector3 endposition = startposition - transform.up * 2f;
        Debug.DrawLine(startposition, endposition, Color.red);
        return Physics2D.Linecast(startposition, endposition, Ground);
    }
    void ChangeScaleX()
    {
        //å¸Ç´îΩì]ÇÃèàóù
        _scale = _scale * -1;
        _scaleX = _scaleX * -1;
        gameObject.transform.localScale = _scale;
        Debug.Log("Change");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_playerName))
        {
            //collision.gameObject.GetComponent<SantaController>().Damage();
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
