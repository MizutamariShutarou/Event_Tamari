using System;
using UnityEngine;

[System.Serializable]
public class WireAction
{
    [InputName, SerializeField]
    private string _fireButtonName = default;
    [SerializeField]
    private bool _isReadyFire = false;

    public void Update()
    {
        if(IsRun())
        {
            Debug.Log("ワイヤーアクションを実行しました。");
        }
    }
    private bool IsRun()
    {
        bool result = false;

        result = 
            _isReadyFire &&
            Input.GetButtonDown(_fireButtonName);

        return result;
    }
}