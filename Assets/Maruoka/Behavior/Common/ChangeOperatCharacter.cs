using System;
using UnityEngine;

/// <summary>
/// 操作キャラを変更する。<br/>
/// （単独行動時のみ実行可能）
/// </summary>
[System.Serializable]
public class ChangeOperatCharacter
{
    [InputName, SerializeField]
    private string _changeButtonName = default;

    private void OnChangeOperatCharacter()
    {
        if (Input.GetButtonDown(_changeButtonName))
        {
            Debug.Log("操作キャラを変更します");
            // ここに操作キャラを変更するコードを記述する。
        }

    }
}