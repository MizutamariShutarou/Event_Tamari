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
    public void Init(GameObject union)
    {
        OperableCharacterManager.Instance.SetUnion(union);
    }
    public void Update()
    {
        if (_isReadySeparation &&
           Input.GetButtonDown(_separationButtonName))
        {
            OperableCharacterManager.Instance.Separate();
            Debug.Log("分離します");
        }
    }
}