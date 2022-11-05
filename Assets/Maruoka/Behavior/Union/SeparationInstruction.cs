using System;
using UnityEngine;

/// <summary>
/// 分離命令
/// </summary>
[System.Serializable]
public class SeparationInstruction
{
    [InputName, SerializeField]
    private string _separationButtonName = default;
    [SerializeField]
    private bool _isReadySeparation = false;

    public void OnReadySeparation()
    {
        _isReadySeparation = true;
    }
    public void OnDisableSeparation()
    {
        _isReadySeparation = false;
    }

    public void Execution()
    {
        if (_isReadySeparation &&
           Input.GetButtonDown(_separationButtonName))
        {
            Debug.Log("分離します");
        }
    }
}