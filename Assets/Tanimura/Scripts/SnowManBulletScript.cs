using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;
    [SerializeField] int _dir = 1;
    [SerializeField] int _addDamage;
    [SerializeField] float _pow;
    [SerializeField] int _stopTime;
    Rigidbody2D _rb;
    //public int dir = 1;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.right * _speed * _dir;
    }

    // Update is called once per frame
    void Update()
    {
        
        Destroy(this.gameObject, _lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //É_ÉÅÅ[ÉWÇó^Ç¶ÇÈèàóù
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
}
