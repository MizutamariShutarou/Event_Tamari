using System;
using UnityEngine;

[System.Serializable]
public class ShootGunBehavior
{
    [InputName, SerializeField]
    private string _fireButtonName = default;
    [SerializeField]
    bool _isReadyFire = false;

    public void Fire()
    {
        if(_isReadyFire &&
            Input.GetButtonDown(_fireButtonName))
        {
            Debug.Log("銃を発砲しました");
        }
    }
}