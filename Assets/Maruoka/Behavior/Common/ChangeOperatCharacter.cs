using System;
using UnityEngine;

/// <summary>
/// 操作キャラの変更を制御するクラス
/// </summary>
[System.Serializable]
public class ChangeOperatCharacter
{
    [InputName, SerializeField]
    protected string _changeButtonName = default;
    [SerializeField]
    protected bool _isReadyChange = false;
    [Tooltip("相棒の名前"), SerializeField]
    private OperableCharacter _buddyName = OperableCharacter.NOT_SET;

    public void Update()
    {
        if (IsRun())
        {
            Debug.Log("操作キャラを変更します");
            // ここに操作キャラを変更するコードを記述する。
            OperableCharacterManager.Instance.SwapSantaAndDeer(_buddyName);
        }
    }

    protected virtual bool IsRun()
    {
        // _isReadyChange &&
        //     Input.GetButtonDown(_changeButtonName)
        return false;
    }
}