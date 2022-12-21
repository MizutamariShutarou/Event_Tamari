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
    Animator _animator;
    [SerializeField] float _attackTimer;
    [SerializeField] float _speed;
    [SerializeField] int _hp;
    [SerializeField] int _addDamage;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
                _animator.SetBool("Attack", false);
                Debug.Log("Stop");
            }


        }
    }
    public void Attack(Vector3 hitPos)
    {
        if (_isAttack == false && _isBack == false)
        {
            _mimicPos = gameObject.transform.position;
            _playerHitPos = hitPos;
            _dir = (_playerHitPos - _mimicPos).normalized;
            _isAttack = true;
            _animator.SetBool("Attack",true);
            Debug.Log(_isAttack);
            Debug.Log(_mimicPos);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //�_���[�W��^���鏈��
            Debug.Log("playerHit");
        }
    }
    
    public void Damage(int damage)
    {
        _hp -= damage;
        if(_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
