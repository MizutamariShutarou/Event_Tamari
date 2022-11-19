using System;
using UnityEngine;

/// <summary>
/// 操作キャラの変更を制御するクラス
/// </summary>
[System.Serializable]
public class ChangeOperatCharacter
{
    [InputName, SerializeField]
    private string _changeButtonName = default;
    [SerializeField]
    private bool _isReadyChange = false;
    [Tooltip("相棒の名前"), SerializeField]
    private OperableCharacter _buddyName = OperableCharacter.NOT_SET;

    public void OnChangeOperatCharacter()
    {
        if (_isReadyChange &&
            Input.GetButtonDown(_changeButtonName))
        {
            Debug.Log("操作キャラを変更します");
            // ここに操作キャラを変更するコードを記述する。
            OperableCharacterManager.Instance.SwapSantaAndDeer(_buddyName);
        }
    }
}