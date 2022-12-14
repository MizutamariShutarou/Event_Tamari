using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuriboScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        StartCoroutine(WaitDestroy());
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2 (0,10), ForceMode2D.Impulse);
    }
    
    IEnumerator WaitDestroy()
    {
        Debug.Log("w");
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
