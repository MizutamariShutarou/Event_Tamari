using System;
using UnityEngine;

/// <summary>
/// 突進攻撃の振る舞いクラス
/// </summary>
[System.Serializable]
public class RushAttackBehavior
{
    [InputName, SerializeField]
    private string _fireButtonName = default;

    [SerializeField]
    private bool _isReadyFire = false;

    #region Public Methods
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
            Debug.Log("突進攻撃を開始した");
        }
    }
    #endregion
}