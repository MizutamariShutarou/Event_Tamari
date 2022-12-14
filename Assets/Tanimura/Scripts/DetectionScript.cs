using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    [SerializeField] GameObject _mimicObj;
    Vector2 _hitPlayerPos;
    MimicScript _mimicScript;
    // Start is called before the first frame update
    void Start()
    {
        _mimicScript = _mimicObj.GetComponent<MimicScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _hitPlayerPos = collision.transform.position;
            _mimicScript.Attack(_hitPlayerPos);
            Debug.Log("hit");
        }
    }
}
