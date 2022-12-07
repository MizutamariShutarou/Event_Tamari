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

    // Hierarchyにいる操作可能キャラクター一覧
    // この変数群にアクセスしてキャラクターの切り替えを制御する。
    private GameObject _santa = null;
    private GameObject _deer = null;
    private GameObject _union = null;

    // 各キャラクターのプレハブを持つクラス
    private AssetsProvider _assets = null;

    public void SetUnion(GameObject union) { _union = union; }
    public void SetAssetsProvider(AssetsProvider assets) { _assets = assets; }

    /// <summary>
    /// 操作キャラクター（「サンタ」と「トナカイ」）を切り替える
    /// </summary>
    /// <param name="newer"> これから操作するキャラクターを表す値 </param>
    public void SwapSantaAndDeer(OperableCharacter newer)
    {
        if (newer == OperableCharacter.SANTA)
        { // これから操作するキャラクターが「サンタ」の場合の処理

            // 「トナカイ」の更新を停止する
            _deer.GetComponent<DeerController>().enabled = false;
            // 「サンタ」の更新を再開する
            _santa.GetComponent<SantaController>().enabled = true;
            // カメラのターゲットを「サンタ」に設定する
            _assets.CinemachineVirtualCamera.Follow = _santa.transform;
        }
        else if (newer == OperableCharacter.DEER)
        { // これから操作するキャラクターが「トナカイ」の場合

            // 「サンタ」の更新を停止する
            _santa.GetComponent<SantaController>().enabled = false;
            // 「トナカイ」の更新を再開する
            _deer.GetComponent<DeerController>().enabled = true;
            // カメラのターゲットを「トナカイ」に設定する
            _assets.CinemachineVirtualCamera.Follow = _deer.transform;
        }
        else
        {
            Debug.LogError("不正な値です！");
        }
    }
    /// <summary>
    /// 「サンタ」と「トナカイ」が「合体」する
    /// </summary>
    public void Coalesce()
    {
        // 後で使う値を保存しておく
        var instantiatePos = (_santa.transform.position + _deer.transform.position) / 2f;
        // 「サンタ」と「トナカイ」をデストロイする
        GameObject.Destroy(_santa);
        GameObject.Destroy(_deer);
        // 「サンタ」と「トナカイ」がいるポジションの真ん中に
        // 「合体」をインスタンシエイトする。戻り値は保存しておく。
        _union = GameObject.Instantiate(_assets.UnionPrefab, instantiatePos, Quaternion.identity);
        // カメラのターゲットを「合体」にする。
        _assets.CinemachineVirtualCamera.Follow = _union.transform;
    }
    /// <summary>
    /// 「合体」が「トナカイ」と「サンタ」に分離する。
    /// </summary>
    public void Separate()
    {
        // 「合体」がいた位置に「トナカイ」と「サンタ」を
        // インスタンシエイトする。戻り値は保存しておく。
        _santa = GameObject.Instantiate(_assets.SantaPrefab, _union.transform.position, Quaternion.identity);
        _deer = GameObject.Instantiate(_assets.DeerPrefab, _union.transform.position, Quaternion.identity);
        // 「合体」をデストロイする
        GameObject.Destroy(_union);
        // カメラのターゲットを「サンタ」にする。
        _assets.CinemachineVirtualCamera.Follow = _santa.transform;
        // 「サンタ」の更新を開始。「トナカイ」の更新を停止する。
        _santa.GetComponent<SantaController>().enabled = true;
        _deer.GetComponent<DeerController>().enabled = false;
    }
    /// <summary>
    /// ワイヤーアクション用分離命令
    /// </summary>
    public void SeparateOnWireAction()
    {
        Separate();// 通常分離する。
        //サンタを飛ばす
        if(_santa.TryGetComponent(out SantaController santaController))
        {
            santaController.SantaWireController.Shot();
        }
    }
}

public enum OperableCharacter
{
    NOT_SET,
    SANTA,
    DEER,
    UNION
}