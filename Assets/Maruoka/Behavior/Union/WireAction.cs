using System;
using UnityEngine;

[System.Serializable]
public class WireAction
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
            Debug.Log("ワイヤーアクションを実行しました。");
        }
    }
}