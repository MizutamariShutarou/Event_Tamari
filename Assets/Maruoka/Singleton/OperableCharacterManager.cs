using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperableCharacterManager
{
    #region Singleton
    private static OperableCharacterManager _instance = new OperableCharacterManager();
    public static OperableCharacterManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError($"Error! Please correct!");
            }
            return _instance;
        }
    }
    private OperableCharacterManager(){}
    #endregion

    #region Member Variables
    #endregion

    #region Properties
    #endregion

    #region Events
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}