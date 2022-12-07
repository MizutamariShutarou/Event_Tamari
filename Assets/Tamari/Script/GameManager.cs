using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = default;
    [Tooltip("前のシーンを保存しておく変数")] static int _beforeSceneNum;

    [SerializeField, Tooltip("クリアしたステージ数を保存する変数")] int _clearNum = 0;

    [Header("ステージ選択画面")]
    [SerializeField, Tooltip("ステージ１")] GameObject _firstStageButton;
    [SerializeField, Tooltip("ステージ２")] GameObject _secondStageButton;
    [SerializeField, Tooltip("ステージ３")] GameObject _thirdStageButton;

    [Header("シーン遷移")]
    [SerializeField, Tooltip("フェードインさせるオブジェクト")] GameObject _fadeInObj;
    [SerializeField, Tooltip("フェードアウトさせるオブジェクト")] GameObject _fadeOutObj;

    [SerializeField, Tooltip("ステージ解放チート")] bool _allStageOpen;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        StageCount();   
    }

    /// <summary>
    /// シーンの遷移に呼び出される関数
    /// </summary>
    /// <param name="sceneNum">シーンの番号</param>
    public void SceneChange(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
        Debug.Log($"{sceneNum}シーンをロードしました");
    }

    /// <summary>
    /// ゲームオーバーシーンを読み込む
    /// </summary>
    public void GameOver()
    {
        _beforeSceneNum = SceneManager.GetActiveScene().buildIndex;
    }

    /// <summary>
    /// ゲームのリトライ
    /// </summary>
    private void RetryGame()
    {
        SceneManager.LoadScene(_beforeSceneNum);
    }

    void StageCount()
    {
        if(!_allStageOpen)
        {
            if (_clearNum == 0)
            {
                _secondStageButton.SetActive(false);
                _thirdStageButton.SetActive(false);
            }
            else if (_clearNum == 1)
            {
                _secondStageButton.SetActive(true);
                _thirdStageButton.SetActive(false);
            }
            else if (_clearNum == 2)
            {
                _secondStageButton.SetActive(true);
                _thirdStageButton.SetActive(true);
            }
            else
            {
                Debug.Log($"{_clearNum}が不適切な値です。");
            }
        }
        else if(_allStageOpen)
        {
            _firstStageButton.SetActive(true);
            _secondStageButton.SetActive(true);
            _thirdStageButton.SetActive(true);
            Debug.Log("ステージが全開放されました");
        }
    }
}
