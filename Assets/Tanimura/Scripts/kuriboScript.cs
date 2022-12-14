using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuriboScript : MonoBehaviour
{
    [SerializeField, TagName] string _playerName;
    [SerializeField] float _bouncePower = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(_playerName))
        {
            StartCoroutine(WaitDestroy());
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _bouncePower), ForceMode2D.Impulse);
        }
    }
    
    IEnumerator WaitDestroy()
    {
        Debug.Log("w");
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
