using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 操作キャラの管理をするシングルトン
/// </summary>
public class OperableCharacterManager
{
    #region Singleton
    private static OperableCharacterManager _instance = new OperableCharacterManager();
    public static OperableCharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError($"Error! Please correct!");
            }
            return _instance;
        }
    }
    private OperableCharacterManager() { }
    #endregion

    /// <summary>
    /// 現在操作しているキャラクター
    /// </summary>
    private OperableCharacter _nowOperableCharacter = OperableCharacter.NOT_SET;

    public void ChangeOperableCharacter(OperableCharacter oc)
    {
        if (oc != _nowOperableCharacter)
        {
            var beforeOperableCharacter = _nowOperableCharacter;
            _nowOperableCharacter = oc;

            // 以下の処理は似たような処理が多いので、
            // 後でまとめられるところはまとめる。
            // 「一旦動くように書く。」
            switch (oc)
            {
                case OperableCharacter.NOT_SET:
                    Debug.Log("値を設定してください");
                    break;
                case OperableCharacter.SANTA:
                    ChangeSantaEnter();
                    break;
                case OperableCharacter.DEER:
                    ChangeDeerEnter();
                    break;
                case OperableCharacter.UNION:
                    ChangeUnionEnter();
                    break;
                default:
                    Debug.Log("不正な値です！修正してください");
                    break;
            }

            switch (beforeOperableCharacter)
            {
                case OperableCharacter.NOT_SET:
                    Debug.LogWarning("異常かもしれない");
                    break;
                case OperableCharacter.SANTA:
                    ChangeSantaExit();
                    break;
                case OperableCharacter.DEER:
                    ChangeDeerExit();
                    break;
                case OperableCharacter.UNION:
                    ChangeUnionExit();
                    break;
                default:
                    Debug.LogError("不正な値です！修正してください");
                    break;
            }
        }
        else
        {
            Debug.LogError("操作キャラクターを変更する必要はありません");
        }
    }

    private void ChangeSantaEnter()
    {
        Debug.Log("操作キャラを「サンタ」に変更します");
    }
    private void ChangeDeerEnter()
    {
        Debug.Log("操作キャラを「トナカイ」に変更します");
    }
    private void ChangeUnionEnter()
    {
        Debug.Log("操作キャラを「合体」に変更します");
    }

    private void ChangeSantaExit()
    {
        Debug.Log("操作キャラを「サンタ」から変更します");
    }
    private void ChangeDeerExit()
    {
        Debug.Log("操作キャラを「トナカイ」から変更します");
    }
    private void ChangeUnionExit()
    {
        Debug.Log("操作キャラを「合体」から変更します");
    }
}

public enum OperableCharacter
{
    NOT_SET,
    SANTA,
    DEER,
    UNION
}