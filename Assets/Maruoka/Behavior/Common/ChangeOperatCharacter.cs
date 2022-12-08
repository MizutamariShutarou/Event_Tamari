using System;
using UnityEngine;

/// <summary>
/// 操作キャラの変更を制御するクラス
/// </summary>
[System.Serializable]
public abstract class ChangeOperatCharacter
{
    [InputName, SerializeField]
    protected string _changeButtonName = default;
    [SerializeField]
    protected bool _isReadyChange = false;
    [Tooltip("相棒の名前"), SerializeField]
    protected OperableCharacter _buddyName = OperableCharacter.NOT_SET;

    public virtual void Update()
    {
        if (IsRun())
        {
            Debug.Log("操作キャラを変更します");
            // ここに操作キャラを変更するコードを記述する。
            OperableCharacterManager.Instance.SwapSantaAndDeer(_buddyName);
        }
    }

    protected abstract bool IsRun();
}