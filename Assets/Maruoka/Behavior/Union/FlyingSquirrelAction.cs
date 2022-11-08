using System;
using UnityEngine;

/// <summary>
/// ムササビアクションクラス
/// </summary>
[System.Serializable]
public class FlyingSquirrelAction
{
    [InputName, SerializeField]
    private string _fireButtonName = default;
    [SerializeField]
    private bool _isReadyFire = false;

    public void Fire()
    {
        if(_isReadyFire &&
            Input.GetButtonDown(_fireButtonName))
        {
            Debug.Log("ムササビアクションを実行します");
        }
    }
}