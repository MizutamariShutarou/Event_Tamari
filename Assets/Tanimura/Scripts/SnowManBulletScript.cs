using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;
    [SerializeField] int _dir = 1;
    //public int dir = 1;
    
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * _speed * _dir;
    }

    // Update is called once per frame
    void Update()
    {
        
        Destroy(this.gameObject, _lifeTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ƒ_ƒ[ƒW‚ğ—^‚¦‚éˆ—‚ğŒã‚Å‘‚­
    }
}
