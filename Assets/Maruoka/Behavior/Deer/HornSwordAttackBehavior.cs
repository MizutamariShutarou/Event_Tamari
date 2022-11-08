using System;
using UnityEngine;

[System.Serializable]
public class HornSwordAttackBehavior
{
    [InputName, SerializeField]
    private string _fireButtonName = default;
    [SerializeField]
    private bool _isReadyFire = false;

    public void Init()
    {

    }
    public void OnReadyFire()
    {
        _isReadyFire = true;
    }
    public void Fire()
    {
        if (_isReadyFire &&
            Input.GetButtonDown(_fireButtonName))
        {
            Debug.Log("角ソードで攻撃した");
        }
    }
}