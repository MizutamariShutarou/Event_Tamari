using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject _rightBullet;
    [SerializeField] GameObject _leftBullet;
    [SerializeField] float _interval;
    [SerializeField] float _bouncePower = 1f;
    float _timer;
    float _scaleX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _scaleX = gameObject.transform.localScale.x;
        _timer += Time.deltaTime;
        if(_timer>=_interval)
        {
           //SnowManBulletScript dir =this.transform.localScale.x
            Shot();
        }
        
    }
    void Shot()
    {
        _timer = 0;
        if (_scaleX < 0)
        {
            Instantiate(_rightBullet, this.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_leftBullet, this.transform.position, Quaternion.identity);
        }
    }
    IEnumerator WaitDestroy()
    {
        Debug.Log("w");
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitDestroy());
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _bouncePower), ForceMode2D.Impulse);
        }
    }
}
