using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = default;
    [Tooltip("�O�̃V�[����ۑ����Ă����ϐ�")] static int _beforeSceneNum;

    [SerializeField, Tooltip("�N���A�����X�e�[�W����ۑ�����ϐ�")] int _clearNum = 0;

    [Header("�X�e�[�W�I�����")]
    [SerializeField, Tooltip("�X�e�[�W�P")] GameObject _firstStageButton;
    [SerializeField, Tooltip("�X�e�[�W�Q")] GameObject _secondStageButton;
    [SerializeField, Tooltip("�X�e�[�W�R")] GameObject _thirdStageButton;

    [Header("�V�[���J��")]
    [SerializeField, Tooltip("�t�F�[�h�C��������I�u�W�F�N�g")] GameObject _fadeInObj;
    [SerializeField, Tooltip("�t�F�[�h�A�E�g������I�u�W�F�N�g")] GameObject _fadeOutObj;

    [SerializeField, Tooltip("�X�e�[�W����`�[�g")] bool _allStageOpen;

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
    /// �V�[���̑J�ڂɌĂяo�����֐�
    /// </summary>
    /// <param name="sceneNum">�V�[���̔ԍ�</param>
    public void SceneChange(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
        Debug.Log($"{sceneNum}�V�[�������[�h���܂���");
    }

    /// <summary>
    /// �Q�[���I�[�o�[�V�[����ǂݍ���
    /// </summary>
    public void GameOver()
    {
        _beforeSceneNum = SceneManager.GetActiveScene().buildIndex;
    }

    /// <summary>
    /// �Q�[���̃��g���C
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
                Debug.Log($"{_clearNum}���s�K�؂Ȓl�ł��B");
            }
        }
        else if(_allStageOpen)
        {
            _firstStageButton.SetActive(true);
            _secondStageButton.SetActive(true);
            _thirdStageButton.SetActive(true);
            Debug.Log("�X�e�[�W���S�J������܂���");
        }
    }
}
