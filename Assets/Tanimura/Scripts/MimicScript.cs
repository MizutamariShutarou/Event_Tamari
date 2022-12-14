using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicScript : MonoBehaviour
{
    Vector3 _playerHitPos;
    Vector3 _dir;
    Vector3 _mimicPos;
    Rigidbody2D _rb;
    bool _isAttack = false;
    bool _isBack = false;
    float _timer;
    float _attackTimer = 1;
    [SerializeField] float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //�U�����Ԃ��I������猳�̃|�W�V�����ɖ߂�

        //���肪�͈͂ɓ������炻�̕����Ɍ�����
        if (_isAttack)
        {
            _rb.velocity = new Vector2(_dir.x * _speed, _dir.y * _speed);
            _timer += Time.deltaTime;
            //�U���I�����ԂɂȂ����猳�̏ꏊ�ɖ߂�
            if (_timer > _attackTimer)
            {
                _timer = 0f;
                _isAttack = false;
                _isBack = true;
            }
        }
        //�߂�Ƃ��̏���
        if (_isBack)
        {
            _rb.velocity = new Vector2(_dir.x * -_speed, _dir.y * -_speed);
            _timer += Time.deltaTime;
            //���̈ʒu�ɖ߂�����
            if (_timer > _attackTimer)
            {
                _timer = 0f;
                _isBack = false;
                _rb.velocity = new Vector2(0, 0);
                Debug.Log("a");
            }


        }
    }
    public void Attack(Vector3 hitPos)
    {
        _mimicPos = gameObject.transform.position;
        _playerHitPos = hitPos;
        _dir = (_playerHitPos - _mimicPos).normalized;
        _isAttack = true;
        Debug.Log(_isAttack);
        Debug.Log(_mimicPos);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //�_���[�W��^���鏈��
            Debug.Log("pHit");
        }
    }
}
